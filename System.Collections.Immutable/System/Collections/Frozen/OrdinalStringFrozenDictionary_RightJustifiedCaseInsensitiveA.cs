using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000077 RID: 119
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveAsciiSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0000DB90 File Offset: 0x0000BD90
		internal OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveAsciiSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000DBA3 File Offset: 0x0000BDA3
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000DBB5 File Offset: 0x0000BDB5
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000DBC3 File Offset: 0x0000BDC3
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000DBD2 File Offset: 0x0000BDD2
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000DBF2 File Offset: 0x0000BDF2
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
