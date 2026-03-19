using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000070 RID: 112
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_FullCaseInsensitiveAscii<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0000D873 File Offset: 0x0000BA73
		internal OrdinalStringFrozenDictionary_FullCaseInsensitiveAscii(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(keys, values, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000D88C File Offset: 0x0000BA8C
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000D895 File Offset: 0x0000BA95
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000D89E File Offset: 0x0000BA9E
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000D8BB File Offset: 0x0000BABB
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(s);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x0400008A RID: 138
		private readonly ulong _lengthFilter;
	}
}
