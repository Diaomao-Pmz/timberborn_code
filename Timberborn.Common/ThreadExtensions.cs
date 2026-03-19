using System;
using System.Threading;

namespace Timberborn.Common
{
	// Token: 0x02000030 RID: 48
	public static class ThreadExtensions
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000038BC File Offset: 0x00001ABC
		public static string DisplayName(this Thread thread)
		{
			if (thread != null)
			{
				return thread.Name ?? thread.ManagedThreadId.ToString();
			}
			return string.Empty;
		}
	}
}
