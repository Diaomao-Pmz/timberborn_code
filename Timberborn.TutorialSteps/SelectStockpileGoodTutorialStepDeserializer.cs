using System;
using System.Collections.Generic;
using Timberborn.BlueprintSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.StockpilesUI;
using Timberborn.TemplateSystem;
using Timberborn.TutorialSystem;
using UnityEngine;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000049 RID: 73
	public class SelectStockpileGoodTutorialStepDeserializer : IStepDeserializer, ILoadableSingleton
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00006180 File Offset: 0x00004380
		public SelectStockpileGoodTutorialStepDeserializer(BuiltBuildingService builtBuildingService, BuildingService buildingService, ILoc loc, IGoodService goodService, StockpileInventoryFragment stockpileInventoryFragment, EntitySelectionService entitySelectionService, Highlighter highlighter, ISpecService specService)
		{
			this._builtBuildingService = builtBuildingService;
			this._buildingService = buildingService;
			this._loc = loc;
			this._goodService = goodService;
			this._stockpileInventoryFragment = stockpileInventoryFragment;
			this._entitySelectionService = entitySelectionService;
			this._highlighter = highlighter;
			this._specService = specService;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000061D0 File Offset: 0x000043D0
		public void Load()
		{
			this._tutorialBuildingHighlight = this._specService.GetSingleSpec<TutorialColorsSpec>().TutorialBuildingHighlight;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000061E8 File Offset: 0x000043E8
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			SelectStockpileGoodTutorialStepSpec selectStockpileGoodTutorialStepSpec = step.Specs[0] as SelectStockpileGoodTutorialStepSpec;
			if (selectStockpileGoodTutorialStepSpec != null)
			{
				tutorialStep = this.Create(selectStockpileGoodTutorialStepSpec.TemplateName, selectStockpileGoodTutorialStepSpec.RequiredAmount, selectStockpileGoodTutorialStepSpec.GoodId);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006230 File Offset: 0x00004430
		public TutorialStep Create(string templateName, int requiredAmount, string goodId)
		{
			LabeledEntitySpec spec = this._buildingService.GetBuildingTemplate(templateName).GetSpec<LabeledEntitySpec>();
			string localizedBuildingName = this._loc.T(spec.DisplayNameLocKey);
			GoodSpec good = this._goodService.GetGood(goodId);
			return TutorialStep.Create(new SelectStockpileGoodTutorialStep(this._builtBuildingService, this._loc, templateName, good, requiredAmount, "Tutorial.SelectGood", localizedBuildingName), delegate(bool state)
			{
				this.Highlight(state, templateName, goodId);
			}, null, null);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000062C8 File Offset: 0x000044C8
		public void Highlight(bool highlight, string templateName, string goodId)
		{
			SelectableObject selectedObject = this._entitySelectionService.SelectedObject;
			if (selectedObject)
			{
				TemplateSpec component = selectedObject.GetComponent<TemplateSpec>();
				if (component != null && component.IsNamedExactly(templateName))
				{
					this._stockpileInventoryFragment.ToggleButtonHighlight(highlight);
					this._highlighter.UnhighlightAllPrimary();
					return;
				}
			}
			this.HighlightBuilding(highlight, templateName, goodId);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006320 File Offset: 0x00004520
		public void HighlightBuilding(bool highlight, string templateName, string goodId)
		{
			if (highlight)
			{
				IReadOnlyList<Building> finishedBuildings = this._builtBuildingService.GetFinishedBuildings(templateName);
				for (int i = 0; i < finishedBuildings.Count; i++)
				{
					SingleGoodAllower component = finishedBuildings[i].GetComponent<SingleGoodAllower>();
					if (component.AllowedGood != goodId)
					{
						this._highlighter.HighlightPrimary(component, this._tutorialBuildingHighlight);
					}
				}
				return;
			}
			this._highlighter.UnhighlightAllPrimary();
		}

		// Token: 0x040000F1 RID: 241
		public readonly BuiltBuildingService _builtBuildingService;

		// Token: 0x040000F2 RID: 242
		public readonly BuildingService _buildingService;

		// Token: 0x040000F3 RID: 243
		public readonly ILoc _loc;

		// Token: 0x040000F4 RID: 244
		public readonly IGoodService _goodService;

		// Token: 0x040000F5 RID: 245
		public readonly StockpileInventoryFragment _stockpileInventoryFragment;

		// Token: 0x040000F6 RID: 246
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x040000F7 RID: 247
		public readonly Highlighter _highlighter;

		// Token: 0x040000F8 RID: 248
		public readonly ISpecService _specService;

		// Token: 0x040000F9 RID: 249
		public Color _tutorialBuildingHighlight;
	}
}
