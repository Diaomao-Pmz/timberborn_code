using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using Timberborn.Common;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Multithreading
{
	// Token: 0x0200000E RID: 14
	public class Parallelizer : IParallelizer, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002386 File Offset: 0x00000586
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000238E File Offset: 0x0000058E
		public int NumberOfThreads { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002397 File Offset: 0x00000597
		public Parallelizer(ScheduledTaskPool scheduledTaskPool)
		{
			this._scheduledTaskPool = scheduledTaskPool;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023C8 File Offset: 0x000005C8
		public long LastTaskTimestamp
		{
			get
			{
				return this._lastTaskTimestamp;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023D0 File Offset: 0x000005D0
		public void Load()
		{
			this._mainThread = Thread.CurrentThread;
			this.NumberOfThreads = Parallelizer.CalculateNumberOfThreads();
			if (!Application.isEditor)
			{
				Debug.Log(string.Format("Number of threads: {0}", this.NumberOfThreads));
			}
			List<Thread> list = new List<Thread>();
			for (int i = 0; i < this.NumberOfThreads; i++)
			{
				Thread item = new Thread(new ThreadStart(this.ThreadStart))
				{
					Name = string.Format("{0}-{1}", "Parallelizer", i)
				};
				list.Add(item);
			}
			this._threads = list.ToImmutableArray<Thread>();
			foreach (Thread thread in this._threads)
			{
				thread.Start();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000248E File Offset: 0x0000068E
		public void Unload()
		{
			this.CloseThreads();
			this.ThrowIfHasExceptions();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000249C File Offset: 0x0000069C
		public ParallelizerHandle Schedule<T>(in T task) where T : struct, IParallelizerSingleTask
		{
			return this.Schedule<T>(task, ReadOnlySpan<ParallelizerHandle>.Empty);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024AC File Offset: 0x000006AC
		public ParallelizerHandle Schedule<T>(in T task, ParallelizerHandle dependency) where T : struct, IParallelizerSingleTask
		{
			this._reusableOneDepdendencyArray[0] = dependency;
			ParallelizerHandle result;
			try
			{
				result = this.Schedule<T>(task, MemoryExtensions.AsSpan<ParallelizerHandle>(this._reusableOneDepdendencyArray));
			}
			finally
			{
				this._reusableOneDepdendencyArray[0] = default(ParallelizerHandle);
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002504 File Offset: 0x00000704
		public ParallelizerHandle Schedule<T>(in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerSingleTask
		{
			this.ValidateScheduling();
			ScheduledTask<SingleTaskRunner<T>> scheduledTask = this._scheduledTaskPool.Rent<SingleTaskRunner<T>>();
			SingleTaskRunner<T> singleTaskRunner = new SingleTaskRunner<T>(task);
			return scheduledTask.Initialize(this, singleTaskRunner, dependencies);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002537 File Offset: 0x00000737
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task) where T : struct, IParallelizerLoopTask
		{
			return this.Schedule<T>(fromInclusive, toExclusive, batchSize, task, ReadOnlySpan<ParallelizerHandle>.Empty);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000254C File Offset: 0x0000074C
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ParallelizerHandle dependency) where T : struct, IParallelizerLoopTask
		{
			this._reusableOneDepdendencyArray[0] = dependency;
			ParallelizerHandle result;
			try
			{
				result = this.Schedule<T>(fromInclusive, toExclusive, batchSize, task, MemoryExtensions.AsSpan<ParallelizerHandle>(this._reusableOneDepdendencyArray));
			}
			finally
			{
				this._reusableOneDepdendencyArray[0] = default(ParallelizerHandle);
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025AC File Offset: 0x000007AC
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerLoopTask
		{
			this.ValidateScheduling();
			Parallelizer.ValidateLoopRange<T>(fromInclusive, toExclusive);
			Parallelizer.ValidateBatchSize<T>(batchSize);
			ScheduledTask<LoopTaskRunner<T>> scheduledTask = this._scheduledTaskPool.Rent<LoopTaskRunner<T>>();
			LoopTaskRunner<T> loopTaskRunner = new LoopTaskRunner<T>(task, fromInclusive, toExclusive, batchSize);
			return scheduledTask.Initialize(this, loopTaskRunner, dependencies);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025F1 File Offset: 0x000007F1
		public void StartScheduling()
		{
			if (this._scheduling)
			{
				throw new InvalidOperationException("Already in scheduling phase.");
			}
			this._scheduling = true;
			this.ThrowIfAnyPendingTasks();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002613 File Offset: 0x00000813
		public void StopScheduling()
		{
			this._scheduling = false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000261C File Offset: 0x0000081C
		public void Wait()
		{
			this.ThrowIfNotMainThread();
			SpinWait spinWait = default(SpinWait);
			while (Interlocked.CompareExchange(ref this._pendingTasks, 0, 0) > 0)
			{
				this.ThrowIfHasExceptions();
				spinWait.SpinOnce();
			}
			this.ThrowIfHasExceptions();
			this.ThrowIfAnyPendingTasks();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002664 File Offset: 0x00000864
		public void ThrowIfAnyPendingTasks()
		{
			int count = this._readyTasks.Count;
			if (Interlocked.CompareExchange(ref this._pendingTasks, 0, 0) > 0 || count > 0)
			{
				throw new ParallelizerException(string.Format("Unexpected pending tasks! Counter: {0}. Queue: {1}", this._pendingTasks, count));
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026B2 File Offset: 0x000008B2
		public void AddReadyTask(IScheduledTask scheduledTask)
		{
			this._readyTasks.Add(scheduledTask);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026C0 File Offset: 0x000008C0
		public void IncrementPendingTasks()
		{
			Interlocked.Increment(ref this._pendingTasks);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026CE File Offset: 0x000008CE
		public void DecrementPendingTasks()
		{
			Interlocked.Decrement(ref this._pendingTasks);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026DC File Offset: 0x000008DC
		public void ThreadStart()
		{
			try
			{
				IScheduledTask scheduledTask;
				while (this._readyTasks.TryTakeBlocking(out scheduledTask) && this._exceptions.IsEmpty)
				{
					this.ExecuteTask(scheduledTask);
				}
			}
			catch (Exception exception)
			{
				this.EnqueueException(exception);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000272C File Offset: 0x0000092C
		public void ExecuteTask(IScheduledTask scheduledTask)
		{
			scheduledTask.Run(this);
			Interlocked.Exchange(ref this._lastTaskTimestamp, Stopwatch.GetTimestamp());
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002748 File Offset: 0x00000948
		public void CloseThreads()
		{
			if (this._closed)
			{
				Debug.Log("Threads already closed");
				return;
			}
			this._readyTasks.CompleteAdding();
			if (new ImmutableArray<Thread>?(this._threads) != null)
			{
				foreach (Thread thread in this._threads)
				{
					thread.Join();
				}
			}
			this._closed = true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027B8 File Offset: 0x000009B8
		public void ThrowIfHasExceptions()
		{
			this.ThrowIfNotMainThread();
			if (!this._exceptions.IsEmpty)
			{
				Debug.Log("Closing threads due to uncaught exceptions");
				this.CloseThreads();
				Debug.Log("Threads closed");
				throw ParallelizerException.From(this.CollectExceptions());
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027F3 File Offset: 0x000009F3
		public void ThrowIfNotMainThread()
		{
			if (Thread.CurrentThread != this._mainThread)
			{
				throw new InvalidOperationException("This operation can only be called on the main thread.");
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002810 File Offset: 0x00000A10
		public ImmutableArray<ParallelizerExceptionLog> CollectExceptions()
		{
			List<ParallelizerExceptionLog> list = new List<ParallelizerExceptionLog>();
			ParallelizerExceptionLog item;
			while (this._exceptions.TryDequeue(out item))
			{
				list.Add(item);
			}
			return list.ToImmutableArray<ParallelizerExceptionLog>();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002841 File Offset: 0x00000A41
		public void EnqueueException(Exception exception)
		{
			this._exceptions.Enqueue(new ParallelizerExceptionLog(exception, Thread.CurrentThread.DisplayName()));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000285E File Offset: 0x00000A5E
		public void ValidateScheduling()
		{
			if (!this._scheduling)
			{
				throw new InvalidOperationException("Cannot schedule tasks outside of schedule phase.");
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002873 File Offset: 0x00000A73
		public static void ValidateLoopRange<T>(int fromInclusive, int toExclusive)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentException(string.Format("{0} {1}", "toExclusive", toExclusive) + string.Format(" must be greater than {0} {1}", "fromInclusive", fromInclusive) + " when scheduling T");
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028B3 File Offset: 0x00000AB3
		public static void ValidateBatchSize<T>(int batchSize)
		{
			if (batchSize < 1)
			{
				throw new ArgumentException(string.Format("{0} {1} must be at least 1", "batchSize", batchSize) + " when scheduling T");
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028DE File Offset: 0x00000ADE
		public static int CalculateNumberOfThreads()
		{
			return Math.Clamp(ProcessorInfo.GetPhysicalProcessorCount() - 1, 3, 8);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028EE File Offset: 0x00000AEE
		public ParallelizerHandle Schedule<T>(in T task) where T : struct, IParallelizerSingleTask
		{
			return this.Schedule<T>(task);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028F7 File Offset: 0x00000AF7
		public ParallelizerHandle Schedule<T>(in T task, ParallelizerHandle dependency) where T : struct, IParallelizerSingleTask
		{
			return this.Schedule<T>(task, dependency);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002901 File Offset: 0x00000B01
		public ParallelizerHandle Schedule<T>(in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerSingleTask
		{
			return this.Schedule<T>(task, dependencies);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000290B File Offset: 0x00000B0B
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task) where T : struct, IParallelizerLoopTask
		{
			return this.Schedule<T>(fromInclusive, toExclusive, batchSize, task);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002918 File Offset: 0x00000B18
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ParallelizerHandle dependency) where T : struct, IParallelizerLoopTask
		{
			return this.Schedule<T>(fromInclusive, toExclusive, batchSize, task, dependency);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002927 File Offset: 0x00000B27
		public ParallelizerHandle Schedule<T>(int fromInclusive, int toExclusive, int batchSize, in T task, ReadOnlySpan<ParallelizerHandle> dependencies) where T : struct, IParallelizerLoopTask
		{
			return this.Schedule<T>(fromInclusive, toExclusive, batchSize, task, dependencies);
		}

		// Token: 0x0400000F RID: 15
		public readonly ScheduledTaskPool _scheduledTaskPool;

		// Token: 0x04000010 RID: 16
		public readonly LockingQueue<IScheduledTask> _readyTasks = new LockingQueue<IScheduledTask>();

		// Token: 0x04000011 RID: 17
		public readonly ConcurrentQueue<ParallelizerExceptionLog> _exceptions = new ConcurrentQueue<ParallelizerExceptionLog>();

		// Token: 0x04000012 RID: 18
		public readonly ParallelizerHandle[] _reusableOneDepdendencyArray = new ParallelizerHandle[1];

		// Token: 0x04000013 RID: 19
		public int _pendingTasks;

		// Token: 0x04000014 RID: 20
		public long _lastTaskTimestamp;

		// Token: 0x04000015 RID: 21
		public ImmutableArray<Thread> _threads;

		// Token: 0x04000016 RID: 22
		public Thread _mainThread;

		// Token: 0x04000017 RID: 23
		public bool _closed;

		// Token: 0x04000018 RID: 24
		public bool _scheduling;
	}
}
