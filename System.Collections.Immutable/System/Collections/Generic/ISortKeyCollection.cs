using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200008E RID: 142
	[NullableContext(1)]
	internal interface ISortKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000591 RID: 1425
		IComparer<TKey> KeyComparer { get; }
	}
}
