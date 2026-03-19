using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GameDistricts;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000015 RID: 21
	public class ZiplineConnectionService : ILoadableSingleton
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000034D8 File Offset: 0x000016D8
		public ZiplineConnectionService(IBlockService blockService, ZiplineConnectionBlockFactory ziplineConnectionBlockFactory, RootObjectProvider rootObjectProvider, ZiplineCableRenderer ziplineCableRenderer, ZiplineCableNavMesh ziplineCableNavMesh, BresenhamLineDrawer bresenhamLineDrawer, BlockValidator blockValidator, ISpecService specService, EventBus eventBus)
		{
			this._blockService = blockService;
			this._ziplineConnectionBlockFactory = ziplineConnectionBlockFactory;
			this._rootObjectProvider = rootObjectProvider;
			this._ziplineCableRenderer = ziplineCableRenderer;
			this._ziplineCableNavMesh = ziplineCableNavMesh;
			this._bresenhamLineDrawer = bresenhamLineDrawer;
			this._blockValidator = blockValidator;
			this._specService = specService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003548 File Offset: 0x00001748
		public void Load()
		{
			this._root = this._rootObjectProvider.CreateRootObject("ZiplineConnectionService").transform;
			ZiplineConnectionServiceSpec singleSpec = this._specService.GetSingleSpec<ZiplineConnectionServiceSpec>();
			this._maxCableInclination = singleSpec.MaxCableInclination;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003588 File Offset: 0x00001788
		public bool CanBeConnected(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			return otherZiplineTower && otherZiplineTower != ziplineTower && otherZiplineTower.HasFreeSlots && !ziplineTower.IsConnectedTo(otherZiplineTower) && this.DistrictCentersAreCompatible(ziplineTower, otherZiplineTower) && this.ConnectionIsValid(ziplineTower, otherZiplineTower);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035BB File Offset: 0x000017BB
		public void GetBlockingObjects(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, IList<BlockObject> blockingObjects)
		{
			this.AddCoordinates(ziplineTower, otherZiplineTower);
			this.GetBlockingObjects(blockingObjects);
			this._coordinatesCache.Clear();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035D7 File Offset: 0x000017D7
		public IEnumerable<Vector3Int> GetConnectionCoordinates()
		{
			foreach (ZiplineConnectionService.ConnectionAtCoordinates connectionAtCoordinates in this._connections)
			{
				yield return connectionAtCoordinates.Coordinates;
			}
			List<ZiplineConnectionService.ConnectionAtCoordinates>.Enumerator enumerator = default(List<ZiplineConnectionService.ConnectionAtCoordinates>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035E8 File Offset: 0x000017E8
		public void Connect(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ziplineTower.AddConnection(otherZiplineTower);
			otherZiplineTower.AddConnection(ziplineTower);
			this.AddCoordinates(ziplineTower, otherZiplineTower);
			foreach (Vector3Int vector3Int in this._coordinatesCache)
			{
				CableBlock bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<CableBlock>(vector3Int);
				BlockObject blockObject = bottomObjectComponentAt ? bottomObjectComponentAt.GetComponent<BlockObject>() : this._ziplineConnectionBlockFactory.CreateConnection(this._root, vector3Int);
				this._connections.Add(new ZiplineConnectionService.ConnectionAtCoordinates(ziplineTower, otherZiplineTower, vector3Int, blockObject));
			}
			if (ziplineTower.IsActive && otherZiplineTower.IsActive)
			{
				this._ziplineCableRenderer.AddActiveConnection(ziplineTower, otherZiplineTower);
				this._ziplineCableNavMesh.AddActiveConnection(ziplineTower, otherZiplineTower);
				this.NotifyConnectionActivated(ziplineTower);
			}
			else
			{
				this._ziplineCableRenderer.AddInactiveConnection(ziplineTower, otherZiplineTower);
				this._ziplineCableNavMesh.AddInactiveConnection(ziplineTower, otherZiplineTower);
			}
			this._coordinatesCache.Clear();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000036E8 File Offset: 0x000018E8
		public void ActivateConnection(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			this._ziplineCableRenderer.ActivateConnection(ziplineTower, otherZiplineTower);
			this._ziplineCableNavMesh.ActivateConnection(ziplineTower, otherZiplineTower);
			this.NotifyConnectionActivated(ziplineTower);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000370C File Offset: 0x0000190C
		public void Disconnect(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			ziplineTower.RemoveConnection(otherZiplineTower);
			otherZiplineTower.RemoveConnection(ziplineTower);
			for (int i = this._connections.Count - 1; i >= 0; i--)
			{
				ZiplineConnectionService.ConnectionAtCoordinates connectionAtCoordinates = this._connections[i];
				if ((connectionAtCoordinates.First == ziplineTower && connectionAtCoordinates.Second == otherZiplineTower) || (connectionAtCoordinates.First == otherZiplineTower && connectionAtCoordinates.Second == ziplineTower))
				{
					this._connections.RemoveAt(i);
					if (!this.IsConnectionAtCoordinates(connectionAtCoordinates.Coordinates))
					{
						connectionAtCoordinates.BlockObject.DeleteEntity();
						Object.Destroy(connectionAtCoordinates.BlockObject.GameObject);
					}
				}
			}
			this._ziplineCableRenderer.RemoveConnection(ziplineTower, otherZiplineTower);
			this._ziplineCableNavMesh.RemoveConnection(ziplineTower, otherZiplineTower);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000037C8 File Offset: 0x000019C8
		public bool InclinationIsValid(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, out float inclination, out float maxInclination)
		{
			ValueTuple<Vector3, Vector3> valueTuple = ZiplineCalculator.CalculateGridAnchors(ziplineTower.CableAnchorPoint, otherZiplineTower.CableAnchorPoint);
			Vector3 item = valueTuple.Item1;
			Vector3 normalized = (valueTuple.Item2 - item).normalized;
			inclination = Math.Abs(Vector3.Angle(new Vector3(0f, 0f, 1f), normalized) - 90f);
			maxInclination = (float)this._maxCableInclination;
			return inclination < maxInclination;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003839 File Offset: 0x00001A39
		public bool DistanceIsValid(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, out float distance, out float maxDistance)
		{
			distance = Vector3.Distance(ziplineTower.CableAnchorPoint, otherZiplineTower.CableAnchorPoint);
			maxDistance = (float)Math.Min(ziplineTower.MaxDistance, otherZiplineTower.MaxDistance);
			return distance <= maxDistance;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003870 File Offset: 0x00001A70
		public bool DistrictCentersAreCompatible(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			DistrictCenter districtCenter = ZiplineConnectionService.GetDistrictCenter(ziplineTower);
			if (districtCenter)
			{
				DistrictCenter districtCenter2 = ZiplineConnectionService.GetDistrictCenter(otherZiplineTower);
				if (districtCenter2 != null)
				{
					return districtCenter == districtCenter2;
				}
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000389C File Offset: 0x00001A9C
		public bool ConnectionIsValid(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			float num;
			float num2;
			if (this.InclinationIsValid(ziplineTower, otherZiplineTower, out num, out num2) && this.DistanceIsValid(ziplineTower, otherZiplineTower, out num2, out num))
			{
				this.AddCoordinates(ziplineTower, otherZiplineTower);
				bool result = this.BlocksValid() && this.PreviewNotBlocking(ziplineTower, otherZiplineTower);
				this._coordinatesCache.Clear();
				return result;
			}
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void AddCoordinates(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			Vector3Int start = ziplineTower.CableAnchorPoint.FloorToInt();
			Vector3Int end = otherZiplineTower.CableAnchorPoint.FloorToInt();
			this._bresenhamLineDrawer.DrawLine(start, end, this._coordinatesCache);
			this.RemoveNonBlockingCoordinates(ziplineTower);
			this.RemoveNonBlockingCoordinates(otherZiplineTower);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003938 File Offset: 0x00001B38
		public void RemoveNonBlockingCoordinates(ZiplineTower ziplineTower)
		{
			this._coordinatesCache.Remove(ziplineTower.CableAnchorPoint.FloorToInt());
			BlockObject component = ziplineTower.GetComponent<BlockObject>();
			foreach (Vector3Int coordinates in ziplineTower.UnobstructedCoordinates)
			{
				this._coordinatesCache.Remove(component.TransformCoordinates(coordinates));
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003998 File Offset: 0x00001B98
		public bool BlocksValid()
		{
			BlockObjectSpec ziplineConnectionBlock = this._ziplineConnectionBlockFactory.ZiplineConnectionBlock;
			foreach (Vector3Int coordinates in this._coordinatesCache)
			{
				if (!this._blockValidator.BlocksValid(ziplineConnectionBlock, new Placement(coordinates)) && !this._blockService.GetBottomObjectComponentAt<CableBlock>(coordinates))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A20 File Offset: 0x00001C20
		public bool PreviewNotBlocking(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			BlockObject component = ziplineTower.GetComponent<BlockObject>();
			BlockObject component2 = otherZiplineTower.GetComponent<BlockObject>();
			return (!component.IsPreview || !this.PreviewIsBlocking(component)) && (!component2.IsPreview || !this.PreviewIsBlocking(component2));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A64 File Offset: 0x00001C64
		public bool PreviewIsBlocking(BlockObject blockObject)
		{
			BlockOccupations ziplineConnectionOccupation = this._ziplineConnectionBlockFactory.ZiplineConnectionOccupation;
			foreach (Vector3Int item in blockObject.PositionedBlocks.GetOccupiedCoordinatesIntersecting(ziplineConnectionOccupation))
			{
				if (this._coordinatesCache.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public void GetBlockingObjects(IList<BlockObject> blockingObjects)
		{
			foreach (Vector3Int coordinates in this._coordinatesCache)
			{
				if (this._blockService.AnyObjectAt(coordinates) && !this._blockService.GetBottomObjectComponentAt<CableBlock>(coordinates))
				{
					foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
					{
						if (blockObject.HasComponent<IBlockObjectModel>())
						{
							blockingObjects.Add(blockObject);
						}
					}
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003B98 File Offset: 0x00001D98
		public bool IsConnectionAtCoordinates(Vector3Int coordinates)
		{
			for (int i = 0; i < this._connections.Count; i++)
			{
				if (coordinates == this._connections[i].Coordinates)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003BDA File Offset: 0x00001DDA
		public void NotifyConnectionActivated(ZiplineTower ziplineTower)
		{
			this._eventBus.Post(new ZiplineConnectionActivatedEvent(ziplineTower));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003BED File Offset: 0x00001DED
		public static DistrictCenter GetDistrictCenter(ZiplineTower ziplineTower)
		{
			return ziplineTower.GetComponent<PathDistrictRetriever>().GetAnyDistrictCenter();
		}

		// Token: 0x04000031 RID: 49
		public readonly IBlockService _blockService;

		// Token: 0x04000032 RID: 50
		public readonly ZiplineConnectionBlockFactory _ziplineConnectionBlockFactory;

		// Token: 0x04000033 RID: 51
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000034 RID: 52
		public readonly ZiplineCableRenderer _ziplineCableRenderer;

		// Token: 0x04000035 RID: 53
		public readonly ZiplineCableNavMesh _ziplineCableNavMesh;

		// Token: 0x04000036 RID: 54
		public readonly BresenhamLineDrawer _bresenhamLineDrawer;

		// Token: 0x04000037 RID: 55
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000038 RID: 56
		public readonly ISpecService _specService;

		// Token: 0x04000039 RID: 57
		public readonly EventBus _eventBus;

		// Token: 0x0400003A RID: 58
		public readonly List<ZiplineConnectionService.ConnectionAtCoordinates> _connections = new List<ZiplineConnectionService.ConnectionAtCoordinates>();

		// Token: 0x0400003B RID: 59
		public readonly HashSet<Vector3Int> _coordinatesCache = new HashSet<Vector3Int>();

		// Token: 0x0400003C RID: 60
		public Transform _root;

		// Token: 0x0400003D RID: 61
		public int _maxCableInclination;

		// Token: 0x02000016 RID: 22
		public readonly struct ConnectionAtCoordinates
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003BFA File Offset: 0x00001DFA
			public ZiplineTower First { get; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003C02 File Offset: 0x00001E02
			public ZiplineTower Second { get; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C0A File Offset: 0x00001E0A
			public Vector3Int Coordinates { get; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003C12 File Offset: 0x00001E12
			public BlockObject BlockObject { get; }

			// Token: 0x060000A4 RID: 164 RVA: 0x00003C1A File Offset: 0x00001E1A
			public ConnectionAtCoordinates(ZiplineTower first, ZiplineTower second, Vector3Int coordinates, BlockObject blockObject)
			{
				this.First = first;
				this.Second = second;
				this.Coordinates = coordinates;
				this.BlockObject = blockObject;
			}
		}
	}
}
