using System;
using Timberborn.ReservableSystem;

namespace Timberborn.Yielding
{
	// Token: 0x0200000A RID: 10
	public interface IRemoveYieldStrategy
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002B RID: 43
		string Id { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44
		ReservableReacher Reacher { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002D RID: 45
		bool IsStillRemovable { get; }
	}
}
