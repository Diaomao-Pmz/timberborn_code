using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.Navigation;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001D RID: 29
	public class WalkerSpeedManager : TickableComponent, IAwakableComponent
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003ABF File Offset: 0x00001CBF
		public WalkerSpeedManager(IThreadSafeWaterMap threadSafeWaterMap, BonusTypeSpecService bonusTypeSpecService)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._bonusTypeSpecService = bonusTypeSpecService;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._movementSpeedAffector = base.GetComponent<IMovementSpeedAffector>();
			base.GetComponents<IWaterPenaltyModifier>(this._waterPenaltyModifiers);
			this._walkerSpeedManagerSpec = base.GetComponent<WalkerSpeedManagerSpec>();
			this._minWalkingSpeedMultiplier = this._bonusTypeSpecService.GetSpec(WalkerSpeedManager.MovementBonusId).MinimumValue;
			this._minimumSpeed = this._walkerSpeedManagerSpec.BaseSlowedSpeed * this._minWalkingSpeedMultiplier;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B50 File Offset: 0x00001D50
		public override void Tick()
		{
			IMovementSpeedAffector movementSpeedAffector = this._movementSpeedAffector;
			this._baseSpeed = ((movementSpeedAffector != null && movementSpeedAffector.IsMovementSlowed) ? this._walkerSpeedManagerSpec.BaseSlowedSpeed : this._walkerSpeedManagerSpec.BaseWalkingSpeed);
			this._bonusMultiplier = this._bonusManager.Multiplier(WalkerSpeedManager.MovementBonusId);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003BA7 File Offset: 0x00001DA7
		public float GetWalkerBaseSpeed()
		{
			return this._baseSpeed * this._bonusMultiplier;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public float GetWalkerSpeedAtCurrentPosition()
		{
			Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(base.Transform.position);
			if (this._threadSafeWaterMap.CellIsUnderwater(coordinates))
			{
				float num = 1f;
				foreach (IWaterPenaltyModifier waterPenaltyModifier in this._waterPenaltyModifiers)
				{
					num *= waterPenaltyModifier.WaterPenaltyModifier;
				}
				if (num > 0f)
				{
					float num2 = Mathf.Max(this._bonusMultiplier - WalkerSpeedManager.SwimmingPenalty, this._minWalkingSpeedMultiplier);
					return Mathf.Max(this._baseSpeed * num2 * num, this._minimumSpeed);
				}
			}
			return this.GetWalkerBaseSpeed();
		}

		// Token: 0x0400005A RID: 90
		public static readonly string MovementBonusId = "MovementSpeed";

		// Token: 0x0400005B RID: 91
		public static readonly float SwimmingPenalty = 0.3f;

		// Token: 0x0400005C RID: 92
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400005D RID: 93
		public readonly BonusTypeSpecService _bonusTypeSpecService;

		// Token: 0x0400005E RID: 94
		public BonusManager _bonusManager;

		// Token: 0x0400005F RID: 95
		public IMovementSpeedAffector _movementSpeedAffector;

		// Token: 0x04000060 RID: 96
		public readonly List<IWaterPenaltyModifier> _waterPenaltyModifiers = new List<IWaterPenaltyModifier>();

		// Token: 0x04000061 RID: 97
		public WalkerSpeedManagerSpec _walkerSpeedManagerSpec;

		// Token: 0x04000062 RID: 98
		public float _minWalkingSpeedMultiplier;

		// Token: 0x04000063 RID: 99
		public float _baseSpeed;

		// Token: 0x04000064 RID: 100
		public float _minimumSpeed;

		// Token: 0x04000065 RID: 101
		public float _bonusMultiplier;
	}
}
