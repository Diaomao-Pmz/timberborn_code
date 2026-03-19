using System;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.Characters;
using Timberborn.NeedSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x0200000C RID: 12
	public class BeaverDiesMiserableAchievement : Achievement
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002820 File Offset: 0x00000A20
		public BeaverDiesMiserableAchievement(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000282F File Offset: 0x00000A2F
		public override string Id
		{
			get
			{
				return "BEAVER_DIES_HUNGRY_THIRSTY_INJURED_SICK";
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002836 File Offset: 0x00000A36
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			this.CheckUnlockConditions(characterKilledEvent.Character);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002844 File Offset: 0x00000A44
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002852 File Offset: 0x00000A52
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002860 File Offset: 0x00000A60
		public void CheckUnlockConditions(Character character)
		{
			if (character.HasComponent<BeaverSpec>())
			{
				NeedManager component = character.GetComponent<NeedManager>();
				if (component.NeedIsActive(BeaverDiesMiserableAchievement.HungerNeedId) && component.NeedIsActive(BeaverDiesMiserableAchievement.ThirstNeedId) && component.NeedIsActive(BeaverDiesMiserableAchievement.InjuryNeedId) && component.NeedIsActive(BeaverDiesMiserableAchievement.ContaminationNeedId))
				{
					base.Unlock();
				}
			}
		}

		// Token: 0x04000014 RID: 20
		public static readonly string HungerNeedId = "Hunger";

		// Token: 0x04000015 RID: 21
		public static readonly string ThirstNeedId = "Thirst";

		// Token: 0x04000016 RID: 22
		public static readonly string InjuryNeedId = "Injury";

		// Token: 0x04000017 RID: 23
		public static readonly string ContaminationNeedId = "BadwaterContamination";

		// Token: 0x04000018 RID: 24
		public readonly EventBus _eventBus;
	}
}
