using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000080 RID: 128
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveAsciiSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		internal OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveAsciiSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000E0CD File Offset: 0x0000C2CD
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000E0D6 File Offset: 0x0000C2D6
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000E0DF File Offset: 0x0000C2DF
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000E0ED File Offset: 0x0000C2ED
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000E0F6 File Offset: 0x0000C2F6
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000E10F File Offset: 0x0000C30F
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
