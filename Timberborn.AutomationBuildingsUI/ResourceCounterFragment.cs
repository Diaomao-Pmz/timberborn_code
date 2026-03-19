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
	// Token: 0x02000026 RID: 38
	public class ResourceCounterFragment : IEntityPanelFragment
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000058B0 File Offset: 0x00003AB0
		public ResourceCounterFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, DropdownItemsSetter dropdownItemsSetter, RadioToggleFactory radioToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._radioToggleFactory = radioToggleFactory;
			this._loc = loc;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005984 File Offset: 0x00003B84
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ResourceCounterFragment");
			this._modeRadioToggle = this._radioToggleFactory.CreateLocalizable(new string[]
			{
				ResourceCounterFragment.PercentModeLocKey,
				ResourceCounterFragment.QuantityModeLocKey
			}, UQueryExtensions.Q<VisualElement>(this._root, "ModeRadioToggleContainer", null));
			this._root.ToggleDisplayStyle(false);
			this._threshold = UQueryExtensions.Q<IntegerField>(this._root, "Threshold", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._threshold, new EventCallback<ChangeEvent<int>>(this.ChangeThreshold));
			this._threshold.isDelayed = true;
			this._goodDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Good", null);
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._comparisonModeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "ComparisonMode", null);
			this._comparisonModeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._resourceCounter.ComparisonMode, delegate(NumericComparisonMode comparisonMode)
			{
				this._resourceCounter.SetComparisonMode(comparisonMode);
			});
			this._modeRadioToggle.RadioButtonSelected += this.OnModeChanged;
			this._fillRateSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "FillRateSlider", null);
			this._fillRateSlider.SetValueChangedCallback(new Action<float>(this.SetFillRate));
			this._fillRateSlider.SetStepWithoutNotify(ResourceCounterFragment.PercentThresholdChangeStep);
			this._fillRateLabel = UQueryExtensions.Q<Label>(this._root, "FillRateLabel", null);
			this._includeInputsToggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._includeInputsToggle, delegate(ChangeEvent<bool> evt)
			{
				this.ChangeIncludeInputs(evt.newValue);
			});
			return this._root;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005B38 File Offset: 0x00003D38
		public void ShowFragment(BaseComponent entity)
		{
			this._resourceCounter = entity.GetComponent<ResourceCounter>();
			if (this._resourceCounter)
			{
				this._threshold.SetValueWithoutNotify(this._resourceCounter.Threshold);
				this._root.ToggleDisplayStyle(true);
				this._goodsDropdownProvider = this._resourceCounter.GetComponent<ResourceCounterGoodsDropdownProvider>();
				this._dropdownItemsSetter.SetItems(this._goodDropdown, this._goodsDropdownProvider);
				this._dropdownItemsSetter.SetItems(this._comparisonModeDropdown, this._comparisonModeDropdownProvider);
				this._fillRateSlider.UpdateValuesWithoutNotify(this._resourceCounter.FillRateThreshold, 1f);
				this._includeInputsToggle.SetValueWithoutNotify(this._resourceCounter.IncludeInputs);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005BF3 File Offset: 0x00003DF3
		public void ClearFragment()
		{
			this._resourceCounter = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005C08 File Offset: 0x00003E08
		public void UpdateFragment()
		{
			if (this._resourceCounter)
			{
				this._modeRadioToggle.Update((int)this._resourceCounter.Mode);
				this._fillRateSlider.SetMarker(this._resourceCounter.SampledFillRate);
				Label measurement = this._measurement;
				ResourceCounterMode mode = this._resourceCounter.Mode;
				string text;
				if (mode != ResourceCounterMode.FillRate)
				{
					if (mode != ResourceCounterMode.StockLevel)
					{
						throw new ArgumentOutOfRangeException();
					}
					text = this._loc.T<int>(this._measurementPhrase, this._resourceCounter.SampledResourceCount);
				}
				else
				{
					text = this._loc.T<float>(this._percentMeasurementPhrase, this._resourceCounter.SampledFillRate);
				}
				measurement.text = text;
				this._fillRateLabel.text = this._loc.T<float>(this._percentThresholdPhrase, this._resourceCounter.FillRateThreshold);
				this._fillRateSlider.ToggleDisplayStyle(this._resourceCounter.Mode == ResourceCounterMode.FillRate);
				this._fillRateLabel.ToggleDisplayStyle(this._resourceCounter.Mode == ResourceCounterMode.FillRate);
				this._threshold.ToggleDisplayStyle(this._resourceCounter.Mode == ResourceCounterMode.StockLevel);
				this._includeInputsToggle.ToggleDisplayStyle(this._resourceCounter.Mode == ResourceCounterMode.StockLevel);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005D44 File Offset: 0x00003F44
		public void OnModeChanged(object sender, int intMode)
		{
			this._resourceCounter.SetMode((ResourceCounterMode)intMode);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005D5F File Offset: 0x00003F5F
		public void SetFillRate(float value)
		{
			this._fillRateLabel.text = this._loc.T<float>(this._percentThresholdPhrase, value);
			this._resourceCounter.SetFillRateThreshold(value);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D8C File Offset: 0x00003F8C
		public void ChangeThreshold(ChangeEvent<int> evt)
		{
			int num = Math.Clamp(evt.newValue, 0, int.MaxValue);
			this._threshold.SetValueWithoutNotify(num);
			this._resourceCounter.SetThreshold(num);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005DC3 File Offset: 0x00003FC3
		public void ChangeIncludeInputs(bool includeInputs)
		{
			this._resourceCounter.SetIncludeInputs(includeInputs);
		}

		// Token: 0x0400010B RID: 267
		public static readonly float PercentThresholdChangeStep = 0.01f;

		// Token: 0x0400010C RID: 268
		public static readonly string PercentModeLocKey = "Building.ResourceCounter.Mode.Percent";

		// Token: 0x0400010D RID: 269
		public static readonly string QuantityModeLocKey = "Building.ResourceCounter.Mode.Quantity";

		// Token: 0x0400010E RID: 270
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400010F RID: 271
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x04000110 RID: 272
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000111 RID: 273
		public readonly RadioToggleFactory _radioToggleFactory;

		// Token: 0x04000112 RID: 274
		public readonly ILoc _loc;

		// Token: 0x04000113 RID: 275
		public readonly Phrase _percentThresholdPhrase = Phrase.New("Automation.Threshold").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));

		// Token: 0x04000114 RID: 276
		public readonly Phrase _measurementPhrase = Phrase.New("Automation.Measurement").Format<int>((int value) => value.ToString());

		// Token: 0x04000115 RID: 277
		public readonly Phrase _percentMeasurementPhrase = Phrase.New("Automation.Measurement").Format<float>((float value) => NumberFormatter.FormatAsPercentRounded((double)value));

		// Token: 0x04000116 RID: 278
		public VisualElement _root;

		// Token: 0x04000117 RID: 279
		public IntegerField _threshold;

		// Token: 0x04000118 RID: 280
		public Dropdown _goodDropdown;

		// Token: 0x04000119 RID: 281
		public RadioToggle _modeRadioToggle;

		// Token: 0x0400011A RID: 282
		public Toggle _includeInputsToggle;

		// Token: 0x0400011B RID: 283
		public Label _measurement;

		// Token: 0x0400011C RID: 284
		public Label _fillRateLabel;

		// Token: 0x0400011D RID: 285
		public ResourceCounter _resourceCounter;

		// Token: 0x0400011E RID: 286
		public ResourceCounterGoodsDropdownProvider _goodsDropdownProvider;

		// Token: 0x0400011F RID: 287
		public Dropdown _comparisonModeDropdown;

		// Token: 0x04000120 RID: 288
		public EnumDropdownProvider<NumericComparisonMode> _comparisonModeDropdownProvider;

		// Token: 0x04000121 RID: 289
		public PreciseSlider _fillRateSlider;
	}
}
