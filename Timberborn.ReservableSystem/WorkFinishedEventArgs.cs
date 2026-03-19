using System;

namespace Timberborn.ReservableSystem
{
	// Token: 0x02000009 RID: 9
	public struct WorkFinishedEventArgs
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025A6 File Offset: 0x000007A6
		public readonly bool WasCompleted { get; }

		// Token: 0x06000033 RID: 51 RVA: 0x000025AE File Offset: 0x000007AE
		public WorkFinishedEventArgs(bool wasCompleted)
		{
			this.WasCompleted = wasCompleted;
		}
	}
}
