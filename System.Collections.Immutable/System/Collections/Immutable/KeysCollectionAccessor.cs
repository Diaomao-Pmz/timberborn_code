using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1,
		1
	})]
	internal sealed class KeysCollectionAccessor<TKey, [Nullable(2)] TValue> : KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00009B52 File Offset: 0x00007D52
		internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary) : base(dictionary, dictionary.Keys)
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00009B61 File Offset: 0x00007D61
		public override bool Contains(TKey item)
		{
			return base.Dictionary.ContainsKey(item);
		}
	}
}
