using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000012 RID: 18
	public class MigrationCoordinator : ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002A42 File Offset: 0x00000C42
		public MigrationCoordinator(EventBus eventBus, MigrationNeighbours migrationNeighbours, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._migrationNeighbours = migrationNeighbours;
			this._specService = specService;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A78 File Offset: 0x00000C78
		public void Load()
		{
			MigrationCoordinatorSpec singleSpec = this._specService.GetSingleSpec<MigrationCoordinatorSpec>();
			this._maxAutomaticMigration = singleSpec.MaxAutomaticMigration;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A9D File Offset: 0x00000C9D
		public void RegisterDistributorToCheck(PopulationDistributor populationDistributor)
		{
			this._populationDistributorsToCheck.Add(populationDistributor);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AAC File Offset: 0x00000CAC
		public void Tick()
		{
			this.CheckMigrationTriggers();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void ProcessAutomaticMigration(PopulationDistributor populationDistributor)
		{
			this._migrating = false;
			this.ProcessAutomaticMigrationInternal(populationDistributor);
			this.FinalizeMigrationProcess();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ACA File Offset: 0x00000CCA
		public void CheckMigrationTriggers()
		{
			if (this._populationDistributorsToCheck.Count > 0)
			{
				this.RunMigrationTriggers();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public void RunMigrationTriggers()
		{
			this._populationDistributorsToCheck.CopyTo(this._populationDistributorsProcessed);
			this._populationDistributorsToCheck.Clear();
			for (int i = 0; i < this._populationDistributorsProcessed.Count; i++)
			{
				PopulationDistributor populationDistributor = this._populationDistributorsProcessed[i];
				if (populationDistributor)
				{
					this.ProcessAutomaticMigration(populationDistributor);
				}
			}
			this._populationDistributorsProcessed.Clear();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B46 File Offset: 0x00000D46
		public void ProcessAutomaticMigrationInternal(PopulationDistributor populationDistributor)
		{
			if (populationDistributor.CanImmigrate)
			{
				this.ProcessAutomaticImmigration(populationDistributor);
				return;
			}
			if (populationDistributor.CanEmigrate)
			{
				this.ProcessAutomaticEmigration(populationDistributor);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B68 File Offset: 0x00000D68
		public void ProcessAutomaticImmigration(PopulationDistributor populationDistributor)
		{
			int num = 0;
			while (num < this._maxAutomaticMigration && populationDistributor.Need > 0)
			{
				this.AutomaticImmigration(populationDistributor);
				num++;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B98 File Offset: 0x00000D98
		public void ProcessAutomaticEmigration(PopulationDistributor populationDistributor)
		{
			int num = 0;
			while (num < this._maxAutomaticMigration && populationDistributor.Spare > 0)
			{
				this.AutomaticEmigration(populationDistributor);
				num++;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public void AutomaticImmigration(PopulationDistributor populationDistributor)
		{
			PopulationDistributor highestSpareNeighbour = this._migrationNeighbours.GetHighestSpareNeighbour(populationDistributor);
			if (highestSpareNeighbour)
			{
				this.Migrate(highestSpareNeighbour, populationDistributor, 1);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void AutomaticEmigration(PopulationDistributor populationDistributor)
		{
			PopulationDistributor lowestSpareNeighbour = this._migrationNeighbours.GetLowestSpareNeighbour(populationDistributor);
			if (lowestSpareNeighbour)
			{
				this.Migrate(populationDistributor, lowestSpareNeighbour, 1);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C1F File Offset: 0x00000E1F
		public void Migrate(PopulationDistributor from, PopulationDistributor to, int amount)
		{
			if (amount > 0)
			{
				this._migrating = true;
				from.MigrateTo(to.DistrictCenter, amount);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C39 File Offset: 0x00000E39
		public void FinalizeMigrationProcess()
		{
			if (this._migrating)
			{
				this._eventBus.Post(new MigrationEvent());
				this._migrating = false;
				this.CheckMigrationTriggers();
			}
		}

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public readonly MigrationNeighbours _migrationNeighbours;

		// Token: 0x0400002A RID: 42
		public readonly ISpecService _specService;

		// Token: 0x0400002B RID: 43
		public readonly HashSet<PopulationDistributor> _populationDistributorsToCheck = new HashSet<PopulationDistributor>();

		// Token: 0x0400002C RID: 44
		public readonly List<PopulationDistributor> _populationDistributorsProcessed = new List<PopulationDistributor>();

		// Token: 0x0400002D RID: 45
		public int _maxAutomaticMigration;

		// Token: 0x0400002E RID: 46
		public bool _migrating;
	}
}
