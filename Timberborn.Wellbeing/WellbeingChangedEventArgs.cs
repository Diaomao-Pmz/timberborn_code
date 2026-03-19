using System;

namespace Timberborn.Wellbeing
{
	// Token: 0x0200000B RID: 11
	public struct WellbeingChangedEventArgs
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002145 File Offset: 0x00000345
		public WellbeingChangedEventArgs(int oldWellbeing, int newWellbeing)
		{
			this.OldWellbeing = oldWellbeing;
			this.NewWellbeing = newWellbeing;
		}

		// Token: 0x0400000B RID: 11
		public readonly int OldWellbeing;

		// Token: 0x0400000C RID: 12
		public readonly int NewWellbeing;
	}
}
