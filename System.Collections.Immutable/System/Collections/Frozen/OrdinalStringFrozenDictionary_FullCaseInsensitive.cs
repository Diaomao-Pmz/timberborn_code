using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_FullCaseInsensitive<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D8E7 File Offset: 0x0000BAE7
		internal OrdinalStringFrozenDictionary_FullCaseInsensitive(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(keys, values, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D900 File Offset: 0x0000BB00
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000D909 File Offset: 0x0000BB09
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D912 File Offset: 0x0000BB12
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(x, y);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000D920 File Offset: 0x0000BB20
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.Equals(x, MemoryExtensions.AsSpan(y), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D92F File Offset: 0x0000BB2F
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D93C File Offset: 0x0000BB3C
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinalIgnoreCase(s);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D944 File Offset: 0x0000BB44
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x0400008B RID: 139
		private readonly ulong _lengthFilter;
	}
}
