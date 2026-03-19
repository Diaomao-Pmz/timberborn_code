using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000074 RID: 116
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000DA3E File Offset: 0x0000BC3E
		internal OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000DA51 File Offset: 0x0000BC51
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000DA5A File Offset: 0x0000BC5A
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000DA63 File Offset: 0x0000BC63
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000DA71 File Offset: 0x0000BC71
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000DA80 File Offset: 0x0000BC80
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000DA99 File Offset: 0x0000BC99
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
