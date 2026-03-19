using System;
using Timberborn.BaseComponentSystem;

namespace Timberborn.ToolSystem
{
	// Token: 0x0200000C RID: 12
	public interface IToolFinder
	{
		// Token: 0x0600000C RID: 12
		bool TryFindTool(BaseComponent entity, out Action toolActivationAction);
	}
}
