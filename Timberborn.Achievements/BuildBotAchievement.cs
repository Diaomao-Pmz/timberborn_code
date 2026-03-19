using System;
using Timberborn.AchievementSystem;
using Timberborn.Bots;
using Timberborn.BotUpkeep;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000013 RID: 19
	public class BuildBotAchievement : Achievement
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002BFD File Offset: 0x00000DFD
		public BuildBotAchievement(EventBus eventBus, BotPopulation botPopulation)
		{
			this._eventBus = eventBus;
			this._botPopulation = botPopulation;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C13 File Offset: 0x00000E13
		public override string Id
		{
			get
			{
				return "BUILD_BOT";
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000261C File Offset: 0x0000081C
		[OnEvent]
		public void OnBotManufactured(BotManufacturedEvent botManufacturedEvent)
		{
			base.Unlock();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C1A File Offset: 0x00000E1A
		public override void EnableInternal()
		{
			if (this._botPopulation.BotCreated)
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C3C File Offset: 0x00000E3C
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000026 RID: 38
		public readonly EventBus _eventBus;

		// Token: 0x04000027 RID: 39
		public readonly BotPopulation _botPopulation;
	}
}
