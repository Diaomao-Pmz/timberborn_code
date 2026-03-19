using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000019 RID: 25
	public class ToolUnlockedEvent
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002860 File Offset: 0x00000A60
		public ITool Tool { get; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002868 File Offset: 0x00000A68
		public ToolUnlockedEvent(ITool tool)
		{
			this.Tool = tool;
		}
	}
}
