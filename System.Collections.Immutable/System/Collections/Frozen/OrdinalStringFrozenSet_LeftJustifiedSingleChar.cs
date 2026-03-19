using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_LeftJustifiedSingleChar : OrdinalStringFrozenSet
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000E17F File Offset: 0x0000C37F
		internal OrdinalStringFrozenSet_LeftJustifiedSingleChar(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, 1)
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000E18F File Offset: 0x0000C38F
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000E198 File Offset: 0x0000C398
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000E1A1 File Offset: 0x0000C3A1
		private protected override int GetHashCode(string s)
		{
			return (int)s[base.HashIndex];
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000E1AF File Offset: 0x0000C3AF
		[NullableContext(0)]
		private protected unsafe override int GetHashCode(ReadOnlySpan<char> s)
		{
			return (int)(*s[base.HashIndex]);
		}
	}
}
