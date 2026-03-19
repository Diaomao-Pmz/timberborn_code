using System;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x02000007 RID: 7
	public class BadtideWeatherUISpecification : IHazardousWeatherUISpecification
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public string NameLocKey
		{
			get
			{
				return "Weather.Badtide";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002107 File Offset: 0x00000307
		public string ApproachingLocKey
		{
			get
			{
				return "Weather.Notification.BadtideApproaching";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public string InProgressLocKey
		{
			get
			{
				return "Weather.Notification.BadtideInProgress";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002115 File Offset: 0x00000315
		public string StartedNotificationLocKey
		{
			get
			{
				return "Weather.BadtideStartedNotification";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000211C File Offset: 0x0000031C
		public string EndedNotificationLocKey
		{
			get
			{
				return "Weather.BadtideEndedNotification";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002123 File Offset: 0x00000323
		public string InProgressClass
		{
			get
			{
				return "weather-panel--badtide";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000212A File Offset: 0x0000032A
		public string IconClass
		{
			get
			{
				return "date-panel--badtide";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002131 File Offset: 0x00000331
		public string NotificationBackgroundClass
		{
			get
			{
				return "hazardous-weather-notification__background--badtide";
			}
		}
	}
}
