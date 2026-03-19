using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0,
		1
	})]
	internal sealed class DefaultFrozenSet<[Nullable(2)] T> : ItemsFrozenSet<T, DefaultFrozenSet<T>.GSW>
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000A743 File Offset: 0x00008943
		internal DefaultFrozenSet(HashSet<T> source) : base(source, false)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A750 File Offset: 0x00008950
		private protected override int FindItemIndex(T item)
		{
			IEqualityComparer<T> comparer = base.Comparer;
			int num = (item == null) ? 0 : comparer.GetHashCode(item);
			int i;
			int num2;
			this._hashTable.FindMatchingEntries(num, out i, out num2);
			while (i <= num2)
			{
				if (num == this._hashTable.HashCodes[i] && comparer.Equals(item, this._items[i]))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x020000C1 RID: 193
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<T, DefaultFrozenSet<T>.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x06000857 RID: 2135 RVA: 0x00016147 File Offset: 0x00014347
			public void Store(FrozenSet<T> set)
			{
				this._set = (DefaultFrozenSet<T>)set;
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000858 RID: 2136 RVA: 0x00016155 File Offset: 0x00014355
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000859 RID: 2137 RVA: 0x00016162 File Offset: 0x00014362
			public IEqualityComparer<T> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x0001616F File Offset: 0x0001436F
			public int FindItemIndex(T item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x0001617D File Offset: 0x0001437D
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<T>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000157 RID: 343
			private DefaultFrozenSet<T> _set;
		}
	}
}
