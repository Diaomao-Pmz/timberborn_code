using System;

namespace Timberborn.ToolSystem
{
	// Token: 0x02000016 RID: 22
	public class ToolLockedEvent
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002681 File Offset: 0x00000881
		public ITool Tool { get; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002689 File Offset: 0x00000889
		public ToolLockedEvent(ITool tool)
		{
			this.Tool = tool;
		}
	}
}
