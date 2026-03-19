using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_Full<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0000D95B File Offset: 0x0000BB5B
		internal OrdinalStringFrozenDictionary_Full(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, ulong lengthFilter) : base(keys, values, comparer, minimumLength, maximumLengthDiff, -1, -1)
		{
			this._lengthFilter = lengthFilter;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000D974 File Offset: 0x0000BB74
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000D97D File Offset: 0x0000BB7D
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000D986 File Offset: 0x0000BB86
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D98F File Offset: 0x0000BB8F
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000D99D File Offset: 0x0000BB9D
		private protected override int GetHashCode(string s)
		{
			return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		[NullableContext(0)]
		private protected override int GetHashCode(ReadOnlySpan<char> s)
		{
			return Hashing.GetHashCodeOrdinal(s);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		private protected override bool CheckLengthQuick(uint length)
		{
			return (this._lengthFilter & 1UL << (int)(length % 64U)) > 0UL;
		}

		// Token: 0x0400008C RID: 140
		private readonly ulong _lengthFilter;
	}
}
