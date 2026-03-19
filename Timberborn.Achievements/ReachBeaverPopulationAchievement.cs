using System;
using Timberborn.AchievementSystem;
using Timberborn.Beavers;
using Timberborn.Characters;
using Timberborn.SingletonSystem;

namespace Timberborn.Achievements
{
	// Token: 0x02000045 RID: 69
	public abstract class ReachBeaverPopulationAchievement : Achievement
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000046E8 File Offset: 0x000028E8
		public ReachBeaverPopulationAchievement(BeaverPopulation beaverPopulation, EventBus eventBus, int threshold)
		{
			this._beaverPopulation = beaverPopulation;
			this._eventBus = eventBus;
			this._threshold = threshold;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004705 File Offset: 0x00002905
		public override string Id
		{
			get
			{
				return string.Format("REACH_POPULATION_OF_{0}_BEAVERS", this._threshold);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000471C File Offset: 0x0000291C
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			this.ValidatePopulation();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004724 File Offset: 0x00002924
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
			this.ValidatePopulation();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004738 File Offset: 0x00002938
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004746 File Offset: 0x00002946
		public void ValidatePopulation()
		{
			if (this._beaverPopulation.NumberOfBeavers >= this._threshold)
			{
				base.Unlock();
			}
		}

		// Token: 0x04000093 RID: 147
		public readonly BeaverPopulation _beaverPopulation;

		// Token: 0x04000094 RID: 148
		public readonly EventBus _eventBus;

		// Token: 0x04000095 RID: 149
		public readonly int _threshold;
	}
}
