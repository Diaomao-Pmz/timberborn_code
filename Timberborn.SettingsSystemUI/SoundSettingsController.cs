using System;
using Timberborn.SoundSettingsSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200001C RID: 28
	public class SoundSettingsController
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003F8E File Offset: 0x0000218E
		public SoundSettingsController(SoundSettings soundSettings)
		{
			this._soundSettings = soundSettings;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003FA0 File Offset: 0x000021A0
		public void Initialize(VisualElement root)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "MasterVolume", null);
			this._masterVolumeValueLabel = UQueryExtensions.Q<Label>(visualElement, "Value", null);
			this._masterVolumeSlider = SoundSettingsController.InitializeSlider(visualElement, this._masterVolumeValueLabel, delegate(float v)
			{
				this._soundSettings.MasterVolume = v;
			});
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(root, "MusicVolume", null);
			this._musicVolumeValueLabel = UQueryExtensions.Q<Label>(visualElement2, "Value", null);
			this._musicVolumeSlider = SoundSettingsController.InitializeSlider(visualElement2, this._musicVolumeValueLabel, delegate(float v)
			{
				this._soundSettings.MusicVolume = v;
			});
			VisualElement visualElement3 = UQueryExtensions.Q<VisualElement>(root, "EnvironmentVolume", null);
			this._environmentVolumeValueLabel = UQueryExtensions.Q<Label>(visualElement3, "Value", null);
			this._environmentVolumeSlider = SoundSettingsController.InitializeSlider(visualElement3, this._environmentVolumeValueLabel, delegate(float v)
			{
				this._soundSettings.EnvironmentVolume = v;
			});
			VisualElement visualElement4 = UQueryExtensions.Q<VisualElement>(root, "UIVolume", null);
			this._uiVolumeElementValueLabel = UQueryExtensions.Q<Label>(visualElement4, "Value", null);
			this._uiVolumeElementSlider = SoundSettingsController.InitializeSlider(visualElement4, this._uiVolumeElementValueLabel, delegate(float v)
			{
				this._soundSettings.UIVolume = v;
			});
			this._muteWhenMinimizedToggle = UQueryExtensions.Q<Toggle>(root, "MuteWhenMinimized", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._muteWhenMinimizedToggle, delegate(ChangeEvent<bool> v)
			{
				this._soundSettings.MuteWhenMinimized = v.newValue;
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000040CC File Offset: 0x000022CC
		public void Update()
		{
			float valueWithoutNotify = Mathf.Clamp01(this._soundSettings.MasterVolume);
			this._masterVolumeSlider.SetValueWithoutNotify(valueWithoutNotify);
			this._masterVolumeValueLabel.text = valueWithoutNotify.ToString("P0");
			float valueWithoutNotify2 = Mathf.Clamp01(this._soundSettings.MusicVolume);
			this._musicVolumeSlider.SetValueWithoutNotify(valueWithoutNotify2);
			this._musicVolumeValueLabel.text = valueWithoutNotify2.ToString("P0");
			float valueWithoutNotify3 = Mathf.Clamp01(this._soundSettings.EnvironmentVolume);
			this._environmentVolumeSlider.SetValueWithoutNotify(valueWithoutNotify3);
			this._environmentVolumeValueLabel.text = valueWithoutNotify3.ToString("P0");
			float valueWithoutNotify4 = Mathf.Clamp01(this._soundSettings.UIVolume);
			this._uiVolumeElementSlider.SetValueWithoutNotify(valueWithoutNotify4);
			this._uiVolumeElementValueLabel.text = valueWithoutNotify4.ToString("P0");
			this._muteWhenMinimizedToggle.SetValueWithoutNotify(this._soundSettings.MuteWhenMinimized);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000041C0 File Offset: 0x000023C0
		public static Slider InitializeSlider(VisualElement root, TextElement valueLabel, Action<float> setter)
		{
			Slider slider = UQueryExtensions.Q<Slider>(root, "Slider", null);
			slider.lowValue = SoundSettingsController.MinVolume;
			slider.highValue = SoundSettingsController.MaxVolume;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(slider, delegate(ChangeEvent<float> v)
			{
				setter(SoundSettingsController.ClampVolume(v.newValue));
				valueLabel.text = v.newValue.ToString("P0");
			});
			return slider;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004216 File Offset: 0x00002416
		public static float ClampVolume(float value)
		{
			return Mathf.Clamp(value, SoundSettingsController.MinVolume, SoundSettingsController.MaxVolume);
		}

		// Token: 0x04000092 RID: 146
		public static readonly float MinVolume = 0f;

		// Token: 0x04000093 RID: 147
		public static readonly float MaxVolume = 1f;

		// Token: 0x04000094 RID: 148
		public readonly SoundSettings _soundSettings;

		// Token: 0x04000095 RID: 149
		public TextElement _masterVolumeValueLabel;

		// Token: 0x04000096 RID: 150
		public Slider _masterVolumeSlider;

		// Token: 0x04000097 RID: 151
		public Label _musicVolumeValueLabel;

		// Token: 0x04000098 RID: 152
		public Slider _musicVolumeSlider;

		// Token: 0x04000099 RID: 153
		public Label _environmentVolumeValueLabel;

		// Token: 0x0400009A RID: 154
		public Slider _environmentVolumeSlider;

		// Token: 0x0400009B RID: 155
		public Label _uiVolumeElementValueLabel;

		// Token: 0x0400009C RID: 156
		public Slider _uiVolumeElementSlider;

		// Token: 0x0400009D RID: 157
		public Toggle _muteWhenMinimizedToggle;
	}
}
