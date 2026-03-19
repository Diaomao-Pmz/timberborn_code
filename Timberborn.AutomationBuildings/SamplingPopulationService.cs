using System;
using System.Collections.Generic;
using Timberborn.Automation;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Population;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200003A RID: 58
	public class SamplingPopulationService : ISamplingSingleton
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000075D2 File Offset: 0x000057D2
		public PopulationData GlobalPopulationData { get; } = new PopulationData();

		// Token: 0x06000288 RID: 648 RVA: 0x000075DC File Offset: 0x000057DC
		public SamplingPopulationService(DistrictCenterRegistry districtCenterRegistry, PopulationDataCollector populationDataCollector, PopulationService populationService)
		{
			this._districtCenterRegistry = districtCenterRegistry;
			this._populationDataCollector = populationDataCollector;
			this._populationService = populationService;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007630 File Offset: 0x00005830
		public void Sample()
		{
			this.GlobalPopulationData.CopyFrom(this._populationService.GlobalPopulationData);
			this._oldDistrictPopulationData.AddRange(this._districtPopulationData);
			this._districtPopulationData.Clear();
			ReadOnlyList<DistrictCenter> finishedDistrictCenters = this._districtCenterRegistry.FinishedDistrictCenters;
			for (int i = 0; i < finishedDistrictCenters.Count; i++)
			{
				DistrictCenter districtCenter = finishedDistrictCenters[i];
				PopulationData populationData2;
				PopulationData populationData = this._oldDistrictPopulationData.TryGetValue(districtCenter, out populationData2) ? populationData2 : new PopulationData();
				this._populationDataCollector.CollectData(districtCenter, populationData);
				this._districtPopulationData.Add(districtCenter, populationData);
			}
			this._oldDistrictPopulationData.Clear();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000076D6 File Offset: 0x000058D6
		public PopulationData GetDistrictData(DistrictCenter districtCenter)
		{
			return CollectionExtensions.GetValueOrDefault<DistrictCenter, PopulationData>(this._districtPopulationData, districtCenter, this._emptyPopulationData);
		}

		// Token: 0x04000136 RID: 310
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000137 RID: 311
		public readonly PopulationDataCollector _populationDataCollector;

		// Token: 0x04000138 RID: 312
		public readonly PopulationService _populationService;

		// Token: 0x04000139 RID: 313
		public readonly Dictionary<DistrictCenter, PopulationData> _districtPopulationData = new Dictionary<DistrictCenter, PopulationData>();

		// Token: 0x0400013A RID: 314
		public readonly Dictionary<DistrictCenter, PopulationData> _oldDistrictPopulationData = new Dictionary<DistrictCenter, PopulationData>();

		// Token: 0x0400013B RID: 315
		public readonly PopulationData _emptyPopulationData = new PopulationData();
	}
}
