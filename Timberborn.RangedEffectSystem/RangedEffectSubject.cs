using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.NeedSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x02000016 RID: 22
	public class RangedEffectSubject : TickableComponent, IAwakableComponent
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003387 File Offset: 0x00001587
		public RangedEffectSubject(RangedEffectService rangedEffectService, IDayNightCycle dayNightCycle)
		{
			this._rangedEffectService = rangedEffectService;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000339D File Offset: 0x0000159D
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._enterer = base.GetComponent<Enterer>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033B7 File Offset: 0x000015B7
		public override void Tick()
		{
			this.ApplyEffects();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033C0 File Offset: 0x000015C0
		public void ApplyEffects()
		{
			ReadOnlyList<RangedEffect> affectingEffects = this.GetAffectingEffects();
			float fixedDeltaTimeInHours = this._dayNightCycle.FixedDeltaTimeInHours;
			foreach (RangedEffect rangedEffect in affectingEffects)
			{
				NeedManager needManager = this._needManager;
				ContinuousEffect continuousEffect = rangedEffect.ToContinuousEffect();
				needManager.ApplyEffect(continuousEffect, fixedDeltaTimeInHours);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003434 File Offset: 0x00001634
		public ReadOnlyList<RangedEffect> GetAffectingEffects()
		{
			if (this._enterer.IsInside)
			{
				return this._enterer.CurrentBuilding.GetComponent<RangedEffectsAffectingEnterable>().ActiveEffects;
			}
			Vector3Int value = CoordinateSystem.WorldToGridInt(base.Transform.position);
			return this._rangedEffectService.GetEffectsAffectingCoordinates(value.XY());
		}

		// Token: 0x0400003A RID: 58
		public readonly RangedEffectService _rangedEffectService;

		// Token: 0x0400003B RID: 59
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400003C RID: 60
		public NeedManager _needManager;

		// Token: 0x0400003D RID: 61
		public Enterer _enterer;
	}
}
