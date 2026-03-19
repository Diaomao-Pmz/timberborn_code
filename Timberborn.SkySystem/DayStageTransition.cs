using System;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000B RID: 11
	public readonly struct DayStageTransition
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029B5 File Offset: 0x00000BB5
		public DayStage CurrentDayStage { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000029BD File Offset: 0x00000BBD
		public string CurrentDayStageHazardousWeatherId { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000029C5 File Offset: 0x00000BC5
		public DayStage NextDayStage { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000029CD File Offset: 0x00000BCD
		public string NextDayStageHazardousWeatherId { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029D5 File Offset: 0x00000BD5
		public float TransitionProgress { get; }

		// Token: 0x06000040 RID: 64 RVA: 0x000029DD File Offset: 0x00000BDD
		public DayStageTransition(DayStage currentDayStage, string currentDayStageHazardousWeatherId, DayStage nextDayStage, string nextDayStageHazardousWeatherId, float transitionProgress)
		{
			this.CurrentDayStage = currentDayStage;
			this.CurrentDayStageHazardousWeatherId = currentDayStageHazardousWeatherId;
			this.NextDayStage = nextDayStage;
			this.NextDayStageHazardousWeatherId = nextDayStageHazardousWeatherId;
			this.TransitionProgress = transitionProgress;
		}
	}
}
