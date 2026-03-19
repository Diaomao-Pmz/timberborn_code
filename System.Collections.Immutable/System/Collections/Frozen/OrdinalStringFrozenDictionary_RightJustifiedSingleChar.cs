using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class OrdinalStringFrozenDictionary_RightJustifiedSingleChar<[Nullable(2)] TValue> : OrdinalStringFrozenDictionary<TValue>
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0000DC14 File Offset: 0x0000BE14
		internal OrdinalStringFrozenDictionary_RightJustifiedSingleChar(string[] keys, TValue[] values, IEqualityComparer<string> comparer, int minimumLength, int maximumLengthDiff, int hashIndex) : base(keys, values, comparer, minimumLength, maximumLengthDiff, hashIndex, 1)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000DC26 File Offset: 0x0000BE26
		private protected override ref readonly TValue GetValueRefOrNullRefCore(string key)
		{
			return base.GetValueRefOrNullRefCore(key);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		private protected override ref readonly TValue GetValueRefOrNullRefCore<TAlternateKey>(TAlternateKey key)
		{
			return base.GetValueRefOrNullRefCore<TAlternateKey>(key);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000DC38 File Offset: 0x0000BE38
		[NullableContext(2)]
		private protected override bool Equals(string x, string y)
		{
			return string.Equals(x, y);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000DC41 File Offset: 0x0000BE41
		[NullableContext(0)]
		private protected override bool Equals(ReadOnlySpan<char> x, [Nullable(2)] string y)
		{
			return MemoryExtensions.SequenceEqual<char>(x, MemoryExtensions.AsSpan(y));
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000DC4F File Offset: 0x0000BE4F
		private protected override int GetHashCode(string s)
		{
			return (int)s[s.Length + base.HashIndex];
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000DC64 File Offset: 0x0000BE64
		[NullableContext(0)]
		private protected unsafe override int GetHashCode(ReadOnlySpan<char> s)
		{
			return (int)(*s[s.Length + base.HashIndex]);
		}
	}
}
