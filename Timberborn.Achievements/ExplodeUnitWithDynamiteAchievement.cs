using System;
using Timberborn.AchievementSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Explosions;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000026 RID: 38
	public class ExplodeUnitWithDynamiteAchievement : Achievement
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000037D8 File Offset: 0x000019D8
		public ExplodeUnitWithDynamiteAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000037E7 File Offset: 0x000019E7
		public override string Id
		{
			get
			{
				return "EXPLODE_UNIT_WITH_DYNAMITE";
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000037EE File Offset: 0x000019EE
		[OnEvent]
		public void OnMortalDiedFromExplosionEvent(MortalDiedFromExplosionEvent mortalDiedFromExplosionEvent)
		{
			BaseComponent source = mortalDiedFromExplosionEvent.Source;
			if (((source != null) ? source.GetComponent<Dynamite>() : null) != null)
			{
				base.Unlock();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000380A File Offset: 0x00001A0A
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003818 File Offset: 0x00001A18
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000058 RID: 88
		public readonly EventBus _eventBus;
	}
}
