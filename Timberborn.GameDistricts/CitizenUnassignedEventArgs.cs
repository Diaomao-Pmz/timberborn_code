using System;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200000A RID: 10
	public class CitizenUnassignedEventArgs
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002452 File Offset: 0x00000652
		public Citizen Citizen { get; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000245A File Offset: 0x0000065A
		public CitizenUnassignedEventArgs(Citizen citizen)
		{
			this.Citizen = citizen;
		}
	}
}
