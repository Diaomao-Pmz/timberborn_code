using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000007 RID: 7
	public class NotificationSaver : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021FC File Offset: 0x000003FC
		public NotificationSaver(ISingletonLoader singletonLoader, NotificationBus notificationBus, NotificationValueSerializer notificationValueSerializer)
		{
			this._singletonLoader = singletonLoader;
			this._notificationBus = notificationBus;
			this._notificationValueSerializer = notificationValueSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002224 File Offset: 0x00000424
		public IEnumerable<Notification> Notifications
		{
			get
			{
				return this._notifications.AsReadOnlyEnumerable<Notification>();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(NotificationSaver.NotificationSaverKey, out objectLoader))
			{
				foreach (Notification notification in objectLoader.Get<Notification>(NotificationSaver.NotificationsKey, this._notificationValueSerializer))
				{
					this.AddNotification(notification);
				}
			}
			this._notificationBus.NotificationPosted += delegate(object _, NotificationEventArgs args)
			{
				this.AddNotification(args.Notification);
			};
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C0 File Offset: 0x000004C0
		public void Save(ISingletonSaver singletonSaver)
		{
			IObjectSaver singleton = singletonSaver.GetSingleton(NotificationSaver.NotificationSaverKey);
			List<Notification> list = new List<Notification>();
			foreach (Notification item in this._notifications)
			{
				list.Add(item);
			}
			singleton.Set<Notification>(NotificationSaver.NotificationsKey, list, this._notificationValueSerializer);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002338 File Offset: 0x00000538
		public void AddNotification(Notification notification)
		{
			if (this._notifications.Count == NotificationSaver.MaxNotifications)
			{
				this._notifications.Dequeue();
			}
			this._notifications.Enqueue(notification);
		}

		// Token: 0x0400000D RID: 13
		public static readonly int MaxNotifications = 25;

		// Token: 0x0400000E RID: 14
		public static readonly SingletonKey NotificationSaverKey = new SingletonKey("NotificationSaver");

		// Token: 0x0400000F RID: 15
		public static readonly ListKey<Notification> NotificationsKey = new ListKey<Notification>("Notifications");

		// Token: 0x04000010 RID: 16
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000011 RID: 17
		public readonly NotificationBus _notificationBus;

		// Token: 0x04000012 RID: 18
		public readonly NotificationValueSerializer _notificationValueSerializer;

		// Token: 0x04000013 RID: 19
		public readonly Queue<Notification> _notifications = new Queue<Notification>();
	}
}
