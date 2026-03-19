using System;
using Timberborn.AchievementSystem;
using Timberborn.FactionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000051 RID: 81
	public class UnlockIronTeethAchievement : Achievement
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00004D16 File Offset: 0x00002F16
		public UnlockIronTeethAchievement(EventBus eventBus, FactionSpecService factionSpecService, FactionUnlockingService factionUnlockingService)
		{
			this._eventBus = eventBus;
			this._factionSpecService = factionSpecService;
			this._factionUnlockingService = factionUnlockingService;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004D33 File Offset: 0x00002F33
		public override string Id
		{
			get
			{
				return "UNLOCK_IRON_TEETH";
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004D3A File Offset: 0x00002F3A
		[OnEvent]
		public void OnFactionUnlocked(FactionUnlockedEvent factionUnlockedEvent)
		{
			if (this.IsFactionUnlocked())
			{
				base.Unlock();
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004D4A File Offset: 0x00002F4A
		public override void EnableInternal()
		{
			if (this.IsFactionUnlocked())
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004D67 File Offset: 0x00002F67
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004D78 File Offset: 0x00002F78
		public bool IsFactionUnlocked()
		{
			FactionSpec faction = this._factionSpecService.GetFaction(AchievementHelper.IronTeeth);
			return !this._factionUnlockingService.IsLocked(faction);
		}

		// Token: 0x040000B5 RID: 181
		public readonly EventBus _eventBus;

		// Token: 0x040000B6 RID: 182
		public readonly FactionSpecService _factionSpecService;

		// Token: 0x040000B7 RID: 183
		public readonly FactionUnlockingService _factionUnlockingService;
	}
}
