using System;
using Timberborn.AutomationBuildings;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000012 RID: 18
	public class GateToggle
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000032B5 File Offset: 0x000014B5
		public GateToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000032CC File Offset: 0x000014CC
		public void Initialize(VisualElement parent, Label modeLabel)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(GateToggle.ToggleOpenLocKey), GateToggle.OpenedClass, delegate()
			{
				this._gate.Open();
			}, () => this._gate.OpenMode);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(GateToggle.ToggleClosedLocKey), GateToggle.ClosedClass, delegate()
			{
				this._gate.Close();
			}, () => this._gate.ClosedMode);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(GateToggle.ToggleAutomatedLocKey), GateToggle.AutomatedClass, delegate()
			{
				this._gate.Automate();
			}, () => this._gate.AutomatedMode);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
			this._modeLabel = modeLabel;
			this._automatedOpenText = this._loc.T(GateToggle.ToggleAutomatedLocKey) + " (" + this._loc.T(GateToggle.ToggleOpenLocKey) + ")";
			this._automatedClosedText = this._loc.T(GateToggle.ToggleAutomatedLocKey) + " (" + this._loc.T(GateToggle.ToggleClosedLocKey) + ")";
			this._automatedConflictText = this._loc.T(GateToggle.ToggleAutomatedLocKey) + " (" + this._loc.T(GateToggle.ToggleConflictLocKey) + ")";
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000343C File Offset: 0x0000163C
		public void Show(Gate gate)
		{
			this._gate = gate;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003445 File Offset: 0x00001645
		public void Update()
		{
			if (this._gate != null)
			{
				this._sliderToggle.Update();
				this._modeLabel.text = this.GetModeLabel();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000346B File Offset: 0x0000166B
		public void Clear()
		{
			this._gate = null;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003474 File Offset: 0x00001674
		public string GetModeLabel()
		{
			if (this._gate.OpenMode)
			{
				return this._loc.T(GateToggle.ToggleOpenLocKey);
			}
			if (this._gate.ClosedMode)
			{
				return this._loc.T(GateToggle.ToggleClosedLocKey);
			}
			if (!this._gate.AutomatedMode)
			{
				return string.Empty;
			}
			if (this._gate.IsConflict)
			{
				return this._automatedConflictText;
			}
			if (!this._gate.IsOpenByAutomation)
			{
				return this._automatedClosedText;
			}
			return this._automatedOpenText;
		}

		// Token: 0x04000064 RID: 100
		public static readonly string OpenedClass = "gate-toggle__icon--open";

		// Token: 0x04000065 RID: 101
		public static readonly string ClosedClass = "gate-toggle__icon--closed";

		// Token: 0x04000066 RID: 102
		public static readonly string AutomatedClass = "gate-toggle__icon--automated";

		// Token: 0x04000067 RID: 103
		public static readonly string ToggleOpenLocKey = "Toggle.State.Open";

		// Token: 0x04000068 RID: 104
		public static readonly string ToggleClosedLocKey = "Toggle.State.Closed";

		// Token: 0x04000069 RID: 105
		public static readonly string ToggleConflictLocKey = "Toggle.State.Conflict";

		// Token: 0x0400006A RID: 106
		public static readonly string ToggleAutomatedLocKey = "Automation.Mode.Automated";

		// Token: 0x0400006B RID: 107
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x0400006C RID: 108
		public readonly ILoc _loc;

		// Token: 0x0400006D RID: 109
		public Gate _gate;

		// Token: 0x0400006E RID: 110
		public SliderToggle _sliderToggle;

		// Token: 0x0400006F RID: 111
		public Label _modeLabel;

		// Token: 0x04000070 RID: 112
		public string _automatedOpenText;

		// Token: 0x04000071 RID: 113
		public string _automatedClosedText;

		// Token: 0x04000072 RID: 114
		public string _automatedConflictText;
	}
}
