using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000008 RID: 8
	public class BotsDistributorTemplate : BaseComponent, IAwakableComponent, IDistributorTemplate
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002207 File Offset: 0x00000407
		public BotsDistributorTemplate(MigrationService migrationService)
		{
			this._migrationService = migrationService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002216 File Offset: 0x00000416
		public string ComponentName
		{
			get
			{
				return "BotsDistributor";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000221D File Offset: 0x0000041D
		public int Current
		{
			get
			{
				return this._districtCenter.DistrictPopulation.NumberOfBots;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000222F File Offset: 0x0000042F
		public void Awake()
		{
			this._districtCenter = base.GetComponent<DistrictCenter>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002240 File Offset: 0x00000440
		public void MigrateTo(DistrictCenter target, int amount)
		{
			IOrderedEnumerable<Bot> charactersToMove = this._districtCenter.DistrictPopulation.Bots.OrderBy(new Func<Bot, bool>(this._migrationService.RefusesWork)).ThenBy(new Func<Bot, bool>(this._migrationService.IsEmployed)).ThenByDescending(new Func<Bot, int>(this._migrationService.GetDayOfBirth));
			this._migrationService.Migrate(charactersToMove, target, amount);
		}

		// Token: 0x0400000B RID: 11
		public readonly MigrationService _migrationService;

		// Token: 0x0400000C RID: 12
		public DistrictCenter _districtCenter;
	}
}
