using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Localization;
using Timberborn.MapEditorConstructionGuidelinesUI;
using Timberborn.MapStateSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.TerrainPhysics;
using Timberborn.TerrainQueryingSystem;
using Timberborn.TerrainSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;
using UnityEngine;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x02000010 RID: 16
	public class SculptingTerrainBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithDirection, IBrushWithGuidelines
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000039FB File Offset: 0x00001BFB
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003A03 File Offset: 0x00001C03
		public bool Increase { get; set; } = true;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003A0C File Offset: 0x00001C0C
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003A14 File Offset: 0x00001C14
		public bool Inverse { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003A20 File Offset: 0x00001C20
		public SculptingTerrainBrushTool(InputService inputService, ITerrainService terrainService, SculptingTerrainPicker sculptingTerrainPicker, TerrainPicker terrainPicker, MarkerDrawerFactory markerDrawerFactory, ITerrainPhysicsService terrainPhysicsService, ILevelVisibilityService levelVisibilityService, TerrainIntegrityService terrainIntegrityService, ILoc loc, MeasurableAreaDrawer measurableAreaDrawer, IUndoRegistry undoRegistry, MapSize mapSize, ISpecService specService)
		{
			this._inputService = inputService;
			this._terrainService = terrainService;
			this._sculptingTerrainPicker = sculptingTerrainPicker;
			this._terrainPicker = terrainPicker;
			this._markerDrawerFactory = markerDrawerFactory;
			this._terrainPhysicsService = terrainPhysicsService;
			this._levelVisibilityService = levelVisibilityService;
			this._terrainIntegrityService = terrainIntegrityService;
			this._loc = loc;
			this._measurableAreaDrawer = measurableAreaDrawer;
			this._undoRegistry = undoRegistry;
			this._mapSize = mapSize;
			this._specService = specService;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public bool IsIncreasing
		{
			get
			{
				return (this.Increase && !this.Inverse) || (!this.Increase && this.Inverse);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003ADC File Offset: 0x00001CDC
		public void Load()
		{
			this._brushColorSpec = this._specService.GetSingleSpec<BrushColorSpec>();
			this._toolDescription = new ToolDescription.Builder(this._loc.T(SculptingTerrainBrushTool.TitleLocKey)).Build();
			this._smallMarkerDrawer = this._markerDrawerFactory.CreateSmallBlockTileDrawer();
			this._largeMarkerDrawer = this._markerDrawerFactory.CreateLargeBlockTileDrawer();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003B3C File Offset: 0x00001D3C
		public bool ProcessInput()
		{
			if (!this.IsIncreasing)
			{
				return this._sculptingTerrainPicker.PickTerrainAreaToRemove(new AreaPicker.IntAreaCallback(this.DrawPreview), new AreaPicker.IntAreaCallback(this.ApplyChanges));
			}
			return this._sculptingTerrainPicker.PickTerrainAreaToAdd(new AreaPicker.IntAreaCallback(this.DrawPreview), new AreaPicker.IntAreaCallback(this.ApplyChanges));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B98 File Offset: 0x00001D98
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._terrainIntegrityService.ClearHighlight();
			this._sculptingTerrainPicker.Reset();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BCA File Offset: 0x00001DCA
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public void ApplyChanges(IEnumerable<Vector3Int> pickedBlocks, Ray ray)
		{
			if (this._terrainPicker.PickTerrainCoordinates(ray) != null)
			{
				this.UpdateBlocksCache(pickedBlocks);
				TerrainIntegrityService terrainIntegrityService = this._terrainIntegrityService;
				IEnumerable<Vector3Int> blocksToApply = this._blocksToApply;
				IEnumerable<Vector3Int> integrityChanges;
				if (!this.IsIncreasing)
				{
					IEnumerable<Vector3Int> blocksToApply2 = this._blocksToApply;
					integrityChanges = blocksToApply2;
				}
				else
				{
					integrityChanges = Enumerable.Empty<Vector3Int>();
				}
				terrainIntegrityService.RemoveViolatingElements(blocksToApply, integrityChanges);
				foreach (Vector3Int coordinates in this._blocksToApply)
				{
					if (this.IsIncreasing)
					{
						this._terrainService.SetTerrain(coordinates, 1);
					}
					else
					{
						this._terrainService.UnsetTerrain(coordinates, 1);
					}
				}
			}
			this.ClearBlocksCache();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public void DrawPreview(IEnumerable<Vector3Int> pickedBlocks, Ray ray)
		{
			this._terrainIntegrityService.ClearHighlight();
			if (this._terrainPicker.PickTerrainCoordinates(ray) != null)
			{
				this.UpdateBlocksCache(pickedBlocks);
				this._measurableAreaDrawer.AddMeasurableCoordinates(this._blocksToApply);
				TerrainIntegrityService terrainIntegrityService = this._terrainIntegrityService;
				IEnumerable<Vector3Int> blocksToApply = this._blocksToApply;
				IEnumerable<Vector3Int> integrityChanges;
				if (!this.IsIncreasing)
				{
					IEnumerable<Vector3Int> blocksToApply2 = this._blocksToApply;
					integrityChanges = blocksToApply2;
				}
				else
				{
					integrityChanges = Enumerable.Empty<Vector3Int>();
				}
				terrainIntegrityService.HighlightViolatingElements(blocksToApply, integrityChanges);
				foreach (Vector3Int coordinates in this._blocksToApply)
				{
					if (this.IsIncreasing)
					{
						this._smallMarkerDrawer.DrawAtCoordinates(coordinates, SculptingTerrainBrushTool.MarkerYOffset, this._brushColorSpec.Positive);
					}
					else
					{
						this._largeMarkerDrawer.DrawAtCoordinates(coordinates, SculptingTerrainBrushTool.MarkerYOffset, this._brushColorSpec.Negative);
					}
				}
			}
			this.ClearBlocksCache();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D98 File Offset: 0x00001F98
		public void UpdateBlocksCache(IEnumerable<Vector3Int> pickedBlocks)
		{
			this.CollectCandidateBlocks(pickedBlocks);
			this.CollectBlocksToApply();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public void CollectCandidateBlocks(IEnumerable<Vector3Int> pickedBlocks)
		{
			foreach (Vector3Int vector3Int in pickedBlocks)
			{
				if (this.IsValidCandidateBlock(vector3Int))
				{
					this._candidateBlocks.Add(vector3Int);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003E00 File Offset: 0x00002000
		public bool IsValidCandidateBlock(Vector3Int block)
		{
			bool flag = this._terrainService.Underground(block);
			return ((this.IsIncreasing && !flag) || (!this.IsIncreasing && flag)) && this._terrainService.Contains(block) && block.z < this._levelVisibilityService.MaxVisibleLevel + 1 && block.z < this._mapSize.MaxMapEditorTerrainHeight;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003E6F File Offset: 0x0000206F
		public void CollectBlocksToApply()
		{
			if (this.IsIncreasing)
			{
				this._terrainPhysicsService.GetValidTerrainToAdd(this._candidateBlocks, this._blocksToApply);
				return;
			}
			this._blocksToApply.AddRange(this._candidateBlocks);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003EA2 File Offset: 0x000020A2
		public void ClearBlocksCache()
		{
			this._candidateBlocks.Clear();
			this._blocksToApply.Clear();
		}

		// Token: 0x0400005F RID: 95
		public static readonly string TitleLocKey = "MapEditor.Brush.SculptingTerrain";

		// Token: 0x04000060 RID: 96
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x04000063 RID: 99
		public readonly InputService _inputService;

		// Token: 0x04000064 RID: 100
		public readonly ITerrainService _terrainService;

		// Token: 0x04000065 RID: 101
		public readonly SculptingTerrainPicker _sculptingTerrainPicker;

		// Token: 0x04000066 RID: 102
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x04000067 RID: 103
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000068 RID: 104
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x04000069 RID: 105
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400006A RID: 106
		public readonly TerrainIntegrityService _terrainIntegrityService;

		// Token: 0x0400006B RID: 107
		public readonly ILoc _loc;

		// Token: 0x0400006C RID: 108
		public readonly MeasurableAreaDrawer _measurableAreaDrawer;

		// Token: 0x0400006D RID: 109
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400006E RID: 110
		public readonly MapSize _mapSize;

		// Token: 0x0400006F RID: 111
		public readonly ISpecService _specService;

		// Token: 0x04000070 RID: 112
		public BrushColorSpec _brushColorSpec;

		// Token: 0x04000071 RID: 113
		public MeshDrawer _smallMarkerDrawer;

		// Token: 0x04000072 RID: 114
		public MeshDrawer _largeMarkerDrawer;

		// Token: 0x04000073 RID: 115
		public ToolDescription _toolDescription;

		// Token: 0x04000074 RID: 116
		public readonly HashSet<Vector3Int> _candidateBlocks = new HashSet<Vector3Int>();

		// Token: 0x04000075 RID: 117
		public readonly HashSet<Vector3Int> _blocksToApply = new HashSet<Vector3Int>();
	}
}
