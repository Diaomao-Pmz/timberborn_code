using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.ConstructionMode;
using Timberborn.CursorToolSystem;
using Timberborn.DropdownSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.ToolSystemUI;
using Timberborn.UISound;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000018 RID: 24
	public class TransmitterPickerTool : ITool, IInputProcessor, IToolDescriptor, IConstructionModeEnabler, IGroupIgnoringTool
	{
		// Token: 0x06000069 RID: 105 RVA: 0x000031D0 File Offset: 0x000013D0
		public TransmitterPickerTool(InputService inputService, SelectableObjectRaycaster selectableObjectRaycaster, ToolService toolService, CursorService cursorService, CursorTool cursorTool, ILoc loc, UISoundController uiSoundController, EventBus eventBus, DropdownListDrawer dropdownListDrawer, TransmitterPickerToolHighlighter transmitterPickerToolHighlighter, AutomatorRegistry automatorRegistry)
		{
			this._inputService = inputService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
			this._toolService = toolService;
			this._cursorService = cursorService;
			this._cursorTool = cursorTool;
			this._loc = loc;
			this._uiSoundController = uiSoundController;
			this._eventBus = eventBus;
			this._dropdownListDrawer = dropdownListDrawer;
			this._transmitterPickerToolHighlighter = transmitterPickerToolHighlighter;
			this._automatorRegistry = automatorRegistry;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003238 File Offset: 0x00001438
		public void SwitchTo(BaseComponent owner, Dropdown dropdown, Action<Automator> setter)
		{
			Asserts.FieldIsNull<TransmitterPickerTool>(this, this._owner, "_owner");
			this._owner = owner;
			this._dropdown = dropdown;
			this._setter = setter;
			this._cursorTool.DisableNextExitUnselect();
			this._toolService.SwitchTool(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003278 File Offset: 0x00001478
		public void Enter()
		{
			this._inputService.AddInputProcessor(this);
			this._eventBus.Register(this);
			this._dropdownListDrawer.IgnoreWorldInput(true);
			this._transmitterPickerToolHighlighter.Highlight(this._owner);
			this._cursorService.SetCursor(TransmitterPickerTool.CursorKey);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032CC File Offset: 0x000014CC
		public void Exit()
		{
			this._inputService.RemoveInputProcessor(this);
			this._eventBus.Unregister(this);
			this._dropdownListDrawer.IgnoreWorldInput(false);
			this._cursorService.ResetCursor();
			this._transmitterPickerToolHighlighter.Clear();
			this._owner = null;
			this._setter = null;
			this._dropdown.Hide();
			this._dropdown = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003334 File Offset: 0x00001534
		public bool ProcessInput()
		{
			if (this._toolService.ActiveTool == this)
			{
				Automator hoveredTransmitter = this.GetHoveredTransmitter();
				this._transmitterPickerToolHighlighter.UpdateHover(hoveredTransmitter);
				if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
				{
					if (hoveredTransmitter)
					{
						this._uiSoundController.PlayClickSound();
						this._setter(hoveredTransmitter);
						this._dropdown.UpdateSelectedValue();
						this._toolService.SwitchToDefaultTool();
						return true;
					}
					this._uiSoundController.PlayCantDoSound();
				}
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033BF File Offset: 0x000015BF
		public ToolDescription DescribeTool()
		{
			return new ToolDescription.Builder().AddPrioritizedSection(this._loc.T(TransmitterPickerTool.DescriptionLocKey)).Build();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033E0 File Offset: 0x000015E0
		[OnEvent]
		public void OnDropdownHidden(DropdownHiddenEvent dropdownHiddenEvent)
		{
			this._toolService.SwitchToDefaultTool();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000033F0 File Offset: 0x000015F0
		public Automator GetHoveredTransmitter()
		{
			SelectableObject selectableObject;
			if (this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject))
			{
				Automator component = selectableObject.GetComponent<Automator>();
				if (component != null && component.IsTransmitter)
				{
					return component;
				}
			}
			return this.GetHoveredTransmitterOnDropdown();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003428 File Offset: 0x00001628
		public Automator GetHoveredTransmitterOnDropdown()
		{
			if (!string.IsNullOrEmpty(this._dropdown.HoveredItem))
			{
				Guid entityId = Guid.Parse(this._dropdown.HoveredItem);
				return this._automatorRegistry.FindTransmitterById(entityId);
			}
			return null;
		}

		// Token: 0x04000050 RID: 80
		public static readonly string DescriptionLocKey = "Automation.SelectTransmitter";

		// Token: 0x04000051 RID: 81
		public static readonly string CursorKey = "PickObjectCursor";

		// Token: 0x04000052 RID: 82
		public readonly InputService _inputService;

		// Token: 0x04000053 RID: 83
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;

		// Token: 0x04000054 RID: 84
		public readonly ToolService _toolService;

		// Token: 0x04000055 RID: 85
		public readonly CursorService _cursorService;

		// Token: 0x04000056 RID: 86
		public readonly CursorTool _cursorTool;

		// Token: 0x04000057 RID: 87
		public readonly ILoc _loc;

		// Token: 0x04000058 RID: 88
		public readonly UISoundController _uiSoundController;

		// Token: 0x04000059 RID: 89
		public readonly EventBus _eventBus;

		// Token: 0x0400005A RID: 90
		public readonly DropdownListDrawer _dropdownListDrawer;

		// Token: 0x0400005B RID: 91
		public readonly TransmitterPickerToolHighlighter _transmitterPickerToolHighlighter;

		// Token: 0x0400005C RID: 92
		public readonly AutomatorRegistry _automatorRegistry;

		// Token: 0x0400005D RID: 93
		public BaseComponent _owner;

		// Token: 0x0400005E RID: 94
		public Dropdown _dropdown;

		// Token: 0x0400005F RID: 95
		public Action<Automator> _setter;
	}
}
