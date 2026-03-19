using System;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000008 RID: 8
	public class AccumulateScienceForBuildingStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021CA File Offset: 0x000003CA
		public AccumulateScienceForBuildingStepDeserializer(BuildingUnlockingService buildingUnlockingService, BuildingService buildingService, ScienceService scienceService, ILoc loc)
		{
			this._buildingUnlockingService = buildingUnlockingService;
			this._buildingService = buildingService;
			this._scienceService = scienceService;
			this._loc = loc;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F0 File Offset: 0x000003F0
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			AccumulateScienceForBuildingStepSpec accumulateScienceForBuildingStepSpec = step.Specs[0] as AccumulateScienceForBuildingStepSpec;
			if (accumulateScienceForBuildingStepSpec != null)
			{
				tutorialStep = this.Create(accumulateScienceForBuildingStepSpec.TemplateName);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
		public TutorialStep Create(string templateName)
		{
			BuildingSpec buildingTemplate = this._buildingService.GetBuildingTemplate(templateName);
			return TutorialStep.Create(new AccumulateScienceForBuildingStep(this._scienceService, this._buildingUnlockingService, this._loc, buildingTemplate), null, null, null);
		}

		// Token: 0x0400000E RID: 14
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x0400000F RID: 15
		public readonly BuildingService _buildingService;

		// Token: 0x04000010 RID: 16
		public readonly ScienceService _scienceService;

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;
	}
}
