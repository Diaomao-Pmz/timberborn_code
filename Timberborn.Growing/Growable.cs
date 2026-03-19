using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.NaturalResourcesLifecycleModelSystem;
using Timberborn.NaturalResourcesReproduction;
using Timberborn.Persistence;
using Timberborn.TerrainSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Growing
{
	// Token: 0x02000007 RID: 7
	public class Growable : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IInitializableEntity, IGroundMatterBelowInvalidator
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		public event EventHandler HasGrown;

		// Token: 0x06000009 RID: 9 RVA: 0x00002165 File Offset: 0x00000365
		public Growable(ITimeTriggerFactory timeTriggerFactory)
		{
			this._timeTriggerFactory = timeTriggerFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public float GrowthTimeInDays
		{
			get
			{
				return this._growableSpec.GrowthTimeInDays;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002181 File Offset: 0x00000381
		public bool IsGrown
		{
			get
			{
				return this._timeTrigger.Finished;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000218E File Offset: 0x0000038E
		public bool GrowthInProgress
		{
			get
			{
				return this._timeTrigger.InProgress;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000219B File Offset: 0x0000039B
		public float GrowthProgress
		{
			get
			{
				return this._timeTrigger.Progress;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Awake()
		{
			this._livingNaturalResource = base.GetComponent<LivingNaturalResource>();
			this._dyingNaturalResource = base.GetComponent<DyingNaturalResource>();
			this._reproducible = base.GetComponent<Reproducible>();
			this._growableSpec = base.GetComponent<GrowableSpec>();
			this._timeTrigger = this._timeTriggerFactory.Create(new Action(this.Grow), this.GrowthTimeInDays);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		public void InitializeEntity()
		{
			GameObject fullModel = base.GetComponent<BlockObjectModel>().FullModel;
			this._matureModel = NaturalResourceLifecycleModel.Create(this, fullModel, "Mature");
			this._seedlingModel = NaturalResourceLifecycleModel.Create(this, fullModel, "Seedling");
			if (!this.IsGrown)
			{
				this._reproducible.BlockReproduction(this);
				this.ResumeGrowing();
			}
			this._dyingNaturalResource.StartedDying += delegate(object _, EventArgs _)
			{
				this.PauseGrowing();
			};
			this._dyingNaturalResource.StoppedDying += delegate(object _, EventArgs _)
			{
				this.ResumeGrowing();
			};
			this._livingNaturalResource.Died += delegate(object _, EventArgs _)
			{
				this.PauseGrowing();
			};
			this._livingNaturalResource.ReversedDeath += delegate(object _, EventArgs _)
			{
				this.ResumeGrowing();
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022BB File Offset: 0x000004BB
		public void DeleteEntity()
		{
			this.PauseGrowing();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C3 File Offset: 0x000004C3
		public void Save(IEntitySaver entitySaver)
		{
			if (this._timeTrigger.Progress < 1f)
			{
				entitySaver.GetComponent(Growable.GrowableKey).Set(Growable.GrowthProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F8 File Offset: 0x000004F8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Growable.GrowableKey, out objectLoader))
			{
				this._timeTrigger.FastForwardProgress(objectLoader.Get(Growable.GrowthProgressKey));
				return;
			}
			this._timeTrigger.FastForwardProgress(1f);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000233B File Offset: 0x0000053B
		public void IncreaseGrowthProgress(float growthProgress)
		{
			this._timeTrigger.FastForwardProgress(growthProgress);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002349 File Offset: 0x00000549
		public void ShowSeedlingModel()
		{
			this._matureModel.Hide();
			this._seedlingModel.Show();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002361 File Offset: 0x00000561
		public void ShowMatureModel()
		{
			this._matureModel.Show();
			this._seedlingModel.Hide();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002379 File Offset: 0x00000579
		public void HideModel()
		{
			this._matureModel.Hide();
			this._seedlingModel.Hide();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002391 File Offset: 0x00000591
		public void ResumeGrowing()
		{
			if (!this.IsGrown && !this._livingNaturalResource.IsDead && !this._dyingNaturalResource.IsDying)
			{
				this._timeTrigger.Resume();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023C0 File Offset: 0x000005C0
		public void PauseGrowing()
		{
			this._timeTrigger.Pause();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023CD File Offset: 0x000005CD
		public void Grow()
		{
			this._reproducible.UnblockReproduction(this);
			EventHandler hasGrown = this.HasGrown;
			if (hasGrown == null)
			{
				return;
			}
			hasGrown(this, EventArgs.Empty);
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey GrowableKey = new ComponentKey("Growable");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<float> GrowthProgressKey = new PropertyKey<float>("GrowthProgress");

		// Token: 0x0400000B RID: 11
		public readonly ITimeTriggerFactory _timeTriggerFactory;

		// Token: 0x0400000C RID: 12
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x0400000D RID: 13
		public DyingNaturalResource _dyingNaturalResource;

		// Token: 0x0400000E RID: 14
		public Reproducible _reproducible;

		// Token: 0x0400000F RID: 15
		public GrowableSpec _growableSpec;

		// Token: 0x04000010 RID: 16
		public ITimeTrigger _timeTrigger;

		// Token: 0x04000011 RID: 17
		public NaturalResourceLifecycleModel _matureModel;

		// Token: 0x04000012 RID: 18
		public NaturalResourceLifecycleModel _seedlingModel;
	}
}
