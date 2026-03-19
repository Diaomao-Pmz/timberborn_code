using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.BlockSystem;
using Timberborn.Bots;
using Timberborn.GameDistricts;
using Timberborn.Navigation;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000017 RID: 23
	public class MigrationTrigger : BaseComponent, IAwakableComponent, IInstantNavMeshListener, IFinishedStateListener
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002FF7 File Offset: 0x000011F7
		public MigrationTrigger(MigrationCoordinator migrationCoordinator, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry, PopulationDistributorRetriever populationDistributorRetriever)
		{
			this._migrationCoordinator = migrationCoordinator;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
			this._populationDistributorRetriever = populationDistributorRetriever;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003014 File Offset: 0x00001214
		public void Awake()
		{
			this._adultsDistributor = this._populationDistributorRetriever.GetPopulationDistributor<AdultsDistributorTemplate>(this);
			this._botsDistributor = this._populationDistributorRetriever.GetPopulationDistributor<BotsDistributorTemplate>(this);
			this._childrenDistributor = this._populationDistributorRetriever.GetPopulationDistributor<ChildrenDistributorTemplate>(this);
			this._contaminatedDistributor = this._populationDistributorRetriever.GetPopulationDistributor<ContaminatedDistributorTemplate>(this);
			DistrictPopulation component = base.GetComponent<DistrictPopulation>();
			component.CitizenAssigned += this.OnCitizenAssigned;
			component.CitizenUnassigned += this.OnCitizenUnassigned;
			base.GetComponent<DistrictBeaverContaminationStatisticsProvider>().ContaminationStatisticsChanged += this.OnContaminationStatisticsChanged;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030A9 File Offset: 0x000012A9
		public void OnEnterFinishedState()
		{
			this._navMeshListenerEntityRegistry.RegisterInstantNavMeshListener(this);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030B7 File Offset: 0x000012B7
		public void OnExitFinishedState()
		{
			this._navMeshListenerEntityRegistry.UnregisterInstantNavMeshListener(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030C5 File Offset: 0x000012C5
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.RegisterAllDistributorsToCheck();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030CD File Offset: 0x000012CD
		public void OnCitizenAssigned(object sender, CitizenAssignedEventArgs e)
		{
			this.RegisterDistributorToCheck(e.Citizen);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030DB File Offset: 0x000012DB
		public void OnCitizenUnassigned(object sender, CitizenUnassignedEventArgs e)
		{
			this.RegisterDistributorToCheck(e.Citizen);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000030E9 File Offset: 0x000012E9
		public void OnContaminationStatisticsChanged(object sender, EventArgs e)
		{
			this._migrationCoordinator.RegisterDistributorToCheck(this._childrenDistributor);
			this._migrationCoordinator.RegisterDistributorToCheck(this._adultsDistributor);
			this._migrationCoordinator.RegisterDistributorToCheck(this._contaminatedDistributor);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003120 File Offset: 0x00001320
		public void RegisterDistributorToCheck(Citizen citizen)
		{
			if (citizen.HasComponent<AdultSpec>())
			{
				this._migrationCoordinator.RegisterDistributorToCheck(this._adultsDistributor);
				this._migrationCoordinator.RegisterDistributorToCheck(this._contaminatedDistributor);
				return;
			}
			if (citizen.HasComponent<Child>())
			{
				this._migrationCoordinator.RegisterDistributorToCheck(this._childrenDistributor);
				this._migrationCoordinator.RegisterDistributorToCheck(this._contaminatedDistributor);
				return;
			}
			if (citizen.HasComponent<BotSpec>())
			{
				this._migrationCoordinator.RegisterDistributorToCheck(this._botsDistributor);
				return;
			}
			throw new ArgumentOutOfRangeException(string.Format("Unexpected citizen type: {0}", citizen.GameObject));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031B4 File Offset: 0x000013B4
		public void RegisterAllDistributorsToCheck()
		{
			this._migrationCoordinator.ProcessAutomaticMigration(this._adultsDistributor);
			this._migrationCoordinator.ProcessAutomaticMigration(this._botsDistributor);
			this._migrationCoordinator.ProcessAutomaticMigration(this._childrenDistributor);
			this._migrationCoordinator.ProcessAutomaticMigration(this._contaminatedDistributor);
		}

		// Token: 0x04000034 RID: 52
		public readonly MigrationCoordinator _migrationCoordinator;

		// Token: 0x04000035 RID: 53
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;

		// Token: 0x04000036 RID: 54
		public readonly PopulationDistributorRetriever _populationDistributorRetriever;

		// Token: 0x04000037 RID: 55
		public PopulationDistributor _adultsDistributor;

		// Token: 0x04000038 RID: 56
		public PopulationDistributor _botsDistributor;

		// Token: 0x04000039 RID: 57
		public PopulationDistributor _childrenDistributor;

		// Token: 0x0400003A RID: 58
		public PopulationDistributor _contaminatedDistributor;
	}
}
