using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Timberborn.Multithreading
{
	// Token: 0x02000012 RID: 18
	public class ScheduledTask<T> : IScheduledTask where T : struct, ITaskRunner
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002A4D File Offset: 0x00000C4D
		public ScheduledTask(ScheduledTaskPool scheduledTaskPool, ISnapshotCollector snapshotCollector)
		{
			this._scheduledTaskPool = scheduledTaskPool;
			this._snapshotCollector = snapshotCollector;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A7C File Offset: 0x00000C7C
		public unsafe ParallelizerHandle Initialize(Parallelizer parallelizer, in T task, ReadOnlySpan<ParallelizerHandle> dependencies)
		{
			parallelizer.IncrementPendingTasks();
			object lockObject = this._lockObject;
			ParallelizerHandle result;
			lock (lockObject)
			{
				this._task = task;
				ReadOnlySpan<ParallelizerHandle> readOnlySpan = dependencies;
				for (int i = 0; i < readOnlySpan.Length; i++)
				{
					ParallelizerHandle parallelizerHandle = *readOnlySpan[i];
					if (parallelizerHandle.Task.AddDependent(parallelizerHandle.Version, this))
					{
						this._prerequisites++;
					}
				}
				if (this._prerequisites == 0)
				{
					this.AddToReadyTasks(parallelizer);
				}
				result = new ParallelizerHandle(this, Volatile.Read(ref this._version), parallelizer);
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B34 File Offset: 0x00000D34
		public bool AddDependent(int expectedVersion, IScheduledTask dependent)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (this._version == expectedVersion)
				{
					this._dependents.Add(dependent);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void Run(Parallelizer parallelizer)
		{
			object lockObject = this._lockObject;
			int runIndex;
			lock (lockObject)
			{
				int nextRunIndex = this._nextRunIndex;
				this._nextRunIndex = nextRunIndex + 1;
				runIndex = nextRunIndex;
			}
			this.RunTask(runIndex);
			lockObject = this._lockObject;
			lock (lockObject)
			{
				this._completedRuns++;
				if (this._completedRuns == this._task.ExpectedRuns)
				{
					this.AdvanceDependents(parallelizer);
					Interlocked.Increment(ref this._version);
					this._scheduledTaskPool.Return<T>(this);
					parallelizer.DecrementPendingTasks();
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C54 File Offset: 0x00000E54
		public void AdvancePrerequisites(Parallelizer parallelizer)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this._prerequisites--;
				if (this._prerequisites == 0)
				{
					this.AddToReadyTasks(parallelizer);
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CAC File Offset: 0x00000EAC
		public void Reset()
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this._task = default(T);
				this._nextRunIndex = 0;
				this._completedRuns = 0;
				this._prerequisites = 0;
				this._dependents.Clear();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D14 File Offset: 0x00000F14
		public void RunTask(int runIndex)
		{
			if (this._snapshotCollector.IsCollecting)
			{
				long timestamp = Stopwatch.GetTimestamp();
				this._task.Run(runIndex);
				long timestamp2 = Stopwatch.GetTimestamp();
				this._snapshotCollector.AddTaskSample(runIndex, this._task.ExpectedRuns, timestamp, timestamp2, this._task.GetType());
				return;
			}
			this._task.Run(runIndex);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D90 File Offset: 0x00000F90
		public void AdvanceDependents(Parallelizer parallelizer)
		{
			foreach (IScheduledTask scheduledTask in this._dependents)
			{
				scheduledTask.AdvancePrerequisites(parallelizer);
			}
			this._dependents.Clear();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DEC File Offset: 0x00000FEC
		public void AddToReadyTasks(Parallelizer parallelizer)
		{
			int expectedRuns = this._task.ExpectedRuns;
			for (int i = 0; i < expectedRuns; i++)
			{
				parallelizer.AddReadyTask(this);
			}
		}

		// Token: 0x0400001E RID: 30
		public readonly ScheduledTaskPool _scheduledTaskPool;

		// Token: 0x0400001F RID: 31
		public readonly ISnapshotCollector _snapshotCollector;

		// Token: 0x04000020 RID: 32
		public int _version;

		// Token: 0x04000021 RID: 33
		public T _task;

		// Token: 0x04000022 RID: 34
		public int _nextRunIndex;

		// Token: 0x04000023 RID: 35
		public int _completedRuns;

		// Token: 0x04000024 RID: 36
		public int _prerequisites;

		// Token: 0x04000025 RID: 37
		public readonly List<IScheduledTask> _dependents = new List<IScheduledTask>();

		// Token: 0x04000026 RID: 38
		public readonly object _lockObject = new object();
	}
}
