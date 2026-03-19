using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000010 RID: 16
	public class ToolEnteredEvent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002117 File Offset: 0x00000317
		public ITool Tool { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000211F File Offset: 0x0000031F
		public bool ShouldCloseGroup { get; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002127 File Offset: 0x00000327
		public ToolEnteredEvent(ITool tool, bool shouldCloseGroup)
		{
			this.Tool = tool;
			this.ShouldCloseGroup = shouldCloseGroup;
		}
	}
}
