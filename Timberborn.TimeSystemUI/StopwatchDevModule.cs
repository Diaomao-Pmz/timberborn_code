using System;
using Timberborn.Debugging;
using Timberborn.QuickNotificationSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000007 RID: 7
	public class StopwatchDevModule : IDevModule
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000026C9 File Offset: 0x000008C9
		public StopwatchDevModule(IDayNightCycle dayNightCycle, EventBus eventBus, QuickNotificationService quickNotificationService)
		{
			this._dayNightCycle = dayNightCycle;
			this._eventBus = eventBus;
			this._quickNotificationService = quickNotificationService;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026E8 File Offset: 0x000008E8
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Stopwatch: Restart", new Action(this.RestartStopwatch))).AddMethod(DevMethod.Create("Stopwatch: Log", new Action(this.LogStopwatch))).Build();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002735 File Offset: 0x00000935
		[OnEvent]
		public void OnDaytimeStarted(DaytimeStartEvent daytimeStartEvent)
		{
			this._dayCounter++;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002748 File Offset: 0x00000948
		public void RestartStopwatch()
		{
			if (this._hoursPassedAtStart == null)
			{
				this._eventBus.Register(this);
				this.LogMessage("Stopwatch started");
			}
			else
			{
				this.LogMessage("Stopwatch restarted\n" + this.GetElapsedHoursMessage(this._hoursPassedAtStart.Value));
			}
			this._hoursPassedAtStart = new float?(this._dayNightCycle.HoursPassedToday);
			this._dayCounter = 0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027B9 File Offset: 0x000009B9
		public void LogStopwatch()
		{
			this.LogMessage((this._hoursPassedAtStart != null) ? this.GetElapsedHoursMessage(this._hoursPassedAtStart.Value) : "Stopwatch not started");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027E6 File Offset: 0x000009E6
		public void LogMessage(string message)
		{
			Debug.Log(message);
			this._quickNotificationService.SendNotification(message);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027FC File Offset: 0x000009FC
		public string GetElapsedHoursMessage(float hoursPassedAtStart)
		{
			float num = this.CalculateElapsedHours(hoursPassedAtStart);
			return string.Format("Elapsed hours: {0:F4}", num);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002824 File Offset: 0x00000A24
		public float CalculateElapsedHours(float hoursPassedAtStart)
		{
			if (this._dayCounter > 0)
			{
				float num = 24f - hoursPassedAtStart;
				int num2 = (this._dayCounter - 1) * 24;
				return num + (float)num2 + this._dayNightCycle.HoursPassedToday;
			}
			return this._dayNightCycle.HoursPassedToday - hoursPassedAtStart;
		}

		// Token: 0x04000023 RID: 35
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x04000024 RID: 36
		public readonly EventBus _eventBus;

		// Token: 0x04000025 RID: 37
		public readonly QuickNotificationService _quickNotificationService;

		// Token: 0x04000026 RID: 38
		public int _dayCounter;

		// Token: 0x04000027 RID: 39
		public float? _hoursPassedAtStart;
	}
}
