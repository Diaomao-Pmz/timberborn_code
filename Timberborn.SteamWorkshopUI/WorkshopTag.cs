using System;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x0200000D RID: 13
	public readonly struct WorkshopTag
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003043 File Offset: 0x00001243
		public WorkshopTagCategory Category { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000304B File Offset: 0x0000124B
		public string Name { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003053 File Offset: 0x00001253
		public int Order { get; }

		// Token: 0x0600006D RID: 109 RVA: 0x0000305B File Offset: 0x0000125B
		public WorkshopTag(WorkshopTagCategory category, string name, int order)
		{
			this.Category = category;
			this.Name = name;
			this.Order = order;
		}
	}
}
