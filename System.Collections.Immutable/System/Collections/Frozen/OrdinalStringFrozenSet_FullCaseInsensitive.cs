using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007D RID: 125
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_FullCaseInsensitive : OrdinalStringFrozenSet
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0000DF8E File Offset: 0x0000C18E
		internal OrdinalStringFrozenSet_FullCaseInsensitive(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(entries, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000DFAE File Offset: 0x0000C1AE
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000DFB7 File Offset: 0x0000C1B7
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000DFC5 File Offset: 0x0000C1C5
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000DFCE File Offset: 0x0000C1CE
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000DFDB File Offset: 0x0000C1DB
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000DFE3 File Offset: 0x0000C1E3
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x04000094 RID: 148
		private readonly ulong _lengthFilter;
	}
}
