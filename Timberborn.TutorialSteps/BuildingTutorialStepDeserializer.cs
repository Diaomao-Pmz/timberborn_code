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
	// Token: 0x0200000E RID: 14
	public class BuildingTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000025E3 File Offset: 0x000007E3
		public BuildingTutorialStepDeserializer(BuiltBuildingService builtBuildingService, BuildingService buildingService, ILoc loc, ToolButtonService toolButtonService)
		{
			this._builtBuildingService = builtBuildingService;
			this._buildingService = buildingService;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002608 File Offset: 0x00000808
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			BuildingTutorialStepSpec buildingTutorialStepSpec = step.Specs[0] as BuildingTutorialStepSpec;
			if (buildingTutorialStepSpec != null)
			{
				tutorialStep = (buildingTutorialStepSpec.OnlyFinishedBuildings ? this.Create(buildingTutorialStepSpec.TemplateNames, buildingTutorialStepSpec.RequiredAmount, true, "Tutorial.Building") : this.Create(buildingTutorialStepSpec.TemplateNames, buildingTutorialStepSpec.RequiredAmount, false, "Tutorial.PlaceBuilding"));
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000267C File Offset: 0x0000087C
		public TutorialStep Create(IEnumerable<string> templateNames, int requiredAmount, bool onlyFinishedBuildings, string mainLocKey)
		{
			List<string> list = templateNames.ToList<string>();
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(list.First<string>()).GetSpec<LabeledEntitySpec>();
			string localizedBuildingName = this._loc.T(spec.DisplayNameLocKey);
			ImmutableArray<ToolButton> immutableArray = this.GetToolButtons(list).ToImmutableArray<ToolButton>();
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(immutableArray.First<ToolButton>());
			return TutorialStep.Create(new BuildingTutorialStep(this._builtBuildingService, this._loc, list, onlyFinishedBuildings, requiredAmount, mainLocKey, localizedBuildingName), toolGroupButton, immutableArray, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026FE File Offset: 0x000008FE
		public IEnumerable<ToolButton> GetToolButtons(IEnumerable<string> templateNames)
		{
			using (IEnumerator<string> enumerator = templateNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string templateName = enumerator.Current;
					yield return this._toolButtonService.GetToolButton<BlockObjectTool>((BlockObjectTool tool) => tool.Template.GetSpec<TemplateSpec>().IsNamedExactly(templateName));
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400001F RID: 31
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x04000020 RID: 32
		public readonly BuildingService _buildingService;

		// Token: 0x04000021 RID: 33
		public readonly ILoc _loc;

		// Token: 0x04000022 RID: 34
		public readonly ToolButtonService _toolButtonService;
	}
}
