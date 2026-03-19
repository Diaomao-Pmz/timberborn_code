using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_FullCaseInsensitiveAscii : OrdinalStringFrozenSet
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0000DF22 File Offset: 0x0000C122
		internal OrdinalStringFrozenSet_FullCaseInsensitiveAscii(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(entries, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000DF39 File Offset: 0x0000C139
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000DF42 File Offset: 0x0000C142
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000DF4B File Offset: 0x0000C14B
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000DF59 File Offset: 0x0000C159
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000DF62 File Offset: 0x0000C162
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000DF6F File Offset: 0x0000C16F
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000DF77 File Offset: 0x0000C177
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x04000093 RID: 147
		private readonly ulong _lengthFilter;
	}
}
