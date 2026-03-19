using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000008 RID: 8
	public class Chronometer : BaseComponent, IAwakableComponent, IPersistentEntity, IDuplicable<Chronometer>, IDuplicable, ISamplingTransmitter, ITransmitter
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002419 File Offset: 0x00000619
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002421 File Offset: 0x00000621
		public float StartTime { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000242A File Offset: 0x0000062A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002432 File Offset: 0x00000632
		public float EndTime { get; private set; } = 16f;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000243B File Offset: 0x0000063B
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002443 File Offset: 0x00000643
		public ChronometerMode Mode { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002454 File Offset: 0x00000654
		public float SampledTime { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000245D File Offset: 0x0000065D
		public Chronometer(IDayNightCycle dayNightCycle, WorkingHoursManager workingHoursManager)
		{
			this._dayNightCycle = dayNightCycle;
			this._workingHoursManager = workingHoursManager;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000247E File Offset: 0x0000067E
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000248C File Offset: 0x0000068C
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Chronometer.ChronometerKey);
			component.Set(Chronometer.StartTimeKey, this.StartTime);
			component.Set(Chronometer.EndTimeKey, this.EndTime);
			component.Set<ChronometerMode>(Chronometer.ModeKey, this.Mode);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024CC File Offset: 0x000006CC
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Chronometer.ChronometerKey);
			this.StartTime = component.Get(Chronometer.StartTimeKey);
			this.EndTime = component.Get(Chronometer.EndTimeKey);
			this.Mode = component.Get<ChronometerMode>(Chronometer.ModeKey);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002518 File Offset: 0x00000718
		public void DuplicateFrom(Chronometer source)
		{
			this.StartTime = source.StartTime;
			this.EndTime = source.EndTime;
			this.Mode = source.Mode;
			this.UpdateOutputState();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002544 File Offset: 0x00000744
		public void SetStartTime(float startTime)
		{
			this.StartTime = startTime;
			this.UpdateOutputState();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002553 File Offset: 0x00000753
		public void SetEndTime(float endTime)
		{
			this.EndTime = endTime;
			this.UpdateOutputState();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002562 File Offset: 0x00000762
		public void SetMode(ChronometerMode mode)
		{
			this.Mode = mode;
			this.UpdateOutputState();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002571 File Offset: 0x00000771
		public void Sample()
		{
			this.SampledTime = this._dayNightCycle.HoursPassedToday;
			this._sampledWorkEndHours = this._workingHoursManager.EndHours;
			this.UpdateOutputState();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000259C File Offset: 0x0000079C
		public void UpdateOutputState()
		{
			ValueTuple<float, float> startAndEndTime = this.GetStartAndEndTime();
			float item = startAndEndTime.Item1;
			float item2 = startAndEndTime.Item2;
			this._automator.SetState(this.IsOn(item, item2));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025D0 File Offset: 0x000007D0
		public ValueTuple<float, float> GetStartAndEndTime()
		{
			ValueTuple<float, float> result;
			switch (this.Mode)
			{
			case ChronometerMode.TimeRange:
				result = new ValueTuple<float, float>(this.StartTime, this.EndTime);
				break;
			case ChronometerMode.WorkingHours:
				result = new ValueTuple<float, float>(0f, this._sampledWorkEndHours);
				break;
			case ChronometerMode.NonWorkingHours:
				result = new ValueTuple<float, float>(this._sampledWorkEndHours, 24f);
				break;
			default:
				throw new ArgumentOutOfRangeException("ChronometerMode", this.Mode, null);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000264A File Offset: 0x0000084A
		public bool IsOn(float startTime, float endTime)
		{
			if (startTime > endTime)
			{
				return this.SampledTime >= startTime || this.SampledTime < endTime;
			}
			return this.SampledTime >= startTime && this.SampledTime < endTime;
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey ChronometerKey = new ComponentKey("Chronometer");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<float> StartTimeKey = new PropertyKey<float>("StartTime");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<float> EndTimeKey = new PropertyKey<float>("EndTime");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<ChronometerMode> ModeKey = new PropertyKey<ChronometerMode>("Mode");

		// Token: 0x04000010 RID: 16
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000011 RID: 17
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x04000012 RID: 18
		public Automator _automator;

		// Token: 0x04000013 RID: 19
		public float _sampledWorkEndHours;
	}
}
