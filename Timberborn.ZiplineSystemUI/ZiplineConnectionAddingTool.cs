using System;
using Timberborn.Common;
using Timberborn.ConstructionMode;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UISound;
using Timberborn.ZiplineSystem;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000008 RID: 8
	public class ZiplineConnectionAddingTool : ITool, IToolDescriptor, IInputProcessor, IConstructionModeEnabler
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023A0 File Offset: 0x000005A0
		public ZiplineConnectionAddingTool(InputService inputService, SelectableObjectRaycaster selectableObjectRaycaster, EntitySelectionService entitySelectionService, ToolService toolService, ZiplineConnectionService ziplineConnectionService, CursorService cursorService, ILoc loc, ZiplinePreviewCableRenderer ziplinePreviewCableRenderer, UISoundController uiSoundController, ConnectionCandidates connectionCandidates)
		{
			this._inputService = inputService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
			this._entitySelectionService = entitySelectionService;
			this._toolService = toolService;
			this._ziplineConnectionService = ziplineConnectionService;
			this._cursorService = cursorService;
			this._loc = loc;
			this._ziplinePreviewCableRenderer = ziplinePreviewCableRenderer;
			this._uiSoundController = uiSoundController;
			this._connectionCandidates = connectionCandidates;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002400 File Offset: 0x00000600
		public void Enter()
		{
			Asserts.FieldIsNotNull<ZiplineConnectionAddingTool>(this, this._currentZiplineTower, "_currentZiplineTower");
			this._connectionCandidates.Enable(this._currentZiplineTower);
			this._inputService.AddInputProcessor(this);
			this._cursorService.SetCursor(ZiplineConnectionAddingTool.CursorKey);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002440 File Offset: 0x00000640
		public void Exit()
		{
			this._connectionCandidates.Disable();
			this._ziplinePreviewCableRenderer.HidePreview();
			this._inputService.RemoveInputProcessor(this);
			this._cursorService.ResetCursor();
			this._entitySelectionService.Select(this._currentZiplineTower);
			this._currentZiplineTower = null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002492 File Offset: 0x00000692
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder().AddPrioritizedSection(this._loc.T(ZiplineConnectionAddingTool.DescriptionLocKey)).Build();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024B4 File Offset: 0x000006B4
		public bool ProcessInput()
		{
			SelectableObject selectableObject;
			ZiplineTower ziplineTower = this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject) ? selectableObject.GetComponent<ZiplineTower>() : null;
			if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
			{
				if (this._ziplineConnectionService.CanBeConnected(this._currentZiplineTower, ziplineTower))
				{
					this.Connect(ziplineTower);
					this._uiSoundController.PlayClickSound();
					return true;
				}
				this._uiSoundController.PlayCantDoSound();
			}
			this._ziplinePreviewCableRenderer.DrawPreview(this._currentZiplineTower, ziplineTower, this._connectionCandidates.Contains(ziplineTower));
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002546 File Offset: 0x00000746
		public void SwitchTo(ZiplineTower ziplineTower)
		{
			this._currentZiplineTower = ziplineTower;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002550 File Offset: 0x00000750
		public void Connect(ZiplineTower ziplineTower)
		{
			this._ziplineConnectionService.Connect(this._currentZiplineTower, ziplineTower);
			this._toolService.SwitchToDefaultTool();
			this._entitySelectionService.Select(ziplineTower);
			if (ziplineTower.HasFreeSlots)
			{
				this.SwitchTo(ziplineTower);
				this._toolService.SwitchTool(this);
			}
		}

		// Token: 0x04000015 RID: 21
		public static readonly string DescriptionLocKey = "Zipline.PickDestination";

		// Token: 0x04000016 RID: 22
		public static readonly string CursorKey = "PickObjectCursor";

		// Token: 0x04000017 RID: 23
		public readonly InputService _inputService;

		// Token: 0x04000018 RID: 24
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000019 RID: 25
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400001A RID: 26
		public readonly ToolService _toolService;

		// Token: 0x0400001B RID: 27
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x0400001C RID: 28
		public readonly CursorService _cursorService;

		// Token: 0x0400001D RID: 29
		public readonly ILoc _loc;

		// Token: 0x0400001E RID: 30
		public readonly ZiplinePreviewCableRenderer _ziplinePreviewCableRenderer;

		// Token: 0x0400001F RID: 31
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000020 RID: 32
		public readonly ConnectionCandidates _connectionCandidates;

		// Token: 0x04000021 RID: 33
		public ZiplineTower _currentZiplineTower;
	}
}
