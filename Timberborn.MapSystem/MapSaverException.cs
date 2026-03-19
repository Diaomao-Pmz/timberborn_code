using System;

namespace Timberborn.MapSystem
{
	// Token: 0x02000006 RID: 6
	public class MapSaverException : Exception
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022A4 File Offset: 0x000004A4
		public MapSaverException()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022AC File Offset: 0x000004AC
		public MapSaverException(string message) : base(message)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B5 File Offset: 0x000004B5
		public MapSaverException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
