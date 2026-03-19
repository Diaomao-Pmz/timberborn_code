using System;
using Timberborn.Automation;
using Timberborn.DropdownSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200001C RID: 28
	public class TransmitterSelectorInitializer
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003808 File Offset: 0x00001A08
		public TransmitterSelectorInitializer(AutomatorRegistry automatorRegistry, EventBus eventBus, ILoc loc, DropdownItemsSetter dropdownItemsSetter, AutomationStateIconBuilder automationStateIconBuilder, TransmitterPickerTool transmitterPickerTool)
		{
			this._automatorRegistry = automatorRegistry;
			this._eventBus = eventBus;
			this._loc = loc;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._automationStateIconBuilder = automationStateIconBuilder;
			this._transmitterPickerTool = transmitterPickerTool;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000383D File Offset: 0x00001A3D
		public void Initialize(TransmitterSelector transmitterSelector, Func<Automator> getter, Action<Automator> setter)
		{
			this.InitializeInternal(transmitterSelector, getter, setter, TransmitterSelectorInitializer.AutomationNoneLocKey, TransmitterSelectorInitializer.AutomationNoneLocKey);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003852 File Offset: 0x00001A52
		public void InitializeOptional(TransmitterSelector transmitterSelector, Func<Automator> getter, Action<Automator> setter)
		{
			this.InitializeInternal(transmitterSelector, getter, setter, TransmitterSelectorInitializer.AutomationNoneLocKey, TransmitterSelectorInitializer.AutomationOptionalLocKey);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003867 File Offset: 0x00001A67
		public void InitializeStandalone(TransmitterSelector transmitterSelector, Func<Automator> getter, Action<Automator> setter)
		{
			this.InitializeInternal(transmitterSelector, getter, setter, TransmitterSelectorInitializer.AutomationNoneLocKey, TransmitterSelectorInitializer.AutomateLocKey);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000387C File Offset: 0x00001A7C
		public void InitializeInternal(TransmitterSelector transmitterSelector, Func<Automator> getter, Action<Automator> setter, string noneLocKey, string selectedNoneLocKey)
		{
			TransmitterDropdownProvider transmitterDropdownProvider = new TransmitterDropdownProvider(this._automatorRegistry, this._loc, getter, setter, noneLocKey, selectedNoneLocKey);
			AutomationStateIcon automationStateIcon = this._automationStateIconBuilder.Create(UQueryExtensions.Q<Image>(transmitterSelector, "StateIcon", null), getter).SetClickableIcon().Build();
			transmitterSelector.Initialize(this._dropdownItemsSetter, this._eventBus, this._transmitterPickerTool, transmitterDropdownProvider, automationStateIcon, setter);
		}

		// Token: 0x04000074 RID: 116
		public static readonly string AutomationNoneLocKey = "Automation.AutomationNone";

		// Token: 0x04000075 RID: 117
		public static readonly string AutomationOptionalLocKey = "Automation.AutomationOptional";

		// Token: 0x04000076 RID: 118
		public static readonly string AutomateLocKey = "Automation.Automate";

		// Token: 0x04000077 RID: 119
		public readonly AutomatorRegistry _automatorRegistry;

		// Token: 0x04000078 RID: 120
		public readonly EventBus _eventBus;

		// Token: 0x04000079 RID: 121
		public readonly ILoc _loc;

		// Token: 0x0400007A RID: 122
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400007B RID: 123
		public readonly AutomationStateIconBuilder _automationStateIconBuilder;

		// Token: 0x0400007C RID: 124
		public readonly TransmitterPickerTool _transmitterPickerTool;
	}
}
