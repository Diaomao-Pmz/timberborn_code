using System;

namespace Timberborn.Workshops
{
	// Token: 0x0200000C RID: 12
	public class HourlyProductivity
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002508 File Offset: 0x00000708
		public int MaxWorkPotential { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002511 File Offset: 0x00000711
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002519 File Offset: 0x00000719
		public int ActualWorkPerformed { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002522 File Offset: 0x00000722
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000252A File Offset: 0x0000072A
		public bool WasWorkingHour { get; private set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002533 File Offset: 0x00000733
		public HourlyProductivity(int maxWorkPotential, int actualWorkPerformed, bool wasWorkingHour)
		{
			this.MaxWorkPotential = maxWorkPotential;
			this.ActualWorkPerformed = actualWorkPerformed;
			this.WasWorkingHour = wasWorkingHour;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002550 File Offset: 0x00000750
		public static HourlyProductivity CreateDefault()
		{
			return new HourlyProductivity(0, 0, false);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000255A File Offset: 0x0000075A
		public float Productivity
		{
			get
			{
				if (this.MaxWorkPotential != 0)
				{
					return (float)this.ActualWorkPerformed / (float)this.MaxWorkPotential;
				}
				return 0f;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002579 File Offset: 0x00000779
		public void Reset()
		{
			this.MaxWorkPotential = 0;
			this.ActualWorkPerformed = 0;
			this.WasWorkingHour = false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002590 File Offset: 0x00000790
		public void AddSample(int maxWorkPotential, int actualWorkPerformed)
		{
			this.MaxWorkPotential += maxWorkPotential;
			this.ActualWorkPerformed += actualWorkPerformed;
			this.WasWorkingHour = true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025B5 File Offset: 0x000007B5
		public void CopyValuesFrom(HourlyProductivity otherProductivity)
		{
			this.MaxWorkPotential = otherProductivity.MaxWorkPotential;
			this.ActualWorkPerformed = otherProductivity.ActualWorkPerformed;
			this.WasWorkingHour = otherProductivity.WasWorkingHour;
		}
	}
}
