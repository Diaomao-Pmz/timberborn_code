using System;
using Timberborn.InputSystem;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.InputSystemUI
{
	// Token: 0x0200000B RID: 11
	public class KeywordMatchNotifier : ILoadableSingleton
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023AC File Offset: 0x000005AC
		public KeywordMatchNotifier(EventBus eventBus, QuickNotificationService quickNotificationService)
		{
			this._eventBus = eventBus;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023C2 File Offset: 0x000005C2
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023D0 File Offset: 0x000005D0
		[OnEvent]
		public void OnKeywordMatched(KeywordMatchedEvent keywordMatchedEvent)
		{
			this._quickNotificationService.SendNotification(keywordMatchedEvent.KeywordNotification);
		}

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly QuickNotificationService _quickNotificationService;
	}
}
