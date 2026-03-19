using System;
using Timberborn.SettingsSystem;

namespace Timberborn.IntroSettingsSystem
{
	// Token: 0x02000004 RID: 4
	public class IntroSettings
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public IntroSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool DisableIntro
		{
			get
			{
				return this._settings.GetBool(IntroSettings.DisableIntroKey, false);
			}
			set
			{
				this._settings.SetBool(IntroSettings.DisableIntroKey, value);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DisableIntroKey = "DisableIntro";

		// Token: 0x04000007 RID: 7
		public readonly ISettings _settings;
	}
}
