using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000009 RID: 9
	public class ChildrenDistributorTemplate : BaseComponent, IAwakableComponent, IDistributorTemplate
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022B3 File Offset: 0x000004B3
		public ChildrenDistributorTemplate(MigrationService migrationService)
		{
			this._migrationService = migrationService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022C2 File Offset: 0x000004C2
		public string ComponentName
		{
			get
			{
				return "ChildrenDistributor";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022CC File Offset: 0x000004CC
		public int Current
		{
			get
			{
				return this._districtCenter.DistrictPopulation.NumberOfChildren - this._districtContaminationStatisticProvider.GetContaminationStatistics().ContaminatedChildren;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022FD File Offset: 0x000004FD
		public void Awake()
		{
			this._districtCenter = base.GetComponent<DistrictCenter>();
			this._districtContaminationStatisticProvider = this._districtCenter.GetComponent<IContaminationStatisticsProvider>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000231C File Offset: 0x0000051C
		public void MigrateTo(DistrictCenter target, int amount)
		{
			IOrderedEnumerable<Beaver> charactersToMove = this._districtCenter.DistrictPopulation.Children.Where(new Func<Beaver, bool>(this._migrationService.IsNotContaminated)).OrderBy(new Func<Beaver, bool>(this._migrationService.HasHome)).ThenByDescending(new Func<Beaver, int>(this._migrationService.GetDayOfBirth));
			this._migrationService.Migrate(charactersToMove, target, amount);
		}

		// Token: 0x0400000D RID: 13
		public readonly MigrationService _migrationService;

		// Token: 0x0400000E RID: 14
		public DistrictCenter _districtCenter;

		// Token: 0x0400000F RID: 15
		public IContaminationStatisticsProvider _districtContaminationStatisticProvider;
	}
}
