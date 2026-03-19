using System;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200002A RID: 42
	public static class NumericComparisonModeExtensions
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000059E8 File Offset: 0x00003BE8
		public static bool Evaluate(this NumericComparisonMode mode, int value, int reference)
		{
			bool result;
			switch (mode)
			{
			case NumericComparisonMode.Greater:
				result = (value > reference);
				break;
			case NumericComparisonMode.Less:
				result = (value < reference);
				break;
			case NumericComparisonMode.GreaterOrEqual:
				result = (value >= reference);
				break;
			case NumericComparisonMode.LessOrEqual:
				result = (value <= reference);
				break;
			case NumericComparisonMode.Equal:
				result = (value == reference);
				break;
			case NumericComparisonMode.NotEqual:
				result = (value != reference);
				break;
			default:
				throw new ArgumentOutOfRangeException("mode", mode, null);
			}
			return result;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005A5C File Offset: 0x00003C5C
		public static bool Evaluate(this NumericComparisonMode mode, float value, float reference)
		{
			bool result;
			switch (mode)
			{
			case NumericComparisonMode.Greater:
				result = (value > reference);
				break;
			case NumericComparisonMode.Less:
				result = (value < reference);
				break;
			case NumericComparisonMode.GreaterOrEqual:
				result = (value >= reference);
				break;
			case NumericComparisonMode.LessOrEqual:
				result = (value <= reference);
				break;
			case NumericComparisonMode.Equal:
				result = (value == reference);
				break;
			case NumericComparisonMode.NotEqual:
				result = (value != reference);
				break;
			default:
				throw new ArgumentOutOfRangeException("mode", mode, null);
			}
			return result;
		}
	}
}
