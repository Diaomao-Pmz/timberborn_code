using System;
using System.Linq;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.Planting;
using Timberborn.SelectionToolSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PlantingUI
{
	// Token: 0x0200001E RID: 30
	public class PlantingToolButtonFactory
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public PlantingToolButtonFactory(ToolButtonFactory toolButtonFactory, PlantableDescriber plantableDescriber, PlantingSelectionService plantingSelectionService, DevModePlantableSpawner devModePlantableSpawner, ToolUnlockingService toolUnlockingService, SelectionToolProcessorFactory selectionToolProcessorFactory, ILoc loc, TemplateService templateService)
		{
			this._toolButtonFactory = toolButtonFactory;
			this._plantableDescriber = plantableDescriber;
			this._plantingSelectionService = plantingSelectionService;
			this._devModePlantableSpawner = devModePlantableSpawner;
			this._toolUnlockingService = toolUnlockingService;
			this._selectionToolProcessorFactory = selectionToolProcessorFactory;
			this._loc = loc;
			this._templateService = templateService;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C38 File Offset: 0x00001E38
		public ToolButton CreatePlantingTool(PlantableSpec plantableSpec, VisualElement buttonParent)
		{
			PlantingTool tool = new PlantingTool(this._plantableDescriber, this._plantingSelectionService, this._devModePlantableSpawner, this._toolUnlockingService, this._selectionToolProcessorFactory, plantableSpec, this.GetPlanterBuildingName(plantableSpec));
			Sprite asset = plantableSpec.GetSpec<LabeledEntitySpec>().Icon.Asset;
			return this._toolButtonFactory.Create(tool, asset, buttonParent);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C90 File Offset: 0x00001E90
		public ToolButton CreateCancelTool(VisualElement buttonParent)
		{
			CancelPlantingTool tool = new CancelPlantingTool(this._plantingSelectionService, this._loc, this._selectionToolProcessorFactory);
			return this._toolButtonFactory.Create(tool, PlantingToolButtonFactory.CancelPlantingImageKey, buttonParent);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public string GetPlanterBuildingName(PlantableSpec plantableSpec)
		{
			string displayNameLocKey = this._templateService.GetAll<PlanterBuildingSpec>().Single((PlanterBuildingSpec building) => building.PlantableResourceGroup == plantableSpec.ResourceGroup).GetSpec<LabeledEntitySpec>().DisplayNameLocKey;
			return this._loc.T(displayNameLocKey);
		}

		// Token: 0x0400006E RID: 110
		public static readonly string CancelPlantingImageKey = "CancelToolIcon";

		// Token: 0x0400006F RID: 111
		public readonly ToolButtonFactory _toolButtonFactory;

		// Token: 0x04000070 RID: 112
		public readonly PlantableDescriber _plantableDescriber;

		// Token: 0x04000071 RID: 113
		public readonly PlantingSelectionService _plantingSelectionService;

		// Token: 0x04000072 RID: 114
		public readonly DevModePlantableSpawner _devModePlantableSpawner;

		// Token: 0x04000073 RID: 115
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x04000074 RID: 116
		public readonly SelectionToolProcessorFactory _selectionToolProcessorFactory;

		// Token: 0x04000075 RID: 117
		public readonly ILoc _loc;

		// Token: 0x04000076 RID: 118
		public readonly TemplateService _templateService;
	}
}
