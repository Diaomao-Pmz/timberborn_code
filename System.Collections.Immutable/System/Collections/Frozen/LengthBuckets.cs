using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006C RID: 108
	internal static class LengthBuckets
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000D38C File Offset: 0x0000B58C
		[NullableContext(1)]
		[return: Nullable(2)]
		internal static int[] CreateLengthBucketsArrayIfAppropriate(string[] keys, IEqualityComparer<string> comparer, int minLength, int maxLength)
		{
			int num = maxLength - minLength + 1;
			if (keys.Length / num > 5)
			{
				return null;
			}
			int num2 = num * 5;
			if (num2 > 2147483591)
			{
				return null;
			}
			int[] array = ArrayPool<int>.Shared.Rent(num2);
			MemoryExtensions.AsSpan<int>(array, 0, num2).Fill(-1);
			int num3 = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				int num4 = (keys[i].Length - minLength) * 5;
				int num5 = num4 + 5;
				int j;
				for (j = num4; j < num5; j++)
				{
					ref int ptr = ref array[j];
					if (ptr < 0)
					{
						if (j == num4)
						{
							num3++;
						}
						ptr = i;
						break;
					}
				}
				if (j == num5)
				{
					ArrayPool<int>.Shared.Return(array, false);
					return null;
				}
			}
			if ((double)num3 / (double)num < 0.2)
			{
				ArrayPool<int>.Shared.Return(array, false);
				return null;
			}
			int[] result = MemoryExtensions.AsSpan<int>(array, 0, num2).ToArray();
			ArrayPool<int>.Shared.Return(array, false);
			return result;
		}

		// Token: 0x04000078 RID: 120
		internal const int MaxPerLength = 5;

		// Token: 0x04000079 RID: 121
		private const double EmptyLengthsRatio = 0.2;
	}
}
