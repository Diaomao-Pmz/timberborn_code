using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200000F RID: 15
	public class FlowSensorFragment : IEntityPanelFragment
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002E30 File Offset: 0x00001030
		public FlowSensorFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
			this._measurementPhrase = Phrase.New("Automation.Measurement").Format<float?>(new Func<float?, string>(this.FormatFlow));
			this._thresholdPhrase = Phrase.New("Automation.Threshold").Format<float>((float value) => this.FormatFlow(new float?(value)));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EA4 File Offset: 0x000010A4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WaterSensorFragment");
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._flowSensor.Mode, delegate(NumericComparisonMode mode)
			{
				this._flowSensor.SetMode(mode);
			});
			this._thresholdLabel = UQueryExtensions.Q<Label>(this._root, "ThresholdLabel", null);
			this._thresholdSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "ThresholdSlider", null);
			this._thresholdSlider.SetValueChangedCallback(new Action<float>(this.OnThresholdChanged));
			this._thresholdSlider.SetStepWithoutNotify(FlowSensorFragment.ThresholdChangeStep);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F88 File Offset: 0x00001188
		public void ShowFragment(BaseComponent entity)
		{
			this._flowSensor = entity.GetComponent<FlowSensor>();
			if (this._flowSensor)
			{
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
				this._thresholdSlider.UpdateValuesWithoutNotify(this._flowSensor.Threshold, 0f, this._flowSensor.MaxThreshold);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002FEB File Offset: 0x000011EB
		public void ClearFragment()
		{
			this._flowSensor = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003000 File Offset: 0x00001200
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._flowSensor);
			if (this._flowSensor)
			{
				this._measurement.text = this._loc.T<float?>(this._measurementPhrase, this._flowSensor.SampledFlow);
				this._thresholdLabel.text = this._loc.T<float>(this._thresholdPhrase, this._flowSensor.Threshold);
				if (this._flowSensor.SampledFlow != null)
				{
					this._thresholdSlider.SetMarker(this._flowSensor.SampledFlow.Value);
					return;
				}
				this._thresholdSlider.ClearMarker();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000030C0 File Offset: 0x000012C0
		public void OnThresholdChanged(float value)
		{
			this._flowSensor.SetThreshold(value);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000030CE File Offset: 0x000012CE
		public string FormatFlow(float? value)
		{
			if (value == null)
			{
				return "-";
			}
			return UnitFormatter.FormatFlow(value.Value, this._loc);
		}

		// Token: 0x0400004D RID: 77
		public static readonly float ThresholdChangeStep = 0.1f;

		// Token: 0x0400004E RID: 78
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400004F RID: 79
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x04000050 RID: 80
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000051 RID: 81
		public readonly ILoc _loc;

		// Token: 0x04000052 RID: 82
		public readonly Phrase _measurementPhrase;

		// Token: 0x04000053 RID: 83
		public readonly Phrase _thresholdPhrase;

		// Token: 0x04000054 RID: 84
		public FlowSensor _flowSensor;

		// Token: 0x04000055 RID: 85
		public VisualElement _root;

		// Token: 0x04000056 RID: 86
		public Label _measurement;

		// Token: 0x04000057 RID: 87
		public Label _thresholdLabel;

		// Token: 0x04000058 RID: 88
		public Dropdown _modeDropdown;

		// Token: 0x04000059 RID: 89
		public PreciseSlider _thresholdSlider;

		// Token: 0x0400005A RID: 90
		public EnumDropdownProvider<NumericComparisonMode> _modeDropdownProvider;
	}
}
