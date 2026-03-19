using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<[Nullable(2)] T>
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x0400004D RID: 77
		internal T Value;
	}
}
