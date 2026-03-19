using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.BonusSystem
{
	// Token: 0x02000009 RID: 9
	public class BonusManager : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001F RID: 31 RVA: 0x00002424 File Offset: 0x00000624
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x0000245C File Offset: 0x0000065C
		public event EventHandler<BonusValueChangedEventArgs> BonusValueChanged;

		// Token: 0x06000021 RID: 33 RVA: 0x00002491 File Offset: 0x00000691
		public BonusManager(BonusTypeSpecService bonusTypeSpecService)
		{
			this._bonusTypeSpecService = bonusTypeSpecService;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024AB File Offset: 0x000006AB
		public void Awake()
		{
			this.InitializeBonuses();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024B4 File Offset: 0x000006B4
		public void AddBonuses(IEnumerable<BonusSpec> bonuses)
		{
			foreach (BonusSpec bonusSpec in bonuses)
			{
				this.AddBonus(bonusSpec.Id, bonusSpec.MultiplierDelta);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002508 File Offset: 0x00000708
		public void RemoveBonuses(IEnumerable<BonusSpec> bonuses)
		{
			foreach (BonusSpec bonusSpec in bonuses)
			{
				this.RemoveBonus(bonusSpec.Id, bonusSpec.MultiplierDelta);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		public void AddBonus(string bonusId, float multiplierDelta)
		{
			this._bonuses[bonusId].ModifyValue(multiplierDelta);
			this.NotifyBonusValueChanged(bonusId);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002577 File Offset: 0x00000777
		public void RemoveBonus(string bonusId, float multiplierDelta)
		{
			this._bonuses[bonusId].ModifyValue(-multiplierDelta);
			this.NotifyBonusValueChanged(bonusId);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002593 File Offset: 0x00000793
		public float Multiplier(string bonusType)
		{
			return this._bonuses[bonusType].ClampedValue;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025A8 File Offset: 0x000007A8
		public void InitializeBonuses()
		{
			foreach (string text in this._bonusTypeSpecService.BonusIds)
			{
				BonusTypeSpec spec = this._bonusTypeSpecService.GetSpec(text);
				this._bonuses[text] = new BonusManager.Bonus(1f, spec.MinimumValue, spec.MaximumValue);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002624 File Offset: 0x00000824
		public void NotifyBonusValueChanged(string bonusId)
		{
			EventHandler<BonusValueChangedEventArgs> bonusValueChanged = this.BonusValueChanged;
			if (bonusValueChanged == null)
			{
				return;
			}
			bonusValueChanged(this, new BonusValueChangedEventArgs(bonusId, this.Multiplier(bonusId)));
		}

		// Token: 0x0400000F RID: 15
		public readonly BonusTypeSpecService _bonusTypeSpecService;

		// Token: 0x04000010 RID: 16
		public readonly Dictionary<string, BonusManager.Bonus> _bonuses = new Dictionary<string, BonusManager.Bonus>();

		// Token: 0x0200000A RID: 10
		public class Bonus
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002644 File Offset: 0x00000844
			public Bonus(float value, float min, float max)
			{
				this._value = value;
				this._min = min;
				this._max = max;
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002661 File Offset: 0x00000861
			public float ClampedValue
			{
				get
				{
					return Mathf.Clamp(this._value, this._min, this._max);
				}
			}

			// Token: 0x0600002C RID: 44 RVA: 0x0000267A File Offset: 0x0000087A
			public void ModifyValue(float change)
			{
				this._value += change;
			}

			// Token: 0x04000011 RID: 17
			public readonly float _min;

			// Token: 0x04000012 RID: 18
			public readonly float _max;

			// Token: 0x04000013 RID: 19
			public float _value;
		}
	}
}
