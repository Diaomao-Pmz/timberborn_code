using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000020 RID: 32
	public class PlantingToolFinder : IToolFinder
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003D39 File Offset: 0x00001F39
		public PlantingToolFinder(ToolButtonService toolButtonService, ToolService toolService)
		{
			this._toolButtonService = toolButtonService;
			this._toolService = toolService;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003D50 File Offset: 0x00001F50
		public bool TryFindTool(BaseComponent entity, out Action toolActivationAction)
		{
			string templateName = entity.GetComponent<TemplateSpec>().TemplateName;
			PlantingTool tool = (from toolButton in this._toolButtonService.ToolButtons
			where toolButton.ToolEnabled
			select toolButton.Tool).OfType<PlantingTool>().SingleOrDefault(PlantingToolFinder.ToolMatchesPlantableSpecName(templateName));
			toolActivationAction = ((tool != null) ? delegate()
			{
				this._toolService.SwitchTool(tool);
			} : null);
			return tool != null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003E06 File Offset: 0x00002006
		public static Func<PlantingTool, bool> ToolMatchesPlantableSpecName(string templateName)
		{
			return (PlantingTool tool) => tool.PlantableSpec.TemplateName.Equals(templateName);
		}

		// Token: 0x04000078 RID: 120
		public readonly ToolButtonService _toolButtonService;

		// Token: 0x04000079 RID: 121
		public readonly ToolService _toolService;
	}
}
