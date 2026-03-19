using System;

namespace Timberborn.TutorialSystemUI
{
	// Token: 0x02000009 RID: 9
	public class TutorialHeaderClickedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022E5 File Offset: 0x000004E5
		public string TutorialId { get; }

		// Token: 0x06000016 RID: 22 RVA: 0x000022ED File Offset: 0x000004ED
		public TutorialHeaderClickedEvent(string tutorialId)
		{
			this.TutorialId = tutorialId;
		}
	}
}
