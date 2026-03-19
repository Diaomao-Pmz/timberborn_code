using System;
using Timberborn.Common;

namespace Timberborn.Workshops
{
	// Token: 0x02000008 RID: 8
	public class DailyProductivity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public HourlyProductivity[] HourlyProductivities { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public HourlyProductivity CurrentProductivity { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public int CurrentHour { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x00002129 File Offset: 0x00000329
		public DailyProductivity(HourlyProductivity[] hourlyProductivities, HourlyProductivity currentProductivity)
		{
			this.HourlyProductivities = hourlyProductivities;
			this.CurrentProductivity = currentProductivity;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000213F File Offset: 0x0000033F
		public static DailyProductivity CreateDefault()
		{
			HourlyProductivity[] array = new HourlyProductivity[24];
			array.Fill(new Func<HourlyProductivity>(HourlyProductivity.CreateDefault));
			return new DailyProductivity(array, HourlyProductivity.CreateDefault());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002164 File Offset: 0x00000364
		public float CalculateProductivity()
		{
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < this.HourlyProductivities.Length; i++)
			{
				if (this.HourlyProductivities[i].WasWorkingHour)
				{
					num += this.HourlyProductivities[i].Productivity;
					num2++;
				}
			}
			if (num2 != 0)
			{
				return num / (float)num2;
			}
			return 0f;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		public void UpdateAndSetCurrentHour(int currentHour)
		{
			this.HourlyProductivities[this.CurrentHour].CopyValuesFrom(this.CurrentProductivity);
			this.CurrentProductivity.Reset();
			this.SetCurrentHour(currentHour);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		public void SetCurrentHour(int currentHour)
		{
			this.CurrentHour = currentHour;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F1 File Offset: 0x000003F1
		public void AddSample(int maxWorkPotential, int actualWorkPerformed)
		{
			this.CurrentProductivity.AddSample(maxWorkPotential, actualWorkPerformed);
		}
	}
}
