using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000004 RID: 4
	public interface IParallelizer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		long LastTaskTimestamp { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		int NumberOfThreads { get; }

		// Token: 0x06000005 RID: 5
		ParallelizerHandle Schedule<T>(in T task) where T : struct, IParallelizerSingleTask;

		// Token: 0x06000006 RID: 6
		ParallelizerHandle Schedule<T>(in T task, ParallelizerHandle dependency) where T : struct, IParallelizerSingleTask;

		// Token: 0x06000007 RID: 7
		ParallelizerHandle Schedule<T>(in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerSingleTask;

		// Token: 0x06000008 RID: 8
		ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task) where T : struct, IParallelizerLoopTask;

		// Token: 0x06000009 RID: 9
		ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ParallelizerHandle dependency) where T : struct, IParallelizerLoopTask;

		// Token: 0x0600000A RID: 10
		ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerLoopTask;

		// Token: 0x0600000B RID: 11
		void StartScheduling();

		// Token: 0x0600000C RID: 12
		void StopScheduling();

		// Token: 0x0600000D RID: 13
		void Wait();

		// Token: 0x0600000E RID: 14
		void ThrowIfAnyPendingTasks();
	}
}
