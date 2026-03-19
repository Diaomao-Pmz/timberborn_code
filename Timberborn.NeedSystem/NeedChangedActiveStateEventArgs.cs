using System;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000005 RID: 5
	public struct NeedChangedActiveStateEventArgs
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000247B File Offset: 0x0000067B
		public readonly NeedSpec NeedSpec { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002483 File Offset: 0x00000683
		public readonly bool IsActive { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000248B File Offset: 0x0000068B
		public NeedChangedActiveStateEventArgs(NeedSpec needSpec, bool isActive)
		{
			this.NeedSpec = needSpec;
			this.IsActive = isActive;
		}
	}
}
