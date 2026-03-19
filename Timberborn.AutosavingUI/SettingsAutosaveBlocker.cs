using System;
using Timberborn.Autosaving;
using Timberborn.SettingsSystem;
using Timberborn.SettingsSystemUI;
using Timberborn.SingletonSystem;

namespace Timberborn.AutosavingUI
{
	// Token: 0x02000007 RID: 7
	public class SettingsAutosaveBlocker : IAutosaveBlocker, ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002257 File Offset: 0x00000457
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000225F File Offset: 0x0000045F
		public bool IsBlocking { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002268 File Offset: 0x00000468
		public SettingsAutosaveBlocker(GameSavingSettings gameSavingSettings)
		{
			this._gameSavingSettings = gameSavingSettings;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002277 File Offset: 0x00000477
		public void Load()
		{
			this.IsBlocking = !this._gameSavingSettings.AutoSavingOn;
			this._gameSavingSettings.AutoSavingOnChanged += delegate(object _, SettingChangedEventArgs<bool> e)
			{
				this.IsBlocking = !e.Value;
			};
		}

		// Token: 0x0400000F RID: 15
		public readonly GameSavingSettings _gameSavingSettings;
	}
}
