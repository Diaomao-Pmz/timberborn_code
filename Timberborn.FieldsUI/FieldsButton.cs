using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BottomBarSystem;
using Timberborn.Fields;
using Timberborn.NaturalResources;
using Timberborn.Planting;
using Timberborn.PlantingUI;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.FieldsUI
{
	// Token: 0x02000009 RID: 9
	public class FieldsButton : IBottomBarElementsProvider
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000238C File Offset: 0x0000058C
		public FieldsButton(ToolGroupButtonFactory toolGroupButtonFactory, TemplateService templateService, PlantingToolButtonFactory plantingToolButtonFactory, ToolGroupService toolGroupService)
		{
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._templateService = templateService;
			this._plantingToolButtonFactory = plantingToolButtonFactory;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023B1 File Offset: 0x000005B1
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolGroupSpec group = this._toolGroupService.GetGroup(FieldsButton.ToolGroupId);
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateBlue(group);
			foreach (PlantableSpec plantableSpec in from template in (from plantable in this._templateService.GetAll<PlantableSpec>()
			where plantable.GetSpec<NaturalResourceSpec>().UsableWithCurrentFeatureToggles
			orderby plantable.GetSpec<NaturalResourceSpec>().Order
			select plantable).ToList<PlantableSpec>()
			where template.HasSpec<CropSpec>()
			select template)
			{
				ITool tool = this.CreateTool(plantableSpec, toolGroupButton);
				this._toolGroupService.AssignToGroup(group, tool);
			}
			ToolButton toolButton = this._plantingToolButtonFactory.CreateCancelTool(toolGroupButton.ToolButtonsElement);
			toolGroupButton.AddTool(toolButton);
			this._toolGroupService.AssignToGroup(group, toolButton.Tool);
			yield return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
			yield break;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023C4 File Offset: 0x000005C4
		public ITool CreateTool(PlantableSpec plantableSpec, ToolGroupButton toolGroupButton)
		{
			ToolButton toolButton = this._plantingToolButtonFactory.CreatePlantingTool(plantableSpec, toolGroupButton.ToolButtonsElement);
			toolGroupButton.AddTool(toolButton);
			return toolButton.Tool;
		}

		// Token: 0x04000018 RID: 24
		public static readonly string ToolGroupId = "Fields";

		// Token: 0x04000019 RID: 25
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x0400001A RID: 26
		public readonly TemplateService _templateService;

		// Token: 0x0400001B RID: 27
		public readonly PlantingToolButtonFactory _plantingToolButtonFactory;

		// Token: 0x0400001C RID: 28
		public readonly ToolGroupService _toolGroupService;
	}
}
