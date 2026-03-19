using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	internal interface IStrongEnumerator<[Nullable(2)] T>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000126 RID: 294
		T Current { get; }

		// Token: 0x06000127 RID: 295
		bool MoveNext();
	}
}
