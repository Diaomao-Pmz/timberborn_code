using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000008 RID: 8
	public class DistrictDwellingStatisticsProvider : BaseComponent, IAwakableComponent, IDwellingStatisticsProvider
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022B2 File Offset: 0x000004B2
		public void Awake()
		{
			DistrictBuildingRegistry component = base.GetComponent<DistrictBuildingRegistry>();
			component.FinishedBuildingRegistered += this.OnFinishedBuildingRegistered;
			component.FinishedBuildingUnregistered += this.OnFinishedBuildingUnregistered;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022DD File Offset: 0x000004DD
		public DwellingStatistics GetDwellingStatistics()
		{
			return this._districtDwellingStatistics;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E8 File Offset: 0x000004E8
		public void OnFinishedBuildingRegistered(object sender, FinishedBuildingRegisteredEventArgs finishedBuildingRegisteredEventArgs)
		{
			DwellerCounter component = finishedBuildingRegisteredEventArgs.Building.GetComponent<DwellerCounter>();
			if (component != null)
			{
				this.AddDwellingBedCounter(component);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000230C File Offset: 0x0000050C
		public void OnFinishedBuildingUnregistered(object sender, FinishedBuildingUnregisteredEventArgs finishedBuildingUnregisteredEventArgs)
		{
			DwellerCounter component = finishedBuildingUnregisteredEventArgs.Building.GetComponent<DwellerCounter>();
			if (component != null)
			{
				this.RemoveDwellingBedCounter(component);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000232F File Offset: 0x0000052F
		public void AddDwellingBedCounter(DwellerCounter dwellerCounter)
		{
			this._districtDwellingStatistics += dwellerCounter.GetCurrentDwellingStatistics();
			dwellerCounter.DwellerCountChanged += this.OnDwellerCountChanged;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000235A File Offset: 0x0000055A
		public void RemoveDwellingBedCounter(DwellerCounter dwellerCounter)
		{
			this._districtDwellingStatistics -= dwellerCounter.GetCurrentDwellingStatistics();
			dwellerCounter.DwellerCountChanged -= this.OnDwellerCountChanged;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002385 File Offset: 0x00000585
		public void OnDwellerCountChanged(object sender, DwellingChangedEventArgs dwellingChangedEventArgs)
		{
			this._districtDwellingStatistics -= dwellingChangedEventArgs.OldDwellingStatistics;
			this._districtDwellingStatistics += dwellingChangedEventArgs.NewDwellingStatistics;
		}

		// Token: 0x0400000B RID: 11
		public DwellingStatistics _districtDwellingStatistics = new DwellingStatistics(0, 0);
	}
}
