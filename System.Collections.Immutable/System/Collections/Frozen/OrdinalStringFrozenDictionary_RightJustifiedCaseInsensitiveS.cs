using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveSubstring<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		internal OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveSubstring(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex, int hashCount) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, hashCount)
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000DB1F File Offset: 0x0000BD1F
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000DB28 File Offset: 0x0000BD28
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000DB31 File Offset: 0x0000BD31
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000DB4E File Offset: 0x0000BD4E
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s, s.Length + base.HashIndex, base.HashCount));
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000DB6E File Offset: 0x0000BD6E
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s.Slice(s.Length + base.HashIndex, base.HashCount));
		}
	}
}
