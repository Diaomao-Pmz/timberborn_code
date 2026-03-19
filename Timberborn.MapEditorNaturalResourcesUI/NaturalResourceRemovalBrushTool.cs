using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Brushes;
using Timberborn.EntitySystem;
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
	// Token: 0x0200000A RID: 10
	public class NaturalResourceRemovalBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithGuidelines
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002580 File Offset: 0x00000780
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002588 File Offset: 0x00000788
		public int BrushSize { get; set; } = 3;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002591 File Offset: 0x00000791
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002599 File Offset: 0x00000799
		public BrushShape BrushShape { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x000025A4 File Offset: 0x000007A4
		public NaturalResourceRemovalBrushTool(InputService inputService, IBlockService blockService, EntityService entityService, MarkerDrawerFactory markerDrawerFactory, ILoc loc, NaturalResourceLayerService naturalResourceLayerService, NaturalResourceBrushIterator naturalResourceBrushIterator, IUndoRegistry undoRegistry, ISpecService specService)
		{
			this._inputService = inputService;
			this._blockService = blockService;
			this._entityService = entityService;
			this._markerDrawerFactory = markerDrawerFactory;
			this._loc = loc;
			this._naturalResourceLayerService = naturalResourceLayerService;
			this._naturalResourceBrushIterator = naturalResourceBrushIterator;
			this._undoRegistry = undoRegistry;
			this._specService = specService;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002610 File Offset: 0x00000810
		public void Load()
		{
			this.InitializeToolDescription();
			NaturalResourceBrushSpec singleSpec = this._specService.GetSingleSpec<NaturalResourceBrushSpec>();
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer(singleSpec.RemovalTileColor);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002646 File Offset: 0x00000846
		public bool ProcessInput()
		{
			this.ProcessBrush();
			if (!this._inputService.MainMouseButtonHeld)
			{
				this._undoRegistry.CommitStack();
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002667 File Offset: 0x00000867
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002675 File Offset: 0x00000875
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._naturalResourceBrushIterator.Reset();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002699 File Offset: 0x00000899
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026A4 File Offset: 0x000008A4
		private void ProcessBrush()
		{
			foreach (Vector3Int vector3Int in this._naturalResourceBrushIterator.Iterate(this.BrushSize, this.BrushShape))
			{
				if (this._inputService.MainMouseButtonHeld && !this._inputService.MouseOverUI)
				{
					this.DeleteNaturalResourcesAt(vector3Int);
				}
				this._meshDrawer.DrawAtCoordinates(vector3Int, NaturalResourceRemovalBrushTool.MarkerYOffset);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002730 File Offset: 0x00000930
		private void DeleteNaturalResourcesAt(Vector3Int coords3D)
		{
			this._naturalResourceLayerService.Enable();
			this._resourcesToDelete.AddRange(this._blockService.GetObjectsWithComponentAt<NaturalResource>(coords3D));
			foreach (NaturalResource entity in this._resourcesToDelete)
			{
				this._entityService.Delete(entity);
			}
			this._resourcesToDelete.Clear();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027B8 File Offset: 0x000009B8
		private void InitializeToolDescription()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(NaturalResourceRemovalBrushTool.TitleLocKey)).Build();
		}

		// Token: 0x04000019 RID: 25
		private static readonly string TitleLocKey = "MapEditor.Brush.NaturalResourceRemoval";

		// Token: 0x0400001A RID: 26
		private static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400001D RID: 29
		private readonly InputService _inputService;

		// Token: 0x0400001E RID: 30
		private readonly IBlockService _blockService;

		// Token: 0x0400001F RID: 31
		private readonly EntityService _entityService;

		// Token: 0x04000020 RID: 32
		private readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000021 RID: 33
		private readonly ILoc _loc;

		// Token: 0x04000022 RID: 34
		private readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x04000023 RID: 35
		private readonly NaturalResourceBrushIterator _naturalResourceBrushIterator;

		// Token: 0x04000024 RID: 36
		private readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000025 RID: 37
		private readonly ISpecService _specService;

		// Token: 0x04000026 RID: 38
		private MeshDrawer _meshDrawer;

		// Token: 0x04000027 RID: 39
		private ToolDescription _toolDescription;

		// Token: 0x04000028 RID: 40
		private readonly List<NaturalResource> _resourcesToDelete = new List<NaturalResource>();
	}
}
