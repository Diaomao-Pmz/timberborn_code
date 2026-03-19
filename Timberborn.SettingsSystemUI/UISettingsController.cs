using System;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000020 RID: 32
	public class UISettingsController
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x0000442C File Offset: 0x0000262C
		public UISettingsController(DropdownItemsSetter dropdownItemsSetter, OnScreenKeyboardDropdownProvider onScreenKeyboardDropdownProvider, UISettings uiSettings, UIScaler uiScaler)
		{
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._onScreenKeyboardDropdownProvider = onScreenKeyboardDropdownProvider;
			this._uiSettings = uiSettings;
			this._uiScaler = uiScaler;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004454 File Offset: 0x00002654
		public void Initialize(VisualElement root)
		{
			this._showFPSToggle = UQueryExtensions.Q<Toggle>(root, "ShowFPS", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._showFPSToggle, delegate(ChangeEvent<bool> v)
			{
				this._uiSettings.ShowFps = v.newValue;
			});
			this._runInBackgroundToggle = UQueryExtensions.Q<Toggle>(root, "RunInBackground", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._runInBackgroundToggle, delegate(ChangeEvent<bool> v)
			{
				this._uiSettings.RunInBackground = v.newValue;
			});
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "UIScaleFactor", null);
			this._uiScaleFactorSlider = UQueryExtensions.Q<SliderInt>(visualElement, "Slider", null);
			this._uiScaleFactorValueLabel = UQueryExtensions.Q<Label>(visualElement, "Value", null);
			this._uiScaleFactorSlider.lowValue = UISettingsController.RoundToInt(UIScaler.MinScaleFactor);
			this._uiScaleFactorSlider.highValue = UISettingsController.RoundToInt(UIScaler.MaxScaleFactor);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._uiScaleFactorSlider, delegate(ChangeEvent<int> v)
			{
				this._uiSettings.UIScaleFactor = this._uiScaler.ClampScaleFactor((float)v.newValue * UISettings.UIScaleStep);
				this._uiScaleFactorValueLabel.text = ((float)v.newValue * UISettings.UIScaleStep).ToString("P0");
			});
			this._onScreenKeyboardDropdown = UQueryExtensions.Q<Dropdown>(root, "OnScreenKeyboard", null);
			this._onScreenKeyboardDropdown.ToggleDisplayStyle(UISettingsController.ShouldShowSteamKeyboardDropdown);
			if (UISettingsController.ShouldShowSteamKeyboardDropdown)
			{
				this._dropdownItemsSetter.SetItems(this._onScreenKeyboardDropdown, this._onScreenKeyboardDropdownProvider);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004568 File Offset: 0x00002768
		public void Update()
		{
			this._showFPSToggle.SetValueWithoutNotify(this._uiSettings.ShowFps);
			this._runInBackgroundToggle.SetValueWithoutNotify(this._uiSettings.RunInBackground);
			float value = this._uiScaler.ClampScaleFactor(this._uiSettings.UIScaleFactor);
			this._uiScaleFactorSlider.SetValueWithoutNotify(UISettingsController.RoundToInt(value));
			this._uiScaleFactorValueLabel.text = value.ToString("P0");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000045E0 File Offset: 0x000027E0
		public static int RoundToInt(float value)
		{
			return Mathf.RoundToInt(value / UISettings.UIScaleStep);
		}

		// Token: 0x040000A6 RID: 166
		public static readonly bool ShouldShowSteamKeyboardDropdown = true;

		// Token: 0x040000A7 RID: 167
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x040000A8 RID: 168
		public readonly OnScreenKeyboardDropdownProvider _onScreenKeyboardDropdownProvider;

		// Token: 0x040000A9 RID: 169
		public readonly UISettings _uiSettings;

		// Token: 0x040000AA RID: 170
		public readonly UIScaler _uiScaler;

		// Token: 0x040000AB RID: 171
		public Toggle _showFPSToggle;

		// Token: 0x040000AC RID: 172
		public Toggle _runInBackgroundToggle;

		// Token: 0x040000AD RID: 173
		public SliderInt _uiScaleFactorSlider;

		// Token: 0x040000AE RID: 174
		public Label _uiScaleFactorValueLabel;

		// Token: 0x040000AF RID: 175
		public Dropdown _onScreenKeyboardDropdown;
	}
}
