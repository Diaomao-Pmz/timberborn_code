using System;

namespace Timberborn.Persistence
{
	// Token: 0x0200000F RID: 15
	public class ObsoleteValueException : Exception
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000029C8 File Offset: 0x00000BC8
		public ObsoleteValueException()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000029D0 File Offset: 0x00000BD0
		public ObsoleteValueException(string message) : base(message)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000029D9 File Offset: 0x00000BD9
		public ObsoleteValueException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
