using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.NotificationSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace Timberborn.NotificationSystemUI
{
	// Token: 0x02000004 RID: 4
	public class NotificationPanel : IPostLoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000002BC
		public NotificationPanel(VisualElementLoader visualElementLoader, UILayout uiLayout, NotificationBus notificationBus, NotificationSaver notificationSaver, EntitySelectionService entitySelectionService, EntityRegistry entityRegistry, TimestampFormatter timestampFormatter, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
			this._notificationBus = notificationBus;
			this._notificationSaver = notificationSaver;
			this._entitySelectionService = entitySelectionService;
			this._entityRegistry = entityRegistry;
			this._timestampFormatter = timestampFormatter;
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public void PostLoad()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/NotificationPanel/NotificationPanel");
			this._notificationView = UQueryExtensions.Q<ScrollView>(this._root, "Notifications", null);
			this._latestNotificationElement = UQueryExtensions.Q<VisualElement>(this._root, "LatestNotification", null);
			this._latestNotificationElement.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.FocusOnLatestNotification();
			}, 0);
			this._latestNotificationElement.ToggleDisplayStyle(false);
			this._extensionToggler = UQueryExtensions.Q<Button>(this._root, "ExtensionToggler", null);
			this._extensionToggler.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleSelection), 0);
			foreach (Notification notification in this._notificationSaver.Notifications)
			{
				this.AddNotification(notification);
			}
			this._notificationBus.NotificationPosted += delegate(object _, NotificationEventArgs args)
			{
				this.AddNotification(args.Notification);
			};
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000222C File Offset: 0x0000042C
		[OnEvent]
		public void OnShowPrimaryUI(ShowPrimaryUIEvent showPrimaryUIEvent)
		{
			this._uiLayout.AddTopLeft(this._root, 3);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002240 File Offset: 0x00000440
		public void ToggleSelection(ClickEvent evt)
		{
			if (this._extended)
			{
				this._notificationView.ToggleDisplayStyle(false);
				this._latestNotificationElement.ToggleDisplayStyle(true);
				this._extensionToggler.AddToClassList(NotificationPanel.HiddenClass);
				this._extended = false;
				return;
			}
			this._notificationView.ToggleDisplayStyle(true);
			this._latestNotificationElement.ToggleDisplayStyle(false);
			this._extensionToggler.RemoveFromClassList(NotificationPanel.HiddenClass);
			this._extended = true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B4 File Offset: 0x000004B4
		public void AddNotification(Notification notification)
		{
			if (this._notifications.Count == NotificationSaver.MaxNotifications)
			{
				this._notificationView.Remove(this._notifications.Dequeue());
			}
			this._latestNotification = notification;
			this._latestNotificationElement.Clear();
			this._latestNotificationElement.Add(this.CreateNotificationElement(notification));
			VisualElement visualElement = this.CreateNotificationElement(notification);
			this._notifications.Enqueue(visualElement);
			this._notificationView.Add(visualElement);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002330 File Offset: 0x00000530
		public VisualElement CreateNotificationElement(Notification notification)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/NotificationPanel/NotificationPanelItem");
			UQueryExtensions.Q<Label>(visualElement, "Date", null).text = this._timestampFormatter.FormatShort(notification.Cycle, notification.CycleDay);
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = notification.Description;
			visualElement.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.FocusOnNotification(notification);
			}, 0);
			return visualElement;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023C2 File Offset: 0x000005C2
		public void FocusOnLatestNotification()
		{
			if (this._latestNotification != null)
			{
				this.FocusOnNotification(this._latestNotification);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023D8 File Offset: 0x000005D8
		public void FocusOnNotification(Notification notification)
		{
			EntityComponent entity = this._entityRegistry.GetEntity(notification.Subject);
			if (entity != null)
			{
				this._entitySelectionService.SelectAndFollow(entity);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string HiddenClass = "extension-clamp-full--hidden";

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public readonly UILayout _uiLayout;

		// Token: 0x04000009 RID: 9
		public readonly NotificationBus _notificationBus;

		// Token: 0x0400000A RID: 10
		public readonly NotificationSaver _notificationSaver;

		// Token: 0x0400000B RID: 11
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400000C RID: 12
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x0400000D RID: 13
		public readonly TimestampFormatter _timestampFormatter;

		// Token: 0x0400000E RID: 14
		public readonly EventBus _eventBus;

		// Token: 0x0400000F RID: 15
		public readonly Queue<VisualElement> _notifications = new Queue<VisualElement>();

		// Token: 0x04000010 RID: 16
		public VisualElement _root;

		// Token: 0x04000011 RID: 17
		public ScrollView _notificationView;

		// Token: 0x04000012 RID: 18
		public Notification _latestNotification;

		// Token: 0x04000013 RID: 19
		public VisualElement _latestNotificationElement;

		// Token: 0x04000014 RID: 20
		public Button _extensionToggler;

		// Token: 0x04000015 RID: 21
		public bool _extended = true;
	}
}
