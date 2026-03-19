using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000016 RID: 22
	public class DistrictPathNavRangeDrawer : BaseComponent, IAwakableComponent, IStartableComponent, ILateUpdatableComponent
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000360D File Offset: 0x0000180D
		public DistrictPathNavRangeDrawer(PathMeshDrawerFactory pathMeshDrawerFactory, IBlockService blockService, INavMeshService navMeshService, PreviewBlockService previewBlockService, ILevelVisibilityService levelVisibilityService, INavigationRangeService navigationRangeService)
		{
			this._pathMeshDrawerFactory = pathMeshDrawerFactory;
			this._blockService = blockService;
			this._navMeshService = navMeshService;
			this._previewBlockService = previewBlockService;
			this._levelVisibilityService = levelVisibilityService;
			this._navigationRangeService = navigationRangeService;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000364D File Offset: 0x0000184D
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			base.DisableComponent();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003661 File Offset: 0x00001861
		public void Start()
		{
			this._regularMeshDrawer = this._pathMeshDrawerFactory.CreateRegularDrawer(new PathMeshDrawer.ConnectionKey(this.RegularConnectionKey));
			this._stairsMeshDrawer = this._pathMeshDrawerFactory.CreateStairsDrawer(new PathMeshDrawer.ConnectionKey(this.StairsConnectionKey));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000369D File Offset: 0x0000189D
		public void LateUpdate()
		{
			if (this._dirty)
			{
				this.UpdateAllNodes();
				this.UpdateDrawers();
				this._dirty = false;
			}
			this.Draw();
			base.DisableComponent();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000036C6 File Offset: 0x000018C6
		public void DrawRange(DrawingParameters drawingParameters)
		{
			if (!this._drawingParameters.Equals(drawingParameters))
			{
				this.MarkDirty();
				this._drawingParameters = drawingParameters;
			}
			base.EnableComponent();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036E9 File Offset: 0x000018E9
		public void MarkDirty()
		{
			this._dirty = true;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000036F4 File Offset: 0x000018F4
		public void UpdateAllNodes()
		{
			this._roadNodes.Clear();
			Vector3? vector = this._drawingParameters.IsPreview ? new Vector3?(this._buildingAccessible.CalculateAccess()) : this._buildingAccessible.Accessible.UnblockedSingleAccessInstant;
			if (vector != null)
			{
				IEnumerable<WeightedCoordinates> values = this._drawingParameters.IsPreview ? this._navigationRangeService.GetRoadPreviewNodesInRange(vector.Value) : this._navigationRangeService.GetRoadNodesInRange(vector.Value);
				this._roadNodes.AddRange(values);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003785 File Offset: 0x00001985
		public void Draw()
		{
			this._regularMeshDrawer.Draw();
			this._stairsMeshDrawer.Draw();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037A0 File Offset: 0x000019A0
		public void UpdateDrawers()
		{
			this._regularMeshDrawer.Reset();
			this._stairsMeshDrawer.Reset();
			foreach (WeightedCoordinates node in this._roadNodes)
			{
				this.AddTile(node);
			}
			this._regularMeshDrawer.Build();
			this._stairsMeshDrawer.Build();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003820 File Offset: 0x00001A20
		public void AddTile(WeightedCoordinates node)
		{
			Vector3Int coordinates = node.Coordinates;
			if (this.IsTileVisible(coordinates) && this._levelVisibilityService.BlockIsVisible(coordinates))
			{
				if (this.IsConnectedToPath(coordinates, coordinates.Above()))
				{
					this._stairsMeshDrawer.Add(node);
					return;
				}
				if (!this.IsConnectedToPath(coordinates, coordinates.Below()))
				{
					this._regularMeshDrawer.Add(node);
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003884 File Offset: 0x00001A84
		public byte StairsConnectionKey(Vector3Int coordinates, Vector3Int direction)
		{
			byte b = this.RegularConnectionKey(coordinates, direction);
			if (b != PathMeshConnectionKeys.Nothing)
			{
				return PathMeshConnectionKeys.ToAlternativeKey(b);
			}
			Vector3Int coordinates2 = coordinates.Above();
			if (this.RoadNodesContains(coordinates2))
			{
				return this.RegularConnectionKey(coordinates2, direction);
			}
			return PathMeshConnectionKeys.Nothing;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000038C8 File Offset: 0x00001AC8
		public byte RegularConnectionKey(Vector3Int coordinates, Vector3Int direction)
		{
			Vector3Int vector3Int = coordinates + direction;
			if (this.IsDoorstep(coordinates, vector3Int))
			{
				return PathMeshConnectionKeys.Building;
			}
			if (!this.IsConnectedToPathInArea(coordinates, vector3Int))
			{
				return PathMeshConnectionKeys.Nothing;
			}
			return PathMeshConnectionKeys.Path;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003904 File Offset: 0x00001B04
		public bool IsDoorstep(Vector3Int entrance, Vector3Int inside)
		{
			foreach (BlockObject entranceOwner in this.GetBlockObjects(inside))
			{
				if (this.HasValidEntrance(entranceOwner, entrance, inside))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003960 File Offset: 0x00001B60
		public bool IsTileVisible(Vector3Int coordinates)
		{
			return !this._blockService.GetPathObjectComponentAt<PathMeshHider>(coordinates) && !this._previewBlockService.GetPathObjectComponentAt<PathMeshHider>(coordinates);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000398C File Offset: 0x00001B8C
		public IEnumerable<BlockObject> GetBlockObjects(Vector3Int coordinates)
		{
			ReadOnlyList<BlockObject> objectsAt = this._blockService.GetObjectsAt(coordinates);
			if (objectsAt.IsEmpty() && this._drawingParameters.IsPreview)
			{
				return this._previewBlockService.GetPreviewsAt(coordinates);
			}
			return objectsAt;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000039D0 File Offset: 0x00001BD0
		public bool HasValidEntrance(BlockObject entranceOwner, Vector3Int entrance, Vector3Int inside)
		{
			return entranceOwner && (entranceOwner.IsFinished || this._drawingParameters.IsPreview) && entranceOwner.HasEntrance && entranceOwner.PositionedEntrance.Coordinates == entrance && entranceOwner.PositionedEntrance.DoorstepCoordinates == inside && this.IsConnected(entrance, inside);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A32 File Offset: 0x00001C32
		public bool IsConnected(Vector3Int coordinates, Vector3Int neighbor)
		{
			if (!this._drawingParameters.IsPreview)
			{
				return this._navMeshService.AreConnectedInstant(coordinates, neighbor);
			}
			return this._navMeshService.AreConnectedPreview(coordinates, neighbor);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003A5C File Offset: 0x00001C5C
		public bool IsConnectedToPathInArea(Vector3Int coordinates, Vector3Int neighbor)
		{
			return this.IsConnectedToPath(coordinates, neighbor) && this.RoadNodesContains(neighbor);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A71 File Offset: 0x00001C71
		public bool IsConnectedToPath(Vector3Int coordinates, Vector3Int neighbor)
		{
			if (!this._drawingParameters.IsPreview)
			{
				return this._navMeshService.AreConnectedRoadInstant(coordinates, neighbor);
			}
			return this._navMeshService.AreConnectedRoadPreview(coordinates, neighbor);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A9B File Offset: 0x00001C9B
		public bool RoadNodesContains(Vector3Int coordinates)
		{
			return this._roadNodes.Contains(new WeightedCoordinates(coordinates, 0f));
		}

		// Token: 0x0400004B RID: 75
		public readonly PathMeshDrawerFactory _pathMeshDrawerFactory;

		// Token: 0x0400004C RID: 76
		public readonly IBlockService _blockService;

		// Token: 0x0400004D RID: 77
		public readonly INavMeshService _navMeshService;

		// Token: 0x0400004E RID: 78
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x0400004F RID: 79
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000050 RID: 80
		public readonly INavigationRangeService _navigationRangeService;

		// Token: 0x04000051 RID: 81
		public BuildingAccessible _buildingAccessible;

		// Token: 0x04000052 RID: 82
		public PathMeshDrawer _regularMeshDrawer;

		// Token: 0x04000053 RID: 83
		public PathMeshDrawer _stairsMeshDrawer;

		// Token: 0x04000054 RID: 84
		public readonly HashSet<WeightedCoordinates> _roadNodes = new HashSet<WeightedCoordinates>();

		// Token: 0x04000055 RID: 85
		public bool _dirty;

		// Token: 0x04000056 RID: 86
		public DrawingParameters _drawingParameters;
	}
}
