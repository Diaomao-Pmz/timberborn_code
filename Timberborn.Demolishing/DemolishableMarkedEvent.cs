using System;

namespace Timberborn.Demolishing
{
	// Token: 0x0200000A RID: 10
	public class DemolishableMarkedEvent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025D8 File Offset: 0x000007D8
		public Demolishable Demolishable { get; }

		// Token: 0x0600002E RID: 46 RVA: 0x000025E0 File Offset: 0x000007E0
		public DemolishableMarkedEvent(Demolishable demolishable)
		{
			this.Demolishable = demolishable;
		}
	}
}
