using System;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.Bots;
using Timberborn.Common;
using Timberborn.DwellingSystem;
using Timberborn.GameDistricts;
using Timberborn.Navigation;
using Timberborn.PopulationWorkStatistics;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.Population
{
	// Token: 0x0200000A RID: 10
	public class PopulationService : ITickableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002608 File Offset: 0x00000808
		public PopulationData GlobalPopulationData { get; } = new PopulationData();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002610 File Offset: 0x00000810
		public PopulationData DistrictPopulationData { get; } = new PopulationData();

		// Token: 0x0600003A RID: 58 RVA: 0x00002618 File Offset: 0x00000818
		public PopulationService(EventBus eventBus, BeaverPopulation beaverPopulation, BotPopulation botPopulation, GlobalDwellingStatisticsProvider globalDwellingStatisticsProvider, GlobalEmploymentStatisticsProvider globalEmploymentStatisticsProvider, GlobalWorkRefusingStatisticsProvider globalWorkRefusingStatisticsProvider, GlobalBeaverContaminationStatisticsProvider globalBeaverContaminationStatisticsProvider, PopulationDataCollector populationDataCollector, [Ordering] INavigationPhase navigationPhase)
		{
			this._eventBus = eventBus;
			this._beaverPopulation = beaverPopulation;
			this._botPopulation = botPopulation;
			this._globalDwellingStatisticsProvider = globalDwellingStatisticsProvider;
			this._globalEmploymentStatisticsProvider = globalEmploymentStatisticsProvider;
			this._globalWorkRefusingStatisticsProvider = globalWorkRefusingStatisticsProvider;
			this._globalBeaverContaminationStatisticsProvider = globalBeaverContaminationStatisticsProvider;
			this._populationDataCollector = populationDataCollector;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000267E File Offset: 0x0000087E
		public bool BotCreated
		{
			get
			{
				return this._botPopulation.BotCreated;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000268C File Offset: 0x0000088C
		public bool IsAnyoneContaminated
		{
			get
			{
				return this.GlobalPopulationData.ContaminationData.ContaminatedAdults > 0 || this.GlobalPopulationData.ContaminationData.ContaminatedChildren > 0;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000026C7 File Offset: 0x000008C7
		public bool OnlyBotsAlive
		{
			get
			{
				return this._beaverPopulation.NumberOfBeavers == 0 && this._botPopulation.NumberOfBots > 0;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000026E6 File Offset: 0x000008E6
		public bool AllDead
		{
			get
			{
				return this._beaverPopulation.NumberOfBeavers == 0 && this._botPopulation.NumberOfBots == 0;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002705 File Offset: 0x00000905
		public void Tick()
		{
			this.UpdateData(false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000270E File Offset: 0x0000090E
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000271C File Offset: 0x0000091C
		public void PostLoad()
		{
			this.UpdateData(true);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002725 File Offset: 0x00000925
		public void SwitchDistrict(DistrictCenter districtCenter)
		{
			this._districtCenter = districtCenter;
			this.UpdateData(true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002705 File Offset: 0x00000905
		[OnEvent]
		public void OnMigration(MigrationEvent migrationEvent)
		{
			this.UpdateData(false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002705 File Offset: 0x00000905
		[OnEvent]
		public void OnNewGameInitialized(NewGameInitializedEvent newGameInitializedEvent)
		{
			this.UpdateData(false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002738 File Offset: 0x00000938
		public void UpdateData(bool forceEvent)
		{
			bool flag = this.UpdateGlobalData();
			bool flag2 = this.UpdateDistrictData();
			if (flag || flag2 || forceEvent)
			{
				this._eventBus.Post(new PopulationChangedEvent());
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002768 File Offset: 0x00000968
		public bool UpdateGlobalData()
		{
			return this._populationDataCollector.CollectData(this._beaverPopulation.NumberOfAdults, this._beaverPopulation.NumberOfChildren, this._botPopulation.NumberOfBots, this._globalWorkRefusingStatisticsProvider, this._globalDwellingStatisticsProvider, this._globalEmploymentStatisticsProvider, this._globalBeaverContaminationStatisticsProvider, this.GlobalPopulationData);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027BF File Offset: 0x000009BF
		public bool UpdateDistrictData()
		{
			return this._districtCenter && this._populationDataCollector.CollectData(this._districtCenter, this.DistrictPopulationData);
		}

		// Token: 0x04000016 RID: 22
		public readonly EventBus _eventBus;

		// Token: 0x04000017 RID: 23
		public readonly BeaverPopulation _beaverPopulation;

		// Token: 0x04000018 RID: 24
		public readonly BotPopulation _botPopulation;

		// Token: 0x04000019 RID: 25
		public readonly GlobalDwellingStatisticsProvider _globalDwellingStatisticsProvider;

		// Token: 0x0400001A RID: 26
		public readonly GlobalEmploymentStatisticsProvider _globalEmploymentStatisticsProvider;

		// Token: 0x0400001B RID: 27
		public readonly GlobalWorkRefusingStatisticsProvider _globalWorkRefusingStatisticsProvider;

		// Token: 0x0400001C RID: 28
		public readonly GlobalBeaverContaminationStatisticsProvider _globalBeaverContaminationStatisticsProvider;

		// Token: 0x0400001D RID: 29
		public readonly PopulationDataCollector _populationDataCollector;

		// Token: 0x0400001E RID: 30
		public DistrictCenter _districtCenter;
	}
}
