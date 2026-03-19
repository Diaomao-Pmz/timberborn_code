using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_LeftJustifiedSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		internal OrdinalStringFrozenDictionary_LeftJustifiedSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000DD0D File Offset: 0x0000BF0D
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000DD16 File Offset: 0x0000BF16
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000DD1F File Offset: 0x0000BF1F
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000DD28 File Offset: 0x0000BF28
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000DD36 File Offset: 0x0000BF36
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000DD4F File Offset: 0x0000BF4F
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
