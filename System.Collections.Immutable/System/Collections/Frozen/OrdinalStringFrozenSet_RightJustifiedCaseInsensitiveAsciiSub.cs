using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000083 RID: 131
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveAsciiSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0000E1BF File Offset: 0x0000C3BF
		internal OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveAsciiSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000E219 File Offset: 0x0000C419
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
