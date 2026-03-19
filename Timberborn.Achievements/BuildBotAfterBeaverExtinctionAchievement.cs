using System;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.BotUpkeep;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000014 RID: 20
	public class BuildBotAfterBeaverExtinctionAchievement : Achievement
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002C4A File Offset: 0x00000E4A
		public BuildBotAfterBeaverExtinctionAchievement(EventBus eventBus, BeaverPopulation beaverPopulation)
		{
			this._eventBus = eventBus;
			this._beaverPopulation = beaverPopulation;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C60 File Offset: 0x00000E60
		public override string Id
		{
			get
			{
				return "BUILD_BOT_AFTER_BEAVER_EXTINCTION";
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C67 File Offset: 0x00000E67
		[OnEvent]
		public void OnBotManufactured(BotManufacturedEvent botManufacturedEvent)
		{
			if (this._beaverPopulation.NumberOfBeavers == 0)
			{
				base.Unlock();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C7C File Offset: 0x00000E7C
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C8A File Offset: 0x00000E8A
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public readonly BeaverPopulation _beaverPopulation;
	}
}
