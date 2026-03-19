using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000006 RID: 6
	public class GlobalEmploymentStatisticsProvider : ILoadableSingleton, IEmploymentStatisticsProvider
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002337 File Offset: 0x00000537
		public GlobalEmploymentStatisticsProvider(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002351 File Offset: 0x00000551
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002360 File Offset: 0x00000560
		public EmploymentStatistics GetEmploymentStatistics(string workerType)
		{
			EmploymentStatistics employmentStatistics = new EmploymentStatistics(0, 0, workerType);
			foreach (DistrictEmploymentStatisticsProvider districtEmploymentStatisticsProvider in this._districtEmploymentStatisticsProviders)
			{
				employmentStatistics += districtEmploymentStatisticsProvider.GetEmploymentStatistics(workerType);
			}
			return employmentStatistics;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023C8 File Offset: 0x000005C8
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			DistrictEmploymentStatisticsProvider component = enteredFinishedStateEvent.BlockObject.GetComponent<DistrictEmploymentStatisticsProvider>();
			if (component != null)
			{
				this._districtEmploymentStatisticsProviders.Add(component);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F0 File Offset: 0x000005F0
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedStateEvent)
		{
			DistrictEmploymentStatisticsProvider component = exitedFinishedStateEvent.BlockObject.GetComponent<DistrictEmploymentStatisticsProvider>();
			if (component != null)
			{
				this._districtEmploymentStatisticsProviders.Remove(component);
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly List<DistrictEmploymentStatisticsProvider> _districtEmploymentStatisticsProviders = new List<DistrictEmploymentStatisticsProvider>();
	}
}
