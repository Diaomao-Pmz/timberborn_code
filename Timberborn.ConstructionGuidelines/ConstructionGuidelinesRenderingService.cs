using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.MapStateSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000009 RID: 9
	public class ConstructionGuidelinesRenderingService : ILoadableSingleton, IInputProcessor, ILateUpdatableSingleton
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023BC File Offset: 0x000005BC
		public ConstructionGuidelinesRenderingService(MapSize mapSize, TileDrawerFactory tileDrawerFactory, ITerrainService terrainService, CursorCoordinatesPicker cursorToolSystem, IBlockService blockService, StackableBlockService stackableBlockService, InputService inputService, MouseController mouseController, ILevelVisibilityService levelVisibilityService, ToolService toolService, ISpecService specService)
		{
			this._mapSize = mapSize;
			this._tileDrawerFactory = tileDrawerFactory;
			this._terrainService = terrainService;
			this._cursorToolSystem = cursorToolSystem;
			this._blockService = blockService;
			this._stackableBlockService = stackableBlockService;
			this._inputService = inputService;
			this._mouseController = mouseController;
			this._levelVisibilityService = levelVisibilityService;
			this._toolService = toolService;
			this._specService = specService;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002474 File Offset: 0x00000674
		public void Load()
		{
			this._radius = this._specService.GetSingleSpec<ConstructionGuidelinesSpec>().Radius;
			this._tilesAtSameLevelDrawer = this._tileDrawerFactory.CrateSameLevelTileDrawer();
			this._footprintDrawer = this._tileDrawerFactory.CreateFootprintTileDrawer();
			this._tilesBelowDrawer = this._tileDrawerFactory.CreateBelowTileDrawer();
			this._tilesAboveDrawer = this._tileDrawerFactory.CreateAboveTileDrawer();
			this._inputService.AddInputProcessor(this);
			this._levelVisibilityService.MaxVisibleLevelChanged += delegate(object _, int _)
			{
				this.UpdateBlockObjectPreviewTiles(this._lastCrossParameters.Min, this._lastCrossParameters.Max, this._lastCrossParameters.Center, this._footprintCoordinates, true);
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024FE File Offset: 0x000006FE
		public bool ProcessInput()
		{
			this._guidelinesKeyHeld = this._inputService.IsKeyHeld(ConstructionGuidelinesRenderingService.ShowGuidelinesKey);
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002518 File Offset: 0x00000718
		public void LateUpdateSingleton()
		{
			if (this.GuidelinesVisible && this._mouseController.IsCursorVisible)
			{
				if (!(this._toolService.ActiveTool is IBlockObjectGridTool) && this.HasCenterMoved())
				{
					this._footprintTiles.Clear();
					this.GetGuidelinesFromMousePosition();
				}
				this._tilesAtSameLevelDrawer.DrawMultipleInstanced(this._tilesAtSameLevel);
				this._tilesBelowDrawer.DrawMultipleInstanced(this._tilesBelow);
				this._tilesAboveDrawer.DrawMultipleInstanced(this._tilesAbove);
				return;
			}
			if (this._tilesAtSameLevel.Count > 0)
			{
				this._tilesAtSameLevel.Clear();
				this._tilesBelow.Clear();
				this._tilesAbove.Clear();
				this._footprintCoordinates.Clear();
				this._lastCrossParameters.Reset();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025E0 File Offset: 0x000007E0
		public ConstructionGuidelinesToggle GetConstructionGuidelinesToggle()
		{
			ConstructionGuidelinesToggle constructionGuidelinesToggle = new ConstructionGuidelinesToggle();
			this._toggles.Add(constructionGuidelinesToggle);
			return constructionGuidelinesToggle;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002600 File Offset: 0x00000800
		public void SetPreviewFootprint(Vector2Int min, Vector2Int max, Vector3 center, IReadOnlyCollection<FootprintCoordinates> footprintCoordinates)
		{
			if (this.GuidelinesVisible)
			{
				this.UpdateBlockObjectPreviewTiles(min, max, center, footprintCoordinates);
				this._footprintDrawer.DrawMultipleInstanced(this._footprintTiles);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002626 File Offset: 0x00000826
		public void EnableGuidelines()
		{
			this._guidelinesEnabled = true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000262F File Offset: 0x0000082F
		public void DisableGuidelines()
		{
			this._guidelinesEnabled = false;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002638 File Offset: 0x00000838
		public bool GuidelinesVisible
		{
			get
			{
				if (!this._guidelinesKeyHeld)
				{
					if (this._guidelinesEnabled)
					{
						if (this._toggles.FastAny((ConstructionGuidelinesToggle toggle) => toggle.Visible))
						{
							goto IL_3C;
						}
					}
					return false;
				}
				IL_3C:
				return !this._inputService.MouseOverUI;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002694 File Offset: 0x00000894
		public bool HasCenterMoved()
		{
			Vector3Int vector3Int;
			return this.TryFindCenter(out vector3Int) && this._lastCrossParameters.CrossParametersUpdated(vector3Int, vector3Int.XY(), vector3Int.XY(), false);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026C8 File Offset: 0x000008C8
		public void GetGuidelinesFromMousePosition()
		{
			ConstructionGuidelinesRenderingService.SetCenterPosition(this._lastCrossParameters.Center);
			IEnumerable<Vector2Int> guidelinesCoordinates = this.GetGuidelinesCoordinates(this._lastCrossParameters.Center, this._lastCrossParameters.Min, this._lastCrossParameters.Max);
			this.AddCoordinatesToGuidelines(this._lastCrossParameters.Center, guidelinesCoordinates);
			this._tilesAtSameLevel.Add(ConstructionGuidelinesRenderingService.CreateMatrix(this._lastCrossParameters.Center, ConstructionGuidelinesRenderingService.MarkerYOffset));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000274E File Offset: 0x0000094E
		public void UpdateBlockObjectPreviewTiles(Vector2Int min, Vector2Int max, Vector3 center, IReadOnlyCollection<FootprintCoordinates> footprintCoordinates)
		{
			this._footprintCoordinates.Clear();
			this._footprintCoordinates.AddRange(footprintCoordinates);
			this.UpdateBlockObjectPreviewTiles(min, max, center, this._footprintCoordinates, false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002778 File Offset: 0x00000978
		public void UpdateBlockObjectPreviewTiles(Vector2Int min, Vector2Int max, Vector3 center, List<FootprintCoordinates> footprintCoordinates, bool forceUpdate = false)
		{
			Vector3Int center2 = center.FloorToInt();
			if (forceUpdate || this._lastCrossParameters.CrossParametersUpdated(center2, min, max, true))
			{
				this.UpdateFootprintTiles(center, footprintCoordinates);
				IEnumerable<Vector2Int> guidelinesCoordinates = this.GetGuidelinesCoordinates(center, min, max);
				IEnumerable<Vector2Int> tilesInsideFootprint = ConstructionGuidelinesRenderingService.GetTilesInsideFootprint(min, max, footprintCoordinates);
				IEnumerable<Vector2Int> guidelinesCoordinates2 = guidelinesCoordinates.Concat(tilesInsideFootprint);
				this.AddCoordinatesToGuidelines(center, guidelinesCoordinates2);
				ConstructionGuidelinesRenderingService.SetCenterPosition(center);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool TryFindCenter(out Vector3Int center)
		{
			CursorCoordinates? cursorCoordinates = this._cursorToolSystem.Pick();
			if (cursorCoordinates != null)
			{
				center = cursorCoordinates.GetValueOrDefault().TileCoordinates;
				return true;
			}
			center = default(Vector3Int);
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002815 File Offset: 0x00000A15
		public static void SetCenterPosition(Vector3 center)
		{
			Shader.SetGlobalVector(ConstructionGuidelinesRenderingService.GuidelinesCenterCoordinatesKey, CoordinateSystem.GridToWorld(center));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000282C File Offset: 0x00000A2C
		public IEnumerable<Vector2Int> GetGuidelinesCoordinates(Vector3 center, Vector2Int min, Vector2Int max)
		{
			Vector3Int mapSize = this._mapSize.TotalSize;
			int num = Math.Max(0, Mathf.FloorToInt(center.x - (float)this._radius)) - 1;
			int maxX = Math.Min(mapSize.x, Mathf.CeilToInt(center.x + (float)this._radius)) + 1;
			int num2;
			for (int x = num; x < maxX; x = num2 + 1)
			{
				for (int y = min.y; y <= max.y; y = num2 + 1)
				{
					if (x < min.x || x > max.x)
					{
						yield return new Vector2Int(x, y);
					}
					num2 = y;
				}
				num2 = x;
			}
			int num3 = Math.Max(0, Mathf.FloorToInt(center.y - (float)this._radius)) - 1;
			int maxY = Math.Min(mapSize.y, Mathf.CeilToInt(center.y + (float)this._radius)) + 1;
			for (int x = num3; x < maxY; x = num2 + 1)
			{
				for (int y = min.x; y <= max.x; y = num2 + 1)
				{
					if (x > max.y || x < min.y)
					{
						yield return new Vector2Int(y, x);
					}
					num2 = y;
				}
				num2 = x;
			}
			yield break;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002854 File Offset: 0x00000A54
		public void AddCoordinatesToGuidelines(Vector3 center, IEnumerable<Vector2Int> guidelinesCoordinates)
		{
			this._tilesAtSameLevel.Clear();
			this._tilesBelow.Clear();
			this._tilesAbove.Clear();
			foreach (Vector3Int coordinates in this._stackableBlockService.GetGroundOrStackableBlocks(guidelinesCoordinates, false))
			{
				int num = Mathf.RoundToInt(center.z);
				if (num <= this._levelVisibilityService.MaxVisibleLevel)
				{
					if (coordinates.z == num)
					{
						this._tilesAtSameLevel.Add(ConstructionGuidelinesRenderingService.CreateMatrix(coordinates, ConstructionGuidelinesRenderingService.MarkerYOffset));
					}
					else if (coordinates.z < num)
					{
						this._tilesBelow.Add(ConstructionGuidelinesRenderingService.CreateMatrix(coordinates, ConstructionGuidelinesRenderingService.MarkerYOffset));
					}
					else
					{
						this._tilesAbove.Add(ConstructionGuidelinesRenderingService.CreateMatrix(coordinates, ConstructionGuidelinesRenderingService.MarkerYOffset));
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000293C File Offset: 0x00000B3C
		public void UpdateFootprintTiles(Vector3 center, IReadOnlyCollection<FootprintCoordinates> footprintCoordinates)
		{
			this._footprintTiles.Clear();
			foreach (FootprintCoordinates footprintCoordinates2 in footprintCoordinates)
			{
				Vector3Int coordinates = footprintCoordinates2.Coordinates;
				int heightBelowFootprint = this.GetHeightBelowFootprint(coordinates, (int)center.z);
				if ((heightBelowFootprint <= this._levelVisibilityService.MaxVisibleLevel && heightBelowFootprint < Mathf.RoundToInt((float)coordinates.z)) || (footprintCoordinates2.CanHaveFootprint && heightBelowFootprint <= Mathf.RoundToInt((float)coordinates.z)))
				{
					this._footprintTiles.Add(ConstructionGuidelinesRenderingService.CreateMatrix(new Vector3Int(coordinates.x, coordinates.y, heightBelowFootprint), ConstructionGuidelinesRenderingService.MarkerYOffset));
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A04 File Offset: 0x00000C04
		public static Matrix4x4 CreateMatrix(Vector3Int coordinates, float markerYOffset)
		{
			return Matrix4x4.TRS(CoordinateSystem.GridToWorld(coordinates) + new Vector3(0.5f, markerYOffset, 0.5f), Quaternion.identity, Vector3.one);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A30 File Offset: 0x00000C30
		public static IEnumerable<Vector2Int> GetTilesInsideFootprint(Vector2Int min, Vector2Int max, IReadOnlyCollection<FootprintCoordinates> footprintCoordinates)
		{
			ConstructionGuidelinesRenderingService.<>c__DisplayClass48_0 CS$<>8__locals1 = new ConstructionGuidelinesRenderingService.<>c__DisplayClass48_0();
			CS$<>8__locals1.x = min.x;
			while (CS$<>8__locals1.x <= max.x)
			{
				ConstructionGuidelinesRenderingService.<>c__DisplayClass48_1 CS$<>8__locals2 = new ConstructionGuidelinesRenderingService.<>c__DisplayClass48_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.y = min.y;
				int num;
				while (CS$<>8__locals2.y <= max.y)
				{
					if (!footprintCoordinates.Any((FootprintCoordinates coordinates) => coordinates.Coordinates.x == CS$<>8__locals2.CS$<>8__locals1.x && coordinates.Coordinates.y == CS$<>8__locals2.y))
					{
						yield return new Vector2Int(CS$<>8__locals2.CS$<>8__locals1.x, CS$<>8__locals2.y);
					}
					num = CS$<>8__locals2.y;
					CS$<>8__locals2.y = num + 1;
				}
				CS$<>8__locals2 = null;
				num = CS$<>8__locals1.x;
				CS$<>8__locals1.x = num + 1;
			}
			CS$<>8__locals1 = null;
			yield break;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A50 File Offset: 0x00000C50
		public int GetHeightBelowFootprint(Vector3Int coordinates, int previewHeight)
		{
			int terrainHeight = this._terrainService.GetTerrainHeight(coordinates);
			for (int i = previewHeight; i >= terrainHeight; i--)
			{
				Vector3Int vector3Int;
				vector3Int..ctor(coordinates.x, coordinates.y, i);
				if (this._blockService.AnyObjectAt(vector3Int) && this._stackableBlockService.IsStackableBlockAt(vector3Int, false))
				{
					return i + 1;
				}
			}
			return terrainHeight;
		}

		// Token: 0x0400000F RID: 15
		public static readonly string ShowGuidelinesKey = "ShowGuidelines";

		// Token: 0x04000010 RID: 16
		public static readonly float MarkerYOffset = 0.022f;

		// Token: 0x04000011 RID: 17
		public static readonly int GuidelinesCenterCoordinatesKey = Shader.PropertyToID("_GuidelinesCenterCoordinates");

		// Token: 0x04000012 RID: 18
		public readonly MapSize _mapSize;

		// Token: 0x04000013 RID: 19
		public readonly TileDrawerFactory _tileDrawerFactory;

		// Token: 0x04000014 RID: 20
		public readonly ITerrainService _terrainService;

		// Token: 0x04000015 RID: 21
		public readonly CursorCoordinatesPicker _cursorToolSystem;

		// Token: 0x04000016 RID: 22
		public readonly IBlockService _blockService;

		// Token: 0x04000017 RID: 23
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000018 RID: 24
		public readonly InputService _inputService;

		// Token: 0x04000019 RID: 25
		public readonly MouseController _mouseController;

		// Token: 0x0400001A RID: 26
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400001B RID: 27
		public readonly ToolService _toolService;

		// Token: 0x0400001C RID: 28
		public readonly ISpecService _specService;

		// Token: 0x0400001D RID: 29
		public readonly List<Matrix4x4> _tilesAtSameLevel = new List<Matrix4x4>();

		// Token: 0x0400001E RID: 30
		public readonly List<Matrix4x4> _footprintTiles = new List<Matrix4x4>();

		// Token: 0x0400001F RID: 31
		public readonly List<Matrix4x4> _tilesBelow = new List<Matrix4x4>();

		// Token: 0x04000020 RID: 32
		public readonly List<Matrix4x4> _tilesAbove = new List<Matrix4x4>();

		// Token: 0x04000021 RID: 33
		public readonly List<FootprintCoordinates> _footprintCoordinates = new List<FootprintCoordinates>();

		// Token: 0x04000022 RID: 34
		public MeshDrawer _tilesAtSameLevelDrawer;

		// Token: 0x04000023 RID: 35
		public MeshDrawer _footprintDrawer;

		// Token: 0x04000024 RID: 36
		public MeshDrawer _tilesBelowDrawer;

		// Token: 0x04000025 RID: 37
		public MeshDrawer _tilesAboveDrawer;

		// Token: 0x04000026 RID: 38
		public bool _guidelinesEnabled;

		// Token: 0x04000027 RID: 39
		public readonly List<ConstructionGuidelinesToggle> _toggles = new List<ConstructionGuidelinesToggle>();

		// Token: 0x04000028 RID: 40
		public bool _guidelinesKeyHeld;

		// Token: 0x04000029 RID: 41
		public readonly CrossParameters _lastCrossParameters = new CrossParameters();

		// Token: 0x0400002A RID: 42
		public int _radius;
	}
}
