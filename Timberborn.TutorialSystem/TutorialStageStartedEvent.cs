using System;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200001A RID: 26
	public class TutorialStageStartedEvent
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000322D File Offset: 0x0000142D
		public string TutorialId { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003235 File Offset: 0x00001435
		public TutorialStage TutorialStage { get; }

		// Token: 0x06000086 RID: 134 RVA: 0x0000323D File Offset: 0x0000143D
		public TutorialStageStartedEvent(string tutorialId, TutorialStage tutorialStage)
		{
			this.TutorialId = tutorialId;
			this.TutorialStage = tutorialStage;
		}
	}
}
