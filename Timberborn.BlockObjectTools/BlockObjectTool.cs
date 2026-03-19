using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.ConstructionGuidelines;
using Timberborn.ConstructionMode;
using Timberborn.Coordinates;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UISound;
using Timberborn.UndoSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200000C RID: 12
	public class BlockObjectTool : IDevModeTool, ITool, IToolDescriptor, IInputProcessor, IConstructionModeEnabler, IBlockObjectGridTool
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000029A3 File Offset: 0x00000BA3
		public PlaceableBlockObjectSpec Template { get; }

		// Token: 0x06000036 RID: 54 RVA: 0x000029AC File Offset: 0x00000BAC
		public BlockObjectTool(PlaceableBlockObjectSpec template, ToolService toolService, InputService inputService, AreaPicker areaPicker, UISoundController uiSoundController, ToolUnlockingService toolUnlockingService, IBlockObjectPlacer blockObjectPlacer, IBlockObjectToolDescriber blockObjectToolDescriber, PreviewPlacer previewPlacer, IUndoRegistry undoRegistry, Duplicator duplicator, PreviewPlacement previewPlacement)
		{
			this.Template = template;
			this._toolService = toolService;
			this._inputService = inputService;
			this._areaPicker = areaPicker;
			this._uiSoundController = uiSoundController;
			this._toolUnlockingService = toolUnlockingService;
			this._blockObjectPlacer = blockObjectPlacer;
			this._blockObjectToolDescriber = blockObjectToolDescriber;
			this._previewPlacer = previewPlacer;
			this._undoRegistry = undoRegistry;
			this._duplicator = duplicator;
			this._previewPlacement = previewPlacement;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A1C File Offset: 0x00000C1C
		public bool IsDevMode
		{
			get
			{
				return this.Template.DevModeTool;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A29 File Offset: 0x00000C29
		public string WarningText
		{
			get
			{
				return this._previewPlacer.WarningText;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A38 File Offset: 0x00000C38
		public bool ProcessInput()
		{
			return this._areaPicker.PickBlockObjectArea(this.Template, this._previewPlacement.Orientation, this._previewPlacement.FlipMode, new AreaPicker.BlockObjectAreaCallback(this.PreviewCallback), new AreaPicker.BlockObjectAreaCallback(this.ActionCallback));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A84 File Offset: 0x00000C84
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A92 File Offset: 0x00000C92
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._previewPlacer.HideAllPreviews();
			this._areaPicker.Reset();
			this._placedAnythingThisFrame = false;
			this._duplicationSource = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public ToolDescription DescribeTool()
		{
			return this._blockObjectToolDescriber.Describe(this, this._blockObjectPlacer);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void ActivateWithDuplicationSource(BaseComponent value)
		{
			this._duplicationSource = value;
			this._previewPlacement.CopyFrom(value.GetComponent<BlockObject>());
			this._toolService.SwitchTool(this);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AFE File Offset: 0x00000CFE
		public void PreviewCallback(IEnumerable<Placement> placements)
		{
			if (this._placedAnythingThisFrame)
			{
				this._placedAnythingThisFrame = false;
				return;
			}
			this.ShowPreviews(placements);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B18 File Offset: 0x00000D18
		public void ActionCallback(IEnumerable<Placement> placements)
		{
			if (this._toolUnlockingService.IsLocked(this))
			{
				this._toolUnlockingService.TryToUnlock(this, delegate()
				{
					this.Place(placements);
				}, new Action(this._previewPlacer.HideAllPreviews));
				return;
			}
			this.Place(placements);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B7D File Offset: 0x00000D7D
		public void ShowPreviews(IEnumerable<Placement> placements)
		{
			this._previewPlacer.ShowPreviews(placements);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void Place(IEnumerable<Placement> placements)
		{
			this._placedAnythingThisFrame = false;
			IEnumerable<Placement> buildableCoordinates = this._previewPlacer.GetBuildableCoordinates(placements);
			BlockObjectSpec spec = this.Template.GetSpec<BlockObjectSpec>();
			foreach (Placement placement in buildableCoordinates)
			{
				this._blockObjectPlacer.Place(spec, placement, new Action<BaseComponent>(this.PlacedCallback));
				this._placedAnythingThisFrame = true;
			}
			this._undoRegistry.CommitStack();
			if (this._placedAnythingThisFrame)
			{
				this._uiSoundController.PlaySound(BlockObjectTool.BlockObjectPlacedSoundName);
				this._previewPlacer.HideAllPreviews();
				return;
			}
			this._uiSoundController.PlayCantDoSound();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C48 File Offset: 0x00000E48
		public void PlacedCallback(BaseComponent baseComponent)
		{
			if (this._duplicationSource)
			{
				this._duplicator.Duplicate(this._duplicationSource, baseComponent.GetComponent<EntityComponent>());
			}
		}

		// Token: 0x04000024 RID: 36
		public static readonly string BlockObjectPlacedSoundName = "UI.BlockObjectPlaced";

		// Token: 0x04000026 RID: 38
		public readonly InputService _inputService;

		// Token: 0x04000027 RID: 39
		public readonly ToolService _toolService;

		// Token: 0x04000028 RID: 40
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000029 RID: 41
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x0400002A RID: 42
		public readonly IBlockObjectPlacer _blockObjectPlacer;

		// Token: 0x0400002B RID: 43
		public readonly IBlockObjectToolDescriber _blockObjectToolDescriber;

		// Token: 0x0400002C RID: 44
		public readonly PreviewPlacer _previewPlacer;

		// Token: 0x0400002D RID: 45
		public readonly IUndoRegistry _undoRegistry;

		// Token: 0x0400002E RID: 46
		public readonly Duplicator _duplicator;

		// Token: 0x0400002F RID: 47
		public readonly PreviewPlacement _previewPlacement;

		// Token: 0x04000030 RID: 48
		public readonly AreaPicker _areaPicker;

		// Token: 0x04000031 RID: 49
		public bool _placedAnythingThisFrame;

		// Token: 0x04000032 RID: 50
		public BaseComponent _duplicationSource;
	}
}
