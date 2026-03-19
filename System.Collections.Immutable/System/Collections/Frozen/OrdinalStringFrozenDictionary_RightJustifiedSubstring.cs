using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000079 RID: 121
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_RightJustifiedSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		internal OrdinalStringFrozenDictionary_RightJustifiedSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000DC8F File Offset: 0x0000BE8F
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000DC98 File Offset: 0x0000BE98
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000DCA1 File Offset: 0x0000BEA1
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
