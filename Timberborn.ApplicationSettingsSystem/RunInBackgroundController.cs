using System;
using Timberborn.CoreUI;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ApplicationSettingsSystem
{
	// Token: 0x02000005 RID: 5
	public class RunInBackgroundController : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public RunInBackgroundController(UISettings uiSettings)
		{
			this._uiSettings = uiSettings;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E3 File Offset: 0x000002E3
		public void Load()
		{
			this.UpdateSetting();
			this._uiSettings.RunInBackgroundChanged += delegate(object _, SettingChangedEventArgs<bool> _)
			{
				this.UpdateSetting();
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		public void UpdateSetting()
		{
			Application.runInBackground = this._uiSettings.RunInBackground;
		}

		// Token: 0x04000006 RID: 6
		public readonly UISettings _uiSettings;
	}
}
