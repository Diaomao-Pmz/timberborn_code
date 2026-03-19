using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;

namespace Timberborn.MultithreadingAnalysis
{
	// Token: 0x02000007 RID: 7
	public class SnapshotCollector : ISnapshotCollector, ITickableSingleton
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000D RID: 13 RVA: 0x00002144 File Offset: 0x00000344
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x0000217C File Offset: 0x0000037C
		public event EventHandler<Snapshot> SnapshotCollected;

		// Token: 0x0600000F RID: 15 RVA: 0x000021B1 File Offset: 0x000003B1
		public SnapshotCollector(SpeedManager speedManager)
		{
			this._speedManager = speedManager;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021D6 File Offset: 0x000003D6
		public bool IsCollecting
		{
			get
			{
				return this._isCollecting;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Tick()
		{
			if (this.IsCollecting)
			{
				this._remainingTicks--;
				if (this._remainingTicks <= 0)
				{
					this.FinishCollection();
					this._isCollecting = false;
					return;
				}
			}
			else if (this._collectionScheduled)
			{
				this._isCollecting = true;
				this._collectionScheduled = false;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002234 File Offset: 0x00000434
		public void ScheduleCollection(int ticks)
		{
			if (!this._collectionScheduled && !this.IsCollecting)
			{
				this._collectionScheduled = true;
				this._scheduledTicks = ticks;
				this._remainingTicks = ticks;
				this._speedManager.ChangeAndLockSpeed(1f);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002278 File Offset: 0x00000478
		public void AddTaskSample(int run, int totalRuns, long startTimestamp, long endTimestamp, Type type)
		{
			Thread currentThread = Thread.CurrentThread;
			TaskSample item = new TaskSample(run, totalRuns, startTimestamp, endTimestamp, currentThread, type);
			this._taskSamples.Enqueue(item);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A6 File Offset: 0x000004A6
		public void AddMarker(string id)
		{
			if (this.IsCollecting)
			{
				this._markers.Enqueue(new Marker(id, Stopwatch.GetTimestamp(), Thread.CurrentThread));
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022CC File Offset: 0x000004CC
		public void FinishCollection()
		{
			EventHandler<Snapshot> snapshotCollected = this.SnapshotCollected;
			if (snapshotCollected != null)
			{
				snapshotCollected(this, new Snapshot(this._scheduledTicks, this._taskSamples.ToList<TaskSample>().AsReadOnlyList<TaskSample>(), this._markers.ToList<Marker>().AsReadOnlyList<Marker>()));
			}
			this._taskSamples.Clear();
			this._markers.Clear();
			this._speedManager.UnlockSpeed();
		}

		// Token: 0x0400000D RID: 13
		public readonly SpeedManager _speedManager;

		// Token: 0x0400000E RID: 14
		public readonly ConcurrentQueue<TaskSample> _taskSamples = new ConcurrentQueue<TaskSample>();

		// Token: 0x0400000F RID: 15
		public readonly ConcurrentQueue<Marker> _markers = new ConcurrentQueue<Marker>();

		// Token: 0x04000010 RID: 16
		public volatile bool _isCollecting;

		// Token: 0x04000011 RID: 17
		public bool _collectionScheduled;

		// Token: 0x04000012 RID: 18
		public int _remainingTicks;

		// Token: 0x04000013 RID: 19
		public int _scheduledTicks;
	}
}
