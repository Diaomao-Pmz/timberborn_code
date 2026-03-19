using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200000D RID: 13
	public class TutorialConfiguration
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C8 File Offset: 0x000003C8
		public string TutorialId { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021D0 File Offset: 0x000003D0
		public ImmutableArray<string> RequiredTutorialIds { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021D8 File Offset: 0x000003D8
		public string SkipIfTutorialFinished { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021E0 File Offset: 0x000003E0
		public string DisplayName { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021E8 File Offset: 0x000003E8
		public int SortOrder { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000021F0 File Offset: 0x000003F0
		public ImmutableArray<TutorialStage> TutorialStages { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021F8 File Offset: 0x000003F8
		public bool KeepBlinking { get; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002200 File Offset: 0x00000400
		public TutorialConfiguration(TutorialSpec tutorialSpec, IEnumerable<TutorialStage> tutorialStages)
		{
			this.TutorialId = tutorialSpec.Id;
			this.RequiredTutorialIds = tutorialSpec.RequiredTutorialIds;
			this.SkipIfTutorialFinished = tutorialSpec.SkipIfTutorialFinished;
			this.DisplayName = tutorialSpec.DisplayName.Value;
			this.SortOrder = tutorialSpec.SortOrder;
			this.TutorialStages = tutorialStages.ToImmutableArray<TutorialStage>();
			this.KeepBlinking = tutorialSpec.HasSpec<BlinkingTutorialSpec>();
		}
	}
}
