using System;
using System.Runtime.CompilerServices;
using Timberborn.BlueprintSystem;
using Timberborn.MapStateSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200000A RID: 10
	public class DayNightCycle : IDayNightCycle, ISaveableSingleton, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023BC File Offset: 0x000005BC
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000023C4 File Offset: 0x000005C4
		public int DayNumber { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023D5 File Offset: 0x000005D5
		public float DayLengthInSeconds { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023DE File Offset: 0x000005DE
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023E6 File Offset: 0x000005E6
		public float DaytimeLengthInHours { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023EF File Offset: 0x000005EF
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000023F7 File Offset: 0x000005F7
		public float NighttimeLengthInHours { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002408 File Offset: 0x00000608
		public float FixedDeltaTimeInHours { get; private set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002411 File Offset: 0x00000611
		public DayNightCycle(EventBus eventBus, ISingletonLoader singletonLoader, MapEditorMode mapEditorMode, ISpecService specService, ITickService tickService, ITickProgressService tickProgressService)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._mapEditorMode = mapEditorMode;
			this._specService = specService;
			this._tickService = tickService;
			this._tickProgressService = tickProgressService;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002446 File Offset: 0x00000646
		public float DayProgress
		{
			get
			{
				return (float)this._ticksPassedToday * 1f / (float)this._dayNightCycleSpec.ConfiguredDayLengthInTicks;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002462 File Offset: 0x00000662
		public float PartialDayNumber
		{
			get
			{
				return (float)this.DayNumber + this.DayProgress;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002472 File Offset: 0x00000672
		public bool IsDaytime
		{
			get
			{
				return this.TimeOfDay == TimeOfDay.Daytime;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000247D File Offset: 0x0000067D
		public bool IsNighttime
		{
			get
			{
				return this.TimeOfDay == TimeOfDay.Nighttime;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002488 File Offset: 0x00000688
		public float HoursPassedToday
		{
			get
			{
				return this.TicksToHours(this._ticksPassedToday);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002496 File Offset: 0x00000696
		public float FluidSecondsPassedToday
		{
			get
			{
				return (float)this._ticksPassedToday * this._tickService.TickIntervalInSeconds + this._tickProgressService.SecondsPassedThisTick;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024B8 File Offset: 0x000006B8
		public void Load()
		{
			this.InitializeConfigurationValues();
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(DayNightCycle.DayNightCycleKey, out objectLoader))
			{
				this.DayNumber = objectLoader.Get(DayNightCycle.DayNumberKey);
				this._ticksPassedToday = (int)(objectLoader.Get(DayNightCycle.DayProgressKey) * (float)this._dayNightCycleSpec.ConfiguredDayLengthInTicks);
				return;
			}
			this._ticksPassedToday = this.HoursToTicks((float)this._dayNightCycleSpec.HoursPassedOnNewGame);
			this.DayNumber = 1;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000252F File Offset: 0x0000072F
		public void Save(ISingletonSaver singletonSaver)
		{
			if (!this._mapEditorMode.IsMapEditor)
			{
				IObjectSaver singleton = singletonSaver.GetSingleton(DayNightCycle.DayNightCycleKey);
				singleton.Set(DayNightCycle.DayNumberKey, this.DayNumber);
				singleton.Set(DayNightCycle.DayProgressKey, this.DayProgress);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000256A File Offset: 0x0000076A
		public void Tick()
		{
			this.ProgressTimeBy(1);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002573 File Offset: 0x00000773
		[return: TupleElementNames(new string[]
		{
			"start",
			"end"
		})]
		public ValueTuple<float, float> BoundsInHours(TimeOfDay timeOfDay)
		{
			if (timeOfDay == TimeOfDay.Daytime)
			{
				return new ValueTuple<float, float>(0f, this.DaytimeLengthInHours);
			}
			if (timeOfDay != TimeOfDay.Nighttime)
			{
				throw new ArgumentOutOfRangeException("timeOfDay", timeOfDay, null);
			}
			return new ValueTuple<float, float>(this.DaytimeLengthInHours, 24f);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025B1 File Offset: 0x000007B1
		public float SecondsToHours(float seconds)
		{
			return seconds / this.DayLengthInSeconds * 24f;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025C1 File Offset: 0x000007C1
		public float DayNumberHoursFromNow(float hours)
		{
			return this.PartialDayNumber + hours / 24f;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025D1 File Offset: 0x000007D1
		public float HoursToNextStartOf(TimeOfDay timeOfDay)
		{
			return this.HoursToNextStartOf(timeOfDay, this.HoursPassedToday);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E0 File Offset: 0x000007E0
		public float FluidHoursToNextStartOf(TimeOfDay timeOfDay)
		{
			return this.HoursToNextStartOf(timeOfDay, this.SecondsToHours(this.FluidSecondsPassedToday));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025F5 File Offset: 0x000007F5
		public float TicksToHours(int ticks)
		{
			return (float)ticks / (float)this._dayNightCycleSpec.ConfiguredDayLengthInTicks * 24f;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000260C File Offset: 0x0000080C
		public int HoursToTicks(float hours)
		{
			return (int)(hours / 24f * (float)this._dayNightCycleSpec.ConfiguredDayLengthInTicks);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002623 File Offset: 0x00000823
		public void SetTimeToNextDay()
		{
			if (this.IsDaytime)
			{
				this.ProgressTimeBy(this._daytimeLengthInTicks - this._ticksPassedToday);
			}
			this.ProgressTimeBy(this._dayNightCycleSpec.ConfiguredDayLengthInTicks - this._ticksPassedToday);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002658 File Offset: 0x00000858
		public void JumpTimeInHours(float hours)
		{
			this.ProgressTimeBy(this.HoursToTicks(hours));
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002667 File Offset: 0x00000867
		public TimeOfDay TimeOfDay
		{
			get
			{
				if (this._ticksPassedToday >= this._daytimeLengthInTicks)
				{
					return TimeOfDay.Nighttime;
				}
				return TimeOfDay.Daytime;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000267A File Offset: 0x0000087A
		public float HoursToNextStartOf(TimeOfDay timeOfDay, float hoursPassedToday)
		{
			if (timeOfDay == TimeOfDay.Daytime)
			{
				return DayNightCycle.HoursToNextHour(0f, hoursPassedToday);
			}
			if (timeOfDay != TimeOfDay.Nighttime)
			{
				throw new ArgumentOutOfRangeException("timeOfDay", timeOfDay, null);
			}
			return DayNightCycle.HoursToNextHour(this.DaytimeLengthInHours, hoursPassedToday);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026AF File Offset: 0x000008AF
		public static float HoursToNextHour(float hour, float hoursPassedToday)
		{
			if (hour < 0f || hour > 24f)
			{
				throw new ArgumentException(string.Format("Hour {0} is outside of the accepted range of 0 to 24", hour));
			}
			if (hoursPassedToday >= hour)
			{
				return hour + 24f - hoursPassedToday;
			}
			return hour - hoursPassedToday;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000026E8 File Offset: 0x000008E8
		public void InitializeConfigurationValues()
		{
			this._dayNightCycleSpec = this._specService.GetSingleSpec<DayNightCycleSpec>();
			this.DaytimeLengthInHours = (float)this._dayNightCycleSpec.ConfiguredDaytimeLengthInHours;
			this._daytimeLengthInTicks = this.HoursToTicks(this.DaytimeLengthInHours);
			this.NighttimeLengthInHours = 24f - this.DaytimeLengthInHours;
			this.DayLengthInSeconds = (float)this._dayNightCycleSpec.ConfiguredDayLengthInTicks * this._tickService.TickIntervalInSeconds;
			this.FixedDeltaTimeInHours = this.SecondsToHours(this._tickService.TickIntervalInSeconds);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002774 File Offset: 0x00000974
		public void ProgressTimeBy(int deltaTicks)
		{
			TimeOfDay timeOfDay = this.TimeOfDay;
			this._ticksPassedToday += deltaTicks;
			if (this._ticksPassedToday >= this._dayNightCycleSpec.ConfiguredDayLengthInTicks)
			{
				this._ticksPassedToday -= this._dayNightCycleSpec.ConfiguredDayLengthInTicks;
				int dayNumber = this.DayNumber + 1;
				this.DayNumber = dayNumber;
			}
			if (timeOfDay != this.TimeOfDay)
			{
				this.PostTimeOfDayEvent();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027E0 File Offset: 0x000009E0
		public void PostTimeOfDayEvent()
		{
			TimeOfDay timeOfDay = this.TimeOfDay;
			if (timeOfDay == TimeOfDay.Daytime)
			{
				this._eventBus.Post(new DaytimeStartEvent());
				return;
			}
			if (timeOfDay != TimeOfDay.Nighttime)
			{
				throw new ArgumentException(string.Format("Unexpected: {0}", this.TimeOfDay));
			}
			this._eventBus.Post(new NighttimeStartEvent());
		}

		// Token: 0x0400000F RID: 15
		public static readonly SingletonKey DayNightCycleKey = new SingletonKey("DayNightCycle");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<int> DayNumberKey = new PropertyKey<int>("DayNumber");

		// Token: 0x04000011 RID: 17
		public static readonly PropertyKey<float> DayProgressKey = new PropertyKey<float>("DayProgress");

		// Token: 0x04000017 RID: 23
		public readonly EventBus _eventBus;

		// Token: 0x04000018 RID: 24
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000019 RID: 25
		public readonly MapEditorMode _mapEditorMode;

		// Token: 0x0400001A RID: 26
		public readonly ISpecService _specService;

		// Token: 0x0400001B RID: 27
		public readonly ITickService _tickService;

		// Token: 0x0400001C RID: 28
		public readonly ITickProgressService _tickProgressService;

		// Token: 0x0400001D RID: 29
		public DayNightCycleSpec _dayNightCycleSpec;

		// Token: 0x0400001E RID: 30
		public int _daytimeLengthInTicks;

		// Token: 0x0400001F RID: 31
		public int _ticksPassedToday;
	}
}
