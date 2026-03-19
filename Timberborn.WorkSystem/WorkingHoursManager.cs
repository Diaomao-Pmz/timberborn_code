using System;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200001E RID: 30
	public class WorkingHoursManager : ISaveableSingleton, ILoadableSingleton, ITickableSingleton
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003757 File Offset: 0x00001957
		public float EndHours { get; private set; }

		// Token: 0x060000B8 RID: 184 RVA: 0x00003760 File Offset: 0x00001960
		public WorkingHoursManager(ISingletonLoader singletonLoader, IDayNightCycle dayNightCycle, EventBus eventBus)
		{
			this._singletonLoader = singletonLoader;
			this._dayNightCycle = dayNightCycle;
			this._eventBus = eventBus;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003780 File Offset: 0x00001980
		public bool AreWorkingHours
		{
			get
			{
				float hoursPassedToday = this._dayNightCycle.HoursPassedToday;
				return this._workedPartOfDay > 0f && hoursPassedToday >= this._startHours && hoursPassedToday < this.EndHours;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000037BA File Offset: 0x000019BA
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000037C2 File Offset: 0x000019C2
		public float WorkedPartOfDay
		{
			get
			{
				return this._workedPartOfDay;
			}
			set
			{
				this._workedPartOfDay = value;
				this.EndHours = this._startHours + this._workedPartOfDay * 24f;
				this._eventBus.Post(new WorkingHoursChangedEvent());
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000037F4 File Offset: 0x000019F4
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WorkingHoursManager.WorkingHoursManagerKey, out objectLoader))
			{
				ValueTuple<float, float> valueTuple = this._dayNightCycle.BoundsInHours(TimeOfDay.Daytime);
				this._startHours = valueTuple.Item1;
				this.WorkedPartOfDay = objectLoader.Get(WorkingHoursManager.WorkedPartOfDayKey);
			}
			else
			{
				ValueTuple<float, float> valueTuple = this._dayNightCycle.BoundsInHours(TimeOfDay.Daytime);
				this._startHours = valueTuple.Item1;
				this.EndHours = valueTuple.Item2;
				float num = this.EndHours - this._startHours;
				this.WorkedPartOfDay = num / 24f;
			}
			this._wereWorkingHours = this.AreWorkingHours;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000388F File Offset: 0x00001A8F
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(WorkingHoursManager.WorkingHoursManagerKey).Set(WorkingHoursManager.WorkedPartOfDayKey, this._workedPartOfDay);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000038AC File Offset: 0x00001AAC
		public void Tick()
		{
			bool areWorkingHours = this.AreWorkingHours;
			if (this._wereWorkingHours != areWorkingHours)
			{
				this._eventBus.Post(new WorkingHoursTransitionedEvent());
			}
			this._wereWorkingHours = areWorkingHours;
		}

		// Token: 0x04000043 RID: 67
		public static readonly SingletonKey WorkingHoursManagerKey = new SingletonKey("WorkingHoursManager");

		// Token: 0x04000044 RID: 68
		public static readonly PropertyKey<float> WorkedPartOfDayKey = new PropertyKey<float>("WorkedPartOfDay");

		// Token: 0x04000046 RID: 70
		public float _startHours;

		// Token: 0x04000047 RID: 71
		public float _workedPartOfDay;

		// Token: 0x04000048 RID: 72
		public bool _wereWorkingHours;

		// Token: 0x04000049 RID: 73
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x0400004A RID: 74
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400004B RID: 75
		public readonly EventBus _eventBus;
	}
}
