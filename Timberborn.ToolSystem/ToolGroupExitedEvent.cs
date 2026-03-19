using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000013 RID: 19
	public class ToolGroupExitedEvent
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000216B File Offset: 0x0000036B
		public ToolGroupSpec ToolGroup { get; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002173 File Offset: 0x00000373
		public ToolGroupExitedEvent(ToolGroupSpec toolGroup)
		{
			this.ToolGroup = toolGroup;
		}
	}
}
