using System;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000B RID: 11
	public readonly struct LoopTaskRunner<T> : ITaskRunner where T : struct, IParallelizerLoopTask
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002232 File Offset: 0x00000432
		public int ExpectedRuns { get; }

		// Token: 0x0600001F RID: 31 RVA: 0x0000223A File Offset: 0x0000043A
		public LoopTaskRunner(T task, int fromInclusive, int toExclusive, int batchSize)
		{
			this._task = task;
			this._fromInclusive = fromInclusive;
			this._toExclusive = toExclusive;
			this._batchSize = batchSize;
			this.ExpectedRuns = (this._toExclusive - this._fromInclusive + batchSize - 1) / batchSize;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002274 File Offset: 0x00000474
		public void Run(int runIndex)
		{
			int expectedRuns = this.ExpectedRuns;
			if (runIndex < 0)
			{
				throw new ArgumentException(string.Format("{0} {1} of task {2}", "runIndex", runIndex, typeof(T)) + " must be at least zero");
			}
			if (runIndex >= expectedRuns)
			{
				throw new ArgumentException(string.Format("{0} {1} of task {2} must be less than {3}", new object[]
				{
					"runIndex",
					runIndex,
					typeof(T),
					expectedRuns
				}));
			}
			int num = this._fromInclusive + runIndex * this._batchSize;
			int num2 = Math.Min(num + this._batchSize, this._toExclusive);
			for (int i = num; i < num2; i++)
			{
				T task = this._task;
				task.Run(i);
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly T _task;

		// Token: 0x0400000A RID: 10
		public readonly int _fromInclusive;

		// Token: 0x0400000B RID: 11
		public readonly int _toExclusive;

		// Token: 0x0400000C RID: 12
		public readonly int _batchSize;
	}
}
