using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000075 RID: 117
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_LeftJustifiedSingleChar<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		internal OrdinalStringFrozenDictionary_LeftJustifiedSingleChar(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, 1)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000DAC5 File Offset: 0x0000BCC5
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000DACE File Offset: 0x0000BCCE
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DAD7 File Offset: 0x0000BCD7
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		private protected override int GetHashCode(string s)
		{
			return (int)s[base.HashIndex];
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		[NullableContext(0)]
		private protected unsafe override int GetHashCode(ReadOnlySpan<char> s)
		{
			return (int)(*s[base.HashIndex]);
		}
	}
}
