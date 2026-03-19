using System;
using Timberborn.BlockObjectTools;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.BuildingTools;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.ScienceSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000059 RID: 89
	public class UnlockBuildingTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00007100 File Offset: 0x00005300
		public UnlockBuildingTutorialStepDeserializer(BuildingUnlockingService buildingUnlockingService, BuildingService buildingService, ILoc loc, ToolButtonService toolButtonService, UnlockSectionController unlockSectionController)
		{
			this._buildingUnlockingService = buildingUnlockingService;
			this._buildingService = buildingService;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
			this._unlockSectionController = unlockSectionController;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007130 File Offset: 0x00005330
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			UnlockBuildingTutorialStepSpec unlockBuildingTutorialStepSpec = step.Specs[0] as UnlockBuildingTutorialStepSpec;
			if (unlockBuildingTutorialStepSpec != null)
			{
				tutorialStep = this.Create(unlockBuildingTutorialStepSpec.TemplateName);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000716C File Offset: 0x0000536C
		public TutorialStep Create(string templateName)
		{
			BuildingSpec buildingTemplate = this._buildingService.GetBuildingTemplate(templateName);
			LabeledEntitySpec spec = buildingTemplate.GetSpec<LabeledEntitySpec>();
			string localizedBuildingName = this._loc.T(spec.DisplayNameLocKey);
			ToolButton toolButton = this._toolButtonService.GetToolButton<BlockObjectTool>((BlockObjectTool tool) => tool.Template.GetSpec<TemplateSpec>().IsNamedExactly(templateName));
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(toolButton);
			return TutorialStep.Create(new UnlockBuildingTutorialStep(this._buildingUnlockingService, this._loc, buildingTemplate.GetSpec<BuildingSpec>(), localizedBuildingName), toolGroupButton, toolButton, delegate(bool state)
			{
				this.Highlight(state, toolButton.Tool);
			});
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007219 File Offset: 0x00005419
		public void Highlight(bool state, ITool tool)
		{
			this._unlockSectionController.ToggleHighlight(state, tool);
		}

		// Token: 0x0400012A RID: 298
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x0400012B RID: 299
		public readonly BuildingService _buildingService;

		// Token: 0x0400012C RID: 300
		public readonly ILoc _loc;

		// Token: 0x0400012D RID: 301
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x0400012E RID: 302
		public readonly UnlockSectionController _unlockSectionController;
	}
}
