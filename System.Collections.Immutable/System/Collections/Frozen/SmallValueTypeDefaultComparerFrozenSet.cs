using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000065 RID: 101
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0,
		1
	})]
	internal sealed class SmallValueTypeDefaultComparerFrozenSet<[Nullable(2)] T> : FrozenSetInternalBase<T, SmallValueTypeDefaultComparerFrozenSet<T>.GSW>
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C8FE File Offset: 0x0000AAFE
		internal SmallValueTypeDefaultComparerFrozenSet(HashSet<T> source) : base(EqualityComparer<T>.Default)
		{
			this._items = source.ToArray<T>();
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000C917 File Offset: 0x0000AB17
		private protected override T[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000C91F File Offset: 0x0000AB1F
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<T>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<T>.Enumerator(this._items);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000C92C File Offset: 0x0000AB2C
		private protected override int CountCore
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000C938 File Offset: 0x0000AB38
		private protected override int FindItemIndex(T item)
		{
			T[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (EqualityComparer<T>.Default.Equals(item, items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000072 RID: 114
		private readonly T[] _items;

		// Token: 0x020000C9 RID: 201
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<T, SmallValueTypeDefaultComparerFrozenSet<T>.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x06000878 RID: 2168 RVA: 0x00016343 File Offset: 0x00014543
			public void Store(FrozenSet<T> set)
			{
				this._set = (SmallValueTypeDefaultComparerFrozenSet<T>)set;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x00016351 File Offset: 0x00014551
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001635E File Offset: 0x0001455E
			public IEqualityComparer<T> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x0001636B File Offset: 0x0001456B
			public int FindItemIndex(T item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x00016379 File Offset: 0x00014579
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<T>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000165 RID: 357
			private SmallValueTypeDefaultComparerFrozenSet<T> _set;
		}
	}
}
