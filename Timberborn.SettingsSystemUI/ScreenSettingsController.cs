using System;
using Timberborn.DropdownSystem;
using Timberborn.ScreenSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000018 RID: 24
	public class ScreenSettingsController
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000036C8 File Offset: 0x000018C8
		public ScreenSettingsController(ScreenSettings screenSettings, DropdownItemsSetter dropdownItemsSetter, ScreenResolutionDropdownProvider screenResolutionDropdownProvider, VSyncDropdownProvider vSyncDropdownProvider, FrameRateLimitDropdownProvider frameRateLimitDropdownProvider)
		{
			this._screenSettings = screenSettings;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._screenResolutionDropdownProvider = screenResolutionDropdownProvider;
			this._vSyncDropdownProvider = vSyncDropdownProvider;
			this._frameRateLimitDropdownProvider = frameRateLimitDropdownProvider;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036F8 File Offset: 0x000018F8
		public void Initialize(VisualElement root)
		{
			this._fullScreenToggle = UQueryExtensions.Q<Toggle>(root, "FullScreen", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._fullScreenToggle, delegate(ChangeEvent<bool> v)
			{
				this._screenSettings.FullScreen = v.newValue;
			});
			this._screenResolutionDropdown = UQueryExtensions.Q<Dropdown>(root, "ScreenResolution", null);
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "ResolutionScale", null);
			this._resolutionScaleSlider = UQueryExtensions.Q<SliderInt>(visualElement, "Slider", null);
			this._resolutionScaleValueLabel = UQueryExtensions.Q<Label>(visualElement, "Value", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._resolutionScaleSlider, new EventCallback<ChangeEvent<int>>(this.OnResolutionScaleChanged));
			this._resolutionScaleSlider.lowValue = 5;
			this._resolutionScaleSlider.highValue = ScreenSettingsController.ResolutionScaleSliderMultiplier;
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(root, "Brightness", null);
			this._brightnessSlider = UQueryExtensions.Q<SliderInt>(visualElement2, "Slider", null);
			this._brightnessValueLabel = UQueryExtensions.Q<Label>(visualElement2, "Value", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<int>(this._brightnessSlider, new EventCallback<ChangeEvent<int>>(this.OnBrightnessChanged));
			this._brightnessSlider.lowValue = ScreenSettingsController.MinBrightness;
			this._brightnessSlider.highValue = ScreenSettingsController.MaxBrightness;
			this._vSyncDropdown = UQueryExtensions.Q<Dropdown>(root, "VSync", null);
			this._vSyncDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateFrameRateLimit();
			};
			this._frameRateLimitDropdown = UQueryExtensions.Q<Dropdown>(root, "FrameRateLimit", null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000384C File Offset: 0x00001A4C
		public void Update()
		{
			this._fullScreenToggle.SetValueWithoutNotify(this._screenSettings.FullScreen);
			this._dropdownItemsSetter.SetItems(this._screenResolutionDropdown, this._screenResolutionDropdownProvider);
			this._resolutionScaleSlider.SetValueWithoutNotify(Mathf.RoundToInt(this._screenSettings.ResolutionScale * (float)ScreenSettingsController.ResolutionScaleSliderMultiplier));
			this._brightnessSlider.SetValueWithoutNotify(Mathf.RoundToInt(this._screenSettings.Brightness * 100f));
			this._dropdownItemsSetter.SetItems(this._vSyncDropdown, this._vSyncDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._frameRateLimitDropdown, this._frameRateLimitDropdownProvider);
			this.UpdateSliderLabels();
			this.UpdateFrameRateLimit();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003903 File Offset: 0x00001B03
		public void Clear()
		{
			this._screenResolutionDropdown.ClearItems();
			this._vSyncDropdown.ClearItems();
			this._frameRateLimitDropdown.ClearItems();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003928 File Offset: 0x00001B28
		public void OnResolutionScaleChanged(ChangeEvent<int> v)
		{
			float resolutionScale = (float)v.newValue / (float)ScreenSettingsController.ResolutionScaleSliderMultiplier;
			this._screenSettings.ResolutionScale = resolutionScale;
			this.UpdateSliderLabels();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003958 File Offset: 0x00001B58
		public void OnBrightnessChanged(ChangeEvent<int> v)
		{
			float brightness = (float)v.newValue / 100f;
			this._screenSettings.Brightness = brightness;
			this.UpdateSliderLabels();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003988 File Offset: 0x00001B88
		public void UpdateSliderLabels()
		{
			this._resolutionScaleValueLabel.text = this._screenSettings.ResolutionScale.ToString("P0");
			this._brightnessValueLabel.text = this._screenSettings.Brightness.ToString("P0");
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000039DB File Offset: 0x00001BDB
		public void UpdateFrameRateLimit()
		{
			this._frameRateLimitDropdown.SetEnabled(this._screenSettings.VSyncCount == 0);
			this._frameRateLimitDropdown.UpdateSelectedValue();
		}

		// Token: 0x0400006C RID: 108
		public static readonly int ResolutionScaleSliderMultiplier = 20;

		// Token: 0x0400006D RID: 109
		public static readonly int MinBrightness = 50;

		// Token: 0x0400006E RID: 110
		public static readonly int MaxBrightness = 125;

		// Token: 0x0400006F RID: 111
		public readonly ScreenSettings _screenSettings;

		// Token: 0x04000070 RID: 112
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000071 RID: 113
		public readonly ScreenResolutionDropdownProvider _screenResolutionDropdownProvider;

		// Token: 0x04000072 RID: 114
		public readonly VSyncDropdownProvider _vSyncDropdownProvider;

		// Token: 0x04000073 RID: 115
		public readonly FrameRateLimitDropdownProvider _frameRateLimitDropdownProvider;

		// Token: 0x04000074 RID: 116
		public Toggle _fullScreenToggle;

		// Token: 0x04000075 RID: 117
		public Dropdown _screenResolutionDropdown;

		// Token: 0x04000076 RID: 118
		public SliderInt _resolutionScaleSlider;

		// Token: 0x04000077 RID: 119
		public Label _resolutionScaleValueLabel;

		// Token: 0x04000078 RID: 120
		public SliderInt _brightnessSlider;

		// Token: 0x04000079 RID: 121
		public Label _brightnessValueLabel;

		// Token: 0x0400007A RID: 122
		public Dropdown _vSyncDropdown;

		// Token: 0x0400007B RID: 123
		public Dropdown _frameRateLimitDropdown;
	}
}
