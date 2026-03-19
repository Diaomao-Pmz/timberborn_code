using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000007 RID: 7
	public class AdultsDistributorTemplate : BaseComponent, IAwakableComponent, IDistributorTemplate
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AdultsDistributorTemplate(MigrationService migrationService)
		{
			this._migrationService = migrationService;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210F File Offset: 0x0000030F
		public string ComponentName
		{
			get
			{
				return "AdultsDistributor";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002118 File Offset: 0x00000318
		public int Current
		{
			get
			{
				return this._districtCenter.DistrictPopulation.NumberOfAdults - this._districtContaminationStatisticProvider.GetContaminationStatistics().ContaminatedAdults;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002149 File Offset: 0x00000349
		public void Awake()
		{
			this._districtCenter = base.GetComponent<DistrictCenter>();
			this._districtContaminationStatisticProvider = this._districtCenter.GetComponent<IContaminationStatisticsProvider>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002168 File Offset: 0x00000368
		public void MigrateTo(DistrictCenter target, int amount)
		{
			IOrderedEnumerable<Beaver> charactersToMove = this._districtCenter.DistrictPopulation.Adults.Where(new Func<Beaver, bool>(this._migrationService.IsNotContaminated)).OrderBy(new Func<Beaver, bool>(this._migrationService.RefusesWork)).ThenBy(new Func<Beaver, bool>(this._migrationService.IsEmployed)).ThenBy(new Func<Beaver, bool>(this._migrationService.HasHome)).ThenByDescending(new Func<Beaver, int>(this._migrationService.GetDayOfBirth));
			this._migrationService.Migrate(charactersToMove, target, amount);
		}

		// Token: 0x04000008 RID: 8
		public readonly MigrationService _migrationService;

		// Token: 0x04000009 RID: 9
		public DistrictCenter _districtCenter;

		// Token: 0x0400000A RID: 10
		public IContaminationStatisticsProvider _districtContaminationStatisticProvider;
	}
}
