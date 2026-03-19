using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Demolishing;
using Timberborn.Effects;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.ReservableSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NeedApplication
{
	// Token: 0x0200000A RID: 10
	public class DemolisherNeedApplier : BaseComponent, IAwakableComponent, IPersistentEntity, IProbabilityGroupProvider
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000026E9 File Offset: 0x000008E9
		public DemolisherNeedApplier(EffectProbabilityService effectProbabilityService, ITimeTriggerFactory timeTriggerFactory)
		{
			this._effectProbabilityService = effectProbabilityService;
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026FF File Offset: 0x000008FF
		public string ProbabilityGroupId
		{
			get
			{
				return "DemolisherNeedApplier";
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002708 File Offset: 0x00000908
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._demolisher = base.GetComponent<Demolisher>();
			this._demolisher.ReservedDemolishableChanged += this.OnReservedDemolishableChanged;
			this._demolishExecutor = base.GetComponent<DemolishExecutor>();
			this._applicationTrigger = this._timeTriggerFactory.Create(new Action(this.ApplyNeeds), DemolisherNeedApplier.CheckIntervalInDays);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002772 File Offset: 0x00000972
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(DemolisherNeedApplier.DemolisherNeedApplierKey).Set(DemolisherNeedApplier.ApplicationTriggerProgressKey, this._applicationTrigger.Progress);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002794 File Offset: 0x00000994
		[BackwardCompatible(2025, 8, 21, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(DemolisherNeedApplier.DemolisherNeedApplierKey, out objectLoader))
			{
				float progress = objectLoader.Get(DemolisherNeedApplier.ApplicationTriggerProgressKey);
				this._applicationTrigger.FastForwardProgress(progress);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027C8 File Offset: 0x000009C8
		public void OnReservedDemolishableChanged(object sender, Demolishable demolishable)
		{
			if (demolishable)
			{
				DemolishableEffectsSpec component = demolishable.GetComponent<DemolishableEffectsSpec>();
				if (component != null)
				{
					this.Enable(component);
					return;
				}
			}
			this.Disable();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027F8 File Offset: 0x000009F8
		public void Enable(DemolishableEffectsSpec demolishableEffectsSpec)
		{
			if (!this._isEnabled)
			{
				this._demolishableEffectsSpec = demolishableEffectsSpec;
				this._demolishExecutor.WorkStarted += this.OnWorkStarted;
				this._demolishExecutor.WorkFinished += this.OnWorkFinished;
				this._isEnabled = true;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000284C File Offset: 0x00000A4C
		public void Disable()
		{
			if (this._isEnabled)
			{
				this._demolishableEffectsSpec = null;
				this._demolishExecutor.WorkStarted -= this.OnWorkStarted;
				this._demolishExecutor.WorkFinished -= this.OnWorkFinished;
				this._applicationTrigger.Pause();
				this._isEnabled = false;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028A8 File Offset: 0x00000AA8
		public void OnWorkStarted(object sender, EventArgs e)
		{
			this._applicationTrigger.Resume();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028B5 File Offset: 0x00000AB5
		public void OnWorkFinished(object sender, WorkFinishedEventArgs e)
		{
			this._applicationTrigger.Pause();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void ApplyNeeds()
		{
			if (this._demolishableEffectsSpec != null)
			{
				foreach (NeedApplierEffectSpec needApplierEffectSpec in this._demolishableEffectsSpec.Effects)
				{
					if (!this._needManager.NeedIsActive(needApplierEffectSpec.NeedId) && this._effectProbabilityService.CanApply(this, needApplierEffectSpec))
					{
						NeedManager needManager = this._needManager;
						InstantEffect instantEffect = needApplierEffectSpec.ToInstantEffect();
						needManager.ApplyEffect(instantEffect);
					}
				}
				this._applicationTrigger.Reset();
				this._applicationTrigger.Resume();
			}
		}

		// Token: 0x04000016 RID: 22
		public static readonly float CheckIntervalInDays = 0.041666668f;

		// Token: 0x04000017 RID: 23
		public static readonly ComponentKey DemolisherNeedApplierKey = new ComponentKey("DemolisherNeedApplier");

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<float> ApplicationTriggerProgressKey = new PropertyKey<float>("ApplicationTriggerProgress");

		// Token: 0x04000019 RID: 25
		public readonly EffectProbabilityService _effectProbabilityService;

		// Token: 0x0400001A RID: 26
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400001B RID: 27
		public NeedManager _needManager;

		// Token: 0x0400001C RID: 28
		public Demolisher _demolisher;

		// Token: 0x0400001D RID: 29
		public DemolishExecutor _demolishExecutor;

		// Token: 0x0400001E RID: 30
		public DemolishableEffectsSpec _demolishableEffectsSpec;

		// Token: 0x0400001F RID: 31
		public ITimeTrigger _applicationTrigger;

		// Token: 0x04000020 RID: 32
		public bool _isEnabled;
	}
}
