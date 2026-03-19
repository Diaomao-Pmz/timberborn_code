using System;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000013 RID: 19
	public class ZiplineConnectionActivatedEvent
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000033E0 File Offset: 0x000015E0
		public ZiplineTower ZiplineTower { get; }

		// Token: 0x06000082 RID: 130 RVA: 0x000033E8 File Offset: 0x000015E8
		public ZiplineConnectionActivatedEvent(ZiplineTower ziplineTower)
		{
			this.ZiplineTower = ziplineTower;
		}
	}
}
