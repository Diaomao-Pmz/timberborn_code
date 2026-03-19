using System;

namespace Timberborn.MainMenuSceneLoading
{
	// Token: 0x02000007 RID: 7
	public class PreMainMenuStartedEvent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002190 File Offset: 0x00000390
		public bool SkipAutoSave { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x00002198 File Offset: 0x00000398
		public PreMainMenuStartedEvent(bool skipAutoSave)
		{
			this.SkipAutoSave = skipAutoSave;
		}
	}
}
