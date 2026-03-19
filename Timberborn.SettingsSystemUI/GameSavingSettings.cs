using System;
using Timberborn.SettingsSystem;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200000D RID: 13
	public class GameSavingSettings
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003C RID: 60 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x000029DC File Offset: 0x00000BDC
		public event EventHandler<SettingChangedEventArgs<bool>> AutoSavingOnChanged;

		// Token: 0x0600003E RID: 62 RVA: 0x00002A11 File Offset: 0x00000C11
		public GameSavingSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A20 File Offset: 0x00000C20
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A33 File Offset: 0x00000C33
		public bool AutoSavingOn
		{
			get
			{
				return this._settings.GetBool(GameSavingSettings.AutoSavingOnKey, true);
			}
			set
			{
				this._settings.SetBool(GameSavingSettings.AutoSavingOnKey, value);
				EventHandler<SettingChangedEventArgs<bool>> autoSavingOnChanged = this.AutoSavingOnChanged;
				if (autoSavingOnChanged == null)
				{
					return;
				}
				autoSavingOnChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x0400002E RID: 46
		public static readonly string AutoSavingOnKey = "AutoSavingOn";

		// Token: 0x04000030 RID: 48
		public readonly ISettings _settings;
	}
}
