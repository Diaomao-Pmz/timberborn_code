using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;
using Timberborn.Yielding;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000019 RID: 25
	public class YieldRemoverNeedApplier : BaseComponent, IAwakableComponent, IPersistentEntity, IProbabilityGroupProvider
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x000037B1 File Offset: 0x000019B1
		public YieldRemoverNeedApplier(EffectProbabilityService effectProbabilityService)
		{
			this._effectProbabilityService = effectProbabilityService;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000037C0 File Offset: 0x000019C0
		public string ProbabilityGroupId
		{
			get
			{
				return "YieldRemoverNeedApplier";
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000037C8 File Offset: 0x000019C8
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._yielderRemover = base.GetComponent<YielderRemover>();
			this._worker = base.GetComponent<Worker>();
			this._worker.GotEmployed += this.OnGotEmployed;
			this._worker.GotUnemployed += this.OnGotUnemployed;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003827 File Offset: 0x00001A27
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(YieldRemoverNeedApplier.YieldRemoverNeedApplierKey).Set(YieldRemoverNeedApplier.AttemptCounterKey, this._attemptCounter);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003844 File Offset: 0x00001A44
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(YieldRemoverNeedApplier.YieldRemoverNeedApplierKey);
			this._attemptCounter = component.Get(YieldRemoverNeedApplier.AttemptCounterKey);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003870 File Offset: 0x00001A70
		public void OnGotEmployed(object sender, EventArgs e)
		{
			YieldRemoverWorkplaceEffectsSpec component = this._worker.Workplace.GetComponent<YieldRemoverWorkplaceEffectsSpec>();
			if (component != null)
			{
				this.Enable(component);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003898 File Offset: 0x00001A98
		public void OnGotUnemployed(object sender, EventArgs e)
		{
			this.Disable();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000038A0 File Offset: 0x00001AA0
		public void Enable(YieldRemoverWorkplaceEffectsSpec yieldRemoverWorkplaceEffectsSpec)
		{
			if (!this._isEnabled)
			{
				this._yieldRemoverWorkplaceEffectsSpec = yieldRemoverWorkplaceEffectsSpec;
				this._yielderRemover.YieldReservationCompleted += this.OnYieldReservationCompleted;
				this._isEnabled = true;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000038CF File Offset: 0x00001ACF
		public void Disable()
		{
			if (this._isEnabled)
			{
				this._yieldRemoverWorkplaceEffectsSpec = null;
				this._yielderRemover.YieldReservationCompleted -= this.OnYieldReservationCompleted;
				this._isEnabled = false;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003900 File Offset: 0x00001B00
		public void OnYieldReservationCompleted(object sender, YieldReservationCompletedEventArgs e)
		{
			if (this._yieldRemoverWorkplaceEffectsSpec != null && e.Yield.GoodId == this._yieldRemoverWorkplaceEffectsSpec.YieldGoodId)
			{
				this.TryApplyNeed();
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003941 File Offset: 0x00001B41
		public void TryApplyNeed()
		{
			this._attemptCounter++;
			if (this.TryApplyAnyNeed())
			{
				this._attemptCounter = 0;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003960 File Offset: 0x00001B60
		public bool TryApplyAnyNeed()
		{
			bool result = false;
			foreach (NeedApplierEffectSpec needApplierEffectSpec in this._yieldRemoverWorkplaceEffectsSpec.Effects)
			{
				if (!this._needManager.NeedIsActive(needApplierEffectSpec.NeedId) && this._attemptCounter > this._yieldRemoverWorkplaceEffectsSpec.MinimumAttemptsThreshold && this._effectProbabilityService.CanApply(this, needApplierEffectSpec))
				{
					NeedManager needManager = this._needManager;
					InstantEffect instantEffect = needApplierEffectSpec.ToInstantEffect();
					needManager.ApplyEffect(instantEffect);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000047 RID: 71
		public static readonly ComponentKey YieldRemoverNeedApplierKey = new ComponentKey("YieldRemoverNeedApplier");

		// Token: 0x04000048 RID: 72
		public static readonly PropertyKey<int> AttemptCounterKey = new PropertyKey<int>("AttemptCounter");

		// Token: 0x04000049 RID: 73
		public readonly EffectProbabilityService _effectProbabilityService;

		// Token: 0x0400004A RID: 74
		public NeedManager _needManager;

		// Token: 0x0400004B RID: 75
		public YielderRemover _yielderRemover;

		// Token: 0x0400004C RID: 76
		public Worker _worker;

		// Token: 0x0400004D RID: 77
		public YieldRemoverWorkplaceEffectsSpec _yieldRemoverWorkplaceEffectsSpec;

		// Token: 0x0400004E RID: 78
		public int _attemptCounter;

		// Token: 0x0400004F RID: 79
		public bool _isEnabled;
	}
}
