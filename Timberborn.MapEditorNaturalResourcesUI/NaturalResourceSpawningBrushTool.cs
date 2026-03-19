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
	// Token: 0x02000012 RID: 18
	public class NaturalResourceSpawningBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithGuidelines
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003133 File Offset: 0x00001333
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000313B File Offset: 0x0000133B
		public int BrushSize { get; set; } = 3;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003144 File Offset: 0x00001344
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000314C File Offset: 0x0000134C
		public BrushShape BrushShape { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003155 File Offset: 0x00001355
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000315D File Offset: 0x0000135D
		public float Density { get; set; } = 1f;

		// Token: 0x06000061 RID: 97 RVA: 0x00003168 File Offset: 0x00001368
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000031D2 File Offset: 0x000013D2
		public bool RandomizeYieldGrowth
		{
			get
			{
				return this._naturalResourceSpawner.RandomizeYieldGrowth;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000031E0 File Offset: 0x000013E0
		public void Load()
		{
			this.InitializeToolDescription();
			NaturalResourceBrushSpec singleSpec = this._specService.GetSingleSpec<NaturalResourceBrushSpec>();
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer(singleSpec.SpawnTileColor);
			this.InitializeEnabledTypes(singleSpec.DefaultNaturalResourceId);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003222 File Offset: 0x00001422
		public bool ProcessInput()
		{
			this.ProcessBrush();
			if (!this._inputService.MainMouseButtonHeld)
			{
				this._undoRegistry.CommitStack();
			}
			return false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003243 File Offset: 0x00001443
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003251 File Offset: 0x00001451
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._naturalResourceBrushIterator.Reset();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003275 File Offset: 0x00001475
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000327D File Offset: 0x0000147D
		public void EnableSpawnableResource(SpawnableResource id)
		{
			this._enabledSpawnableResources.Add(id);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000328C File Offset: 0x0000148C
		public void DisableSpawnableResource(SpawnableResource id)
		{
			this._enabledSpawnableResources.Remove(id);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000329B File Offset: 0x0000149B
		public bool IsNaturalResourceEnabled(SpawnableResource id)
		{
			return this._enabledSpawnableResources.Contains(id);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032A9 File Offset: 0x000014A9
		public void SwitchRandomizeYieldGrowth(bool state)
		{
			this._naturalResourceSpawner.RandomizeYieldGrowth = state;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032B8 File Offset: 0x000014B8
		public void InitializeEnabledTypes(string defaultType)
		{
			SpawnableResource item = new SpawnableResource(defaultType, false);
			this._enabledSpawnableResources = new HashSet<SpawnableResource>
			{
				item
			};
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032E4 File Offset: 0x000014E4
		public void ProcessBrush()
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

		// Token: 0x0600006E RID: 110 RVA: 0x00003370 File Offset: 0x00001570
		public void ProcessClickedTile(Vector3Int coords3D)
		{
			this._naturalResourceLayerService.Enable();
			if (!this._enabledSpawnableResources.IsEmpty<SpawnableResource>() && this._brushProbabilityMap.TestProbabilityAtCoordinates(coords3D.XY(), this.Density))
			{
				this._naturalResourceSpawner.Spawn(this._enabledSpawnableResources, coords3D);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033C0 File Offset: 0x000015C0
		public void InitializeToolDescription()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(NaturalResourceSpawningBrushTool.TitleLocKey)).Build();
		}

		// Token: 0x04000056 RID: 86
		public static readonly string TitleLocKey = "MapEditor.Brush.NaturalResourceSpawning";

		// Token: 0x04000057 RID: 87
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400005B RID: 91
		public readonly InputService _inputService;

		// Token: 0x0400005C RID: 92
		public readonly NaturalResourceSpawner _naturalResourceSpawner;

		// Token: 0x0400005D RID: 93
		public readonly BrushProbabilityMap _brushProbabilityMap;

		// Token: 0x0400005E RID: 94
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400005F RID: 95
		public readonly ILoc _loc;

		// Token: 0x04000060 RID: 96
		public readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x04000061 RID: 97
		public readonly NaturalResourceBrushIterator _naturalResourceBrushIterator;

		// Token: 0x04000062 RID: 98
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000063 RID: 99
		public readonly ISpecService _specService;

		// Token: 0x04000064 RID: 100
		public MeshDrawer _meshDrawer;

		// Token: 0x04000065 RID: 101
		public ToolDescription _toolDescription;

		// Token: 0x04000066 RID: 102
		public HashSet<SpawnableResource> _enabledSpawnableResources;
	}
}
