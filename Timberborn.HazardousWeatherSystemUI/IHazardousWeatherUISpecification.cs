using System;

namespace Timberborn.HazardousWeatherSystemUI
{
	// Token: 0x02000010 RID: 16
	public interface IHazardousWeatherUISpecification
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88
		string NameLocKey { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000059 RID: 89
		string ApproachingLocKey { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005A RID: 90
		string InProgressLocKey { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91
		string StartedNotificationLocKey { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005C RID: 92
		string EndedNotificationLocKey { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005D RID: 93
		string InProgressClass { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005E RID: 94
		string IconClass { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95
		string NotificationBackgroundClass { get; }
	}
}
