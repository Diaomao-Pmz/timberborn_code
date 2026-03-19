using System;
using System.Collections.Generic;
using Timberborn.Automation;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.ResourceCountingSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003B RID: 59
	public class SamplingResourcesService : ISamplingSingleton, IPostLoadableSingleton
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000076EA File Offset: 0x000058EA
		public SamplingResourcesService(DistrictCenterRegistry districtCenterRegistry, ResourceCountingService resourceCountingService)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._resourceCountingService = resourceCountingService;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007724 File Offset: 0x00005924
		public void PostLoad()
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				this._resourceCountingService.GetDistrictResourceCounter(districtCenter).UpdateCounters();
			}
			this.Sample();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007790 File Offset: 0x00005990
		public void Sample()
		{
			this._oldDistrictResourceCounters.AddRange(this._districtResourceCounters);
			this._districtResourceCounters.Clear();
			ReadOnlyList<DistrictCenter> finishedDistrictCenters = this._districtCenterRegistry.FinishedDistrictCenters;
			for (int i = 0; i < finishedDistrictCenters.Count; i++)
			{
				DistrictCenter districtCenter = finishedDistrictCenters[i];
				DistrictResourceCounter districtResourceCounter;
				DistrictResourceCounter value = this._oldDistrictResourceCounters.TryGetValue(districtCenter, out districtResourceCounter) ? districtResourceCounter : this._resourceCountingService.GetDistrictResourceCounter(districtCenter);
				this._districtResourceCounters.Add(districtCenter, value);
			}
			this._oldDistrictResourceCounters.Clear();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00007819 File Offset: 0x00005A19
		public DistrictResourceCounter GetDistrictCounter(DistrictCenter districtCenter)
		{
			if (districtCenter == null)
			{
				return this._emptyDistrictResourcesCounter;
			}
			return CollectionExtensions.GetValueOrDefault<DistrictCenter, DistrictResourceCounter>(this._districtResourceCounters, districtCenter, this._emptyDistrictResourcesCounter);
		}

		// Token: 0x0400013C RID: 316
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x0400013D RID: 317
		public readonly ResourceCountingService _resourceCountingService;

		// Token: 0x0400013E RID: 318
		public readonly Dictionary<DistrictCenter, DistrictResourceCounter> _districtResourceCounters = new Dictionary<DistrictCenter, DistrictResourceCounter>();

		// Token: 0x0400013F RID: 319
		public readonly Dictionary<DistrictCenter, DistrictResourceCounter> _oldDistrictResourceCounters = new Dictionary<DistrictCenter, DistrictResourceCounter>();

		// Token: 0x04000140 RID: 320
		public readonly DistrictResourceCounter _emptyDistrictResourcesCounter = new DistrictResourceCounter();
	}
}
