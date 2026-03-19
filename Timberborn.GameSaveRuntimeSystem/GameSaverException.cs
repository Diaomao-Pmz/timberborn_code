using System;

namespace Timberborn.GameSaveRuntimeSystem
{
	// Token: 0x02000007 RID: 7
	public class GameSaverException : Exception
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023EB File Offset: 0x000005EB
		public GameSaverException()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023F3 File Offset: 0x000005F3
		public GameSaverException(string message) : base(message)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023FC File Offset: 0x000005FC
		public GameSaverException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
