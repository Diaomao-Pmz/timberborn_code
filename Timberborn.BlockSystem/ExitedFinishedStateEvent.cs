using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200003B RID: 59
	public class ExitedFinishedStateEvent
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000639D File Offset: 0x0000459D
		public BlockObject BlockObject { get; }

		// Token: 0x060001B5 RID: 437 RVA: 0x000063A5 File Offset: 0x000045A5
		public ExitedFinishedStateEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
