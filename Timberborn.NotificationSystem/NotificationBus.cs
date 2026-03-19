using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.GameCycleSystem;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000005 RID: 5
	public class NotificationBus
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public event EventHandler<NotificationEventArgs> NotificationPosted;

		// Token: 0x0600000A RID: 10 RVA: 0x00002171 File Offset: 0x00000371
		public NotificationBus(GameCycleService gameCycleService)
		{
			this._gameCycleService = gameCycleService;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		public void Post(string description, BaseComponent subject)
		{
			EntityComponent component = subject.GetComponent<EntityComponent>();
			this.Post(description, component);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000219C File Offset: 0x0000039C
		public void Post(string description, EntityComponent entityComponent)
		{
			Guid entityId = entityComponent.EntityId;
			Notification notification = new Notification(description, entityId, this._gameCycleService.Cycle, this._gameCycleService.CycleDay);
			EventHandler<NotificationEventArgs> notificationPosted = this.NotificationPosted;
			if (notificationPosted == null)
			{
				return;
			}
			notificationPosted(this, new NotificationEventArgs(notification));
		}

		// Token: 0x0400000B RID: 11
		public readonly GameCycleService _gameCycleService;
	}
}
