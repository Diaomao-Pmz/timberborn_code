using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1,
		1
	})]
	internal sealed class ValuesCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00009B6F File Offset: 0x00007D6F
		internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary, dictionary.Values)
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009B80 File Offset: 0x00007D80
		public override bool Contains(TValue item)
		{
			ImmutableSortedDictionary<TKey, TValue> immutableSortedDictionary = base.Dictionary as ImmutableSortedDictionary<TKey, TValue>;
			if (immutableSortedDictionary != null)
			{
				return immutableSortedDictionary.ContainsValue(item);
			}
			IImmutableDictionaryInternal<TKey, TValue> immutableDictionaryInternal = base.Dictionary as IImmutableDictionaryInternal<TKey, TValue>;
			if (immutableDictionaryInternal != null)
			{
				return immutableDictionaryInternal.ContainsValue(item);
			}
			throw new NotSupportedException();
		}
	}
}
