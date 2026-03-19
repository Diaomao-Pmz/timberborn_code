using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005F RID: 95
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal abstract class KeysAndValuesFrozenDictionary<TKey, [Nullable(2)] TValue> : FrozenDictionary<TKey, TValue>, IDictionary<!0, !1>, ICollection<KeyValuePair<!0, !1>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000C45C File Offset: 0x0000A65C
		protected unsafe KeysAndValuesFrozenDictionary(Dictionary<TKey, TValue> source, bool keysAreHashCodes = false) : base(source.Comparer)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[source.Count];
			((ICollection<KeyValuePair<!0, !1>>)source).CopyTo(array, 0);
			this._keys = new TKey[array.Length];
			this._values = new TValue[array.Length];
			int[] array2 = ArrayPool<int>.Shared.Rent(array.Length);
			Span<int> hashCodes = MemoryExtensions.AsSpan<int>(array2, 0, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				*hashCodes[i] = base.Comparer.GetHashCode(array[i].Key);
			}
			this._hashTable = FrozenHashTable.Create(hashCodes, keysAreHashCodes);
			for (int j = 0; j < hashCodes.Length; j++)
			{
				int num = *hashCodes[j];
				this._keys[num] = array[j].Key;
				this._values[num] = array[j].Value;
			}
			ArrayPool<int>.Shared.Return(array2, false);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000C55B File Offset: 0x0000A75B
		private protected sealed override TKey[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000C563 File Offset: 0x0000A763
		private protected sealed override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000C56B File Offset: 0x0000A76B
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		private protected sealed override FrozenDictionary<TKey, TValue>.Enumerator GetEnumeratorCore()
		{
			return new FrozenDictionary<TKey, TValue>.Enumerator(this._keys, this._values);
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000C57E File Offset: 0x0000A77E
		private protected sealed override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x04000065 RID: 101
		private protected readonly FrozenHashTable _hashTable;

		// Token: 0x04000066 RID: 102
		private protected readonly TKey[] _keys;

		// Token: 0x04000067 RID: 103
		private protected readonly TValue[] _values;
	}
}
