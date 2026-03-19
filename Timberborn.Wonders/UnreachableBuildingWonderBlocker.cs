using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;

namespace Timberborn.Wonders
{
	// Token: 0x0200000B RID: 11
	public class UnreachableBuildingWonderBlocker : BaseComponent, IAwakableComponent, IWonderBlocker
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002266 File Offset: 0x00000466
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002274 File Offset: 0x00000474
		public bool IsWonderBlocked()
		{
			return !this._districtBuilding.InstantDistrict;
		}

		// Token: 0x04000010 RID: 16
		public DistrictBuilding _districtBuilding;
	}
}
