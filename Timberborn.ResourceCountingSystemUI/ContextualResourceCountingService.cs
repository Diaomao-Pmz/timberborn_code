using System;
using Timberborn.GameDistricts;
using Timberborn.ResourceCountingSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.ResourceCountingSystemUI
{
	// Token: 0x02000004 RID: 4
	public class ContextualResourceCountingService : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ContextualResourceCountingService(ResourceCountingService resourceCountingService, DistrictContextService districtContextService, EventBus eventBus)
		{
			this._resourceCountingService = resourceCountingService;
			this._districtContextService = districtContextService;
			this._eventBus = eventBus;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DB File Offset: 0x000002DB
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E9 File Offset: 0x000002E9
		[OnEvent]
		public void OnDistrictSelected(DistrictSelectedEvent districtSelectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E9 File Offset: 0x000002E9
		[OnEvent]
		public void OnDistrictUnselected(DistrictUnselectedEvent districtUnselectedEvent)
		{
			this.UpdateDistrict();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F1 File Offset: 0x000002F1
		public ResourceCount GetContextualResourceCount(string goodId)
		{
			if (!this._districtContextService.SelectedDistrict)
			{
				return this._resourceCountingService.GetGlobalResourceCount(goodId);
			}
			return this._resourceCountingService.GetDistrictResourceCount(goodId);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211E File Offset: 0x0000031E
		public void UpdateDistrict()
		{
			this._resourceCountingService.SwitchDistrict(this._districtContextService.SelectedDistrict);
		}

		// Token: 0x04000006 RID: 6
		public readonly ResourceCountingService _resourceCountingService;

		// Token: 0x04000007 RID: 7
		public readonly DistrictContextService _districtContextService;

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;
	}
}
