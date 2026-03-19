using System;
using System.Collections.Immutable;
using System.Text;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000F RID: 15
	public class ParallelizerException : Exception
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002936 File Offset: 0x00000B36
		public ParallelizerException()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000293E File Offset: 0x00000B3E
		public ParallelizerException(string message) : base(message)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002947 File Offset: 0x00000B47
		public ParallelizerException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002954 File Offset: 0x00000B54
		public static ParallelizerException From(ImmutableArray<ParallelizerExceptionLog> parallelizerExceptionLogs)
		{
			int length = parallelizerExceptionLogs.Length;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				ParallelizerExceptionLog parallelizerExceptionLog = parallelizerExceptionLogs[i];
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(string.Format("Exception {0}/{1} thrown by {2}:", i + 1, length, parallelizerExceptionLog.ThreadName));
				stringBuilder.AppendLine(parallelizerExceptionLog.Exception.ToString());
			}
			return new ParallelizerException(stringBuilder.ToString());
		}
	}
}
