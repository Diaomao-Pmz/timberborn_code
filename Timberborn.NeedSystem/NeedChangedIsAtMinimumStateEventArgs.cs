using System;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000007 RID: 7
	public class NeedChangedIsAtMinimumStateEventArgs
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024C1 File Offset: 0x000006C1
		public NeedSpec NeedSpec { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024C9 File Offset: 0x000006C9
		public bool IsAtMinimum { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x000024D1 File Offset: 0x000006D1
		public NeedChangedIsAtMinimumStateEventArgs(NeedSpec needSpec, bool isAtMinimum)
		{
			this.NeedSpec = needSpec;
			this.IsAtMinimum = isAtMinimum;
		}
	}
}
