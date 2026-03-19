using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005A RID: 90
	internal readonly struct FrozenHashTable
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0000B254 File Offset: 0x00009454
		private FrozenHashTable(int[] hashCodes, FrozenHashTable.Bucket[] buckets, ulong fastModMultiplier)
		{
			this.HashCodes = hashCodes;
			this._buckets = buckets;
			this._fastModMultiplier = fastModMultiplier;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000B26C File Offset: 0x0000946C
		public unsafe static FrozenHashTable Create(Span<int> hashCodes, bool hashCodesAreUnique = false)
		{
			int num = FrozenHashTable.CalcNumBuckets(hashCodes, hashCodesAreUnique);
			ulong fastModMultiplier = HashHelpers.GetFastModMultiplier((uint)num);
			int[] array = ArrayPool<int>.Shared.Rent(num + hashCodes.Length);
			Span<int> span = MemoryExtensions.AsSpan<int>(array, 0, num);
			Span<int> span2 = MemoryExtensions.AsSpan<int>(array, num, hashCodes.Length);
			span.Fill(-1);
			for (int i = 0; i < hashCodes.Length; i++)
			{
				int num2 = (int)HashHelpers.FastMod((uint)(*hashCodes[i]), (uint)span.Length, fastModMultiplier);
				ref int ptr = span[num2];
				*span2[i] = ptr;
				ptr = i;
			}
			int[] array2 = new int[hashCodes.Length];
			FrozenHashTable.Bucket[] array3 = new FrozenHashTable.Bucket[span.Length];
			int num3 = 0;
			for (int j = 0; j < array3.Length; j++)
			{
				int num4 = *span[j];
				if (num4 >= 0)
				{
					int num5 = 0;
					int k = num4;
					num4 = num3;
					while (k >= 0)
					{
						ref int ptr2 = hashCodes[k];
						array2[num3] = ptr2;
						ptr2 = num3;
						num3++;
						num5++;
						k = *span2[k];
					}
					array3[j] = new FrozenHashTable.Bucket(num4, num5);
				}
			}
			ArrayPool<int>.Shared.Return(array, false);
			return new FrozenHashTable(array2, array3, fastModMultiplier);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000B3BC File Offset: 0x000095BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void FindMatchingEntries(int hashCode, out int startIndex, out int endIndex)
		{
			FrozenHashTable.Bucket[] buckets = this._buckets;
			ref FrozenHashTable.Bucket ptr = ref buckets[(int)HashHelpers.FastMod((uint)hashCode, (uint)buckets.Length, this._fastModMultiplier)];
			startIndex = ptr.StartIndex;
			endIndex = ptr.EndIndex;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000B3F6 File Offset: 0x000095F6
		public int Count
		{
			get
			{
				return this.HashCodes.Length;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000B400 File Offset: 0x00009600
		[Nullable(1)]
		internal int[] HashCodes { [NullableContext(1)] get; }

		// Token: 0x0600043F RID: 1087 RVA: 0x0000B408 File Offset: 0x00009608
		private unsafe static int CalcNumBuckets(ReadOnlySpan<int> hashCodes, bool hashCodesAreUnique)
		{
			HashSet<int> hashSet = null;
			int num = hashCodes.Length;
			if (!hashCodesAreUnique)
			{
				hashSet = new HashSet<int>();
				ReadOnlySpan<int> readOnlySpan = hashCodes;
				for (int i = 0; i < readOnlySpan.Length; i++)
				{
					int item = *readOnlySpan[i];
					hashSet.Add(item);
				}
				num = hashSet.Count;
			}
			int num2 = num * 2;
			ReadOnlySpan<int> primes = HashHelpers.Primes;
			int num3 = 0;
			while (num3 < primes.Length && num2 > *primes[num3])
			{
				num3++;
			}
			if (num3 >= primes.Length)
			{
				return HashHelpers.GetPrime(num);
			}
			int num4 = num * ((num >= 1000) ? 3 : 16);
			int num5 = num3;
			while (num5 < primes.Length && num4 > *primes[num5])
			{
				num5++;
			}
			if (num5 < primes.Length)
			{
				num4 = *primes[num5 - 1];
			}
			FrozenHashTable.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.seenBuckets = ArrayPool<int>.Shared.Rent(num4 / 32 + 1);
			int result = num4;
			CS$<>8__locals1.bestNumCollisions = num;
			CS$<>8__locals1.numBuckets = 0;
			CS$<>8__locals1.numCollisions = 0;
			int j = num3;
			while (j < num5)
			{
				CS$<>8__locals1.numBuckets = *primes[j];
				Array.Clear(CS$<>8__locals1.seenBuckets, 0, Math.Min(CS$<>8__locals1.numBuckets, CS$<>8__locals1.seenBuckets.Length));
				CS$<>8__locals1.numCollisions = 0;
				if (hashSet != null && num != hashCodes.Length)
				{
					using (HashSet<int>.Enumerator enumerator = hashSet.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!FrozenHashTable.<CalcNumBuckets>g__IsBucketFirstVisit|10_0(enumerator.Current, ref CS$<>8__locals1))
							{
								break;
							}
						}
						goto IL_1BA;
					}
					goto IL_18E;
				}
				goto IL_18E;
				IL_1BA:
				if (CS$<>8__locals1.numCollisions < CS$<>8__locals1.bestNumCollisions)
				{
					result = CS$<>8__locals1.numBuckets;
					if ((double)CS$<>8__locals1.numCollisions / (double)num <= 0.05)
					{
						break;
					}
					CS$<>8__locals1.bestNumCollisions = CS$<>8__locals1.numCollisions;
				}
				j++;
				continue;
				IL_18E:
				ReadOnlySpan<int> readOnlySpan = hashCodes;
				int i = 0;
				while (i < readOnlySpan.Length && FrozenHashTable.<CalcNumBuckets>g__IsBucketFirstVisit|10_0(*readOnlySpan[i], ref CS$<>8__locals1))
				{
					i++;
				}
				goto IL_1BA;
			}
			ArrayPool<int>.Shared.Return(CS$<>8__locals1.seenBuckets, false);
			return result;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000B63C File Offset: 0x0000983C
		[CompilerGenerated]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool <CalcNumBuckets>g__IsBucketFirstVisit|10_0(int code, ref FrozenHashTable.<>c__DisplayClass10_0 A_1)
		{
			uint num = (uint)(code % A_1.numBuckets);
			if ((A_1.seenBuckets[(int)(num / 32U)] & 1 << (int)num) != 0)
			{
				int numCollisions = A_1.numCollisions;
				A_1.numCollisions = numCollisions + 1;
				if (A_1.numCollisions >= A_1.bestNumCollisions)
				{
					return false;
				}
			}
			else
			{
				A_1.seenBuckets[(int)(num / 32U)] |= 1 << (int)num;
			}
			return true;
		}

		// Token: 0x0400005D RID: 93
		private readonly FrozenHashTable.Bucket[] _buckets;

		// Token: 0x0400005E RID: 94
		private readonly ulong _fastModMultiplier;

		// Token: 0x020000C3 RID: 195
		private readonly struct Bucket
		{
			// Token: 0x06000862 RID: 2146 RVA: 0x00016228 File Offset: 0x00014428
			public Bucket(int startIndex, int count)
			{
				this.StartIndex = startIndex;
				this.EndIndex = startIndex + count - 1;
			}

			// Token: 0x0400015B RID: 347
			public readonly int StartIndex;

			// Token: 0x0400015C RID: 348
			public readonly int EndIndex;
		}
	}
}
