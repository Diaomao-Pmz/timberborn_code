using System;
using Timberborn.SingletonSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x0200001E RID: 30
	public class DistrictContextService
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000040FC File Offset: 0x000022FC
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004104 File Offset: 0x00002304
		public DistrictCenter SelectedDistrict { get; private set; }

		// Token: 0x060000E0 RID: 224 RVA: 0x0000410D File Offset: 0x0000230D
		public DistrictContextService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000411C File Offset: 0x0000231C
		public void SelectDistrict(DistrictCenter districtCenter)
		{
			if (this.SelectedDistrict != districtCenter)
			{
				this.UnselectDistrict();
				this.SelectedDistrict = districtCenter;
				this._eventBus.Post(new DistrictSelectedEvent(districtCenter));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004145 File Offset: 0x00002345
		public void UnselectDistrict()
		{
			if (this.SelectedDistrict)
			{
				this.SelectedDistrict = null;
				this._eventBus.Post(new DistrictUnselectedEvent());
			}
		}

		// Token: 0x04000057 RID: 87
		public readonly EventBus _eventBus;
	}
}
