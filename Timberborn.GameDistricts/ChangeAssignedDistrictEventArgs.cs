using System;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000007 RID: 7
	public readonly struct ChangeAssignedDistrictEventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public DistrictCenter PreviousDistrict { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public DistrictCenter CurrentDistrict { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public ChangeAssignedDistrictEventArgs(DistrictCenter previousDistrict, DistrictCenter currentDistrict)
		{
			this.PreviousDistrict = previousDistrict;
			this.CurrentDistrict = currentDistrict;
		}
	}
}
