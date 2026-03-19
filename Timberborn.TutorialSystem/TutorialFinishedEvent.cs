using System;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200000F RID: 15
	public class TutorialFinishedEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002283 File Offset: 0x00000483
		public string TutorialId { get; }

		// Token: 0x06000025 RID: 37 RVA: 0x0000228B File Offset: 0x0000048B
		public TutorialFinishedEvent(string tutorialId)
		{
			this.TutorialId = tutorialId;
		}
	}
}
