using System;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x0200004D RID: 77
	internal static class SecureObjectPool
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00009BCC File Offset: 0x00007DCC
		internal static int NewId()
		{
			int num;
			do
			{
				num = Interlocked.Increment(ref SecureObjectPool.s_poolUserIdCounter);
			}
			while (num == -1);
			return num;
		}

		// Token: 0x0400004E RID: 78
		private static int s_poolUserIdCounter;

		// Token: 0x0400004F RID: 79
		internal const int UnassignedId = -1;
	}
}
