using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x02000009 RID: 9
	public class LivingWaterNaturalResource : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IDyingProgressProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001E RID: 30 RVA: 0x00002450 File Offset: 0x00000650
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x00002488 File Offset: 0x00000688
		public event EventHandler StartedDying;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000020 RID: 32 RVA: 0x000024C0 File Offset: 0x000006C0
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x000024F8 File Offset: 0x000006F8
		public event EventHandler StoppedDying;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000252D File Offset: 0x0000072D
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002535 File Offset: 0x00000735
		public bool DeathByFlooding { get; private set; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000253E File Offset: 0x0000073E
		public LivingWaterNaturalResource(ITimeTriggerFactory timeTriggerFactory, IRandomNumberGenerator randomNumberGenerator)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002554 File Offset: 0x00000754
		public DyingProgress DyingProgress
		{
			get
			{
				return DyingProgress.Create(this._timeTrigger);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002564 File Offset: 0x00000764
		public void Awake()
		{
			this._floodableNaturalResourceSpec = base.GetComponent<FloodableNaturalResourceSpec>();
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._livingNaturalResource.Died += delegate(object _, EventArgs _)
			{
				this.StopDying();
			};
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this._livingNaturalResource.Die), this.GenerateRandomDaysToDie());
			LivingWaterObject component = base.GetComponent<LivingWaterObject>();
			component.WaterNeedsUnmet += delegate(object _, WaterNeedsUnmetEventArgs e)
			{
				this.StartDying(e.Flooded);
			};
			component.WaterNeedsMet += delegate(object _, EventArgs _)
			{
				this.StopDying();
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F1 File Offset: 0x000007F1
		public void DeleteEntity()
		{
			this._timeTrigger.Reset();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002600 File Offset: 0x00000800
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress != 0f || this.DeathByFlooding)
			{
				IObjectSaver component = entitySaver.GetComponent(LivingWaterNaturalResource.LivingWaterNaturalResourceKey);
				component.Set(LivingWaterNaturalResource.DyingProgressKey, this._timeTrigger.Progress);
				component.Set(LivingWaterNaturalResource.DeathByFloodingKey, this.DeathByFlooding);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002658 File Offset: 0x00000858
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(LivingWaterNaturalResource.LivingWaterNaturalResourceKey, out objectLoader))
			{
				this.DeathByFlooding = (objectLoader.Has<bool>(LivingWaterNaturalResource.DeathByFloodingKey) && objectLoader.Get(LivingWaterNaturalResource.DeathByFloodingKey));
				this._timeTrigger.FastForwardProgress(objectLoader.Get(LivingWaterNaturalResource.DyingProgressKey));
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026AB File Offset: 0x000008AB
		public float GenerateRandomDaysToDie()
		{
			return this._floodableNaturalResourceSpec.DaysToDie * this._randomNumberGenerator.Range(0.9f, 1.1f);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026CE File Offset: 0x000008CE
		public void StartDying(bool deathByFlooding)
		{
			if (!this._livingNaturalResource.IsDead)
			{
				this._timeTrigger.Resume();
				this.DeathByFlooding = deathByFlooding;
				EventHandler startedDying = this.StartedDying;
				if (startedDying == null)
				{
					return;
				}
				startedDying(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002705 File Offset: 0x00000905
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

		// Token: 0x0400000E RID: 14
		public static readonly ComponentKey LivingWaterNaturalResourceKey = new ComponentKey("LivingWaterNaturalResource");

		// Token: 0x0400000F RID: 15
		public static readonly PropertyKey<float> DyingProgressKey = new PropertyKey<float>("DyingProgress");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<bool> DeathByFloodingKey = new PropertyKey<bool>("DeathByFlooding");

		// Token: 0x04000014 RID: 20
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x04000015 RID: 21
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000016 RID: 22
		public FloodableNaturalResourceSpec _floodableNaturalResourceSpec;

		// Token: 0x04000017 RID: 23
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000018 RID: 24
		public ITimeTrigger _timeTrigger;
	}
}
