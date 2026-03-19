using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.Common;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.MapEditorConstructionGuidelinesUI;
using Timberborn.MapEditorNaturalResources;
using Timberborn.NaturalResources;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UndoSystem;
using UnityEngine;

namespace Timberborn.MapEditorNaturalResourcesUI
{
	// Token: 0x0200000C RID: 12
	public class NaturalResourceSpawningBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithGuidelines
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C0A File Offset: 0x00000E0A
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002C12 File Offset: 0x00000E12
		public int BrushSize { get; set; } = 3;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C1B File Offset: 0x00000E1B
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002C23 File Offset: 0x00000E23
		public BrushShape BrushShape { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C2C File Offset: 0x00000E2C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002C34 File Offset: 0x00000E34
		public float Density { get; set; } = 1f;

		// Token: 0x06000047 RID: 71 RVA: 0x00002C40 File Offset: 0x00000E40
		public NaturalResourceSpawningBrushTool(InputService inputService, NaturalResourceSpawner naturalResourceSpawner, BrushProbabilityMap brushProbabilityMap, MarkerDrawerFactory markerDrawerFactory, ILoc loc, NaturalResourceLayerService naturalResourceLayerService, NaturalResourceBrushIterator naturalResourceBrushIterator, IUndoRegistry undoRegistry, ISpecService specService)
		{
			this._inputService = inputService;
			this._naturalResourceSpawner = naturalResourceSpawner;
			this._brushProbabilityMap = brushProbabilityMap;
			this._markerDrawerFactory = markerDrawerFactory;
			this._loc = loc;
			this._naturalResourceLayerService = naturalResourceLayerService;
			this._naturalResourceBrushIterator = naturalResourceBrushIterator;
			this._undoRegistry = undoRegistry;
			this._specService = specService;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CAA File Offset: 0x00000EAA
		public bool RandomizeYieldGrowth
		{
			get
			{
				return this._naturalResourceSpawner.RandomizeYieldGrowth;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void Load()
		{
			this.InitializeToolDescription();
			NaturalResourceBrushSpec singleSpec = this._specService.GetSingleSpec<NaturalResourceBrushSpec>();
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer(singleSpec.SpawnTileColor);
			this.InitializeEnabledTypes(singleSpec.DefaultNaturalResourceId);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CFA File Offset: 0x00000EFA
		public bool ProcessInput()
		{
			this.ProcessBrush();
			if (!this._inputService.MainMouseButtonHeld)
			{
				this._undoRegistry.CommitStack();
			}
			return false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D1B File Offset: 0x00000F1B
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D29 File Offset: 0x00000F29
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._naturalResourceBrushIterator.Reset();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D4D File Offset: 0x00000F4D
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D55 File Offset: 0x00000F55
		public void EnableSpawnableResource(SpawnableResource id)
		{
			this._enabledSpawnableResources.Add(id);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D64 File Offset: 0x00000F64
		public void DisableSpawnableResource(SpawnableResource id)
		{
			this._enabledSpawnableResources.Remove(id);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D73 File Offset: 0x00000F73
		public bool IsNaturalResourceEnabled(SpawnableResource id)
		{
			return this._enabledSpawnableResources.Contains(id);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D81 File Offset: 0x00000F81
		public void SwitchRandomizeYieldGrowth(bool state)
		{
			this._naturalResourceSpawner.RandomizeYieldGrowth = state;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D90 File Offset: 0x00000F90
		private void InitializeEnabledTypes(string defaultType)
		{
			SpawnableResource item = new SpawnableResource(defaultType, false);
			this._enabledSpawnableResources = new HashSet<SpawnableResource>
			{
				item
			};
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DBC File Offset: 0x00000FBC
		private void ProcessBrush()
		{
			foreach (Vector3Int vector3Int in this._naturalResourceBrushIterator.Iterate(this.BrushSize, this.BrushShape))
			{
				if (this._inputService.MainMouseButtonHeld && !this._inputService.MouseOverUI)
				{
					this.ProcessClickedTile(vector3Int);
				}
				this._meshDrawer.DrawAtCoordinates(vector3Int, NaturalResourceSpawningBrushTool.MarkerYOffset);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E48 File Offset: 0x00001048
		private void ProcessClickedTile(Vector3Int coords3D)
		{
			this._naturalResourceLayerService.Enable();
			if (!this._enabledSpawnableResources.IsEmpty<SpawnableResource>() && this._brushProbabilityMap.TestProbabilityAtCoordinates(coords3D.XY(), this.Density))
			{
				this._naturalResourceSpawner.Spawn(this._enabledSpawnableResources, coords3D);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E98 File Offset: 0x00001098
		private void InitializeToolDescription()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(NaturalResourceSpawningBrushTool.TitleLocKey)).Build();
		}

		// Token: 0x04000036 RID: 54
		private static readonly string TitleLocKey = "MapEditor.Brush.NaturalResourceSpawning";

		// Token: 0x04000037 RID: 55
		private static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400003B RID: 59
		private readonly InputService _inputService;

		// Token: 0x0400003C RID: 60
		private readonly NaturalResourceSpawner _naturalResourceSpawner;

		// Token: 0x0400003D RID: 61
		private readonly BrushProbabilityMap _brushProbabilityMap;

		// Token: 0x0400003E RID: 62
		private readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400003F RID: 63
		private readonly ILoc _loc;

		// Token: 0x04000040 RID: 64
		private readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x04000041 RID: 65
		private readonly NaturalResourceBrushIterator _naturalResourceBrushIterator;

		// Token: 0x04000042 RID: 66
		private readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000043 RID: 67
		private readonly ISpecService _specService;

		// Token: 0x04000044 RID: 68
		private MeshDrawer _meshDrawer;

		// Token: 0x04000045 RID: 69
		private ToolDescription _toolDescription;

		// Token: 0x04000046 RID: 70
		private HashSet<SpawnableResource> _enabledSpawnableResources;
	}
}
