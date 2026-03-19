using System;
using Timberborn.TimeSystem;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200004C RID: 76
	public class TimerInterval
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000091A4 File Offset: 0x000073A4
		// (set) Token: 0x0600033E RID: 830 RVA: 0x000091AC File Offset: 0x000073AC
		public IntervalType Type { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000091B5 File Offset: 0x000073B5
		// (set) Token: 0x06000340 RID: 832 RVA: 0x000091BD File Offset: 0x000073BD
		public int Ticks { get; private set; }

		// Token: 0x06000341 RID: 833 RVA: 0x000091C6 File Offset: 0x000073C6
		public TimerInterval(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000091D8 File Offset: 0x000073D8
		public float GetTypeTime()
		{
			if (this.Type == IntervalType.Ticks)
			{
				return (float)this.Ticks;
			}
			if (this._hours == null)
			{
				throw new InvalidOperationException("Hours value is not set for non-ticks interval type.");
			}
			if (this.Type != IntervalType.Hours)
			{
				return this._hours.Value / 24f;
			}
			return this._hours.Value;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009233 File Offset: 0x00007433
		public bool TryGetHours(out float hours)
		{
			if (this._hours != null)
			{
				hours = this._hours.Value;
				return true;
			}
			hours = 0f;
			return false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009259 File Offset: 0x00007459
		public void DuplicateFrom(TimerInterval source)
		{
			this.Type = source.Type;
			this.Ticks = source.Ticks;
			this._hours = source._hours;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000927F File Offset: 0x0000747F
		public void SetTicks(int ticks)
		{
			this.Type = IntervalType.Ticks;
			this._hours = null;
			this.Ticks = Math.Max(1, ticks);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000092A1 File Offset: 0x000074A1
		public void SetHours(float hours)
		{
			this.Type = IntervalType.Hours;
			this._hours = new float?(this.ClampHours(hours));
			this.Ticks = this.ConvertHoursToTicks(this._hours.Value);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000092D3 File Offset: 0x000074D3
		public void SetDays(float days)
		{
			this.Type = IntervalType.Days;
			this._hours = new float?(this.ClampHours(days * 24f));
			this.Ticks = this.ConvertHoursToTicks(this._hours.Value);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000930C File Offset: 0x0000750C
		public float ClampHours(float hours)
		{
			float val = this._dayNightCycle.TicksToHours(1);
			return Math.Max(hours, val);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000932D File Offset: 0x0000752D
		public int ConvertHoursToTicks(float hours)
		{
			return Math.Max(this._dayNightCycle.HoursToTicks(hours), 1);
		}

		// Token: 0x04000194 RID: 404
		public float? _hours;

		// Token: 0x04000195 RID: 405
		public readonly IDayNightCycle _dayNightCycle;
	}
}
