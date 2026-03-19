using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.Effects;
using Timberborn.EnterableSystem;
using Timberborn.NeedSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.Workshops;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NeedApplication
{
	// Token: 0x02000017 RID: 23
	public class WorkshopRandomNeedApplier : BaseComponent, IAwakableComponent, IFinishedStateListener, IPersistentEntity, IProbabilityGroupProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600009C RID: 156 RVA: 0x000033B0 File Offset: 0x000015B0
		// (remove) Token: 0x0600009D RID: 157 RVA: 0x000033E8 File Offset: 0x000015E8
		public event EventHandler<NeedAppliedEventArgs> NeedApplied;

		// Token: 0x0600009E RID: 158 RVA: 0x0000341D File Offset: 0x0000161D
		public WorkshopRandomNeedApplier(EffectProbabilityService effectProbabilityService, IRandomNumberGenerator randomNumberGenerator, ITimeTriggerFactory timeTriggerFactory)
		{
			this._effectProbabilityService = effectProbabilityService;
			this._randomNumberGenerator = randomNumberGenerator;
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000343A File Offset: 0x0000163A
		public string ProbabilityGroupId
		{
			get
			{
				return "WorkshopRandomNeedApplier";
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003444 File Offset: 0x00001644
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._workshop = base.GetComponent<Workshop>();
			this._enterable = base.GetComponent<Enterable>();
			this._workshopRandomNeedApplierSpec = base.GetComponent<WorkshopRandomNeedApplierSpec>();
			this._applicationTrigger = this._timeTriggerFactory.Create(new Action(this.TryApplyNeeds), WorkshopRandomNeedApplier.CheckIntervalInDays);
			base.DisableComponent();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034A9 File Offset: 0x000016A9
		public void OnEnterFinishedState()
		{
			this._applicationTrigger.Resume();
			base.EnableComponent();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034BC File Offset: 0x000016BC
		public void OnExitFinishedState()
		{
			this._applicationTrigger.Pause();
			base.DisableComponent();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000034CF File Offset: 0x000016CF
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(WorkshopRandomNeedApplier.WorkshopRandomNeedApplierKey).Set(WorkshopRandomNeedApplier.ApplicationTriggerProgressKey, this._applicationTrigger.Progress);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000034F4 File Offset: 0x000016F4
		public void Load(IEntityLoader entityLoader)
		{
			float progress = entityLoader.GetComponent(WorkshopRandomNeedApplier.WorkshopRandomNeedApplierKey).Get(WorkshopRandomNeedApplier.ApplicationTriggerProgressKey);
			this._applicationTrigger.FastForwardProgress(progress);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003524 File Offset: 0x00001724
		public void TryApplyNeeds()
		{
			if (this._workshop.CurrentlyWorking)
			{
				for (int i = 0; i < this._workplace.AssignedWorkers.Count; i++)
				{
					Worker worker = this._workplace.AssignedWorkers[i];
					if (worker.GetComponent<Enterer>().CurrentBuilding == this._enterable)
					{
						this.TryApplyRandomNeedToWorker(worker);
					}
				}
			}
			this._applicationTrigger.Reset();
			this._applicationTrigger.Resume();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000035A4 File Offset: 0x000017A4
		public void TryApplyRandomNeedToWorker(Worker worker)
		{
			ImmutableArray<NeedApplierEffectSpec> effects = this._workshopRandomNeedApplierSpec.Effects;
			NeedApplierEffectSpec listElement = this._randomNumberGenerator.GetListElement<NeedApplierEffectSpec>(effects);
			if (this._effectProbabilityService.CanApply(this, listElement))
			{
				this.ApplyNeed(worker, listElement);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035E8 File Offset: 0x000017E8
		public void ApplyNeed(Worker worker, NeedApplierEffectSpec effectToApply)
		{
			NeedManager component = worker.GetComponent<NeedManager>();
			if (component && component.HasNeed(effectToApply.NeedId))
			{
				InstantEffect needEffect = effectToApply.ToInstantEffect();
				component.ApplyEffect(needEffect);
				EventHandler<NeedAppliedEventArgs> needApplied = this.NeedApplied;
				if (needApplied == null)
				{
					return;
				}
				needApplied(this, new NeedAppliedEventArgs(worker.GetComponent<Character>(), needEffect));
			}
		}

		// Token: 0x0400003A RID: 58
		public static readonly float CheckIntervalInDays = 0.041666668f;

		// Token: 0x0400003B RID: 59
		public static readonly ComponentKey WorkshopRandomNeedApplierKey = new ComponentKey("WorkshopRandomNeedApplier");

		// Token: 0x0400003C RID: 60
		public static readonly PropertyKey<float> ApplicationTriggerProgressKey = new PropertyKey<float>("ApplicationTriggerProgress");

		// Token: 0x0400003E RID: 62
		public readonly EffectProbabilityService _effectProbabilityService;

		// Token: 0x0400003F RID: 63
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000040 RID: 64
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x04000041 RID: 65
		public Workplace _workplace;

		// Token: 0x04000042 RID: 66
		public Workshop _workshop;

		// Token: 0x04000043 RID: 67
		public Enterable _enterable;

		// Token: 0x04000044 RID: 68
		public WorkshopRandomNeedApplierSpec _workshopRandomNeedApplierSpec;

		// Token: 0x04000045 RID: 69
		public ITimeTrigger _applicationTrigger;
	}
}
