using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.UILayoutSystem
{
	// Token: 0x02000005 RID: 5
	public class OverlayPanelSpeedLocker : ILoadableSingleton
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021E9 File Offset: 0x000003E9
		public OverlayPanelSpeedLocker(EventBus eventBus, SpeedManager speedManager)
		{
			this._eventBus = eventBus;
			this._speedManager = speedManager;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FF File Offset: 0x000003FF
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000220D File Offset: 0x0000040D
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			if (panelShownEvent.LockSpeed)
			{
				this._speedManager.ChangeAndLockSpeed(0f);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002227 File Offset: 0x00000427
		[OnEvent]
		public void OnPanelHidden(PanelHiddenEvent panelHiddenEvent)
		{
			if (panelHiddenEvent.UnlockSpeed)
			{
				this._speedManager.UnlockSpeed();
			}
		}

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly SpeedManager _speedManager;
	}
}
