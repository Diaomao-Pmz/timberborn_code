using System;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using Timberborn.WaterSourceSystem;
using UnityEngine.UIElements;

namespace Timberborn.WaterSourceSystemUI
{
	// Token: 0x0200000A RID: 10
	public class WaterSourceRegulatorToggle
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002601 File Offset: 0x00000801
		public WaterSourceRegulatorToggle(SliderToggleFactory sliderToggleFactory, ILoc loc)
		{
			this._sliderToggleFactory = sliderToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002618 File Offset: 0x00000818
		public void Initialize(VisualElement parent, Label modeLabel)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(() => this._loc.T(WaterSourceRegulatorToggle.ToggleClosedLocKey), WaterSourceRegulatorToggle.ClosedClass, delegate()
			{
				this._waterSourceRegulator.Close();
			}, () => this._waterSourceRegulator.ClosedMode);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(() => this._loc.T(WaterSourceRegulatorToggle.ToggleOpenLocKey), WaterSourceRegulatorToggle.OpenedClass, delegate()
			{
				this._waterSourceRegulator.Open();
			}, () => this._waterSourceRegulator.OpenMode);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(() => this._loc.T(WaterSourceRegulatorToggle.ToggleAutomatedLocKey), WaterSourceRegulatorToggle.AutomatedClass, delegate()
			{
				this._waterSourceRegulator.Automate();
			}, () => this._waterSourceRegulator.AutomatedMode);
			this._sliderToggle = this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
			this._modeLabel = modeLabel;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026E9 File Offset: 0x000008E9
		public void Show(WaterSourceRegulator waterSourceRegulator)
		{
			this._waterSourceRegulator = waterSourceRegulator;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026F2 File Offset: 0x000008F2
		public void Update()
		{
			if (this._waterSourceRegulator != null)
			{
				this._sliderToggle.Update();
				this._modeLabel.text = this.GetModeLabel();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002718 File Offset: 0x00000918
		public void Clear()
		{
			this._waterSourceRegulator = null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002724 File Offset: 0x00000924
		public string GetModeLabel()
		{
			if (this._waterSourceRegulator.OpenMode)
			{
				return this._loc.T(WaterSourceRegulatorToggle.ToggleOpenLocKey);
			}
			if (this._waterSourceRegulator.ClosedMode)
			{
				return this._loc.T(WaterSourceRegulatorToggle.ToggleClosedLocKey);
			}
			if (this._waterSourceRegulator.AutomatedMode)
			{
				string str = "(" + this._loc.T(this._waterSourceRegulator.IsOpen ? WaterSourceRegulatorToggle.ToggleOpenLocKey : WaterSourceRegulatorToggle.ToggleClosedLocKey) + ")";
				return this._loc.T(WaterSourceRegulatorToggle.ToggleAutomatedLocKey) + " " + str;
			}
			return string.Empty;
		}

		// Token: 0x04000022 RID: 34
		public static readonly string ClosedClass = "water-source-regulator-toggle__icon--closed";

		// Token: 0x04000023 RID: 35
		public static readonly string OpenedClass = "water-source-regulator-toggle__icon--open";

		// Token: 0x04000024 RID: 36
		public static readonly string AutomatedClass = "water-source-regulator-toggle__icon--automated";

		// Token: 0x04000025 RID: 37
		public static readonly string ToggleClosedLocKey = "Toggle.State.Closed";

		// Token: 0x04000026 RID: 38
		public static readonly string ToggleOpenLocKey = "Toggle.State.Open";

		// Token: 0x04000027 RID: 39
		public static readonly string ToggleAutomatedLocKey = "Automation.Mode.Automated";

		// Token: 0x04000028 RID: 40
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000029 RID: 41
		public readonly ILoc _loc;

		// Token: 0x0400002A RID: 42
		public WaterSourceRegulator _waterSourceRegulator;

		// Token: 0x0400002B RID: 43
		public SliderToggle _sliderToggle;

		// Token: 0x0400002C RID: 44
		public Label _modeLabel;
	}
}
