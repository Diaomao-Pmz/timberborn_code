using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000038 RID: 56
	public class EnteredFinishedStateEvent
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000061A1 File Offset: 0x000043A1
		public BlockObject BlockObject { get; }

		// Token: 0x060001A2 RID: 418 RVA: 0x000061A9 File Offset: 0x000043A9
		public EnteredFinishedStateEvent(BlockObject blockObject)
		{
			this.BlockObject = blockObject;
		}
	}
}
