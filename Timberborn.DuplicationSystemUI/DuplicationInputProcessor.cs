using System;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x0200000A RID: 10
	public class DuplicationInputProcessor : ILoadableSingleton, IUnloadableSingleton, IInputProcessor
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000257E File Offset: 0x0000077E
		public DuplicationInputProcessor(InputService inputService, ToolService toolService, DuplicateSettingsTool duplicateSettingsTool, DuplicationValidator duplicationValidator, EntitySelectionService entitySelectionService, SelectableObjectRaycaster selectableObjectRaycaster)
		{
			this._inputService = inputService;
			this._toolService = toolService;
			this._duplicateSettingsTool = duplicateSettingsTool;
			this._duplicationValidator = duplicationValidator;
			this._entitySelectionService = entitySelectionService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025B3 File Offset: 0x000007B3
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025C1 File Offset: 0x000007C1
		public void Unload()
		{
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025D0 File Offset: 0x000007D0
		public bool ProcessInput()
		{
			if (this._toolService.IsDefaultToolActive && !this._entitySelectionService.IsAnythingSelected)
			{
				SelectableObject selectableObject;
				if (this._inputService.IsKeyDown(DuplicationInputProcessor.DuplicateSettingsKey) && this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject) && this._duplicationValidator.CanDuplicateSettings(selectableObject))
				{
					this._duplicateSettingsTool.ActivateWithSource(selectableObject);
					return true;
				}
				SelectableObject entity;
				Action action;
				if (this._inputService.IsKeyDown(DuplicationInputProcessor.DuplicateObjectKey) && this._selectableObjectRaycaster.TryHitSelectableObject(out entity) && this._duplicationValidator.CanDuplicateObject(entity, out action))
				{
					action();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400002C RID: 44
		public static readonly string DuplicateSettingsKey = "DuplicateSettings";

		// Token: 0x0400002D RID: 45
		public static readonly string DuplicateObjectKey = "DuplicateObject";

		// Token: 0x0400002E RID: 46
		public readonly InputService _inputService;

		// Token: 0x0400002F RID: 47
		public readonly ToolService _toolService;

		// Token: 0x04000030 RID: 48
		public readonly DuplicateSettingsTool _duplicateSettingsTool;

		// Token: 0x04000031 RID: 49
		public readonly DuplicationValidator _duplicationValidator;

		// Token: 0x04000032 RID: 50
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000033 RID: 51
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;
	}
}
