using System;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000009 RID: 9
	public class CitizenAssignedEventArgs
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000243B File Offset: 0x0000063B
		public Citizen Citizen { get; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002443 File Offset: 0x00000643
		public CitizenAssignedEventArgs(Citizen citizen)
		{
			this.Citizen = citizen;
		}
	}
}
