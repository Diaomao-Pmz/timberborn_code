using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005E RID: 94
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		0
	})]
	internal abstract class ItemsFrozenSet<[Nullable(2)] T, [Nullable(0)] TThisWrapper> : FrozenSetInternalBase<T, TThisWrapper> where TThisWrapper : struct, FrozenSetInternalBase<T, TThisWrapper>.IGenericSpecializedWrapper
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x0000C35C File Offset: 0x0000A55C
		protected unsafe ItemsFrozenSet(HashSet<T> source, bool keysAreHashCodes = false) : base(source.Comparer)
		{
			T[] array = new T[source.Count];
			source.CopyTo(array);
			this._items = new T[array.Length];
			int[] array2 = ArrayPool<int>.Shared.Rent(array.Length);
			Span<int> hashCodes = MemoryExtensions.AsSpan<int>(array2, 0, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				ref int ptr = hashCodes[i];
				T t = array[i];
				ptr = ((t != null) ? base.Comparer.GetHashCode(t) : 0);
			}
			this._hashTable = FrozenHashTable.Create(hashCodes, keysAreHashCodes);
			for (int j = 0; j < hashCodes.Length; j++)
			{
				int num = *hashCodes[j];
				this._items[num] = array[j];
			}
			ArrayPool<int>.Shared.Return(array2, false);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000C438 File Offset: 0x0000A638
		private protected sealed override T[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000C440 File Offset: 0x0000A640
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected sealed override FrozenSet<T>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<T>.Enumerator(this._items);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000C44D File Offset: 0x0000A64D
		private protected sealed override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x04000063 RID: 99
		private protected readonly FrozenHashTable _hashTable;

		// Token: 0x04000064 RID: 100
		private protected readonly T[] _items;
	}
}
