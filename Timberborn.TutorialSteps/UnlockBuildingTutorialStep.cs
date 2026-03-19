using System;
using Timberborn.Buildings;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000058 RID: 88
	public class UnlockBuildingTutorialStep : ITutorialStep
	{
		// Token: 0x06000257 RID: 599 RVA: 0x000070A4 File Offset: 0x000052A4
		public UnlockBuildingTutorialStep(BuildingUnlockingService buildingUnlockingService, ILoc loc, BuildingSpec buildingSpec, string localizedBuildingName)
		{
			this._buildingUnlockingService = buildingUnlockingService;
			this._loc = loc;
			this._buildingSpec = buildingSpec;
			this._localizedBuildingName = localizedBuildingName;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000070C9 File Offset: 0x000052C9
		public string Description()
		{
			return this._loc.T<string>(UnlockBuildingTutorialStep.UnlockLocKey, this._localizedBuildingName);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000070E1 File Offset: 0x000052E1
		public bool Achieved()
		{
			return this._buildingUnlockingService.Unlocked(this._buildingSpec);
		}

		// Token: 0x04000125 RID: 293
		public static readonly string UnlockLocKey = "Tutorial.Science.Unlock";

		// Token: 0x04000126 RID: 294
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x04000127 RID: 295
		public readonly ILoc _loc;

		// Token: 0x04000128 RID: 296
		public readonly BuildingSpec _buildingSpec;

		// Token: 0x04000129 RID: 297
		public readonly string _localizedBuildingName;
	}
}
