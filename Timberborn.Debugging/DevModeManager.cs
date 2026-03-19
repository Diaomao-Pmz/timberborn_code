using System;
using Timberborn.ErrorReporting;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Debugging
{
	// Token: 0x0200000B RID: 11
	public class DevModeManager
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000231F File Offset: 0x0000051F
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002327 File Offset: 0x00000527
		public bool Enabled { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002330 File Offset: 0x00000530
		public DevModeManager(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000233F File Offset: 0x0000053F
		public void Enable()
		{
			if (!this.Enabled)
			{
				Debug.Log("Dev mode enabled");
				this.EnableSilently();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002359 File Offset: 0x00000559
		public void Disable()
		{
			if (this.Enabled)
			{
				this.Enabled = false;
				CrashSceneLoader.DevModeEnabled = false;
				this._eventBus.Post(new DevModeToggledEvent(false));
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002381 File Offset: 0x00000581
		public void EnableSilently()
		{
			if (!this.Enabled)
			{
				this.Enabled = true;
				CrashSceneLoader.DevModeEnabled = true;
				this._eventBus.Post(new DevModeToggledEvent(true));
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly EventBus _eventBus;
	}
}
