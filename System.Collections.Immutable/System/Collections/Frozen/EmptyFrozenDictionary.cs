using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	internal sealed class EmptyFrozenDictionary<TKey, [Nullable(2)] TValue> : FrozenDictionary<TKey, TValue>
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A7B9 File Offset: 0x000089B9
		internal EmptyFrozenDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000A7C2 File Offset: 0x000089C2
		private protected override TKey[] KeysCore
		{
			get
			{
				return Array.Empty<TKey>();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000A7C9 File Offset: 0x000089C9
		private protected override TValue[] ValuesCore
		{
			get
			{
				return Array.Empty<TValue>();
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A7D0 File Offset: 0x000089D0
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		private protected override FrozenDictionary<TKey, TValue>.Enumerator GetEnumeratorCore()
		{
			return new FrozenDictionary<TKey, TValue>.Enumerator(Array.Empty<TKey>(), Array.Empty<TValue>());
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A7E1 File Offset: 0x000089E1
		private protected override int CountCore
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A7E4 File Offset: 0x000089E4
		private protected override ref readonly TValue GetValueRefOrNullRefCore(TKey key)
		{
			return Unsafe.NullRef<TValue>();
		}
	}
}
