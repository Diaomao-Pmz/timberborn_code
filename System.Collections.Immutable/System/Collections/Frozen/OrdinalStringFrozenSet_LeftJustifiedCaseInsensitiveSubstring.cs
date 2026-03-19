using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0000E04F File Offset: 0x0000C24F
		internal OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000E060 File Offset: 0x0000C260
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000E069 File Offset: 0x0000C269
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000E072 File Offset: 0x0000C272
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000E080 File Offset: 0x0000C280
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000E089 File Offset: 0x0000C289
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000E0A2 File Offset: 0x0000C2A2
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
