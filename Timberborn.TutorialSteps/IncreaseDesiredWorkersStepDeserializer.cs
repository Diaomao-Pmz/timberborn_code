using System;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002F RID: 47
	public class IncreaseDesiredWorkersStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public IncreaseDesiredWorkersStepDeserializer(ILoc loc, EntitySelectionService entitySelectionService, BuildingService buildingService)
		{
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
			this._buildingService = buildingService;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			IncreaseDesiredWorkersStepSpec increaseDesiredWorkersStepSpec = step.Specs[0] as IncreaseDesiredWorkersStepSpec;
			if (increaseDesiredWorkersStepSpec != null)
			{
				tutorialStep = this.Create(increaseDesiredWorkersStepSpec.TemplateName);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E1C File Offset: 0x0000301C
		public TutorialStep Create(string templateName)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string description = this._loc.T<string>("Tutorial.IncreaseDesiredWorkers", this._loc.T(spec.DisplayNameLocKey));
			return TutorialStep.Create(new IncreaseDesiredWorkersStep(this._entitySelectionService, description, templateName), null, null, null);
		}

		// Token: 0x0400009C RID: 156
		public readonly ILoc _loc;

		// Token: 0x0400009D RID: 157
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400009E RID: 158
		public readonly BuildingService _buildingService;
	}
}
