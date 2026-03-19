using System;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000015 RID: 21
	public readonly struct StartedNewPathEventArgs
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E01 File Offset: 0x00001001
		public float Distance { get; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002E09 File Offset: 0x00001009
		public StartedNewPathEventArgs(float distance)
		{
			this.Distance = distance;
		}
	}
}
