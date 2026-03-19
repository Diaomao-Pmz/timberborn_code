using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlockObjectTools;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000040 RID: 64
	public class PowerBuildingsTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00005923 File Offset: 0x00003B23
		public PowerBuildingsTutorialStepDeserializer(BuiltBuildingService builtBuildingService, BuildingService buildingService, ILoc loc, ToolButtonService toolButtonService)
		{
			this._builtBuildingService = builtBuildingService;
			this._buildingService = buildingService;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005948 File Offset: 0x00003B48
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			PowerBuildingsTutorialStepSpec powerBuildingsTutorialStepSpec = step.Specs[0] as PowerBuildingsTutorialStepSpec;
			if (powerBuildingsTutorialStepSpec != null)
			{
				tutorialStep = this.Create(powerBuildingsTutorialStepSpec.TemplateName, powerBuildingsTutorialStepSpec.RequiredAmount);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005988 File Offset: 0x00003B88
		public TutorialStep Create(string templateName, int requiredAmount)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string localizedBuildingName = this._loc.T(spec.DisplayNameLocKey);
			ImmutableArray<ToolButton> immutableArray = this.GetToolButtons(new string[]
			{
				"PowerShaft.Folktails",
				"VerticalPowerShaft.Folktails"
			}).ToImmutableArray<ToolButton>();
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(immutableArray.First<ToolButton>());
			return TutorialStep.Create(new PowerBuildingsTutorialStep(this._builtBuildingService, this._loc, templateName, requiredAmount, localizedBuildingName), toolGroupButton, immutableArray, null);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005A0E File Offset: 0x00003C0E
		public IEnumerable<ToolButton> GetToolButtons(params string[] templateNames)
		{
			string[] array = templateNames;
			for (int i = 0; i < array.Length; i++)
			{
				string templateName = array[i];
				yield return this._toolButtonService.GetToolButton<BlockObjectTool>((BlockObjectTool tool) => tool.Template.GetSpec<TemplateSpec>().IsNamed(templateName));
			}
			array = null;
			yield break;
		}

		// Token: 0x040000CF RID: 207
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x040000D0 RID: 208
		public readonly BuildingService _buildingService;

		// Token: 0x040000D1 RID: 209
		public readonly ILoc _loc;

		// Token: 0x040000D2 RID: 210
		public readonly ToolButtonService _toolButtonService;
	}
}
