using System;

namespace Timberborn.Demolishing
{
	// Token: 0x02000016 RID: 22
	public class DemolishableUnmarkedEvent
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000030EA File Offset: 0x000012EA
		public Demolishable Demolishable { get; }

		// Token: 0x0600009B RID: 155 RVA: 0x000030F2 File Offset: 0x000012F2
		public DemolishableUnmarkedEvent(Demolishable demolishable)
		{
			this.Demolishable = demolishable;
		}
	}
}
