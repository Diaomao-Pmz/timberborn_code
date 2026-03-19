using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200008D RID: 141
	[NullableContext(1)]
	internal interface IHashKeyCollection<[Nullable(2)] in TKey>
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000590 RID: 1424
		IEqualityComparer<TKey> KeyComparer { get; }
	}
}
