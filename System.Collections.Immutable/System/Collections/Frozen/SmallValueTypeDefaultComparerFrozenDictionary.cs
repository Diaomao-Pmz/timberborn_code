using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class SmallValueTypeDefaultComparerFrozenDictionary<TKey, [Nullable(2)] TValue> : FrozenDictionary<TKey, TValue>
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000C857 File Offset: 0x0000AA57
		internal SmallValueTypeDefaultComparerFrozenDictionary(Dictionary<TKey, TValue> source) : base(EqualityComparer<TKey>.Default)
		{
			this._keys = source.Keys.ToArray<TKey>();
			this._values = source.Values.ToArray<TValue>();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000C886 File Offset: 0x0000AA86
		private protected override TKey[] KeysCore
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000C88E File Offset: 0x0000AA8E
		private protected override TValue[] ValuesCore
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000C896 File Offset: 0x0000AA96
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000C8A9 File Offset: 0x0000AAA9
		private protected override int CountCore
		{
			get
			{
				return this._keys.Length;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			TKey[] keys = this._keys;
			for (int i = 0; i < keys.Length; i++)
			{
				if (EqualityComparer<TKey>.Default.Equals(keys[i], key))
				{
					return ref this._values[i];
				}
			}
			return Unsafe.NullRef<TValue>();
		}

		// Token: 0x04000070 RID: 112
		private readonly TKey[] _keys;

		// Token: 0x04000071 RID: 113
		private readonly TValue[] _values;
	}
}
