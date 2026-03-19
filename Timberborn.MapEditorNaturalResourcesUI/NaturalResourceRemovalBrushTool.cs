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
	// Token: 0x0200000E RID: 14
	public class NaturalResourceRemovalBrushTool : ITool, IToolDescriptor, IInputProcessor, ILoadableSingleton, IBrushWithSize, IBrushWithShape, IBrushWithGuidelines
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A74 File Offset: 0x00000C74
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A7C File Offset: 0x00000C7C
		public int BrushSize { get; set; } = 3;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A85 File Offset: 0x00000C85
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A8D File Offset: 0x00000C8D
		public BrushShape BrushShape { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002A98 File Offset: 0x00000C98
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

		// Token: 0x06000040 RID: 64 RVA: 0x00002B04 File Offset: 0x00000D04
		public void Load()
		{
			this.InitializeToolDescription();
			NaturalResourceBrushSpec singleSpec = this._specService.GetSingleSpec<NaturalResourceBrushSpec>();
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer(singleSpec.RemovalTileColor);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B3A File Offset: 0x00000D3A
		public bool ProcessInput()
		{
			this.ProcessBrush();
			if (!this._inputService.MainMouseButtonHeld)
			{
				this._undoRegistry.CommitStack();
			}
			return false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B5B File Offset: 0x00000D5B
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B69 File Offset: 0x00000D69
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._naturalResourceBrushIterator.Reset();
			this._undoRegistry.CommitStack();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B8D File Offset: 0x00000D8D
		public ToolDescription DescribeTool()
		{
			return this._toolDescription;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B98 File Offset: 0x00000D98
		public void ProcessBrush()
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

		// Token: 0x06000046 RID: 70 RVA: 0x00002C24 File Offset: 0x00000E24
		public void DeleteNaturalResourcesAt(Vector3Int coords3D)
		{
			this._naturalResourceLayerService.Enable();
			this._resourcesToDelete.AddRange(this._blockService.GetObjectsWithComponentAt<NaturalResource>(coords3D));
			foreach (NaturalResource entity in this._resourcesToDelete)
			{
				this._entityService.Delete(entity);
			}
			this._resourcesToDelete.Clear();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CAC File Offset: 0x00000EAC
		public void InitializeToolDescription()
		{
			this._toolDescription = new ToolDescription.Builder(this._loc.T(NaturalResourceRemovalBrushTool.TitleLocKey)).Build();
		}

		// Token: 0x04000034 RID: 52
		public static readonly string TitleLocKey = "MapEditor.Brush.NaturalResourceRemoval";

		// Token: 0x04000035 RID: 53
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x04000038 RID: 56
		public readonly InputService _inputService;

		// Token: 0x04000039 RID: 57
		public readonly IBlockService _blockService;

		// Token: 0x0400003A RID: 58
		public readonly EntityService _entityService;

		// Token: 0x0400003B RID: 59
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400003C RID: 60
		public readonly ILoc _loc;

		// Token: 0x0400003D RID: 61
		public readonly NaturalResourceLayerService _naturalResourceLayerService;

		// Token: 0x0400003E RID: 62
		public readonly NaturalResourceBrushIterator _naturalResourceBrushIterator;

		// Token: 0x0400003F RID: 63
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x04000040 RID: 64
		public readonly ISpecService _specService;

		// Token: 0x04000041 RID: 65
		public MeshDrawer _meshDrawer;

		// Token: 0x04000042 RID: 66
		public ToolDescription _toolDescription;

		// Token: 0x04000043 RID: 67
		public readonly List<NaturalResource> _resourcesToDelete = new List<NaturalResource>();
	}
}
