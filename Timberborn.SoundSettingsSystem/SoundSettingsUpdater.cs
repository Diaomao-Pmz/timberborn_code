using System;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;

namespace Timberborn.SoundSettingsSystem
{
	// Token: 0x02000007 RID: 7
	public class SoundSettingsUpdater : ILoadableSingleton
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000254C File Offset: 0x0000074C
		public SoundSettingsUpdater(ISoundSystem soundSystem, SoundSettings soundSettings)
		{
			this._soundSystem = soundSystem;
			this._soundSettings = soundSettings;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002562 File Offset: 0x00000762
		public void Load()
		{
			this.SetSoundSystemVolumes();
			this.SubscribeToSettingsEvents();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002570 File Offset: 0x00000770
		public void SetSoundSystemVolumes()
		{
			this._soundSystem.SetMasterVolume(this._soundSettings.MasterVolume);
			this._soundSystem.SetMusicVolume(this._soundSettings.MusicVolume);
			this._soundSystem.SetEnvironmentVolume(this._soundSettings.EnvironmentVolume);
			this._soundSystem.SetUIVolume(this._soundSettings.UIVolume);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025D8 File Offset: 0x000007D8
		public void SubscribeToSettingsEvents()
		{
			this._soundSettings.MasterVolumeChanged += delegate(object _, SettingChangedEventArgs<float> e)
			{
				this._soundSystem.SetMasterVolume(e.Value);
			};
			this._soundSettings.MusicVolumeChanged += delegate(object _, SettingChangedEventArgs<float> e)
			{
				this._soundSystem.SetMusicVolume(e.Value);
			};
			this._soundSettings.EnvironmentVolumeChanged += delegate(object _, SettingChangedEventArgs<float> e)
			{
				this._soundSystem.SetEnvironmentVolume(e.Value);
			};
			this._soundSettings.UIVolumeChanged += delegate(object _, SettingChangedEventArgs<float> e)
			{
				this._soundSystem.SetUIVolume(e.Value);
			};
		}

		// Token: 0x04000013 RID: 19
		public readonly ISoundSystem _soundSystem;

		// Token: 0x04000014 RID: 20
		public readonly SoundSettings _soundSettings;
	}
}
