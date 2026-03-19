using System;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x0200000A RID: 10
	public readonly struct Decision
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002684 File Offset: 0x00000884
		public IExecutor Executor { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000268C File Offset: 0x0000088C
		public Behavior Behavior { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002694 File Offset: 0x00000894
		public bool ShouldReleaseNow { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000269C File Offset: 0x0000089C
		public bool ShouldReturnToBehavior { get; }

		// Token: 0x06000028 RID: 40 RVA: 0x000026A4 File Offset: 0x000008A4
		public Decision(IExecutor executor, Behavior behavior, bool shouldReleaseNow, bool shouldReturnToBehavior)
		{
			this.Executor = executor;
			this.Behavior = behavior;
			this.ShouldReleaseNow = shouldReleaseNow;
			this.ShouldReturnToBehavior = shouldReturnToBehavior;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026C3 File Offset: 0x000008C3
		public static Decision ReturnWhenFinished(IExecutor executor)
		{
			return new Decision(executor, null, false, true);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026CE File Offset: 0x000008CE
		public static Decision ReleaseWhenFinished(IExecutor executor)
		{
			return new Decision(executor, null, false, false);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026D9 File Offset: 0x000008D9
		public static Decision ReturnNextTick()
		{
			return new Decision(null, null, false, true);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026E4 File Offset: 0x000008E4
		public static Decision ReleaseNextTick()
		{
			return new Decision(null, null, false, false);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026F0 File Offset: 0x000008F0
		public static Decision TransferNow(Behavior behavior, in Decision decision)
		{
			Behavior behavior2 = decision.Behavior ?? behavior;
			return new Decision(decision.Executor, behavior2, decision.ShouldReleaseNow, decision.ShouldReturnToBehavior);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002721 File Offset: 0x00000921
		public static Decision ReleaseNow()
		{
			return new Decision(null, null, true, false);
		}
	}
}
