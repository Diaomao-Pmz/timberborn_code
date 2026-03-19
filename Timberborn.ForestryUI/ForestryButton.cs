using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BottomBarSystem;
using Timberborn.Forestry;
using Timberborn.NaturalResources;
using Timberborn.Planting;
using Timberborn.PlantingUI;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.ForestryUI
{
	// Token: 0x0200000A RID: 10
	public class ForestryButton : IBottomBarElementsProvider
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000022AF File Offset: 0x000004AF
		public ForestryButton(ToolGroupButtonFactory toolGroupButtonFactory, TemplateService templateService, PlantingToolButtonFactory plantingToolButtonFactory, ToolGroupService toolGroupService)
		{
			this._toolGroupButtonFactory = toolGroupButtonFactory;
			this._templateService = templateService;
			this._plantingToolButtonFactory = plantingToolButtonFactory;
			this._toolGroupService = toolGroupService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022D4 File Offset: 0x000004D4
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolGroupSpec group = this._toolGroupService.GetGroup(ForestryButton.ToolGroupId);
			ToolGroupButton toolGroupButton = this._toolGroupButtonFactory.CreateBlue(group);
			List<PlantableSpec> source = (from plantable in this._templateService.GetAll<PlantableSpec>()
			where plantable.GetSpec<NaturalResourceSpec>().UsableWithCurrentFeatureToggles
			orderby plantable.GetSpec<NaturalResourceSpec>().Order
			select plantable).ToList<PlantableSpec>();
			IEnumerable<PlantableSpec> first = from template in source
			where template.HasSpec<BushSpec>()
			select template;
			IEnumerable<PlantableSpec> second = from template in source
			where template.HasSpec<TreeComponentSpec>()
			select template;
			foreach (PlantableSpec plantableSpec in first.Concat(second))
			{
				ITool tool = this.CreateTool(plantableSpec, toolGroupButton);
				this._toolGroupService.AssignToGroup(group, tool);
			}
			ToolButton toolButton = this._plantingToolButtonFactory.CreateCancelTool(toolGroupButton.ToolButtonsElement);
			this._toolGroupService.AssignToGroup(group, toolButton.Tool);
			toolGroupButton.AddTool(toolButton);
			yield return BottomBarElement.CreateMultiLevel(toolGroupButton.Root, toolGroupButton.ToolButtonsElement);
			yield break;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022E4 File Offset: 0x000004E4
		public ITool CreateTool(PlantableSpec plantableSpec, ToolGroupButton toolGroupButton)
		{
			ToolButton toolButton = this._plantingToolButtonFactory.CreatePlantingTool(plantableSpec, toolGroupButton.ToolButtonsElement);
			toolGroupButton.AddTool(toolButton);
			return toolButton.Tool;
		}

		// Token: 0x04000010 RID: 16
		public static readonly string ToolGroupId = "Forestry";

		// Token: 0x04000011 RID: 17
		public readonly ToolGroupButtonFactory _toolGroupButtonFactory;

		// Token: 0x04000012 RID: 18
		public readonly TemplateService _templateService;

		// Token: 0x04000013 RID: 19
		public readonly PlantingToolButtonFactory _plantingToolButtonFactory;

		// Token: 0x04000014 RID: 20
		public readonly ToolGroupService _toolGroupService;
	}
}
