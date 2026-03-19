using System;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;

namespace Timberborn.BatchControl
{
	// Token: 0x0200000D RID: 13
	public class BatchControlDistrict
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002CA2 File Offset: 0x00000EA2
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002CAA File Offset: 0x00000EAA
		public DistrictCenter SelectedDistrict { get; private set; }

		// Token: 0x06000042 RID: 66 RVA: 0x00002CB3 File Offset: 0x00000EB3
		public BatchControlDistrict(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public void SetDistrict(DistrictCenter districtCenter)
		{
			if (districtCenter != this.SelectedDistrict)
			{
				this.SelectedDistrict = districtCenter;
				this._eventBus.Post(new BatchControlDistrictChangedEvent());
			}
		}

		// Token: 0x04000036 RID: 54
		public readonly EventBus _eventBus;
	}
}
