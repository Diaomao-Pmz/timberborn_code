using System;
using UnityEngine;

namespace Timberborn.TimeSystem
{
	// Token: 0x0200001A RID: 26
	public class TimeTrigger : ITimeTrigger
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002F3B File Offset: 0x0000113B
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002F43 File Offset: 0x00001143
		public bool Finished { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002F4C File Offset: 0x0000114C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002F54 File Offset: 0x00001154
		public bool InProgress { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00002F5D File Offset: 0x0000115D
		public TimeTrigger(IDayNightCycle dayNightCycle, TimeTriggerService timeTriggerService, Action action, float fullDelayInDays)
		{
			this._dayNightCycle = dayNightCycle;
			this._timeTriggerService = timeTriggerService;
			this._action = action;
			this._fullDelayInDays = fullDelayInDays;
			this._delayLeftInDays = fullDelayInDays;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002F8A File Offset: 0x0000118A
		public float DaysLeft
		{
			get
			{
				if (!this.InProgress)
				{
					return this._delayLeftInDays;
				}
				return this._delayLeftInDays - this.DaysSinceStart;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002FA8 File Offset: 0x000011A8
		public float Progress
		{
			get
			{
				return 1f - Mathf.Clamp01(this.DaysLeft / this._fullDelayInDays);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002FC2 File Offset: 0x000011C2
		public void Reset()
		{
			this.Finished = false;
			this.Pause();
			this._delayLeftInDays = this._fullDelayInDays;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002FE0 File Offset: 0x000011E0
		public void Resume()
		{
			if (!this.InProgress && !this.Finished)
			{
				float partialDayNumber = this._dayNightCycle.PartialDayNumber;
				this._timeTriggerService.Add(this, partialDayNumber + this._delayLeftInDays);
				this._resumedTimestamp = partialDayNumber;
				this.InProgress = true;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000302B File Offset: 0x0000122B
		public void Pause()
		{
			if (this.InProgress)
			{
				this._timeTriggerService.Remove(this);
				this._delayLeftInDays -= this.DaysSinceStart;
				this.InProgress = false;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000305B File Offset: 0x0000125B
		public void FastForwardProgress(float progress)
		{
			bool inProgress = this.InProgress;
			this.Pause();
			this._delayLeftInDays -= this._fullDelayInDays * progress;
			if (this._delayLeftInDays <= 0f)
			{
				this.Finish();
			}
			if (inProgress)
			{
				this.Resume();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003099 File Offset: 0x00001299
		public void Finish()
		{
			if (!this.Finished)
			{
				this.InProgress = false;
				this.Finished = true;
				this._delayLeftInDays = 0f;
				this._action();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000030C7 File Offset: 0x000012C7
		public float DaysSinceStart
		{
			get
			{
				return this._dayNightCycle.PartialDayNumber - this._resumedTimestamp;
			}
		}

		// Token: 0x0400003B RID: 59
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400003C RID: 60
		public readonly TimeTriggerService _timeTriggerService;

		// Token: 0x0400003D RID: 61
		public readonly Action _action;

		// Token: 0x0400003E RID: 62
		public readonly float _fullDelayInDays;

		// Token: 0x0400003F RID: 63
		public float _delayLeftInDays;

		// Token: 0x04000040 RID: 64
		public float _resumedTimestamp;
	}
}
