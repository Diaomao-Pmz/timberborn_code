using System;
using Timberborn.BlueprintSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.Planting;
using Timberborn.PlantingUI;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000032 RID: 50
	public class MarkPlantablesTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000502E File Offset: 0x0000322E
		public MarkPlantablesTutorialStepDeserializer(PlantableResourceCounter plantableResourceCounter, ILoc loc, ToolButtonService toolButtonService)
		{
			this._plantableResourceCounter = plantableResourceCounter;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000504C File Offset: 0x0000324C
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			MarkPlantablesTutorialStepSpec markPlantablesTutorialStepSpec = step.Specs[0] as MarkPlantablesTutorialStepSpec;
			if (markPlantablesTutorialStepSpec != null)
			{
				tutorialStep = this.Create(markPlantablesTutorialStepSpec.TemplateName, markPlantablesTutorialStepSpec.RequiredAmount);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000508C File Offset: 0x0000328C
		public TutorialStep Create(string templateName, int requiredAmount)
		{
			ToolButton toolButton = this._toolButtonService.GetToolButton<PlantingTool>((PlantingTool plantingTool) => plantingTool.PlantableSpec.GetSpec<TemplateSpec>().IsNamedExactly(templateName));
			PlantableSpec plantableSpec = ((PlantingTool)toolButton.Tool).PlantableSpec;
			string localizedResourceName = this._loc.T(plantableSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey);
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(toolButton);
			return TutorialStep.Create(new MarkPlantablesTutorialStep(this._plantableResourceCounter, this._loc, templateName, requiredAmount, localizedResourceName), toolGroupButton, toolButton, null);
		}

		// Token: 0x040000A6 RID: 166
		public readonly PlantableResourceCounter _plantableResourceCounter;

		// Token: 0x040000A7 RID: 167
		public readonly ILoc _loc;

		// Token: 0x040000A8 RID: 168
		public readonly ToolButtonService _toolButtonService;
	}
}
