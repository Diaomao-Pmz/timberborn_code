using System;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using Timberborn.Wellbeing;

namespace Timberborn.WellbeingUI
{
	// Token: 0x02000022 RID: 34
	public class WellbeingServiceDistrictSwitcher : ILoadableSingleton
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004878 File Offset: 0x00002A78
		public WellbeingServiceDistrictSwitcher(WellbeingService wellbeingService, DistrictContextService districtContextService, EventBus eventBus)
		{
			this._wellbeingService = wellbeingService;
			this._districtContextService = districtContextService;
			this._eventBus = eventBus;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004895 File Offset: 0x00002A95
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000048A3 File Offset: 0x00002AA3
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000048A3 File Offset: 0x00002AA3
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000048AB File Offset: 0x00002AAB
		public void UpdateDistrict()
		{
			this._wellbeingService.SwitchDistrict(this._districtContextService.SelectedDistrict);
		}

		// Token: 0x040000AD RID: 173
		public readonly WellbeingService _wellbeingService;

		// Token: 0x040000AE RID: 174
		public readonly DistrictContextService _districtContextService;

		// Token: 0x040000AF RID: 175
		public readonly EventBus _eventBus;
	}
}
