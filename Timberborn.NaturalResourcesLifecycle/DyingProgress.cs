using System;
using Timberborn.TimeSystem;

namespace Timberborn.NaturalResourcesLifecycle
{
	// Token: 0x02000005 RID: 5
	public readonly struct DyingProgress
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000238F File Offset: 0x0000058F
		public bool IsDying { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002397 File Offset: 0x00000597
		public bool Died { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000239F File Offset: 0x0000059F
		public float Progress { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023A7 File Offset: 0x000005A7
		public float DaysLeft { get; }

		// Token: 0x06000013 RID: 19 RVA: 0x000023AF File Offset: 0x000005AF
		public DyingProgress(bool isDying, bool died, float progress, float daysLeft)
		{
			this.IsDying = isDying;
			this.Died = died;
			this.Progress = progress;
			this.DaysLeft = daysLeft;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023CE File Offset: 0x000005CE
		public static DyingProgress Create(ITimeTrigger timeTrigger)
		{
			return new DyingProgress(timeTrigger.InProgress, timeTrigger.Finished, timeTrigger.Progress, timeTrigger.DaysLeft);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023ED File Offset: 0x000005ED
		public static DyingProgress Healthy
		{
			get
			{
				return new DyingProgress(false, false, 0f, float.MaxValue);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002400 File Offset: 0x00000600
		public static DyingProgress Dead
		{
			get
			{
				return new DyingProgress(false, true, 1f, 0f);
			}
		}
	}
}
