using System;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000017 RID: 23
	public class TimeFastForwarder
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00002DF6 File Offset: 0x00000FF6
		public TimeFastForwarder(IDayNightCycle dayNightCycle)
		{
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002E05 File Offset: 0x00001005
		public void JumpToNextDaytime()
		{
			this._dayNightCycle.JumpTimeInHours(this.GetJumpDeltaInHours());
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002E18 File Offset: 0x00001018
		public float GetJumpDeltaInHours()
		{
			float hoursPassedToday = this._dayNightCycle.HoursPassedToday;
			for (int i = 0; i < TimeFastForwarder.JumpHours.Length; i++)
			{
				float num = TimeFastForwarder.JumpHours[i];
				if (hoursPassedToday < num)
				{
					return num - hoursPassedToday;
				}
			}
			return 24f - hoursPassedToday + TimeFastForwarder.JumpHours[0];
		}

		// Token: 0x04000034 RID: 52
		public static readonly float[] JumpHours = new float[]
		{
			0.5f,
			4f,
			16.5f,
			20f
		};

		// Token: 0x04000035 RID: 53
		public readonly IDayNightCycle _dayNightCycle;
	}
}
