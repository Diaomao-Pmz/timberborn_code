using System;
using Timberborn.Autosaving;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;

namespace Timberborn.AutosavingUI
{
	// Token: 0x02000006 RID: 6
	public class PanelAutosaveBlocker : ILoadableSingleton, IAutosaveBlocker
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002203 File Offset: 0x00000403
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000220B File Offset: 0x0000040B
		public bool IsBlocking { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002214 File Offset: 0x00000414
		public PanelAutosaveBlocker(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002223 File Offset: 0x00000423
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002231 File Offset: 0x00000431
		[OnEvent]
		public void OnPanelShown(PanelShownEvent panelShownEvent)
		{
			this.IsBlocking |= panelShownEvent.LockSpeed;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002246 File Offset: 0x00000446
		[OnEvent]
		public void OnPanelHidden(PanelHiddenEvent panelHiddenEvent)
		{
			this.IsBlocking = !panelHiddenEvent.UnlockSpeed;
		}

		// Token: 0x0400000D RID: 13
		public readonly EventBus _eventBus;
	}
}
