using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000081 RID: 129
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_LeftJustifiedSubstring : OrdinalStringFrozenSet
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0000E129 File Offset: 0x0000C329
		internal OrdinalStringFrozenSet_LeftJustifiedSubstring(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000E13A File Offset: 0x0000C33A
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000E143 File Offset: 0x0000C343
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000E14C File Offset: 0x0000C34C
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000E165 File Offset: 0x0000C365
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
