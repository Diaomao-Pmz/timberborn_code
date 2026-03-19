using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class ValueTypeDefaultComparerFrozenDictionary<TKey, [Nullable(2)] TValue> : KeysAndValuesFrozenDictionary<TKey, TValue>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000C971 File Offset: 0x0000AB71
		internal ValueTypeDefaultComparerFrozenDictionary(Dictionary<TKey, TValue> source) : base(source, Constants.KeysAreHashCodes<TKey>())
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000C980 File Offset: 0x0000AB80
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			int hashCode = EqualityComparer<TKey>.Default.GetHashCode(key);
			int i;
			int num;
			this._hashTable.FindMatchingEntries(hashCode, out i, out num);
			while (i <= num)
			{
				if (hashCode == this._hashTable.HashCodes[i] && EqualityComparer<TKey>.Default.Equals(key, this._keys[i]))
				{
					return ref this._values[i];
				}
				i++;
			}
			return Unsafe.NullRef<TValue>();
		}
	}
}
