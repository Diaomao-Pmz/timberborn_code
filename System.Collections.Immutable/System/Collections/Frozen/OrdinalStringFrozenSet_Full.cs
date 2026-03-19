using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007E RID: 126
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_Full : OrdinalStringFrozenSet
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x0000DFFA File Offset: 0x0000C1FA
		internal OrdinalStringFrozenSet_Full(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(entries, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000E011 File Offset: 0x0000C211
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000E01A File Offset: 0x0000C21A
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000E023 File Offset: 0x0000C223
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000E030 File Offset: 0x0000C230
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000E038 File Offset: 0x0000C238
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x04000095 RID: 149
		private readonly ulong _lengthFilter;
	}
}
