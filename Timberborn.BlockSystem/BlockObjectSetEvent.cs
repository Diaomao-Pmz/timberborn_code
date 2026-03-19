using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000018 RID: 24
	public class BlockObjectSetEvent
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003997 File Offset: 0x00001B97
		public BlockObject BlockObject { get; }

		// Token: 0x060000A8 RID: 168 RVA: 0x0000399F File Offset: 0x00001B9F
		public BlockObjectSetEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
