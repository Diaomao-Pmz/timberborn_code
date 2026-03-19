using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolButtonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000011 RID: 17
	public class BlockObjectToolFinder : IToolFinder
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002E59 File Offset: 0x00001059
		public BlockObjectToolFinder(ToolButtonService toolButtonService)
		{
			this._toolButtonService = toolButtonService;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E68 File Offset: 0x00001068
		public bool TryFindTool(BaseComponent entity, out Action toolActivationAction)
		{
			string templateName = entity.GetComponent<TemplateSpec>().TemplateName;
			BlockObjectTool tool = (from toolButton in this._toolButtonService.ToolButtons
			where toolButton.ToolEnabled
			select toolButton.Tool).OfType<BlockObjectTool>().SingleOrDefault(BlockObjectToolFinder.ToolMatchesTemplate(templateName));
			toolActivationAction = ((tool != null) ? delegate()
			{
				tool.ActivateWithDuplicationSource(entity);
			} : null);
			return tool != null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F23 File Offset: 0x00001123
		public static Func<BlockObjectTool, bool> ToolMatchesTemplate(string templateName)
		{
			return (BlockObjectTool tool) => tool.Template.GetSpec<TemplateSpec>().IsNamedExactly(templateName);
		}

		// Token: 0x04000045 RID: 69
		public readonly ToolButtonService _toolButtonService;
	}
}
