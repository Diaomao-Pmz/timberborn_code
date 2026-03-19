using System;
using Timberborn.AchievementSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Characters;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200001E RID: 30
	public class CureContaminatedBeaverAchievement : Achievement
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000034AC File Offset: 0x000016AC
		public CureContaminatedBeaverAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000034BB File Offset: 0x000016BB
		public override string Id
		{
			get
			{
				return "CURE_CONTAMINATED_BEAVER";
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000034C4 File Offset: 0x000016C4
		[OnEvent]
		public void OnContaminableContaminationChanged(ContaminableContaminationChangedEvent contaminableContaminationChangedEvent)
		{
			Contaminable contaminable = contaminableContaminationChangedEvent.Contaminable;
			if (!contaminable.IsContaminated && contaminable.GetComponent<Character>().Alive)
			{
				base.Unlock();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034F3 File Offset: 0x000016F3
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003501 File Offset: 0x00001701
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000049 RID: 73
		public readonly EventBus _eventBus;
	}
}
