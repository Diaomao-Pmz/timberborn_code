using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x02000032 RID: 50
	public class WeatherStationFragment : IEntityPanelFragment
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00006D7F File Offset: 0x00004F7F
		public WeatherStationFragment(VisualElementLoader visualElementLoader, RadioToggleFactory radioToggleFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._radioToggleFactory = radioToggleFactory;
			this._loc = loc;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/WeatherStationFragment");
			this._modeRadioToggle = this._radioToggleFactory.CreateLocalizable<WeatherStationMode>(WeatherStationFragment.ModeLocKeyPrefix, UQueryExtensions.Q<VisualElement>(this._root, "ModeRadioToggleContainer", null));
			this._modeRadioToggle.RadioButtonSelected += this.OnModeChanged;
			this._earlyActivationToggle = UQueryExtensions.Q<Toggle>(this._root, "EarlyActivationToggle", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._earlyActivationToggle, new EventCallback<ChangeEvent<bool>>(this.OnEarlyActivationToggleChanged));
			this._earlyActivationWrapper = UQueryExtensions.Q(this._root, "EarlyActivationWrapper", null);
			this._earlyActivationLabel = UQueryExtensions.Q<Label>(this._root, "EarlyActivationLabel", null);
			this._earlyActivationSlider = UQueryExtensions.Q<PreciseSlider>(this._root, "EarlyActivationSlider", null);
			this._earlyActivationSlider.SetValueChangedCallback(new Action<float>(this.OnEarlyActivationSliderChanged));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006EB6 File Offset: 0x000050B6
		public void ShowFragment(BaseComponent entity)
		{
			this._weatherStation = entity.GetComponent<WeatherStation>();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006EC4 File Offset: 0x000050C4
		public void ClearFragment()
		{
			this._weatherStation = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006EDC File Offset: 0x000050DC
		public void UpdateFragment()
		{
			if (this._weatherStation)
			{
				this._modeRadioToggle.Update((int)this._weatherStation.Mode);
				this._earlyActivationToggle.value = this._weatherStation.EarlyActivationEnabled;
				this._earlyActivationWrapper.ToggleDisplayStyle(this._weatherStation.EarlyActivationEnabled);
				if (this._weatherStation.EarlyActivationEnabled)
				{
					this._earlyActivationLabel.text = this._loc.T<int>(this._earlyActivationPhrase, this._weatherStation.EarlyActivationHours);
					this._earlyActivationSlider.UpdateValuesWithoutNotify((float)this._weatherStation.EarlyActivationHours, this._weatherStation.MaxEarlyActivationHours);
				}
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006FAA File Offset: 0x000051AA
		public void OnModeChanged(object sender, int intMode)
		{
			this._weatherStation.SetMode((WeatherStationMode)intMode);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006FB8 File Offset: 0x000051B8
		public void OnEarlyActivationToggleChanged(ChangeEvent<bool> evt)
		{
			this._weatherStation.SetEarlyActivationEnabled(evt.newValue);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006FCB File Offset: 0x000051CB
		public void OnEarlyActivationSliderChanged(float value)
		{
			this._weatherStation.SetEarlyActivationHours(Mathf.RoundToInt(value));
		}

		// Token: 0x04000177 RID: 375
		public static readonly string ModeLocKeyPrefix = "Weather.";

		// Token: 0x04000178 RID: 376
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000179 RID: 377
		public readonly RadioToggleFactory _radioToggleFactory;

		// Token: 0x0400017A RID: 378
		public readonly ILoc _loc;

		// Token: 0x0400017B RID: 379
		public readonly Phrase _earlyActivationPhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatHours));

		// Token: 0x0400017C RID: 380
		public VisualElement _root;

		// Token: 0x0400017D RID: 381
		public RadioToggle _modeRadioToggle;

		// Token: 0x0400017E RID: 382
		public Toggle _earlyActivationToggle;

		// Token: 0x0400017F RID: 383
		public VisualElement _earlyActivationWrapper;

		// Token: 0x04000180 RID: 384
		public Label _earlyActivationLabel;

		// Token: 0x04000181 RID: 385
		public PreciseSlider _earlyActivationSlider;

		// Token: 0x04000182 RID: 386
		public WeatherStation _weatherStation;
	}
}
