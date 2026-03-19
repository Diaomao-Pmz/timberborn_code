using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Growing;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Gathering
{
	// Token: 0x0200000C RID: 12
	public class GatherableYieldGrower : BaseComponent, IAwakableComponent, IStartableComponent, IDeletableEntity, IPersistentEntity
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002791 File Offset: 0x00000991
		public GatherableYieldGrower(ITimeTriggerFactory timeTriggerFactory)
		{
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027A0 File Offset: 0x000009A0
		public float GrowthProgress
		{
			get
			{
				return this._timeTrigger.Progress;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027B0 File Offset: 0x000009B0
		public void Awake()
		{
			this._gatherable = base.GetComponent<Gatherable>();
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._dyingNaturalResource = base.GetComponent<DyingNaturalResource>();
			this._growable = base.GetComponent<Growable>();
			this._timeTrigger = this._timeTriggerFactory.Create(delegate
			{
				this._gatherable.Yielder.ResetYield();
			}, this._gatherable.YieldGrowthTimeInDays);
			this._gatherable.Gathered += delegate(object _, EventArgs _)
			{
				this.RestartGrowth();
			};
			this._livingNaturalResource.Died += delegate(object _, EventArgs _)
			{
				this.RemoveYield();
			};
			this._livingNaturalResource.ReversedDeath += delegate(object _, EventArgs _)
			{
				this.ReverseDeath();
			};
			this._dyingNaturalResource.StartedDying += delegate(object _, EventArgs _)
			{
				this.PauseGrowth();
			};
			this._dyingNaturalResource.StoppedDying += delegate(object _, EventArgs _)
			{
				this.ResumeGrowth();
			};
			this._growable.HasGrown += delegate(object _, EventArgs _)
			{
				this.ResumeGrowth();
			};
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000289F File Offset: 0x00000A9F
		public void Start()
		{
			this.ResumeGrowth();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A7 File Offset: 0x00000AA7
		public void DeleteEntity()
		{
			this.PauseGrowth();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028AF File Offset: 0x00000AAF
		public void FastForwardGrowth(float progress)
		{
			this._timeTrigger.FastForwardProgress(progress);
			this._progressWhenDied = progress;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028C4 File Offset: 0x00000AC4
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress != 0f)
			{
				entitySaver.GetComponent(GatherableYieldGrower.GatherableYieldGrowerKey).Set(GatherableYieldGrower.GrowthProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028F8 File Offset: 0x00000AF8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(GatherableYieldGrower.GatherableYieldGrowerKey, out objectLoader))
			{
				this.FastForwardGrowth(objectLoader.Get(GatherableYieldGrower.GrowthProgressKey));
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002928 File Offset: 0x00000B28
		public bool GrowthIsBlocked
		{
			get
			{
				return !this._growable.IsGrown || this._livingNaturalResource.IsDead || this._dyingNaturalResource.IsDying || this._gatherable.Yielder.IsYielding || !this._gatherable.UsableWithCurrentFeatureToggles;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000297E File Offset: 0x00000B7E
		public void RestartGrowth()
		{
			this._timeTrigger.Reset();
			this.ResumeGrowth();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002991 File Offset: 0x00000B91
		public void ResumeGrowth()
		{
			if (!this.GrowthIsBlocked)
			{
				this._timeTrigger.Resume();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029A6 File Offset: 0x00000BA6
		public void PauseGrowth()
		{
			this._timeTrigger.Pause();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029B3 File Offset: 0x00000BB3
		public void RemoveYield()
		{
			if (this._timeTrigger.InProgress)
			{
				this._progressWhenDied = this._timeTrigger.Progress;
			}
			this._timeTrigger.Reset();
			this._gatherable.Yielder.RemoveRemainingYield();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029EE File Offset: 0x00000BEE
		public void ReverseDeath()
		{
			if (this._growable.IsGrown)
			{
				this.FastForwardGrowth(this._progressWhenDied);
			}
		}

		// Token: 0x04000019 RID: 25
		public static readonly ComponentKey GatherableYieldGrowerKey = new ComponentKey("GatherableYieldGrower");

		// Token: 0x0400001A RID: 26
		public static readonly PropertyKey<float> GrowthProgressKey = new PropertyKey<float>("GrowthProgress");

		// Token: 0x0400001B RID: 27
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400001C RID: 28
		public Gatherable _gatherable;

		// Token: 0x0400001D RID: 29
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400001E RID: 30
		public DyingNaturalResource _dyingNaturalResource;

		// Token: 0x0400001F RID: 31
		public Growable _growable;

		// Token: 0x04000020 RID: 32
		public ITimeTrigger _timeTrigger;

		// Token: 0x04000021 RID: 33
		public float _progressWhenDied;
	}
}
