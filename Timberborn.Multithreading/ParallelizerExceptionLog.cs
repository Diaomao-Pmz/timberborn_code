using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000010 RID: 16
	public class ParallelizerExceptionLog
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029D4 File Offset: 0x00000BD4
		public Exception Exception { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000029DC File Offset: 0x00000BDC
		public string ThreadName { get; }

		// Token: 0x0600004F RID: 79 RVA: 0x000029E4 File Offset: 0x00000BE4
		public ParallelizerExceptionLog(Exception exception, string threadName)
		{
			this.Exception = exception;
			this.ThreadName = threadName;
		}
	}
}
