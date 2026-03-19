using System;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000023 RID: 35
	public class StatusSpecification
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000455F File Offset: 0x0000275F
		public string SpriteName { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004567 File Offset: 0x00002767
		public string StatusDescription { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000456F File Offset: 0x0000276F
		public string AlertDescription { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004577 File Offset: 0x00002777
		public float DelayInHours { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000457F File Offset: 0x0000277F
		public bool ShowFloatingIcon { get; }

		// Token: 0x060000E4 RID: 228 RVA: 0x00004587 File Offset: 0x00002787
		public StatusSpecification(string spriteName, string statusDescription, string alertDescription, float delayInHours, bool showFloatingIcon)
		{
			this.SpriteName = spriteName;
			this.StatusDescription = statusDescription;
			this.AlertDescription = alertDescription;
			this.DelayInHours = delayInHours;
			this.ShowFloatingIcon = showFloatingIcon;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000045B4 File Offset: 0x000027B4
		public static StatusSpecification Create(string spriteName, string statusDescription)
		{
			return new StatusSpecification(spriteName, statusDescription, "", 0f, false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000045C8 File Offset: 0x000027C8
		public static StatusSpecification CreateWithIcon(string spriteName, string statusDescription, float delayInHours)
		{
			return new StatusSpecification(spriteName, statusDescription, "", delayInHours, true);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000045D8 File Offset: 0x000027D8
		public static StatusSpecification CreateWithAlert(string spriteName, string statusDescription, string alertDescription, float delayInHours)
		{
			return new StatusSpecification(spriteName, statusDescription, alertDescription, delayInHours, false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000045E4 File Offset: 0x000027E4
		public static StatusSpecification CreateWithAlertAndIcon(string spriteName, string statusDescription, string alertDescription, float delayInHours)
		{
			return new StatusSpecification(spriteName, statusDescription, alertDescription, delayInHours, true);
		}
	}
}
