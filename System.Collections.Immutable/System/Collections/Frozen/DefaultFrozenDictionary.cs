using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class DefaultFrozenDictionary<TKey, [Nullable(2)] TValue> : KeysAndValuesFrozenDictionary<TKey, TValue>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000A6CA File Offset: 0x000088CA
		internal DefaultFrozenDictionary(Dictionary<TKey, TValue> source) : base(source, false)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A6D4 File Offset: 0x000088D4
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			IEqualityComparer<TKey> comparer = base.Comparer;
			int hashCode = comparer.GetHashCode(key);
			int i;
			int num;
			this._hashTable.FindMatchingEntries(hashCode, out i, out num);
			while (i <= num)
			{
				if (hashCode == this._hashTable.HashCodes[i] && comparer.Equals(key, this._keys[i]))
				{
					return ref this._values[i];
				}
				i++;
			}
			return Unsafe.NullRef<TValue>();
		}
	}
}
