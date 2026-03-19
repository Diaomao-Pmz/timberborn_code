using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000086 RID: 134
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OrdinalStringFrozenSet_RightJustifiedSingleChar : OrdinalStringFrozenSet
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x0000E31C File Offset: 0x0000C51C
		internal OrdinalStringFrozenSet_RightJustifiedSingleChar(string[] entries, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex) : base(entries, comparer, minimumLength, maximumLengthDiff, hashIndex, 1)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000E32C File Offset: 0x0000C52C
		private protected override int FindItemIndex(string item)
		{
			return base.FindItemIndex(item);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000E335 File Offset: 0x0000C535
		private protected override int FindItemIndex<[Nullable(2)] TAlternate>(TAlternate item)
		{
			return base.FindItemIndex<TAlternate>(item);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000E33E File Offset: 0x0000C53E
		private protected override int GetHashCode(string s)
		{
			return (int)s[s.Length + base.HashIndex];
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000E353 File Offset: 0x0000C553
		[NullableContext(0)]
		private protected unsafe override int GetHashCode(ReadOnlySpan<char> s)
		{
			return (int)(*s[s.Length + base.HashIndex]);
		}
	}
}
