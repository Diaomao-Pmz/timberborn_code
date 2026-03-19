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
	// Token: 0x02000022 RID: 34
	public class PowerMeterFragment : IEntityPanelFragment
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004FE0 File Offset: 0x000031E0
		public PowerMeterFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, EnumDropdownProviderFactory enumDropdownProviderFactory, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._enumDropdownProviderFactory = enumDropdownProviderFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000050A4 File Offset: 0x000032A4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/PowerMeterFragment");
			this._root.ToggleDisplayStyle(false);
			this._intThresholdField = UQueryExtensions.Q<IntegerField>(this._root, "IntThreshold", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._intThresholdField, new EventCallback<ChangeEvent<int>>(this.OnIntThresholdChanged));
			this._intThresholdField.isDelayed = true;
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._powerMeterModeDropdownProvider = this._enumDropdownProviderFactory.CreateLocalized<PowerMeterMode>(() => this._powerMeter.Mode, new Action<PowerMeterMode>(this.SetMode), PowerMeterFragment.PowerMeterModeLocKeyPrefix);
			this._comparisonModeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "ComparisonMode", null);
			this._comparisonModeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._powerMeter.ComparisonMode, delegate(NumericComparisonMode comparisonMode)
			{
				this._powerMeter.SetComparisonMode(comparisonMode);
			});
			this._measurementLabel = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._percentThresholdLabel = UQueryExtensions.Q<Label>(this._root, "PercentThresholdLabel", null);
			this._percentThresholdSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "PercentThresholdSlider", null);
			this._percentThresholdSlider.SetValueChangedCallback(new Action<float>(this.OnPercentThresholdChanged));
			this._percentThresholdSlider.SetStepWithoutNotify(PowerMeterFragment.PercentThresholdStep);
			return this._root;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005208 File Offset: 0x00003408
		public void ShowFragment(BaseComponent entity)
		{
			this._powerMeter = entity.GetComponent<PowerMeter>();
			if (this._powerMeter)
			{
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._powerMeterModeDropdownProvider);
				this._dropdownItemsSetter.SetItems(this._comparisonModeDropdown, this._comparisonModeDropdownProvider);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005268 File Offset: 0x00003468
		public void ClearFragment()
		{
			this._powerMeter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005280 File Offset: 0x00003480
		public void UpdateFragment()
		{
			if (this._powerMeter)
			{
				this._percentThresholdLabel.ToggleDisplayStyle(this._powerMeter.IsPercentThreshold);
				this._percentThresholdSlider.ToggleDisplayStyle(this._powerMeter.IsPercentThreshold);
				this._intThresholdField.ToggleDisplayStyle(!this._powerMeter.IsPercentThreshold);
				if (this._powerMeter.IsPercentThreshold)
				{
					this._percentThresholdSlider.UpdateValuesWithoutNotify(this._powerMeter.PercentThreshold, 0f, 1f);
					this._percentThresholdSlider.SetMarker(this._powerMeter.PercentMeasurement);
					this._percentThresholdLabel.text = this._loc.T<float>(this._percentThresholdPhrase, this._powerMeter.PercentThreshold);
					this._measurementLabel.text = this._loc.T<float>(this._percentMeasurementPhrase, this._powerMeter.PercentMeasurement);
					return;
				}
				this._intThresholdField.SetValueWithoutNotify(this._powerMeter.IntThreshold);
				this._measurementLabel.text = this._loc.T<int>(this._measurementPhrase, this._powerMeter.IntMeasurement);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000053B4 File Offset: 0x000035B4
		public void SetMode(PowerMeterMode mode)
		{
			this._powerMeter.SetMode(mode);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000053C4 File Offset: 0x000035C4
		public void OnIntThresholdChanged(ChangeEvent<int> evt)
		{
			int num = this.ClampIntThreshold(evt.newValue);
			this._intThresholdField.SetValueWithoutNotify(num);
			this._powerMeter.SetIntThreshold(num);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000053F6 File Offset: 0x000035F6
		public void OnPercentThresholdChanged(float value)
		{
			this._powerMeter.SetPercentThreshold(value);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005404 File Offset: 0x00003604
		public int ClampIntThreshold(int value)
		{
			int result;
			switch (this._powerMeter.Mode)
			{
			case PowerMeterMode.Supply:
				result = Math.Clamp(value, 0, int.MaxValue);
				break;
			case PowerMeterMode.Demand:
				result = Math.Clamp(value, 0, int.MaxValue);
				break;
			case PowerMeterMode.Surplus:
				result = value;
				break;
			default:
				throw new ArgumentOutOfRangeException(this._powerMeter.Mode.ToString());
			}
			return result;
		}

		// Token: 0x040000E1 RID: 225
		public static readonly string PowerMeterModeLocKeyPrefix = "Building.PowerMeter.Mode.";

		// Token: 0x040000E2 RID: 226
		public static readonly float PercentThresholdStep = 0.01f;

		// Token: 0x040000E3 RID: 227
		public readonly Phrase _measurementPhrase = Phrase.New("Automation.Measurement").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower));

		// Token: 0x040000E4 RID: 228
		public readonly Phrase _percentMeasurementPhrase = Phrase.New("Automation.Measurement").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));

		// Token: 0x040000E5 RID: 229
		public readonly Phrase _percentThresholdPhrase = Phrase.New("Automation.Threshold").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));

		// Token: 0x040000E6 RID: 230
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000E7 RID: 231
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x040000E8 RID: 232
		public readonly EnumDropdownProviderFactory _enumDropdownProviderFactory;

		// Token: 0x040000E9 RID: 233
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x040000EA RID: 234
		public readonly ILoc _loc;

		// Token: 0x040000EB RID: 235
		public VisualElement _root;

		// Token: 0x040000EC RID: 236
		public Dropdown _modeDropdown;

		// Token: 0x040000ED RID: 237
		public EnumDropdownProvider<PowerMeterMode> _powerMeterModeDropdownProvider;

		// Token: 0x040000EE RID: 238
		public Dropdown _comparisonModeDropdown;

		// Token: 0x040000EF RID: 239
		public EnumDropdownProvider<NumericComparisonMode> _comparisonModeDropdownProvider;

		// Token: 0x040000F0 RID: 240
		public Label _measurementLabel;

		// Token: 0x040000F1 RID: 241
		public IntegerField _intThresholdField;

		// Token: 0x040000F2 RID: 242
		public Label _percentThresholdLabel;

		// Token: 0x040000F3 RID: 243
		public PreciseSlider _percentThresholdSlider;

		// Token: 0x040000F4 RID: 244
		public PowerMeter _powerMeter;
	}
}
