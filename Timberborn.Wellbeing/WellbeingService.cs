using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.NeedSpecs;
using Timberborn.NeedSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.Wellbeing
{
	// Token: 0x02000010 RID: 16
	public class WellbeingService : ITickableSingleton, ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023B3 File Offset: 0x000005B3
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023BB File Offset: 0x000005BB
		public int AverageGlobalWellbeing { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023C4 File Offset: 0x000005C4
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023CC File Offset: 0x000005CC
		public int AverageDistrictWellbeing { get; private set; }

		// Token: 0x06000025 RID: 37 RVA: 0x000023D5 File Offset: 0x000005D5
		public WellbeingService(EntityComponentRegistry entityComponentRegistry, EventBus eventBus, GlobalWellbeingTrackerRegistry globalWellbeingTrackerRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
			this._eventBus = eventBus;
			this._globalWellbeingTrackerRegistry = globalWellbeingTrackerRegistry;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F2 File Offset: 0x000005F2
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002400 File Offset: 0x00000600
		public void PostLoad()
		{
			this.UpdateAverageGlobalWellbeing();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002408 File Offset: 0x00000608
		public void Tick()
		{
			this.UpdateAverageGlobalWellbeing();
			this.UpdateAverageDistrictWellbeing();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002416 File Offset: 0x00000616
		[OnEvent]
		public void OnMigration(MigrationEvent migrationEvent)
		{
			this.UpdateAverageDistrictWellbeing();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002400 File Offset: 0x00000600
		[OnEvent]
		public void OnNewGameInitialized(NewGameInitializedEvent newGameInitializedEvent)
		{
			this.UpdateAverageGlobalWellbeing();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000241E File Offset: 0x0000061E
		public void SwitchDistrict(DistrictCenter districtCenter)
		{
			this._districtCenter = districtCenter;
			this.UpdateAverageDistrictWellbeing();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000242D File Offset: 0x0000062D
		public void GlobalAppliedNeeds(Dictionary<string, int> appliedNeeds)
		{
			WellbeingService.AppliedNeeds(this._entityComponentRegistry.GetEnabled<NeedManager>(), appliedNeeds);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002440 File Offset: 0x00000640
		public void DistrictAppliedNeeds(Dictionary<string, int> appliedNeeds)
		{
			WellbeingService.AppliedNeeds(this._districtCenter.DistrictPopulation.GetEnabledCharacters<NeedManager>(), appliedNeeds);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002458 File Offset: 0x00000658
		public static void AppliedNeeds(IEnumerable<NeedManager> needManagers, IDictionary<string, int> appliedNeeds)
		{
			foreach (NeedManager needManager in needManagers)
			{
				if (needManager.HasComponent<WellbeingTrackerRegistrar>())
				{
					foreach (NeedSpec needSpec in needManager.NeedSpecs)
					{
						string id = needSpec.Id;
						if (WellbeingService.NeedShouldBeCounted(needManager, id))
						{
							int orAdd = appliedNeeds.GetOrAdd(id);
							appliedNeeds[id] = orAdd + 1;
						}
					}
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024EC File Offset: 0x000006EC
		public void UpdateAverageGlobalWellbeing()
		{
			this.AverageGlobalWellbeing = this._globalWellbeingTrackerRegistry.Registry.GetAverageWellbeing();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002504 File Offset: 0x00000704
		public void UpdateAverageDistrictWellbeing()
		{
			if (this._districtCenter)
			{
				this.AverageDistrictWellbeing = this._districtCenter.GetComponent<DistrictWellbeingTrackerRegistry>().Registry.GetAverageWellbeing();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000252E File Offset: 0x0000072E
		public static bool NeedShouldBeCounted(NeedManager needManager, string needId)
		{
			if (!needManager.GetNeedSpec(needId).IsNeverPositive)
			{
				return needManager.NeedIsFavorable(needId);
			}
			return !needManager.NeedIsFavorable(needId);
		}

		// Token: 0x0400001B RID: 27
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400001C RID: 28
		public readonly EventBus _eventBus;

		// Token: 0x0400001D RID: 29
		public readonly GlobalWellbeingTrackerRegistry _globalWellbeingTrackerRegistry;

		// Token: 0x0400001E RID: 30
		public int _averageDistrictWellbeing;

		// Token: 0x0400001F RID: 31
		public DistrictCenter _districtCenter;
	}
}
