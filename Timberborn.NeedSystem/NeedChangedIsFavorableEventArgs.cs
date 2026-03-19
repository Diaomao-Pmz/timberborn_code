using System;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000008 RID: 8
	public class NeedChangedIsFavorableEventArgs
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024E7 File Offset: 0x000006E7
		public NeedSpec NeedSpec { get; }

		// Token: 0x0600002C RID: 44 RVA: 0x000024EF File Offset: 0x000006EF
		public NeedChangedIsFavorableEventArgs(NeedSpec needSpec)
		{
			this.NeedSpec = needSpec;
		}
	}
}
