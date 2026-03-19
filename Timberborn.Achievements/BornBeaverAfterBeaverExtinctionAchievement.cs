using System;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.GameFactionSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200000E RID: 14
	public class BornBeaverAfterBeaverExtinctionAchievement : Achievement
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002A5A File Offset: 0x00000C5A
		public BornBeaverAfterBeaverExtinctionAchievement(EventBus eventBus, BeaverPopulation beaverPopulation, FactionService factionService)
		{
			this._eventBus = eventBus;
			this._beaverPopulation = beaverPopulation;
			this._factionService = factionService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A77 File Offset: 0x00000C77
		public override string Id
		{
			get
			{
				return "BORN_BEAVER_AFTER_BEAVER_EXTINCTION";
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A7E File Offset: 0x00000C7E
		[OnEvent]
		public void OnBeaverBorn(BeaverBornEvent beaverBornEvent)
		{
			if (this._beaverPopulation.NumberOfBeavers == 0)
			{
				base.Unlock();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A93 File Offset: 0x00000C93
		public override void EnableInternal()
		{
			if (this._factionService.Current.Id == AchievementHelper.IronTeeth)
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002ABD File Offset: 0x00000CBD
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0400001D RID: 29
		public readonly EventBus _eventBus;

		// Token: 0x0400001E RID: 30
		public readonly BeaverPopulation _beaverPopulation;

		// Token: 0x0400001F RID: 31
		public readonly FactionService _factionService;
	}
}
