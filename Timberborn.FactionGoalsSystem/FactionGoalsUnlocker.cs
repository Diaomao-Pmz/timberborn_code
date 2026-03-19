using System;
using Timberborn.FactionSystem;
using Timberborn.GameFactionSystem;
using Timberborn.TickSystem;
using Timberborn.Wellbeing;

namespace Timberborn.FactionGoalsSystem
{
	// Token: 0x02000005 RID: 5
	public class FactionGoalsUnlocker : ITickableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public FactionGoalsUnlocker(FactionUnlockingService factionUnlockingService, WellbeingService wellbeingService, FactionService factionService, FactionSpecService factionSpecService)
		{
			this._factionUnlockingService = factionUnlockingService;
			this._wellbeingService = wellbeingService;
			this._factionService = factionService;
			this._factionSpecService = factionSpecService;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		public void Tick()
		{
			foreach (FactionSpec factionSpec in this._factionSpecService.Factions)
			{
				if (this._factionUnlockingService.IsLocked(factionSpec) && this.UnlockConditionsAreSatisfied(factionSpec))
				{
					this._factionUnlockingService.UnlockFaction(factionSpec);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public bool UnlockConditionsAreSatisfied(FactionSpec factionSpec)
		{
			UnlockableFactionSpec spec = factionSpec.GetSpec<UnlockableFactionSpec>();
			return this._factionService.Current.Id == spec.PrerequisiteFaction && this._wellbeingService.AverageGlobalWellbeing >= spec.AverageWellbeingToUnlock;
		}

		// Token: 0x04000006 RID: 6
		public readonly FactionUnlockingService _factionUnlockingService;

		// Token: 0x04000007 RID: 7
		public readonly WellbeingService _wellbeingService;

		// Token: 0x04000008 RID: 8
		public readonly FactionService _factionService;

		// Token: 0x04000009 RID: 9
		public readonly FactionSpecService _factionSpecService;
	}
}
