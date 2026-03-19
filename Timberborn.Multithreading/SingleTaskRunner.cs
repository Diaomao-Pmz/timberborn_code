using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000015 RID: 21
	public readonly struct SingleTaskRunner<T> : ITaskRunner where T : struct, IParallelizerSingleTask
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002EF6 File Offset: 0x000010F6
		public SingleTaskRunner(T task)
		{
			this._task = task;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002EFF File Offset: 0x000010FF
		public int ExpectedRuns
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F04 File Offset: 0x00001104
		public void Run(int runIndex)
		{
			if (runIndex != 0)
			{
				throw new ArgumentException("runIndex must be zero");
			}
			T task = this._task;
			task.Run();
		}

		// Token: 0x0400002B RID: 43
		public readonly T _task;
	}
}
