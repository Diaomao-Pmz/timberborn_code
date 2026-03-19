using System;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x02000008 RID: 8
	public class DroughtWeatherUISpecification : IHazardousWeatherUISpecification
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002138 File Offset: 0x00000338
		public string NameLocKey
		{
			get
			{
				return "Weather.Drought";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000213F File Offset: 0x0000033F
		public string ApproachingLocKey
		{
			get
			{
				return "Weather.Notification.DroughtApproaching";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002146 File Offset: 0x00000346
		public string InProgressLocKey
		{
			get
			{
				return "Weather.Notification.DroughtInProgress";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000214D File Offset: 0x0000034D
		public string StartedNotificationLocKey
		{
			get
			{
				return "Weather.DroughtStartedNotification";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002154 File Offset: 0x00000354
		public string EndedNotificationLocKey
		{
			get
			{
				return "Weather.DroughtEndedNotification";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000215B File Offset: 0x0000035B
		public string InProgressClass
		{
			get
			{
				return "weather-panel--dry";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002162 File Offset: 0x00000362
		public string IconClass
		{
			get
			{
				return "date-panel--drought";
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002169 File Offset: 0x00000369
		public string NotificationBackgroundClass
		{
			get
			{
				return "hazardous-weather-notification__background--dry";
			}
		}
	}
}
