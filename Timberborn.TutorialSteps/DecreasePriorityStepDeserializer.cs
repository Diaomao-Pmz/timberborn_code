using System;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000028 RID: 40
	public class DecreasePriorityStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0000475B File Offset: 0x0000295B
		public DecreasePriorityStepDeserializer(ILoc loc, EntitySelectionService entitySelectionService, BuildingService buildingService)
		{
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
			this._buildingService = buildingService;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004778 File Offset: 0x00002978
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			DecreasePriorityStepSpec decreasePriorityStepSpec = step.Specs[0] as DecreasePriorityStepSpec;
			if (decreasePriorityStepSpec != null)
			{
				tutorialStep = this.Create(decreasePriorityStepSpec.TemplateName);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000047B4 File Offset: 0x000029B4
		public TutorialStep Create(string templateName)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string description = this._loc.T<string>("Tutorial.DecreaseWorkplacePriority", this._loc.T(spec.DisplayNameLocKey));
			return TutorialStep.Create(new DecreasePriorityStep(this._entitySelectionService, description, templateName), null, null, null);
		}

		// Token: 0x04000088 RID: 136
		public readonly ILoc _loc;

		// Token: 0x04000089 RID: 137
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0400008A RID: 138
		public readonly BuildingService _buildingService;
	}
}
