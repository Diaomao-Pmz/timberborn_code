using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000029 RID: 41
	[NullableContext(2)]
	internal interface IImmutableDictionaryInternal<TKey, TValue>
	{
		// Token: 0x060000A5 RID: 165
		[NullableContext(1)]
		bool ContainsValue(TValue value);
	}
}
