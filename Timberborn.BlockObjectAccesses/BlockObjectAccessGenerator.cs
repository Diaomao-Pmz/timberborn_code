using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Navigation;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000009 RID: 9
	public class BlockObjectAccessGenerator : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000023B1 File Offset: 0x000005B1
		public BlockObjectAccessGenerator(INavMeshService navMeshService, IBlockService blockService, ITerrainService terrainService)
		{
			this._navMeshService = navMeshService;
			this._blockService = blockService;
			this._terrainService = terrainService;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E4 File Offset: 0x000005E4
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._parentedNeighborCalculator = base.GetComponent<ParentedNeighborCalculator>();
			this._isFullyUnderground = this._blockObject.Blocks.GetAllBlocks().All((Block block) => block.Underground);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002444 File Offset: 0x00000644
		public BoundingBox GenerateAccessBounds(int minLevel, int maxLevel)
		{
			BoundingBox.Builder builder = default(BoundingBox.Builder);
			foreach (ParentedNeighbor2D parentedNeighbor2D in this._parentedNeighborCalculator.GetNonInternalParentedNeighbors())
			{
				Vector2Int neighbor = parentedNeighbor2D.Neighbor;
				builder.Expand(new Vector3Int(neighbor.x, neighbor.y, minLevel));
				builder.Expand(new Vector3Int(neighbor.x, neighbor.y, maxLevel));
			}
			return builder.Build();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024DC File Offset: 0x000006DC
		public IEnumerable<Vector3> GenerateAccesses(int minLevel, int maxLevel)
		{
			this.UpdateFoundationToTerrainDistances();
			return this.GetValidAccessWithParentedNeighbor(minLevel, maxLevel).Concat(this.GetInternalParentedNeighbors(maxLevel)).Select(new Func<BlockObjectAccessGenerator.AccessWithParentedNeighbor, Vector3>(this.GetWorldSpaceAccess)).Concat(this.GetBottomAccesses());
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002514 File Offset: 0x00000714
		public IEnumerable<BlockObjectAccessGenerator.AccessWithParentedNeighbor> GetValidAccessWithParentedNeighbor(int minLevel, int maxLevel)
		{
			int topLevel = this._blockObject.Coordinates.z + this._blockObject.Blocks.Size.z;
			foreach (ParentedNeighbor2D neighbor2D in this._parentedNeighborCalculator.GetNonInternalParentedNeighbors())
			{
				Vector2Int accessColumn = neighbor2D.Neighbor;
				Vector2Int parentColumn = neighbor2D.Parent;
				int num;
				for (int z = minLevel; z <= maxLevel; z = num)
				{
					Vector3Int vector3Int = accessColumn.ToVector3Int(z);
					if (!this.NoTerrainInTheWay(parentColumn, vector3Int) && (!this._isFullyUnderground || z > topLevel))
					{
						break;
					}
					if (!this._visitedValidAccesses.Contains(vector3Int) && this.AccessIsValid(vector3Int))
					{
						this._visitedValidAccesses.Add(vector3Int);
						yield return new BlockObjectAccessGenerator.AccessWithParentedNeighbor(vector3Int, neighbor2D);
					}
					num = z + 1;
				}
				accessColumn = default(Vector2Int);
				parentColumn = default(Vector2Int);
				neighbor2D = default(ParentedNeighbor2D);
			}
			IEnumerator<ParentedNeighbor2D> enumerator = null;
			this._visitedValidAccesses.Clear();
			yield break;
			yield break;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002532 File Offset: 0x00000732
		public IEnumerable<BlockObjectAccessGenerator.AccessWithParentedNeighbor> GetInternalParentedNeighbors(int maxLevel)
		{
			foreach (Vector2Int column in this._terrainHeightCache.Keys)
			{
				int num;
				for (int z = this._blockObject.CoordinatesAtBaseZ.z; z <= maxLevel; z = num)
				{
					Vector3Int vector3Int = column.ToVector3Int(z);
					BlockObjectAccessGenerator.AccessWithParentedNeighbor accessWithParentedNeighbor;
					if (!this._visitedValidAccesses.Contains(vector3Int) && this.AccessIsValid(vector3Int) && this.TryGetAccessWithParentedNeighbour(vector3Int, out accessWithParentedNeighbor))
					{
						yield return accessWithParentedNeighbor;
					}
					num = z + 1;
				}
				column = default(Vector2Int);
			}
			Dictionary<Vector2Int, int>.KeyCollection.Enumerator enumerator = default(Dictionary<Vector2Int, int>.KeyCollection.Enumerator);
			this._visitedValidAccesses.Clear();
			this._terrainHeightCache.Clear();
			yield break;
			yield break;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000254C File Offset: 0x0000074C
		public bool TryGetAccessWithParentedNeighbour(Vector3Int access, out BlockObjectAccessGenerator.AccessWithParentedNeighbor accessWithNeighbor)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors8Vector3IntOrdered)
			{
				Vector3Int vector3Int2 = access + vector3Int;
				if (this.NoTerrainInTheWay(vector3Int2.XY(), access))
				{
					accessWithNeighbor = new BlockObjectAccessGenerator.AccessWithParentedNeighbor(access, ParentedNeighbor2D.FromVectors(access, vector3Int2));
					return true;
				}
			}
			accessWithNeighbor = default(BlockObjectAccessGenerator.AccessWithParentedNeighbor);
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025AC File Offset: 0x000007AC
		public void UpdateFoundationToTerrainDistances()
		{
			int num = this._blockObject.Coordinates.z + this._blockObject.Blocks.Size.z;
			foreach (Vector3Int vector3Int in this._blockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				if (vector3Int.z == this._blockObject.CoordinatesAtBaseZ.z)
				{
					Vector3Int vector3Int2 = this._isFullyUnderground ? new Vector3Int(vector3Int.x, vector3Int.y, num) : vector3Int;
					int num2;
					if (this._terrainService.TryGetDistanceToTerrainAbove(vector3Int2, out num2))
					{
						this._terrainHeightCache[vector3Int2.XY()] = vector3Int2.z + num2;
					}
					else
					{
						this._terrainHeightCache[vector3Int2.XY()] = int.MaxValue;
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026B4 File Offset: 0x000008B4
		public Vector3 GetWorldSpaceAccess(BlockObjectAccessGenerator.AccessWithParentedNeighbor accessWithParentedNeighbor)
		{
			Vector3Int access = accessWithParentedNeighbor.Access;
			Vector2Int neighbor = accessWithParentedNeighbor.ParentedNeighbor.Neighbor;
			Vector2Int parent = accessWithParentedNeighbor.ParentedNeighbor.Parent;
			Vector3 accessWithOffset = BlockObjectAccessGenerator.OffsetTowards(neighbor, parent).ToVector3(access.z);
			return this.GetAccessWithPathAdjustedHeight(access, accessWithOffset);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002703 File Offset: 0x00000903
		public IEnumerable<Vector3> GetBottomAccesses()
		{
			foreach (Block block in this._blockObject.PositionedBlocks.GetAllBlocks())
			{
				if (block.IsFoundationBlock || block.Stackable == BlockStackable.UnfinishedGround)
				{
					Vector3Int vector3Int = block.Coordinates.Below();
					if (this.AccessIsValid(vector3Int))
					{
						yield return CoordinateSystem.GridToWorldCentered(vector3Int);
					}
				}
			}
			ImmutableArray<Block>.Enumerator enumerator = default(ImmutableArray<Block>.Enumerator);
			yield break;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002714 File Offset: 0x00000914
		public bool NoTerrainInTheWay(Vector2Int parentColumn, Vector3Int access)
		{
			int num;
			return this._terrainHeightCache.TryGetValue(parentColumn, out num) && access.z < num;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002740 File Offset: 0x00000940
		public bool AccessIsValid(Vector3Int access)
		{
			if (!this.IsAccessBlockedBySelf(access) && this._navMeshService.IsOnNavMesh(access))
			{
				BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(access);
				return !bottomObjectAt || BlockObjectAccessGenerator.CanPlaceAccessInsideObject(access, bottomObjectAt);
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002784 File Offset: 0x00000984
		public bool IsAccessBlockedBySelf(Vector3Int access)
		{
			Block block;
			return this._blockObject.PositionedBlocks.TryGetBlock(access, out block) && block.IsOccupied && access.z == this._blockObject.CoordinatesAtBaseZ.z;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027D0 File Offset: 0x000009D0
		public static Vector2 OffsetTowards(Vector2Int coordinates, Vector2Int target)
		{
			Vector2Int vector2Int = coordinates - target;
			float num = BlockObjectAccessGenerator.OffsetIsDiagonal(vector2Int) ? BlockObjectAccessGenerator.DiagonalOffsetScale : BlockObjectAccessGenerator.StraightOffsetScale;
			Vector2 vector = vector2Int.normalized * num;
			return coordinates - vector;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000281C File Offset: 0x00000A1C
		public Vector3 GetAccessWithPathAdjustedHeight(Vector3Int access, Vector3 accessWithOffset)
		{
			Vector3 vector = CoordinateSystem.GridToWorldCentered(accessWithOffset);
			IPathHeightProvider bottomObjectComponentAt = this._blockService.GetBottomObjectComponentAt<IPathHeightProvider>(access);
			float num;
			if (bottomObjectComponentAt != null && bottomObjectComponentAt.TryGetHeight(vector, out num))
			{
				vector.y = ((num < vector.y) ? num : vector.y);
			}
			return vector;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002868 File Offset: 0x00000A68
		public static bool CanPlaceAccessInsideObject(Vector3Int access, BlockObject blockObject)
		{
			BlockObjectAccesses component = blockObject.GetComponent<BlockObjectAccesses>();
			if (component != null)
			{
				if (component.IsBlocked(access))
				{
					return false;
				}
				if (component.IsAllowed(access))
				{
					return true;
				}
			}
			return !blockObject.Entrance.HasEntrance || blockObject.IsUnfinished;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028AA File Offset: 0x00000AAA
		public static bool OffsetIsDiagonal(Vector2Int offset)
		{
			return offset.x != 0 && offset.y != 0;
		}

		// Token: 0x0400000E RID: 14
		public static readonly float DiagonalOffsetScale = 0.3f;

		// Token: 0x0400000F RID: 15
		public static readonly float StraightOffsetScale = 0.2f;

		// Token: 0x04000010 RID: 16
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000011 RID: 17
		public readonly IBlockService _blockService;

		// Token: 0x04000012 RID: 18
		public readonly ITerrainService _terrainService;

		// Token: 0x04000013 RID: 19
		public readonly HashSet<Vector3Int> _visitedValidAccesses = new HashSet<Vector3Int>();

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<Vector2Int, int> _terrainHeightCache = new Dictionary<Vector2Int, int>();

		// Token: 0x04000015 RID: 21
		public BlockObject _blockObject;

		// Token: 0x04000016 RID: 22
		public ParentedNeighborCalculator _parentedNeighborCalculator;

		// Token: 0x04000017 RID: 23
		public bool _isFullyUnderground;

		// Token: 0x0200000A RID: 10
		public readonly struct AccessWithParentedNeighbor
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000031 RID: 49 RVA: 0x000028D7 File Offset: 0x00000AD7
			public Vector3Int Access { get; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000032 RID: 50 RVA: 0x000028DF File Offset: 0x00000ADF
			public ParentedNeighbor2D ParentedNeighbor { get; }

			// Token: 0x06000033 RID: 51 RVA: 0x000028E7 File Offset: 0x00000AE7
			public AccessWithParentedNeighbor(Vector3Int access, ParentedNeighbor2D parentedNeighbor)
			{
				this.Access = access;
				this.ParentedNeighbor = parentedNeighbor;
			}
		}
	}
}
