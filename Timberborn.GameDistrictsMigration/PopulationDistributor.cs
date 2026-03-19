using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000018 RID: 24
	public class PopulationDistributor : BaseComponent, IAwakableComponent, INamedComponent, IPersistentEntity
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003205 File Offset: 0x00001405
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000320D File Offset: 0x0000140D
		public DistrictCenter DistrictCenter { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003216 File Offset: 0x00001416
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000321E File Offset: 0x0000141E
		public int Minimum { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003227 File Offset: 0x00001427
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000322F File Offset: 0x0000142F
		public bool AllowEmigration { get; private set; } = true;

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003238 File Offset: 0x00001438
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003240 File Offset: 0x00001440
		public bool AllowImmigration { get; private set; } = true;

		// Token: 0x06000087 RID: 135 RVA: 0x00003249 File Offset: 0x00001449
		public PopulationDistributor(MigrationCoordinator migrationCoordinator)
		{
			this._migrationCoordinator = migrationCoordinator;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003266 File Offset: 0x00001466
		public string ComponentName
		{
			get
			{
				return this._distributorTemplate.ComponentName;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003273 File Offset: 0x00001473
		public int Need
		{
			get
			{
				return this.Minimum - this.Current;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003282 File Offset: 0x00001482
		public int Spare
		{
			get
			{
				return this.Current - this.Minimum;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003291 File Offset: 0x00001491
		public int Current
		{
			get
			{
				return this._distributorTemplate.Current;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000329E File Offset: 0x0000149E
		public bool CanEmigrate
		{
			get
			{
				return this.AllowEmigration && this.Spare > 0;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000032B3 File Offset: 0x000014B3
		public bool CanImmigrate
		{
			get
			{
				return this.AllowImmigration && this.Need > 0;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032C8 File Offset: 0x000014C8
		public void Awake()
		{
			this.DistrictCenter = base.GetComponent<DistrictCenter>();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032D6 File Offset: 0x000014D6
		public void Initialize(IDistributorTemplate distributorTemplate)
		{
			this._distributorTemplate = distributorTemplate;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032DF File Offset: 0x000014DF
		public void SetMinimumAndMigrate(int minimum)
		{
			minimum = Math.Max(minimum, 0);
			if (minimum != this.Minimum)
			{
				this.Minimum = minimum;
				this._migrationCoordinator.ProcessAutomaticMigration(this);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003306 File Offset: 0x00001506
		public void ToggleAllowEmigrationAndMigrate()
		{
			this.AllowEmigration = !this.AllowEmigration;
			this._migrationCoordinator.ProcessAutomaticMigration(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003323 File Offset: 0x00001523
		public void ToggleAllowImmigrationAndMigrate()
		{
			this.AllowImmigration = !this.AllowImmigration;
			this._migrationCoordinator.ProcessAutomaticMigration(this);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003340 File Offset: 0x00001540
		public PopulationDistributor GetOtherDistrictPopulationDistributor(DistrictCenter districtCenter)
		{
			return districtCenter.GetNamedComponent(this.ComponentName);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003350 File Offset: 0x00001550
		public void MigrateToAndCheckAutomaticMigration(DistrictCenter target, int amount)
		{
			this.MigrateTo(target, amount);
			PopulationDistributor otherDistrictPopulationDistributor = this.GetOtherDistrictPopulationDistributor(target);
			this._migrationCoordinator.ProcessAutomaticMigration(otherDistrictPopulationDistributor);
			this._migrationCoordinator.ProcessAutomaticMigration(this);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003385 File Offset: 0x00001585
		public void MigrateTo(DistrictCenter target, int amount)
		{
			this._distributorTemplate.MigrateTo(target, amount);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003394 File Offset: 0x00001594
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(PopulationDistributor.PopulationDistributorKey, this.ComponentName);
			component.Set(PopulationDistributor.MinimumKey, this.Minimum);
			component.Set(PopulationDistributor.AllowEmigrationKey, this.AllowEmigration);
			component.Set(PopulationDistributor.AllowImmigrationKey, this.AllowImmigration);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033E4 File Offset: 0x000015E4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(PopulationDistributor.PopulationDistributorKey, this.ComponentName);
			this.Minimum = component.Get(PopulationDistributor.MinimumKey);
			this.AllowEmigration = component.Get(PopulationDistributor.AllowEmigrationKey);
			this.AllowImmigration = component.Get(PopulationDistributor.AllowImmigrationKey);
		}

		// Token: 0x0400003B RID: 59
		public static readonly ComponentKey PopulationDistributorKey = new ComponentKey("PopulationDistributor");

		// Token: 0x0400003C RID: 60
		public static readonly PropertyKey<int> MinimumKey = new PropertyKey<int>("Minimum");

		// Token: 0x0400003D RID: 61
		public static readonly PropertyKey<bool> AllowEmigrationKey = new PropertyKey<bool>("AllowEmigration");

		// Token: 0x0400003E RID: 62
		public static readonly PropertyKey<bool> AllowImmigrationKey = new PropertyKey<bool>("AllowImmigration");

		// Token: 0x04000043 RID: 67
		public IDistributorTemplate _distributorTemplate;

		// Token: 0x04000044 RID: 68
		public readonly MigrationCoordinator _migrationCoordinator;
	}
}
