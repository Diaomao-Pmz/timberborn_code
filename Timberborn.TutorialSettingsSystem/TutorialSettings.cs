using System;
using Timberborn.SettingsSystem;

namespace Timberborn.TutorialSettingsSystem
{
	// Token: 0x02000004 RID: 4
	public class TutorialSettings
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler<SettingChangedEventArgs<bool>> DisableTutorialChanged;

		// Token: 0x06000005 RID: 5 RVA: 0x0000212D File Offset: 0x0000032D
		public TutorialSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000214F File Offset: 0x0000034F
		public bool DisableTutorial
		{
			get
			{
				return this._settings.GetBool(TutorialSettings.DisableTutorialKey, false);
			}
			set
			{
				this._settings.SetBool(TutorialSettings.DisableTutorialKey, value);
				EventHandler<SettingChangedEventArgs<bool>> disableTutorialChanged = this.DisableTutorialChanged;
				if (disableTutorialChanged == null)
				{
					return;
				}
				disableTutorialChanged(this, new SettingChangedEventArgs<bool>(value));
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string DisableTutorialKey = "DisableTutorial2022-12-05";

		// Token: 0x04000008 RID: 8
		public readonly ISettings _settings;
	}
}
