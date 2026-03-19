using System;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x0200000B RID: 11
	public static class ExecutorExtensions
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000272C File Offset: 0x0000092C
		public static string GetName(this IExecutor executor)
		{
			return executor.GetType().Name;
		}
	}
}
