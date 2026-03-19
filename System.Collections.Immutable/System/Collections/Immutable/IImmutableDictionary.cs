using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	public interface IImmutableDictionary<[Nullable(2)] TKey, [Nullable(2)] TValue> : IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x0600009C RID: 156
		IImmutableDictionary<TKey, TValue> Clear();

		// Token: 0x0600009D RID: 157
		IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

		// Token: 0x0600009E RID: 158
		IImmutableDictionary<TKey, TValue> AddRange([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> pairs);

		// Token: 0x0600009F RID: 159
		IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

		// Token: 0x060000A0 RID: 160
		IImmutableDictionary<TKey, TValue> SetItems([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerable<KeyValuePair<TKey, TValue>> items);

		// Token: 0x060000A1 RID: 161
		IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

		// Token: 0x060000A2 RID: 162
		IImmutableDictionary<TKey, TValue> Remove(TKey key);

		// Token: 0x060000A3 RID: 163
		bool Contains([Nullable(new byte[]
		{
			0,
			1,
			1
		})] KeyValuePair<TKey, TValue> pair);

		// Token: 0x060000A4 RID: 164
		bool TryGetKey(TKey equalKey, out TKey actualKey);
	}
}
