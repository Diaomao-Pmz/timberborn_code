using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.Persistence;
using Timberborn.SoilMoistureSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x0200000C RID: 12
	public class WateredNaturalResource : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IDyingProgressProvider
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002A20 File Offset: 0x00000C20
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x00002A58 File Offset: 0x00000C58
		public event EventHandler StartedDying;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000043 RID: 67 RVA: 0x00002A90 File Offset: 0x00000C90
		// (remove) Token: 0x06000044 RID: 68 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public event EventHandler StoppedDying;

		// Token: 0x06000045 RID: 69 RVA: 0x00002AFD File Offset: 0x00000CFD
		public WateredNaturalResource(ITimeTriggerFactory timeTriggerFactory, IRandomNumberGenerator randomNumberGenerator)
		{
			this._timeTriggerFactory = timeTriggerFactory;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B13 File Offset: 0x00000D13
		public DyingProgress DyingProgress
		{
			get
			{
				return DyingProgress.Create(this._timeTrigger);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._livingNaturalResource.Died += delegate(object _, EventArgs _)
			{
				this.StopDryingOut();
			};
			this._wateredNaturalResourceSpec = base.GetComponent<WateredNaturalResourceSpec>();
			DryObject component = base.GetComponent<DryObject>();
			component.EnteredDryState += delegate(object _, EventArgs _)
			{
				this.StartDryingOut();
			};
			component.ExitedDryState += delegate(object _, EventArgs _)
			{
				this.StopDryingOut();
			};
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this._livingNaturalResource.Die), this.GenerateRandomDaysToDry());
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BAD File Offset: 0x00000DAD
		public void DeleteEntity()
		{
			this._timeTrigger.Reset();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BBA File Offset: 0x00000DBA
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress != 0f)
			{
				entitySaver.GetComponent(WateredNaturalResource.WateredNaturalResourceKey).Set(WateredNaturalResource.DyingProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BF0 File Offset: 0x00000DF0
		[BackwardCompatible(2025, 2, 28, Compatibility.Map)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(WateredNaturalResource.WateredNaturalResourceKey, out objectLoader))
			{
				float progress = objectLoader.Has<float>(WateredNaturalResource.DyingProgressKey) ? objectLoader.Get(WateredNaturalResource.DyingProgressKey) : objectLoader.Get(new PropertyKey<float>("DryingProgress"));
				this._timeTrigger.FastForwardProgress(progress);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C43 File Offset: 0x00000E43
		public float GenerateRandomDaysToDry()
		{
			return this._wateredNaturalResourceSpec.DaysToDieDry * this._randomNumberGenerator.Range(0.9f, 1.1f);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C66 File Offset: 0x00000E66
		public void StartDryingOut()
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

		// Token: 0x0600004D RID: 77 RVA: 0x00002C96 File Offset: 0x00000E96
		public void StopDryingOut()
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

		// Token: 0x0400001F RID: 31
		public static readonly ComponentKey WateredNaturalResourceKey = new ComponentKey("WateredNaturalResource");

		// Token: 0x04000020 RID: 32
		public static readonly PropertyKey<float> DyingProgressKey = new PropertyKey<float>("DyingProgress");

		// Token: 0x04000023 RID: 35
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x04000024 RID: 36
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000025 RID: 37
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000026 RID: 38
		public WateredNaturalResourceSpec _wateredNaturalResourceSpec;

		// Token: 0x04000027 RID: 39
		public ITimeTrigger _timeTrigger;
	}
}
