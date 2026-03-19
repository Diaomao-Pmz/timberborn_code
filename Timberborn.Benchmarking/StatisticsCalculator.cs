using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.Benchmarking
{
	// Token: 0x02000015 RID: 21
	public class StatisticsCalculator
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000036C0 File Offset: 0x000018C0
		public float Median(IEnumerable<float> numbers)
		{
			List<float> list = StatisticsCalculator.OrderedList(numbers);
			if (list.IsEmpty<float>())
			{
				throw new ArgumentException("Can't calculate median of an empty sequence");
			}
			if (!StatisticsCalculator.HasOddNumberOfElements<float>(list))
			{
				return StatisticsCalculator.GetAverageOfTwoMiddleElements(list);
			}
			return StatisticsCalculator.GetMiddleElement<float>(list);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000036FC File Offset: 0x000018FC
		public float Percentile(IEnumerable<float> numbers, float percentile)
		{
			if (percentile < 0f || percentile > 100f)
			{
				throw new ArgumentException("Percentile should be a number between 0 and 100");
			}
			List<float> list = StatisticsCalculator.OrderedList(numbers);
			if (list.IsEmpty<float>())
			{
				throw new ArgumentException("Can't calculate percentile of an empty sequence");
			}
			double num = (double)(percentile / 100f);
			int num2 = list.Count - 1;
			int index = (int)Math.Round(num * (double)((float)num2));
			return list[index];
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003760 File Offset: 0x00001960
		public static List<float> OrderedList(IEnumerable<float> numbers)
		{
			return (from number in numbers
			orderby number
			select number).ToList<float>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000378C File Offset: 0x0000198C
		public static bool HasOddNumberOfElements<T>(IReadOnlyCollection<T> collection)
		{
			return collection.Count % 2 == 1;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003799 File Offset: 0x00001999
		public static T GetMiddleElement<T>(IReadOnlyList<T> list)
		{
			return list[list.Count / 2];
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000037AC File Offset: 0x000019AC
		public static float GetAverageOfTwoMiddleElements(IReadOnlyList<float> orderedNumbers)
		{
			int num = orderedNumbers.Count / 2;
			float num2 = orderedNumbers[num];
			int index = num - 1;
			return (orderedNumbers[index] + num2) / 2f;
		}
	}
}
