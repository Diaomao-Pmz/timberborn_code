using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000069 RID: 105
	internal sealed class Int32FrozenSet : FrozenSetInternalBase<int, Int32FrozenSet.GSW>
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		[NullableContext(1)]
		internal Int32FrozenSet(HashSet<int> source) : base(EqualityComparer<int>.Default)
		{
			int count = source.Count;
			int[] array = ArrayPool<int>.Shared.Rent(count);
			source.CopyTo(array);
			this._hashTable = FrozenHashTable.Create(new Span<int>(array, 0, count), true);
			ArrayPool<int>.Shared.Return(array, false);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		[Nullable(1)]
		private protected override int[] ItemsCore
		{
			[NullableContext(1)]
			get
			{
				return this._hashTable.HashCodes;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000CC17 File Offset: 0x0000AE17
		private protected override FrozenSet<int>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<int>.Enumerator(this._hashTable.HashCodes);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000CC29 File Offset: 0x0000AE29
		private protected override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000CC38 File Offset: 0x0000AE38
		private protected override int FindItemIndex(int item)
		{
			int i;
			int num;
			this._hashTable.FindMatchingEntries(item, out i, out num);
			int[] hashCodes = this._hashTable.HashCodes;
			while (i <= num)
			{
				if (item == hashCodes[i])
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x04000075 RID: 117
		private readonly FrozenHashTable _hashTable;

		// Token: 0x020000CB RID: 203
		internal struct GSW : FrozenSetInternalBase<int, Int32FrozenSet.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x06000882 RID: 2178 RVA: 0x000163C9 File Offset: 0x000145C9
			[NullableContext(1)]
			public void Store(FrozenSet<int> set)
			{
				this._set = (Int32FrozenSet)set;
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x000163D7 File Offset: 0x000145D7
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x000163E4 File Offset: 0x000145E4
			[Nullable(1)]
			public IEqualityComparer<int> Comparer
			{
				[NullableContext(1)]
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x000163F1 File Offset: 0x000145F1
			public int FindItemIndex(int item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x000163FF File Offset: 0x000145FF
			public FrozenSet<int>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000167 RID: 359
			private Int32FrozenSet _set;
		}
	}
}
