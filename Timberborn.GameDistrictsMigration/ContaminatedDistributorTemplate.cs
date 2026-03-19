using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x0200000A RID: 10
	public class ContaminatedDistributorTemplate : BaseComponent, IAwakableComponent, IDistributorTemplate
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000238F File Offset: 0x0000058F
		public ContaminatedDistributorTemplate(MigrationService migrationService)
		{
			this._migrationService = migrationService;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000239E File Offset: 0x0000059E
		public string ComponentName
		{
			get
			{
				return "ContaminatedDistributor";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023A8 File Offset: 0x000005A8
		public int Current
		{
			get
			{
				return this._districtContaminationStatisticProvider.GetContaminationStatistics().Total;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C8 File Offset: 0x000005C8
		public void Awake()
		{
			this._districtCenter = base.GetComponent<DistrictCenter>();
			this._districtContaminationStatisticProvider = this._districtCenter.GetComponent<IContaminationStatisticsProvider>();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023E8 File Offset: 0x000005E8
		public void MigrateTo(DistrictCenter target, int amount)
		{
			IOrderedEnumerable<Beaver> charactersToMove = this._districtCenter.DistrictPopulation.Beavers.Where(new Func<Beaver, bool>(this._migrationService.IsContaminated)).OrderBy(new Func<Beaver, int>(this._migrationService.GetDayOfBirth));
			this._migrationService.Migrate(charactersToMove, target, amount);
		}

		// Token: 0x04000010 RID: 16
		public readonly MigrationService _migrationService;

		// Token: 0x04000011 RID: 17
		public DistrictCenter _districtCenter;

		// Token: 0x04000012 RID: 18
		public IContaminationStatisticsProvider _districtContaminationStatisticProvider;
	}
}
