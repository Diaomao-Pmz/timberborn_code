using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Growing;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Pollination
{
	// Token: 0x0200000A RID: 10
	public class Pollinatee : BaseComponent, IAwakableComponent, IPersistentEntity
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002851 File Offset: 0x00000A51
		public Pollinatee(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000286B File Offset: 0x00000A6B
		public bool CanPollinate
		{
			get
			{
				return this._dayNightCycle.PartialDayNumber > this._lastPollinationTimestamp + 1f && this._growable.GrowthInProgress;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002893 File Offset: 0x00000A93
		public void Awake()
		{
			this._growable = base.GetComponent<Growable>();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028A1 File Offset: 0x00000AA1
		public void Save(IEntitySaver entitySaver)
		{
			if (this._lastPollinationTimestamp != Pollinatee.DefaultLastPollinationTimestamp)
			{
				entitySaver.GetComponent(Pollinatee.PollinateeKey).Set(Pollinatee.LastPollinationTimestampKey, this._lastPollinationTimestamp);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028CC File Offset: 0x00000ACC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(Pollinatee.PollinateeKey, out objectLoader))
			{
				this._lastPollinationTimestamp = objectLoader.Get(Pollinatee.LastPollinationTimestampKey);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028FC File Offset: 0x00000AFC
		public void Pollinate(float growthTimeReduction)
		{
			if (this.CanPollinate)
			{
				float num = 1f / (1f - growthTimeReduction) - 1f;
				this._growable.IncreaseGrowthProgress(num / this._growable.GrowthTimeInDays);
				this._lastPollinationTimestamp = this._dayNightCycle.PartialDayNumber;
			}
		}

		// Token: 0x0400001C RID: 28
		public static readonly float DefaultLastPollinationTimestamp = -1f;

		// Token: 0x0400001D RID: 29
		public static readonly ComponentKey PollinateeKey = new ComponentKey("Pollinatee");

		// Token: 0x0400001E RID: 30
		public static readonly PropertyKey<float> LastPollinationTimestampKey = new PropertyKey<float>("LastPollinationTimestamp");

		// Token: 0x0400001F RID: 31
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000020 RID: 32
		public Growable _growable;

		// Token: 0x04000021 RID: 33
		public float _lastPollinationTimestamp = Pollinatee.DefaultLastPollinationTimestamp;
	}
}
