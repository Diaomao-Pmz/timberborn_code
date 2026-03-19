using System;
using Timberborn.SettingsSystem;

namespace Timberborn.SoundSettingsSystem
{
	// Token: 0x02000005 RID: 5
	public class SoundSettings
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000C RID: 12 RVA: 0x000021F4 File Offset: 0x000003F4
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public event EventHandler<SettingChangedEventArgs<float>> MasterVolumeChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002264 File Offset: 0x00000464
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x0000229C File Offset: 0x0000049C
		public event EventHandler<SettingChangedEventArgs<float>> MusicVolumeChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000022D4 File Offset: 0x000004D4
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x0000230C File Offset: 0x0000050C
		public event EventHandler<SettingChangedEventArgs<float>> EnvironmentVolumeChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002344 File Offset: 0x00000544
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x0000237C File Offset: 0x0000057C
		public event EventHandler<SettingChangedEventArgs<float>> UIVolumeChanged;

		// Token: 0x06000014 RID: 20 RVA: 0x000023B1 File Offset: 0x000005B1
		public SoundSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023C0 File Offset: 0x000005C0
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000023D7 File Offset: 0x000005D7
		public float MasterVolume
		{
			get
			{
				return this._settings.GetFloat(SoundSettings.MasterVolumeKey, 1f);
			}
			set
			{
				this._settings.SetFloat(SoundSettings.MasterVolumeKey, value);
				EventHandler<SettingChangedEventArgs<float>> masterVolumeChanged = this.MasterVolumeChanged;
				if (masterVolumeChanged == null)
				{
					return;
				}
				masterVolumeChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002401 File Offset: 0x00000601
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002418 File Offset: 0x00000618
		public float MusicVolume
		{
			get
			{
				return this._settings.GetFloat(SoundSettings.MusicVolumeKey, 1f);
			}
			set
			{
				this._settings.SetFloat(SoundSettings.MusicVolumeKey, value);
				EventHandler<SettingChangedEventArgs<float>> musicVolumeChanged = this.MusicVolumeChanged;
				if (musicVolumeChanged == null)
				{
					return;
				}
				musicVolumeChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002442 File Offset: 0x00000642
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002459 File Offset: 0x00000659
		public float EnvironmentVolume
		{
			get
			{
				return this._settings.GetFloat(SoundSettings.EnvironmentVolumeKey, 1f);
			}
			set
			{
				this._settings.SetFloat(SoundSettings.EnvironmentVolumeKey, value);
				EventHandler<SettingChangedEventArgs<float>> environmentVolumeChanged = this.EnvironmentVolumeChanged;
				if (environmentVolumeChanged == null)
				{
					return;
				}
				environmentVolumeChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002483 File Offset: 0x00000683
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000249A File Offset: 0x0000069A
		public float UIVolume
		{
			get
			{
				return this._settings.GetFloat(SoundSettings.UIVolumeKey, 1f);
			}
			set
			{
				this._settings.SetFloat(SoundSettings.UIVolumeKey, value);
				EventHandler<SettingChangedEventArgs<float>> uivolumeChanged = this.UIVolumeChanged;
				if (uivolumeChanged == null)
				{
					return;
				}
				uivolumeChanged(this, new SettingChangedEventArgs<float>(value));
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024C4 File Offset: 0x000006C4
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024D7 File Offset: 0x000006D7
		public bool MuteWhenMinimized
		{
			get
			{
				return this._settings.GetBool(SoundSettings.MuteWhenMinimizedKey, true);
			}
			set
			{
				this._settings.SetBool(SoundSettings.MuteWhenMinimizedKey, value);
			}
		}

		// Token: 0x04000009 RID: 9
		public static readonly string MasterVolumeKey = "MasterVolume";

		// Token: 0x0400000A RID: 10
		public static readonly string MusicVolumeKey = "MusicVolume";

		// Token: 0x0400000B RID: 11
		public static readonly string EnvironmentVolumeKey = "EnvironmentVolume";

		// Token: 0x0400000C RID: 12
		public static readonly string UIVolumeKey = "UIVolume";

		// Token: 0x0400000D RID: 13
		public static readonly string MuteWhenMinimizedKey = "MuteWhenMinimized";

		// Token: 0x04000012 RID: 18
		public readonly ISettings _settings;
	}
}
