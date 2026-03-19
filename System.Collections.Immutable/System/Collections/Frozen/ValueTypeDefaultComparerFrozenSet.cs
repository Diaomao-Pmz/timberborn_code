using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000067 RID: 103
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0,
		1
	})]
	internal sealed class ValueTypeDefaultComparerFrozenSet<[Nullable(2)] T> : ItemsFrozenSet<T, ValueTypeDefaultComparerFrozenSet<T>.GSW>
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		internal ValueTypeDefaultComparerFrozenSet(HashSet<T> source) : base(source, Constants.KeysAreHashCodes<T>())
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000CA00 File Offset: 0x0000AC00
		private protected override int FindItemIndex(T item)
		{
			int hashCode = EqualityComparer<T>.Default.GetHashCode(item);
			int i;
			int num;
			this._hashTable.FindMatchingEntries(hashCode, out i, out num);
			while (i <= num)
			{
				if (hashCode == this._hashTable.HashCodes[i] && EqualityComparer<T>.Default.Equals(item, this._items[i]))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x020000CA RID: 202
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<T, ValueTypeDefaultComparerFrozenSet<T>.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x0600087D RID: 2173 RVA: 0x00016386 File Offset: 0x00014586
			public void Store(FrozenSet<T> set)
			{
				this._set = (ValueTypeDefaultComparerFrozenSet<T>)set;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x0600087E RID: 2174 RVA: 0x00016394 File Offset: 0x00014594
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x0600087F RID: 2175 RVA: 0x000163A1 File Offset: 0x000145A1
			public IEqualityComparer<T> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x000163AE File Offset: 0x000145AE
			public int FindItemIndex(T item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x000163BC File Offset: 0x000145BC
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<T>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000166 RID: 358
			private ValueTypeDefaultComparerFrozenSet<T> _set;
		}
	}
}
