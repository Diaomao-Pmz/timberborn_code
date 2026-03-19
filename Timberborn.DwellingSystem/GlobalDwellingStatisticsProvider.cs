using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000011 RID: 17
	public class GlobalDwellingStatisticsProvider : ILoadableSingleton, IDwellingStatisticsProvider
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003240 File Offset: 0x00001440
		public GlobalDwellingStatisticsProvider(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000325A File Offset: 0x0000145A
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003268 File Offset: 0x00001468
		public DwellingStatistics GetDwellingStatistics()
		{
			DwellingStatistics dwellingStatistics = new DwellingStatistics(0, 0);
			foreach (DistrictDwellingStatisticsProvider districtDwellingStatisticsProvider in this._districtDwellingStatisticsProviders)
			{
				dwellingStatistics += districtDwellingStatisticsProvider.GetDwellingStatistics();
			}
			return dwellingStatistics;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000032CC File Offset: 0x000014CC
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			DistrictDwellingStatisticsProvider component = enteredFinishedStateEvent.BlockObject.GetComponent<DistrictDwellingStatisticsProvider>();
			if (component != null)
			{
				this._districtDwellingStatisticsProviders.Add(component);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032F4 File Offset: 0x000014F4
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictDwellingStatisticsProvider component = exitedFinishedStateEvent.BlockObject.GetComponent<DistrictDwellingStatisticsProvider>();
			if (component != null)
			{
				this._districtDwellingStatisticsProviders.Remove(component);
			}
		}

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public readonly List<DistrictDwellingStatisticsProvider> _districtDwellingStatisticsProviders = new List<DistrictDwellingStatisticsProvider>();
	}
}
