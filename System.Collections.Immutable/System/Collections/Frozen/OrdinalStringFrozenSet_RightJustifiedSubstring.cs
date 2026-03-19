using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_RightJustifiedSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
		internal OrdinalStringFrozenSet_RightJustifiedSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000E2FA File Offset: 0x0000C4FA
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
