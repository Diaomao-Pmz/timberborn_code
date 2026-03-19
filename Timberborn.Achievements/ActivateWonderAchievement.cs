using System;
using Timberborn.AchievementSystem;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;
using Timberborn.Wonders;

namespace Timberborn.Achievements
{
	// Token: 0x02000007 RID: 7
	public abstract class ActivateWonderAchievement : Achievement
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000025E8 File Offset: 0x000007E8
		public ActivateWonderAchievement(EventBus eventBus, FactionService factionService, string faction)
		{
			this._eventBus = eventBus;
			this._factionService = factionService;
			this._faction = faction;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002605 File Offset: 0x00000805
		public override string Id
		{
			get
			{
				return "ACTIVATE_WONDER_" + this._faction.ToUpperInvariant();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000261C File Offset: 0x0000081C
		[OnEvent]
		public void OnWonderActivated(WonderActivatedEvent wonderActivatedEvent)
		{
			base.Unlock();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002624 File Offset: 0x00000824
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == this._faction)
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000264F File Offset: 0x0000084F
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000009 RID: 9
		public readonly EventBus _eventBus;

		// Token: 0x0400000A RID: 10
		public readonly FactionService _factionService;

		// Token: 0x0400000B RID: 11
		public readonly string _faction;
	}
}
