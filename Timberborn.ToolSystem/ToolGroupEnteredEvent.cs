using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000012 RID: 18
	public class ToolGroupEnteredEvent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002154 File Offset: 0x00000354
		public ToolGroupSpec ToolGroup { get; }

		// Token: 0x06000018 RID: 24 RVA: 0x0000215C File Offset: 0x0000035C
		public ToolGroupEnteredEvent(ToolGroupSpec toolGroup)
		{
			this.ToolGroup = toolGroup;
		}
	}
}
