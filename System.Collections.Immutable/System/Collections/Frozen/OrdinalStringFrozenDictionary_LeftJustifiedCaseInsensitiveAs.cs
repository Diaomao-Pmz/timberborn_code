using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveAsciiSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D9C9 File Offset: 0x0000BBC9
		internal OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveAsciiSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D9EE File Offset: 0x0000BBEE
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000DA0B File Offset: 0x0000BC0B
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s, base.HashIndex, base.HashCount));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000DA24 File Offset: 0x0000BC24
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s.Slice(base.HashIndex, base.HashCount));
		}
	}
}
