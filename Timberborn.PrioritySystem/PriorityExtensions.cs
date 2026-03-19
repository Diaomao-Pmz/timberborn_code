using System;

namespace Timberborn.PrioritySystem
{
	// Token: 0x02000009 RID: 9
	public static class PriorityExtensions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000213C File Offset: 0x0000033C
		public static string GetLocKey(this Priority priority)
		{
			string result;
			switch (priority)
			{
			case Priority.VeryLow:
				result = "Priorities.VeryLow";
				break;
			case Priority.Low:
				result = "Priorities.Low";
				break;
			case Priority.Normal:
				result = "Priorities.Normal";
				break;
			case Priority.High:
				result = "Priorities.High";
				break;
			case Priority.VeryHigh:
				result = "Priorities.VeryHigh";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}
	}
}
