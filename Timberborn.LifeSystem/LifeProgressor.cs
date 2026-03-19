using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.Characters;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.LifeSystem
{
	// Token: 0x02000008 RID: 8
	public class LifeProgressor : TickableComponent, IAwakableComponent, IPersistentEntity, IChildhoodInfluenced
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002106 File Offset: 0x00000306
		public float LifeProgress { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000210F File Offset: 0x0000030F
		public LifeProgressor(IDayNightCycle dayNightCycle, LifeService lifeService)
		{
			this._dayNightCycle = dayNightCycle;
			this._lifeService = lifeService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002125 File Offset: 0x00000325
		public bool ShouldDie
		{
			get
			{
				return this.LifeProgress > this._longevity.ExpectedLongevity;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213A File Offset: 0x0000033A
		public void Awake()
		{
			this._bonusManager = base.GetComponent<BonusManager>();
			this._longevity = base.GetComponent<ILongevity>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002154 File Offset: 0x00000354
		public override void StartTickable()
		{
			this._lifeProgressIncreasePerTick = 0.041666668f * this._dayNightCycle.FixedDeltaTimeInHours / (float)this._lifeService.AverageLifespan;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000217A File Offset: 0x0000037A
		public override void Tick()
		{
			this.IncreaseLifeProgress();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002182 File Offset: 0x00000382
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(LifeProgressor.LifeProgressorKey).Set(LifeProgressor.LifeProgressKey, this.LifeProgress);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021A0 File Offset: 0x000003A0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(LifeProgressor.LifeProgressorKey);
			this.LifeProgress = component.Get(LifeProgressor.LifeProgressKey);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021CA File Offset: 0x000003CA
		public void InfluenceByChildhood(Character child)
		{
			this.LifeProgress = child.GetComponent<LifeProgressor>().LifeProgress;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021DD File Offset: 0x000003DD
		public void IncreaseLifeProgress()
		{
			this.LifeProgress += this._lifeProgressIncreasePerTick / this._bonusManager.Multiplier(LifeProgressor.LifeExpectancyBonusId);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string LifeExpectancyBonusId = "LifeExpectancy";

		// Token: 0x04000009 RID: 9
		public static readonly ComponentKey LifeProgressorKey = new ComponentKey("LifeProgressor");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> LifeProgressKey = new PropertyKey<float>("LifeProgress");

		// Token: 0x0400000C RID: 12
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000D RID: 13
		public readonly LifeService _lifeService;

		// Token: 0x0400000E RID: 14
		public BonusManager _bonusManager;

		// Token: 0x0400000F RID: 15
		public ILongevity _longevity;

		// Token: 0x04000010 RID: 16
		public float _lifeProgressIncreasePerTick;
	}
}
