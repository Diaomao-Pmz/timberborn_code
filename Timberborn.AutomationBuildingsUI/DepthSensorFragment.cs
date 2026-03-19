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
	// Token: 0x0200000D RID: 13
	public class DepthSensorFragment : IEntityPanelFragment
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002ACC File Offset: 0x00000CCC
		public DepthSensorFragment(VisualElementLoader visualElementLoader, NumericComparisonModeDropdownFactory numericComparisonModeDropdownFactory, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._numericComparisonModeDropdownFactory = numericComparisonModeDropdownFactory;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B40 File Offset: 0x00000D40
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WaterSensorFragment");
			this._measurement = UQueryExtensions.Q<Label>(this._root, "Measurement", null);
			this._modeDropdown = UQueryExtensions.Q<Dropdown>(this._root, "Mode", null);
			this._modeDropdownProvider = this._numericComparisonModeDropdownFactory.Create(() => this._depthSensor.Mode, delegate(NumericComparisonMode mode)
			{
				this._depthSensor.SetMode(mode);
			});
			this._thresholdLabel = UQueryExtensions.Q<Label>(this._root, "ThresholdLabel", null);
			this._thresholdSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "ThresholdSlider", null);
			this._thresholdSlider.SetValueChangedCallback(new Action<float>(this.OnThresholdChanged));
			this._thresholdSlider.SetStepWithoutNotify(DepthSensorFragment.ThresholdChangeStep);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C21 File Offset: 0x00000E21
		public void ShowFragment(BaseComponent entity)
		{
			this._depthSensor = entity.GetComponent<DepthSensor>();
			if (this._depthSensor)
			{
				this._dropdownItemsSetter.SetItems(this._modeDropdown, this._modeDropdownProvider);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C53 File Offset: 0x00000E53
		public void ClearFragment()
		{
			this._depthSensor = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C68 File Offset: 0x00000E68
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._depthSensor);
			if (this._depthSensor)
			{
				this._measurement.text = this._loc.T<float>(this._measurementPhrase, this._depthSensor.DepthFromFloor);
				this._thresholdLabel.text = this._loc.T<float>(this._thresholdPhrase, this._depthSensor.ThresholdFromFloor);
				this._thresholdSlider.UpdateValuesWithoutNotify(this._depthSensor.Threshold, (float)this._depthSensor.MinThreshold, this._depthSensor.MaxThreshold);
				this._thresholdSlider.SetMarker(this._depthSensor.Depth);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D2C File Offset: 0x00000F2C
		public void OnThresholdChanged(float value)
		{
			this._depthSensor.SetThreshold(value);
		}

		// Token: 0x04000039 RID: 57
		public static readonly float ThresholdChangeStep = 0.05f;

		// Token: 0x0400003A RID: 58
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003B RID: 59
		public readonly NumericComparisonModeDropdownFactory _numericComparisonModeDropdownFactory;

		// Token: 0x0400003C RID: 60
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400003D RID: 61
		public readonly ILoc _loc;

		// Token: 0x0400003E RID: 62
		public DepthSensor _depthSensor;

		// Token: 0x0400003F RID: 63
		public VisualElement _root;

		// Token: 0x04000040 RID: 64
		public Label _measurement;

		// Token: 0x04000041 RID: 65
		public Label _thresholdLabel;

		// Token: 0x04000042 RID: 66
		public Dropdown _modeDropdown;

		// Token: 0x04000043 RID: 67
		public PreciseSlider _thresholdSlider;

		// Token: 0x04000044 RID: 68
		public EnumDropdownProvider<NumericComparisonMode> _modeDropdownProvider;

		// Token: 0x04000045 RID: 69
		public readonly Phrase _measurementPhrase = Phrase.New("Automation.Measurement").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x04000046 RID: 70
		public readonly Phrase _thresholdPhrase = Phrase.New("Automation.Threshold").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
