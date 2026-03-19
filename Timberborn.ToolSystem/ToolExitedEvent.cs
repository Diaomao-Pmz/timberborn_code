using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000011 RID: 17
	public class ToolExitedEvent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000213D File Offset: 0x0000033D
		public ITool Tool { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002145 File Offset: 0x00000345
		public ToolExitedEvent(ITool tool)
		{
			this.Tool = tool;
		}
	}
}
