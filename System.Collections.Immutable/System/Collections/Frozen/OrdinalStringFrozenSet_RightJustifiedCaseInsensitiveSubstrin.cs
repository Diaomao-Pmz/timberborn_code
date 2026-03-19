using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000084 RID: 132
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0000E23B File Offset: 0x0000C43B
		internal OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000E24C File Offset: 0x0000C44C
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000E255 File Offset: 0x0000C455
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000E25E File Offset: 0x0000C45E
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000E26C File Offset: 0x0000C46C
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return OrdinalStringFrozenSet.EqualsOrdinalIgnoreCase(x, y);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000E275 File Offset: 0x0000C475
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000E295 File Offset: 0x0000C495
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
