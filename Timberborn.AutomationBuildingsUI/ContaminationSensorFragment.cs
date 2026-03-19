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
	// Token: 0x0200000B RID: 11
	public class ContaminationSensorFragment : IEntityPanelFragment
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000027D4 File Offset: 0x000009D4
		public ContaminationSensorFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000285C File Offset: 0x00000A5C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WaterSensorFragment");
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._contaminationSensor.Mode, delegate(NumericComparisonMode mode)
			{
				this._contaminationSensor.SetMode(mode);
			});
			this._thresholdLabel = UQueryExtensions.Q<Label>(this._root, "ThresholdLabel", null);
			this._thresholdSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "ThresholdSlider", null);
			this._thresholdSlider.SetValueChangedCallback(new Action<float>(this.OnThresholdChanged));
			this._thresholdSlider.SetStepWithoutNotify(ContaminationSensor.Precision);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002940 File Offset: 0x00000B40
		public void ShowFragment(BaseComponent entity)
		{
			this._contaminationSensor = entity.GetComponent<ContaminationSensor>();
			if (this._contaminationSensor)
			{
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
				this._thresholdSlider.UpdateValuesWithoutNotify(this._contaminationSensor.Threshold, 1f);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002998 File Offset: 0x00000B98
		public void ClearFragment()
		{
			this._contaminationSensor = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._contaminationSensor);
			if (this._contaminationSensor)
			{
				this._measurement.text = this._loc.T<float?>(this._measurementPhrase, this._contaminationSensor.SampledContamination);
				this._thresholdLabel.text = this._loc.T<float>(this._thresholdPhrase, this._contaminationSensor.Threshold);
				if (this._contaminationSensor.SampledContamination != null)
				{
					this._thresholdSlider.SetMarker(this._contaminationSensor.SampledContamination.Value);
					return;
				}
				this._thresholdSlider.ClearMarker();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A70 File Offset: 0x00000C70
		public void OnThresholdChanged(float value)
		{
			this._contaminationSensor.SetThreshold(value);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A7E File Offset: 0x00000C7E
		public static string FormatMeasurement(float? contamination)
		{
			if (contamination == null)
			{
				return "-";
			}
			return NumberFormatter.FormatAsPercentRounded((double)contamination.Value);
		}

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x0400002C RID: 44
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400002D RID: 45
		public readonly ILoc _loc;

		// Token: 0x0400002E RID: 46
		public ContaminationSensor _contaminationSensor;

		// Token: 0x0400002F RID: 47
		public VisualElement _root;

		// Token: 0x04000030 RID: 48
		public Label _measurement;

		// Token: 0x04000031 RID: 49
		public Label _thresholdLabel;

		// Token: 0x04000032 RID: 50
		public Dropdown _modeDropdown;

		// Token: 0x04000033 RID: 51
		public PreciseSlider _thresholdSlider;

		// Token: 0x04000034 RID: 52
		public EnumDropdownProvider<NumericComparisonMode> _modeDropdownProvider;

		// Token: 0x04000035 RID: 53
		public readonly Phrase _measurementPhrase = Phrase.New("Automation.Measurement").Format<float?>(new Func<float?, string>(ContaminationSensorFragment.FormatMeasurement));

		// Token: 0x04000036 RID: 54
		public readonly Phrase _thresholdPhrase = Phrase.New("Automation.Threshold").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));
	}
}
