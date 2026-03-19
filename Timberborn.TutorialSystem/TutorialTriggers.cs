using System;
using System.Collections.Generic;
using Timberborn.FactionSystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200001D RID: 29
	public class TutorialTriggers : ITutorialTriggers, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000333B File Offset: 0x0000153B
		public TutorialTriggers(TutorialService tutorialService, FactionService factionService)
		{
			this._tutorialService = tutorialService;
			this._factionService = factionService;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000335C File Offset: 0x0000155C
		public void Load()
		{
			this._canTrigger = this._factionService.Current.HasSpec<StartingFactionSpec>();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003374 File Offset: 0x00001574
		public bool TriggerPending(string triggerId)
		{
			return this._canTrigger && !this._tutorialService.TutorialWasFinished(triggerId);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000338F File Offset: 0x0000158F
		public void AddTrigger(string triggerId)
		{
			this._pendingTriggers.Enqueue(triggerId);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033A0 File Offset: 0x000015A0
		public void Tick()
		{
			string triggerId;
			while (this._pendingTriggers.TryDequeue(ref triggerId))
			{
				this._tutorialService.AddTutorialTrigger(triggerId);
			}
		}

		// Token: 0x04000049 RID: 73
		public readonly TutorialService _tutorialService;

		// Token: 0x0400004A RID: 74
		public readonly FactionService _factionService;

		// Token: 0x0400004B RID: 75
		public readonly Queue<string> _pendingTriggers = new Queue<string>();

		// Token: 0x0400004C RID: 76
		public bool _canTrigger;
	}
}
