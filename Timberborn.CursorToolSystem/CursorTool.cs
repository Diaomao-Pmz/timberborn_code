using System;
using Timberborn.InputSystem;
using Timberborn.Options;
using Timberborn.SelectionSystem;
using Timberborn.ToolSystem;
using Timberborn.UILayoutSystem;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x0200000C RID: 12
	public class CursorTool : ITool, IInputProcessor
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002850 File Offset: 0x00000A50
		public CursorTool(EntitySelectionService entitySelectionService, InputService inputService, IOptionsBox optionsBox, UIVisibilityManager uiVisibilityManager, SelectableObjectRaycaster selectableObjectRaycaster)
		{
			this._entitySelectionService = entitySelectionService;
			this._inputService = inputService;
			this._optionsBox = optionsBox;
			this._uiVisibilityManager = uiVisibilityManager;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000287D File Offset: 0x00000A7D
		public bool ProcessInput()
		{
			return this.ProcessSelectObject() || this.ProcessUnselectObject() || this.ProcessShowOptions();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002897 File Offset: 0x00000A97
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._disableNextExitUnselect = false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028AC File Offset: 0x00000AAC
		public void Exit()
		{
			if (this._disableNextExitUnselect)
			{
				this._disableNextExitUnselect = false;
			}
			else
			{
				this._entitySelectionService.Unselect();
			}
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028D6 File Offset: 0x00000AD6
		public void DisableNextExitUnselect()
		{
			this._disableNextExitUnselect = true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028E0 File Offset: 0x00000AE0
		public bool ProcessSelectObject()
		{
			if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
			{
				SelectableObject target;
				if (this._selectableObjectRaycaster.TryHitSelectableObjectIncludeTerrainStump(out target))
				{
					this._entitySelectionService.Select(target);
				}
				else
				{
					this._entitySelectionService.Unselect();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002932 File Offset: 0x00000B32
		public bool ProcessUnselectObject()
		{
			if (this._entitySelectionService.IsAnythingSelected && this._inputService.Cancel)
			{
				this._entitySelectionService.Unselect();
				return true;
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000295C File Offset: 0x00000B5C
		public bool ProcessShowOptions()
		{
			if (this._inputService.UICancel && this._uiVisibilityManager.GUIVisible)
			{
				this._optionsBox.Show();
				return true;
			}
			return false;
		}

		// Token: 0x0400002C RID: 44
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400002D RID: 45
		public readonly InputService _inputService;

		// Token: 0x0400002E RID: 46
		public readonly IOptionsBox _optionsBox;

		// Token: 0x0400002F RID: 47
		public readonly UIVisibilityManager _uiVisibilityManager;

		// Token: 0x04000030 RID: 48
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000031 RID: 49
		public bool _disableNextExitUnselect;
	}
}
