using System;

namespace Timberborn.Multithreading
{
	// Token: 0x02000011 RID: 17
	public readonly struct ParallelizerHandle
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029FA File Offset: 0x00000BFA
		internal IScheduledTask Task { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A02 File Offset: 0x00000C02
		internal int Version { get; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002A0A File Offset: 0x00000C0A
		public ParallelizerHandle(IScheduledTask task, int version, Parallelizer parallelizer)
		{
			this.Task = task;
			this.Version = version;
			this._parallelizer = parallelizer;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A21 File Offset: 0x00000C21
		public ParallelizerHandle ContinueWith<T>(in T task) where T : struct, IParallelizerSingleTask
		{
			return this._parallelizer.Schedule<T>(task, this);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A35 File Offset: 0x00000C35
		public ParallelizerHandle ContinueWith<T>(int fromInclusive, int toExclusive, int batchSize, in T task) where T : struct, IParallelizerLoopTask
		{
			return this._parallelizer.Schedule<T>(fromInclusive, toExclusive, batchSize, task, this);
		}

		// Token: 0x0400001D RID: 29
		public readonly Parallelizer _parallelizer;
	}
}
