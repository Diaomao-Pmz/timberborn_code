using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.BuildingsUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;
using UnityEngine;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200001E RID: 30
	public class ChangePausedStateStepDeserializer : IStepDeserializer, ILoadableSingleton
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003D10 File Offset: 0x00001F10
		public ChangePausedStateStepDeserializer(BuildingService buildingService, BuiltBuildingService builtBuildingService, ILoc loc, EntitySelectionService entitySelectionService, PausableBuildingFragment pausableBuildingFragment, Highlighter highlighter, ISpecService specService)
		{
			this._buildingService = buildingService;
			this._builtBuildingService = builtBuildingService;
			this._loc = loc;
			this._entitySelectionService = entitySelectionService;
			this._pausableBuildingFragment = pausableBuildingFragment;
			this._highlighter = highlighter;
			this._specService = specService;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D4D File Offset: 0x00001F4D
		public void Load()
		{
			this._tutorialBuildingHighlight = this._specService.GetSingleSpec<TutorialColorsSpec>().TutorialBuildingHighlight;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D68 File Offset: 0x00001F68
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			ChangePausedStateStepSpec changePausedStateStepSpec = step.Specs[0] as ChangePausedStateStepSpec;
			if (changePausedStateStepSpec != null)
			{
				tutorialStep = this.Create(changePausedStateStepSpec.ShouldBePaused, changePausedStateStepSpec.TemplateName);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public TutorialStep Create(bool shouldBePaused, string templateName)
		{
			return TutorialStep.Create(new ChangePausedStateStep(this._entitySelectionService, this.GetDescription(shouldBePaused, templateName), shouldBePaused, templateName), delegate(bool state)
			{
				this.Highlight(state, templateName);
			}, "ToggleBuildingPause", null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003E00 File Offset: 0x00002000
		public string GetDescription(bool shouldBePaused, string templateName)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string key = shouldBePaused ? "Tutorial.PauseBuilding" : "Tutorial.UnpauseBuilding";
			return this._loc.T<string>(key, this._loc.T(spec.DisplayNameLocKey));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003E4C File Offset: 0x0000204C
		public void Highlight(bool highlight, string templateName)
		{
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				TemplateSpec component = selectedObject.GetComponent<TemplateSpec>();
				if (component != null && component.IsNamedExactly(templateName))
				{
					this._pausableBuildingFragment.ToggleHighlight(highlight);
					this._highlighter.UnhighlightAllPrimary();
					return;
				}
			}
			this.HighlightBuilding(highlight, templateName);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003EA0 File Offset: 0x000020A0
		public void HighlightBuilding(bool highlight, string templateName)
		{
			if (highlight)
			{
				IReadOnlyList<Building> finishedBuildings = this._builtBuildingService.GetFinishedBuildings(templateName);
				for (int i = 0; i < finishedBuildings.Count; i++)
				{
					this._highlighter.HighlightPrimary(finishedBuildings[i], this._tutorialBuildingHighlight);
				}
				return;
			}
			this._highlighter.UnhighlightAllPrimary();
		}

		// Token: 0x0400005E RID: 94
		public readonly BuildingService _buildingService;

		// Token: 0x0400005F RID: 95
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x04000060 RID: 96
		public readonly ILoc _loc;

		// Token: 0x04000061 RID: 97
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000062 RID: 98
		public readonly PausableBuildingFragment _pausableBuildingFragment;

		// Token: 0x04000063 RID: 99
		public readonly Highlighter _highlighter;

		// Token: 0x04000064 RID: 100
		public readonly ISpecService _specService;

		// Token: 0x04000065 RID: 101
		public Color _tutorialBuildingHighlight;
	}
}
