using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal abstract class OrdinalStringFrozenSet : FrozenSetInternalBase<string, OrdinalStringFrozenSet.GSW>
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		internal unsafe OrdinalStringFrozenSet(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex = -1, int hashCount = -1) : base(comparer)
		{
			this._items = new string[entries.Length];
			this._minimumLength = minimumLength;
			this._maximumLengthDiff = maximumLengthDiff;
			this.HashIndex = hashIndex;
			this.HashCount = hashCount;
			int[] array = ArrayPool<int>.Shared.Rent(entries.Length);
			Span<int> hashCodes = MemoryExtensions.AsSpan<int>(array, 0, entries.Length);
			for (int i = 0; i < entries.Length; i++)
			{
				*hashCodes[i] = this.GetHashCode(entries[i]);
			}
			this._hashTable = FrozenHashTable.Create(hashCodes, false);
			for (int j = 0; j < hashCodes.Length; j++)
			{
				int num = *hashCodes[j];
				this._items[num] = entries[j];
			}
			ArrayPool<int>.Shared.Return(array, false);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000DE28 File Offset: 0x0000C028
		private protected int HashIndex { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000DE30 File Offset: 0x0000C030
		private protected int HashCount { get; }

		// Token: 0x0600052D RID: 1325 RVA: 0x0000DE38 File Offset: 0x0000C038
		[NullableContext(2)]
		private protected virtual bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000DE41 File Offset: 0x0000C041
		[NullableContext(0)]
		private protected virtual bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinal(x, y);
		}

		// Token: 0x0600052F RID: 1327
		private protected abstract int GetHashCode(string s);

		// Token: 0x06000530 RID: 1328
		[NullableContext(0)]
		private protected abstract int GetHashCode(ReadOnlySpan<char> s);

		// Token: 0x06000531 RID: 1329 RVA: 0x0000DE4A File Offset: 0x0000C04A
		private protected virtual bool CheckLengthQuick(uint length)
		{
			return true;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000DE4D File Offset: 0x0000C04D
		private protected override string[] ItemsCore
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000DE55 File Offset: 0x0000C055
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<string>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<string>.Enumerator(this._items);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000DE62 File Offset: 0x0000C062
		private protected override int CountCore
		{
			get
			{
				return this._hashTable.Count;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000DE70 File Offset: 0x0000C070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override int FindItemIndex(string item)
		{
			if (item != null && item.Length - this._minimumLength <= this._maximumLengthDiff && this.CheckLengthQuick((uint)item.Length))
			{
				int hashCode = this.GetHashCode(item);
				int i;
				int num;
				this._hashTable.FindMatchingEntries(hashCode, out i, out num);
				while (i <= num)
				{
					if (hashCode == this._hashTable.HashCodes[i] && this.Equals(item, this._items[i]))
					{
						return i;
					}
					i++;
				}
			}
			return -1;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected static bool EqualsOrdinal(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return (!x.IsEmpty || y != null) && MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000DF05 File Offset: 0x0000C105
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected static bool EqualsOrdinalIgnoreCase(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return (!x.IsEmpty || y != null) && MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400008D RID: 141
		private readonly FrozenHashTable _hashTable;

		// Token: 0x0400008E RID: 142
		private readonly string[] _items;

		// Token: 0x0400008F RID: 143
		private readonly int _minimumLength;

		// Token: 0x04000090 RID: 144
		private readonly int _maximumLengthDiff;

		// Token: 0x020000D4 RID: 212
		[Nullable(0)]
		internal struct GSW : FrozenSetInternalBase<string, OrdinalStringFrozenSet.GSW>.IGenericSpecializedWrapper
		{
			// Token: 0x060008AA RID: 2218 RVA: 0x000166DF File Offset: 0x000148DF
			public void Store(FrozenSet<string> set)
			{
				this._set = (OrdinalStringFrozenSet)set;
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x060008AB RID: 2219 RVA: 0x000166ED File Offset: 0x000148ED
			public int Count
			{
				get
				{
					return this._set.Count;
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x060008AC RID: 2220 RVA: 0x000166FA File Offset: 0x000148FA
			public IEqualityComparer<string> Comparer
			{
				get
				{
					return this._set.Comparer;
				}
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00016707 File Offset: 0x00014907
			public int FindItemIndex(string item)
			{
				return this._set.FindItemIndex(item);
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x00016715 File Offset: 0x00014915
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			public FrozenSet<string>.Enumerator GetEnumerator()
			{
				return this._set.GetEnumerator();
			}

			// Token: 0x04000176 RID: 374
			private OrdinalStringFrozenSet _set;
		}
	}
}
