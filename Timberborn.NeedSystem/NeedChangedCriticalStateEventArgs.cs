using System;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000006 RID: 6
	public class NeedChangedCriticalStateEventArgs
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000249B File Offset: 0x0000069B
		public NeedSpec NeedSpec { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024A3 File Offset: 0x000006A3
		public bool IsInCriticalState { get; }

		// Token: 0x06000027 RID: 39 RVA: 0x000024AB File Offset: 0x000006AB
		public NeedChangedCriticalStateEventArgs(NeedSpec needSpec, bool isInCriticalState)
		{
			this.NeedSpec = needSpec;
			this.IsInCriticalState = isInCriticalState;
		}
	}
}
