using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.GameCycleSystem
{
	// Token: 0x02000007 RID: 7
	public class GameCycleService : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020EC File Offset: 0x000002EC
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		public int Cycle { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020FD File Offset: 0x000002FD
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002105 File Offset: 0x00000305
		public int CycleDay { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x0000210E File Offset: 0x0000030E
		public GameCycleService(EventBus eventBus, ISingletonLoader singletonLoader, IDayNightCycle dayNightCycle, MapEditorMode mapEditorMode, IEnumerable<ICycleDuration> cycleDurations)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._dayNightCycle = dayNightCycle;
			this._mapEditorMode = mapEditorMode;
			this._cycleDurations = cycleDurations.ToImmutableArray<ICycleDuration>();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002140 File Offset: 0x00000340
		public float PartialCycleDay
		{
			get
			{
				return (float)this.CycleDay + this._dayNightCycle.DayProgress;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002155 File Offset: 0x00000355
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(GameCycleService.GameCycleServiceKey);
				singleton.Set(GameCycleService.CycleKey, this.Cycle);
				singleton.Set(GameCycleService.CycleDayKey, this.CycleDay);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(GameCycleService.GameCycleServiceKey, out objectLoader))
			{
				this.Cycle = Math.Max(objectLoader.Get(GameCycleService.CycleKey), 0);
				this.CycleDay = objectLoader.Get(GameCycleService.CycleDayKey);
				this._cycleDurationInDays = this._cycleDurations.Sum((ICycleDuration duration) => duration.DurationInDays);
			}
			else
			{
				this.StartNextCycle();
			}
			this._eventBus.Register(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002222 File Offset: 0x00000422
		[OnEvent]
		public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent)
		{
			this.StartNextDay();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222C File Offset: 0x0000042C
		public void StartNextDay()
		{
			int cycleDay = this.CycleDay + 1;
			this.CycleDay = cycleDay;
			if (this.CycleDay > this._cycleDurationInDays)
			{
				this.StartNextCycle();
			}
			this._eventBus.Post(new CycleDayStartedEvent());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002270 File Offset: 0x00000470
		public void StartNextCycle()
		{
			if (this.Cycle > 0)
			{
				this._eventBus.Post(new CycleEndedEvent(this.Cycle));
			}
			int cycle = this.Cycle + 1;
			this.Cycle = cycle;
			this.CycleDay = 1;
			foreach (ICycleDuration cycleDuration in this._cycleDurations)
			{
				cycleDuration.SetForCycle(this.Cycle);
			}
			this._cycleDurationInDays = this._cycleDurations.Sum((ICycleDuration duration) => duration.DurationInDays);
			this._eventBus.Post(new CycleStartedEvent(this.Cycle));
		}

		// Token: 0x04000008 RID: 8
		public static readonly SingletonKey GameCycleServiceKey = new SingletonKey("GameCycleService");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<int> CycleKey = new PropertyKey<int>("Cycle");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<int> CycleDayKey = new PropertyKey<int>("CycleDay");

		// Token: 0x0400000D RID: 13
		public readonly EventBus _eventBus;

		// Token: 0x0400000E RID: 14
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400000F RID: 15
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000010 RID: 16
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x04000011 RID: 17
		public readonly ImmutableArray<ICycleDuration> _cycleDurations;

		// Token: 0x04000012 RID: 18
		public int _cycleDurationInDays;
	}
}
