using System;
using Timberborn.GameSaveRuntimeSystem;

namespace Timberborn.Autosaving
{
	// Token: 0x02000007 RID: 7
	public class AutosaveEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public bool Successful { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public GameSaverException Exception { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public AutosaveEvent(bool successful, GameSaverException exception)
		{
			this.Successful = successful;
			this.Exception = exception;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public static AutosaveEvent CreateSuccess()
		{
			return new AutosaveEvent(true, null);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000212D File Offset: 0x0000032D
		public static AutosaveEvent CreateFailure(GameSaverException exception)
		{
			return new AutosaveEvent(false, exception);
		}
	}
}
