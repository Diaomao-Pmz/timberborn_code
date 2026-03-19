using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.LifeSystem
{
	// Token: 0x02000009 RID: 9
	public class LifeService : ILoadableSingleton
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000222D File Offset: 0x0000042D
		public LifeService(IDayNightCycle dayNightCycle, ISpecService specService)
		{
			this._dayNightCycle = dayNightCycle;
			this._specService = specService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002243 File Offset: 0x00000443
		public int AverageLifespan
		{
			get
			{
				return this._averageLifespan;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000224C File Offset: 0x0000044C
		public void Load()
		{
			LifeServiceSpec singleSpec = this._specService.GetSingleSpec<LifeServiceSpec>();
			this._averageLifespan = singleSpec.AverageLifespan;
			this._daysOfChildhood = singleSpec.DaysOfChildhood;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000227D File Offset: 0x0000047D
		public float ChildhoodProgressToLifeProgress(float childhoodProgress)
		{
			return childhoodProgress * (float)this._daysOfChildhood / (float)this._averageLifespan;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
		public float AdulthoodProgressToLifeProgress(float adulthoodProgress)
		{
			return (adulthoodProgress * (float)this.DaysOfAdulthood + (float)this._daysOfChildhood) / (float)this._averageLifespan;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022AB File Offset: 0x000004AB
		public int CalculateDayOfBirth(float lifeProgress)
		{
			return this._dayNightCycle.DayNumber - Mathf.RoundToInt(lifeProgress * (float)this._averageLifespan);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022C8 File Offset: 0x000004C8
		public float CalculateGrowthProgress(float deltaTimeInHours)
		{
			int num = this._daysOfChildhood * 24;
			return deltaTimeInHours / (float)num;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022E3 File Offset: 0x000004E3
		public int DaysOfAdulthood
		{
			get
			{
				return this._averageLifespan - this._daysOfChildhood;
			}
		}

		// Token: 0x04000011 RID: 17
		public int _averageLifespan;

		// Token: 0x04000012 RID: 18
		public int _daysOfChildhood;

		// Token: 0x04000013 RID: 19
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000014 RID: 20
		public readonly ISpecService _specService;
	}
}
