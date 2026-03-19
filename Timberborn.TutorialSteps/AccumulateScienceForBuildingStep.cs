using System;
using Timberborn.Buildings;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000007 RID: 7
	public class AccumulateScienceForBuildingStep : ITutorialStep
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AccumulateScienceForBuildingStep(ScienceService scienceService, BuildingUnlockingService buildingUnlockingService, ILoc loc, BuildingSpec buildingSpec)
		{
			this._scienceService = scienceService;
			this._buildingUnlockingService = buildingUnlockingService;
			this._loc = loc;
			this._buildingSpec = buildingSpec;
			this._requiredPoints = this._buildingSpec.ScienceCost;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public string Description()
		{
			int param = (this._buildingUnlockingService.Unlocked(this._buildingSpec) || this._scienceService.SciencePoints > this._requiredPoints) ? this._requiredPoints : this._scienceService.SciencePoints;
			return this._loc.T<int, int>(AccumulateScienceForBuildingStep.AccumulateScienceLocKey, param, this._requiredPoints);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002196 File Offset: 0x00000396
		public bool Achieved()
		{
			return this._scienceService.SciencePoints >= this._requiredPoints || this._buildingUnlockingService.Unlocked(this._buildingSpec);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string AccumulateScienceLocKey = "Tutorial.Science.AccumulateScience";

		// Token: 0x04000009 RID: 9
		public readonly ScienceService _scienceService;

		// Token: 0x0400000A RID: 10
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x0400000B RID: 11
		public readonly ILoc _loc;

		// Token: 0x0400000C RID: 12
		public readonly BuildingSpec _buildingSpec;

		// Token: 0x0400000D RID: 13
		public readonly int _requiredPoints;
	}
}
