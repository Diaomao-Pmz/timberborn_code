using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	internal interface IOrderedCollection<[Nullable(2)] out T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000128 RID: 296
		int Count { get; }

		// Token: 0x1700003E RID: 62
		T this[int index]
		{
			get;
		}
	}
}
