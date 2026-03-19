using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Persistence;
using Timberborn.SoilContaminationSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NaturalResourcesContamination
{
	// Token: 0x02000007 RID: 7
	public class ContaminatedNaturalResource : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IDyingProgressProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		public event EventHandler StartedDying;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002170 File Offset: 0x00000370
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public event EventHandler StoppedDying;

		// Token: 0x0600000B RID: 11 RVA: 0x000021DD File Offset: 0x000003DD
		public ContaminatedNaturalResource(ITimeTriggerFactory timeTriggerFactory, IRandomNumberGenerator randomNumberGenerator)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F3 File Offset: 0x000003F3
		public DyingProgress DyingProgress
		{
			get
			{
				return DyingProgress.Create(this._timeTrigger);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002200 File Offset: 0x00000400
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._livingNaturalResource.Died += delegate(object _, EventArgs _)
			{
				this.StopDying();
			};
			ContaminatedObject component = base.GetComponent<ContaminatedObject>();
			component.EnteredContaminatedState += delegate(object _, EventArgs _)
			{
				this.StartDying();
			};
			component.ExitedContaminatedState += delegate(object _, EventArgs _)
			{
				this.StopDying();
			};
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this._livingNaturalResource.Die), this.GetDaysToDie());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002281 File Offset: 0x00000481
		public void DeleteEntity()
		{
			this._timeTrigger.Reset();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000228E File Offset: 0x0000048E
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress != 0f)
			{
				entitySaver.GetComponent(ContaminatedNaturalResource.ContaminatedNaturalResourceKey).Set(ContaminatedNaturalResource.DyingProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022C4 File Offset: 0x000004C4
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(ContaminatedNaturalResource.ContaminatedNaturalResourceKey, out objectLoader))
			{
				this._timeTrigger.FastForwardProgress(objectLoader.Get(ContaminatedNaturalResource.DyingProgressKey));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F6 File Offset: 0x000004F6
		public float GetDaysToDie()
		{
			return this._randomNumberGenerator.Range(ContaminatedNaturalResource.MinDaysToDie, ContaminatedNaturalResource.MaxDaysToDie);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000230D File Offset: 0x0000050D
		public void StartDying()
		{
			if (!this._livingNaturalResource.IsDead)
			{
				this._timeTrigger.Resume();
				EventHandler startedDying = this.StartedDying;
				if (startedDying == null)
				{
					return;
				}
				startedDying(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000233D File Offset: 0x0000053D
		public void StopDying()
		{
			if (this._livingNaturalResource.IsDead)
			{
				this._timeTrigger.Pause();
			}
			else
			{
				this._timeTrigger.Reset();
			}
			EventHandler stoppedDying = this.StoppedDying;
			if (stoppedDying == null)
			{
				return;
			}
			stoppedDying(this, EventArgs.Empty);
		}

		// Token: 0x04000008 RID: 8
		public static readonly float MinDaysToDie = 0.2f;

		// Token: 0x04000009 RID: 9
		public static readonly float MaxDaysToDie = 0.3f;

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey ContaminatedNaturalResourceKey = new ComponentKey("ContaminatedNaturalResource");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> DyingProgressKey = new PropertyKey<float>("DyingProgress");

		// Token: 0x0400000E RID: 14
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400000F RID: 15
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000010 RID: 16
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000011 RID: 17
		public ITimeTrigger _timeTrigger;
	}
}
