using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x02000020 RID: 32
	internal static class HashHelpers
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002DFF File Offset: 0x00000FFF
		internal static ReadOnlySpan<int> Primes
		{
			get
			{
				int[] array;
				if ((array = <PrivateImplementationDetails>.74BCD6ED20AF2231F2BB1CDE814C5F4FF48E54BAC46029EEF90DDF4A208E2B20_A6) == null)
				{
					array = (<PrivateImplementationDetails>.74BCD6ED20AF2231F2BB1CDE814C5F4FF48E54BAC46029EEF90DDF4A208E2B20_A6 = new int[]
					{
						3,
						7,
						11,
						17,
						23,
						29,
						37,
						47,
						59,
						71,
						89,
						107,
						131,
						163,
						197,
						239,
						293,
						353,
						431,
						521,
						631,
						761,
						919,
						1103,
						1327,
						1597,
						1931,
						2333,
						2801,
						3371,
						4049,
						4861,
						5839,
						7013,
						8419,
						10103,
						12143,
						14591,
						17519,
						21023,
						25229,
						30293,
						36353,
						43627,
						52361,
						62851,
						75431,
						90523,
						108631,
						130363,
						156437,
						187751,
						225307,
						270371,
						324449,
						389357,
						467237,
						560689,
						672827,
						807403,
						968897,
						1162687,
						1395263,
						1674319,
						2009191,
						2411033,
						2893249,
						3471899,
						4166287,
						4999559,
						5999471,
						7199369
					});
				}
				return new ReadOnlySpan<int>(array);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E28 File Offset: 0x00001028
		public static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)candidate);
				for (int i = 3; i <= num; i += 2)
				{
					if (candidate % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return candidate == 2;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E5C File Offset: 0x0000105C
		public unsafe static int GetPrime(int min)
		{
			if (min < 0)
			{
				throw new ArgumentException(SR.Arg_HTCapacityOverflow);
			}
			ReadOnlySpan<int> primes = HashHelpers.Primes;
			for (int i = 0; i < primes.Length; i++)
			{
				int num = *primes[i];
				if (num >= min)
				{
					return num;
				}
			}
			for (int j = min | 1; j < 2147483647; j += 2)
			{
				if (HashHelpers.IsPrime(j) && (j - 1) % 101 != 0)
				{
					return j;
				}
			}
			return min;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002EC8 File Offset: 0x000010C8
		public static int ExpandPrime(int oldSize)
		{
			int num = 2 * oldSize;
			if (num > 2147483587 && 2147483587 > oldSize)
			{
				return 2147483587;
			}
			return HashHelpers.GetPrime(num);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002EF5 File Offset: 0x000010F5
		public static ulong GetFastModMultiplier(uint divisor)
		{
			return ulong.MaxValue / (ulong)divisor + 1UL;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EFF File Offset: 0x000010FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint FastMod(uint value, uint divisor, ulong multiplier)
		{
			return (uint)(((multiplier * (ulong)value >> 32) + 1UL) * (ulong)divisor >> 32);
		}

		// Token: 0x0400001A RID: 26
		public const uint HashCollisionThreshold = 100U;

		// Token: 0x0400001B RID: 27
		public const int MaxPrimeArrayLength = 2147483587;

		// Token: 0x0400001C RID: 28
		public const int HashPrime = 101;
	}
}
