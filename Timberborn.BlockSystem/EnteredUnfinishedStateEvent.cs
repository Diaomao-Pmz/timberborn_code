using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000039 RID: 57
	public class EnteredUnfinishedStateEvent
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000061B8 File Offset: 0x000043B8
		public BlockObject BlockObject { get; }

		// Token: 0x060001A4 RID: 420 RVA: 0x000061C0 File Offset: 0x000043C0
		public EnteredUnfinishedStateEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
