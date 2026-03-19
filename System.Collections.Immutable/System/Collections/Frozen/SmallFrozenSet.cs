using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000061 RID: 97
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0,
		1
	})]
	internal sealed class SmallFrozenSet<[Nullable(2)] T> : FrozenSetInternalBase<T, SmallFrozenSet<T>.GSW>
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000C635 File Offset: 0x0000A835
		internal SmallFrozenSet(HashSet<T> source) : base(source.Comparer)
		{
			this._items = source.ToArray<T>();
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000C64F File Offset: 0x0000A84F
		private protected override T[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000C657 File Offset: 0x0000A857
		private protected override int CountCore
		{
			get
			{
				return this._items.Length;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000C664 File Offset: 0x0000A864
		private protected override int FindItemIndex(T item)
		{
			T[] items = this._items;
			for (int i = 0; i < items.Length; i++)
			{
				if (base.Comparer.Equals(item, items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000C69E File Offset: 0x0000A89E
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<T>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<T>.Enumerator(this._items);
		}

		// Token: 0x0400006A RID: 106
		private readonly T[] _items;

		// Token: 0x020000C7 RID: 199
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<T, SmallFrozenSet<T>.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x0600086E RID: 2158 RVA: 0x000162BD File Offset: 0x000144BD
			public void Store(FrozenSet<T> set)
			{
				this._set = (SmallFrozenSet<T>)set;
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600086F RID: 2159 RVA: 0x000162CB File Offset: 0x000144CB
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000870 RID: 2160 RVA: 0x000162D8 File Offset: 0x000144D8
			public IEqualityComparer<T> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x000162E5 File Offset: 0x000144E5
			public int FindItemIndex(T item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x000162F3 File Offset: 0x000144F3
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<T>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000163 RID: 355
			private SmallFrozenSet<T> _set;
		}
	}
}
