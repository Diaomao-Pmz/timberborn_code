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
	// Token: 0x02000023 RID: 35
	public class ConnectBuildingsTutorialStepDeserializer : IStepDeserializer
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000041F8 File Offset: 0x000023F8
		public ConnectBuildingsTutorialStepDeserializer(BuiltBuildingService builtBuildingService, BuildingService buildingService, ILoc loc, ToolButtonService toolButtonService)
		{
			this._builtBuildingService = builtBuildingService;
			this._buildingService = buildingService;
			this._loc = loc;
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004220 File Offset: 0x00002420
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			ConnectBuildingsTutorialStepSpec connectBuildingsTutorialStepSpec = step.Specs[0] as ConnectBuildingsTutorialStepSpec;
			if (connectBuildingsTutorialStepSpec != null)
			{
				tutorialStep = this.Create(connectBuildingsTutorialStepSpec.TemplateName, connectBuildingsTutorialStepSpec.RequiredAmount, connectBuildingsTutorialStepSpec.CountUnfinishedBuildings, connectBuildingsTutorialStepSpec.HighlightableBuildingIds);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000426C File Offset: 0x0000246C
		public TutorialStep Create(string templateName, int requiredAmount, bool countUnfinishedBuildings, ImmutableArray<string> highlightableBuildingIds)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string localizedBuildingName = this._loc.T(spec.DisplayNameLocKey);
			ImmutableArray<ToolButton> immutableArray = this.GetToolButtons(highlightableBuildingIds).ToImmutableArray<ToolButton>();
			ToolGroupButton toolGroupButton = this._toolButtonService.GetToolGroupButton(immutableArray.First<ToolButton>());
			return TutorialStep.Create(new ConnectBuildingsTutorialStep(this._builtBuildingService, this._loc, templateName, requiredAmount, localizedBuildingName, countUnfinishedBuildings), toolGroupButton, immutableArray, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000042DF File Offset: 0x000024DF
		public IEnumerable<ToolButton> GetToolButtons(ImmutableArray<string> templateNames)
		{
			ImmutableArray<string>.Enumerator enumerator = templateNames.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string templateName = enumerator.Current;
				yield return this._toolButtonService.GetToolButton<BlockObjectTool>((BlockObjectTool tool) => tool.Template.GetSpec<TemplateSpec>().IsNamed(templateName));
			}
			enumerator = default(ImmutableArray<string>.Enumerator);
			yield break;
		}

		// Token: 0x04000074 RID: 116
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x04000075 RID: 117
		public readonly BuildingService _buildingService;

		// Token: 0x04000076 RID: 118
		public readonly ILoc _loc;

		// Token: 0x04000077 RID: 119
		public readonly ToolButtonService _toolButtonService;
	}
}
