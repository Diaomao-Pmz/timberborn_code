using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x0200000E RID: 14
	public class TemporaryToolEnteredEvent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002100 File Offset: 0x00000300
		public ITool Tool { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002108 File Offset: 0x00000308
		public TemporaryToolEnteredEvent(ITool tool)
		{
			this.Tool = tool;
		}
	}
}
