using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class SmallFrozenDictionary<TKey, [Nullable(2)] TValue> : FrozenDictionary<TKey, TValue>
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000C58B File Offset: 0x0000A78B
		internal SmallFrozenDictionary(Dictionary<TKey, TValue> source) : base(source.Comparer)
		{
			this._keys = source.Keys.ToArray<TKey>();
			this._values = source.Values.ToArray<TValue>();
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		private protected override TKey[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		private protected override int CountCore
		{
			get
			{
				return this._keys.Length;
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000C5D5 File Offset: 0x0000A7D5
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

		// Token: 0x06000489 RID: 1161 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			IEqualityComparer<TKey> comparer = base.Comparer;
			TKey[] keys = this._keys;
			for (int i = 0; i < keys.Length; i++)
			{
				if (comparer.Equals(keys[i], key))
				{
					return ref this._values[i];
				}
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x04000068 RID: 104
		private readonly TKey[] _keys;

		// Token: 0x04000069 RID: 105
		private readonly TValue[] _values;
	}
}
