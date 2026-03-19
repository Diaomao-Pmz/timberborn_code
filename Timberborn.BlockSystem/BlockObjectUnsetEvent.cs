using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200001F RID: 31
	public class BlockObjectUnsetEvent
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000045C9 File Offset: 0x000027C9
		public BlockObject BlockObject { get; }

		// Token: 0x060000F3 RID: 243 RVA: 0x000045D1 File Offset: 0x000027D1
		public BlockObjectUnsetEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
