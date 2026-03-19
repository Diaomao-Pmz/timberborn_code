using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.AreaSelectionSystemUI;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Demolishing;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using UnityEngine;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000D RID: 13
	public class DemolishableUnselectionTool : ITool, IToolDescriptor, ILoadableSingleton, IInputProcessor
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002B74 File Offset: 0x00000D74
		public DemolishableUnselectionTool(ILoc loc, ISpecService specService, InputService inputService, CursorService cursorService, AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory)
		{
			this._loc = loc;
			this._specService = specService;
			this._inputService = inputService;
			this._cursorService = cursorService;
			this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
			this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BAC File Offset: 0x00000DAC
		public void Load()
		{
			DemolishingColorsSpec singleSpec = this._specService.GetSingleSpec<DemolishingColorsSpec>();
			this._blockObjectSelectionDrawer = this._blockObjectSelectionDrawerFactory.Create(singleSpec.DeletedObjectHighlightColor, singleSpec.DeletedAreaTileColor, singleSpec.DeletedAreaSideColor);
			this._areaBlockObjectPicker = this._areaBlockObjectPickerFactory.CreatePickingUpwards();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BF9 File Offset: 0x00000DF9
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder(this._loc.T(DemolishableUnselectionTool.TitleLocKey)).AddSection(this._loc.T(DemolishableUnselectionTool.DescriptionLocKey)).Build();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C2A File Offset: 0x00000E2A
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(DemolishableUnselectionTool.CursorKey);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C48 File Offset: 0x00000E48
		public void Exit()
		{
			this._blockObjectSelectionDrawer.StopDrawing();
			this._areaBlockObjectPicker.Reset();
			this._inputService.RemoveInputProcessor(this);
			this._cursorService.ResetCursor();
			this.ShowNoneCallback();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C7D File Offset: 0x00000E7D
		public bool ProcessInput()
		{
			return this._areaBlockObjectPicker.PickBlockObjects<Demolishable>(new AreaBlockObjectPicker.Callback(this.PreviewCallback), new AreaBlockObjectPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback), null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CAF File Offset: 0x00000EAF
		public void PreviewCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			this._blockObjectSelectionDrawer.Draw(blockObjects, start, end, selectingArea);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public void ActionCallback(IEnumerable<BlockObject> blockObjects, Vector3Int start, Vector3Int end, bool selectionStarted, bool selectingArea)
		{
			foreach (BlockObject blockObject in blockObjects)
			{
				blockObject.GetComponent<Demolishable>().Unmark();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D10 File Offset: 0x00000F10
		public void ShowNoneCallback()
		{
			this._blockObjectSelectionDrawer.StopDrawing();
		}

		// Token: 0x04000035 RID: 53
		public static readonly string CursorKey = "CancelCursor";

		// Token: 0x04000036 RID: 54
		public static readonly string TitleLocKey = "DemolishUnselectionTool.Title";

		// Token: 0x04000037 RID: 55
		public static readonly string DescriptionLocKey = "DemolishUnselectionTool.Description";

		// Token: 0x04000038 RID: 56
		public readonly ILoc _loc;

		// Token: 0x04000039 RID: 57
		public readonly ISpecService _specService;

		// Token: 0x0400003A RID: 58
		public readonly InputService _inputService;

		// Token: 0x0400003B RID: 59
		public readonly CursorService _cursorService;

		// Token: 0x0400003C RID: 60
		public readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

		// Token: 0x0400003D RID: 61
		public readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

		// Token: 0x0400003E RID: 62
		public BlockObjectSelectionDrawer _blockObjectSelectionDrawer;

		// Token: 0x0400003F RID: 63
		public AreaBlockObjectPicker _areaBlockObjectPicker;
	}
}
