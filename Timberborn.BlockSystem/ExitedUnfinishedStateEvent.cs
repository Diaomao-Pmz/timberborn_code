using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200003C RID: 60
	public class ExitedUnfinishedStateEvent
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000063B4 File Offset: 0x000045B4
		public BlockObject BlockObject { get; }

		// Token: 0x060001B7 RID: 439 RVA: 0x000063BC File Offset: 0x000045BC
		public ExitedUnfinishedStateEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
