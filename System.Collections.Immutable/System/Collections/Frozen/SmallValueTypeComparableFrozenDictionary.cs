using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class SmallValueTypeComparableFrozenDictionary<TKey, [Nullable(2)] TValue> : FrozenDictionary<TKey, TValue>
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		internal SmallValueTypeComparableFrozenDictionary(Dictionary<TKey, TValue> source) : base(EqualityComparer<TKey>.Default)
		{
			this._keys = source.Keys.ToArray<TKey>();
			this._values = source.Values.ToArray<TValue>();
			Array.Sort<TKey, TValue>(this._keys, this._values);
			this._max = this._keys[this._keys.Length - 1];
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000C712 File Offset: 0x0000A912
		private protected override TKey[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000C71A File Offset: 0x0000A91A
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000C722 File Offset: 0x0000A922
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		private protected override FrozenDictionary<TKey, TValue>.Enumerator GetEnumeratorCore()
		{
			return new FrozenDictionary<TKey, TValue>.Enumerator(this._keys, this._values);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000C735 File Offset: 0x0000A935
		private protected override int CountCore
		{
			get
			{
				return this._keys.Length;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000C740 File Offset: 0x0000A940
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			if (Comparer<TKey>.Default.Compare(key, this._max) <= 0)
			{
				TKey[] keys = this._keys;
				int i = 0;
				while (i < keys.Length)
				{
					int num = Comparer<TKey>.Default.Compare(key, keys[i]);
					if (num <= 0)
					{
						if (num == 0)
						{
							return ref this._values[i];
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x0400006B RID: 107
		private readonly TKey[] _keys;

		// Token: 0x0400006C RID: 108
		private readonly TValue[] _values;

		// Token: 0x0400006D RID: 109
		private readonly TKey _max;
	}
}
