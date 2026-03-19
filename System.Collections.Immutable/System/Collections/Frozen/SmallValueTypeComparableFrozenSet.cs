using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0,
		1
	})]
	internal sealed class SmallValueTypeComparableFrozenSet<[Nullable(2)] T> : FrozenSetInternalBase<T, SmallValueTypeComparableFrozenSet<T>.GSW>
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		internal SmallValueTypeComparableFrozenSet(HashSet<T> source) : base(EqualityComparer<T>.Default)
		{
			this._items = source.ToArray<T>();
			Array.Sort<T>(this._items);
			this._max = this._items[this._items.Length - 1];
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		private protected override T[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<T>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<T>.Enumerator(this._items);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		private protected override int CountCore
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000C804 File Offset: 0x0000AA04
		private protected override int FindItemIndex(T item)
		{
			if (Comparer<T>.Default.Compare(item, this._max) <= 0)
			{
				T[] items = this._items;
				int i = 0;
				while (i < items.Length)
				{
					int num = Comparer<T>.Default.Compare(item, items[i]);
					if (num <= 0)
					{
						if (num == 0)
						{
							return i;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x0400006E RID: 110
		private readonly T[] _items;

		// Token: 0x0400006F RID: 111
		private readonly T _max;

		// Token: 0x020000C8 RID: 200
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<T, SmallValueTypeComparableFrozenSet<T>.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x06000873 RID: 2163 RVA: 0x00016300 File Offset: 0x00014500
			public void Store(FrozenSet<T> set)
			{
				this._set = (SmallValueTypeComparableFrozenSet<T>)set;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001630E File Offset: 0x0001450E
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001631B File Offset: 0x0001451B
			public IEqualityComparer<T> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x00016328 File Offset: 0x00014528
			public int FindItemIndex(T item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x00016336 File Offset: 0x00014536
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<T>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000164 RID: 356
			private SmallValueTypeComparableFrozenSet<T> _set;
		}
	}
}
