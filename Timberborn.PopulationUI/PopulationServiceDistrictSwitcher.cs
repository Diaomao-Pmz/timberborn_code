using System;
using Timberborn.GameDistricts;
using Timberborn.Population;
using Timberborn.SingletonSystem;

namespace Timberborn.PopulationUI
{
	// Token: 0x0200000C RID: 12
	public class PopulationServiceDistrictSwitcher : ILoadableSingleton
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002974 File Offset: 0x00000B74
		public PopulationServiceDistrictSwitcher(PopulationService populationService, DistrictContextService districtContextService, EventBus eventBus)
		{
			this._populationService = populationService;
			this._districtContextService = districtContextService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002991 File Offset: 0x00000B91
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000299F File Offset: 0x00000B9F
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000299F File Offset: 0x00000B9F
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029A7 File Offset: 0x00000BA7
		public void UpdateDistrict()
		{
			this._populationService.SwitchDistrict(this._districtContextService.SelectedDistrict);
		}

		// Token: 0x04000035 RID: 53
		public readonly PopulationService _populationService;

		// Token: 0x04000036 RID: 54
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000037 RID: 55
		public readonly EventBus _eventBus;
	}
}
