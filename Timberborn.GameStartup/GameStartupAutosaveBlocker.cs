using System;
using Timberborn.Autosaving;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;

namespace Timberborn.GameStartup
{
	// Token: 0x02000007 RID: 7
	public class GameStartupAutosaveBlocker : IAutosaveBlocker, ILoadableSingleton
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000232B File Offset: 0x0000052B
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002333 File Offset: 0x00000533
		public bool IsBlocking { get; private set; } = true;

		// Token: 0x06000016 RID: 22 RVA: 0x0000233C File Offset: 0x0000053C
		public GameStartupAutosaveBlocker(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002352 File Offset: 0x00000552
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002360 File Offset: 0x00000560
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this.IsBlocking = false;
			this._eventBus.Unregister(this);
		}

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;
	}
}
