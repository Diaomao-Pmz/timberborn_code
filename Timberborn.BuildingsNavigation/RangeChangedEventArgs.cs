using System;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000023 RID: 35
	public class RangeChangedEventArgs
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004654 File Offset: 0x00002854
		public bool IsInitialChange { get; }

		// Token: 0x060000E6 RID: 230 RVA: 0x0000465C File Offset: 0x0000285C
		public RangeChangedEventArgs(bool isInitialChange)
		{
			this.IsInitialChange = isInitialChange;
		}
	}
}
