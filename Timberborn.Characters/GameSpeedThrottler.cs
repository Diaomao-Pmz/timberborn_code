using System;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.Characters
{
	// Token: 0x0200000E RID: 14
	public class GameSpeedThrottler : IPostLoadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000259F File Offset: 0x0000079F
		public GameSpeedThrottler(CharacterPopulation characterPopulation, EventBus eventBus, SpeedManager speedManager, ISpecService specService)
		{
			this._characterPopulation = characterPopulation;
			this._eventBus = eventBus;
			this._speedManager = speedManager;
			this._specService = specService;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025C4 File Offset: 0x000007C4
		public void PostLoad()
		{
			this._spec = this._specService.GetSingleSpec<GameSpeedThrottlerSpec>();
			this._eventBus.Register(this);
			this.ThrottleGameSpeed();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025E9 File Offset: 0x000007E9
		[OnEvent]
		public void OnCharacterCreated(CharacterCreatedEvent characterCreatedEvent)
		{
			this.ThrottleGameSpeed();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025E9 File Offset: 0x000007E9
		[OnEvent]
		public void OnCharacterKilled(CharacterKilledEvent characterKilledEvent)
		{
			this.ThrottleGameSpeed();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025F4 File Offset: 0x000007F4
		public void ThrottleGameSpeed()
		{
			float speedScale = this.PopulationToSpeedScale(this._characterPopulation.NumberOfCharacters);
			this._speedManager.ChangeSpeedScale(speedScale);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002620 File Offset: 0x00000820
		public float PopulationToSpeedScale(int population)
		{
			int num = Mathf.Clamp(population, this._spec.MinPopulation, this._spec.MaxPopulation);
			float num2 = Mathf.InverseLerp((float)this._spec.MinPopulation, (float)this._spec.MaxPopulation, (float)num);
			return Mathf.Lerp(this._spec.MinGameSpeedScale, this._spec.MaxGameSpeedScale, 1f - num2);
		}

		// Token: 0x0400001D RID: 29
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly SpeedManager _speedManager;

		// Token: 0x04000020 RID: 32
		public readonly ISpecService _specService;

		// Token: 0x04000021 RID: 33
		public GameSpeedThrottlerSpec _spec;
	}
}
