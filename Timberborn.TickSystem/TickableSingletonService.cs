using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Timberborn.Metrics;
using Timberborn.Multithreading;
using Timberborn.SingletonSystem;

namespace Timberborn.TickSystem
{
	// Token: 0x02000015 RID: 21
	public class TickableSingletonService : ILoadableSingleton, ITickableSingletonService
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003F RID: 63 RVA: 0x00002690 File Offset: 0x00000890
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x000026C8 File Offset: 0x000008C8
		public event EventHandler ForcedParallelTickFinished;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000026FD File Offset: 0x000008FD
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002705 File Offset: 0x00000905
		public TimeSpan LastParallelTickDuration { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000270E File Offset: 0x0000090E
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002716 File Offset: 0x00000916
		public bool ParalleTicklIsFinished { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000271F File Offset: 0x0000091F
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002727 File Offset: 0x00000927
		public bool IsStartingParallelTick { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x00002730 File Offset: 0x00000930
		public TickableSingletonService(ISingletonRepository singletonRepository, ITickingMode tickingMode, IMetricsService metricsService, IParallelizer parallelizer, ISnapshotCollector snapshotCollector)
		{
			this._singletonRepository = singletonRepository;
			this._tickingMode = tickingMode;
			this._metricsService = metricsService;
			this._parallelizer = parallelizer;
			this._snapshotCollector = snapshotCollector;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002760 File Offset: 0x00000960
		public void Load()
		{
			this._tickableSingletons = this._singletonRepository.GetSingletons<ITickableSingleton>().Where(new Func<ITickableSingleton, bool>(this._tickingMode.SingletonIsActiveInThisMode)).OrderBy(delegate(ITickableSingleton tickable)
			{
				if (!(tickable is ILateTickable))
				{
					return 0;
				}
				return 1;
			}).Select(new Func<ITickableSingleton, TickableSingletonService.MeteredSingleton>(this.CreateMeteredSingleton)).ToImmutableArray<TickableSingletonService.MeteredSingleton>();
			this._parallelTickableSingletons = this._singletonRepository.GetSingletons<IParallelTickableSingleton>().Where(new Func<IParallelTickableSingleton, bool>(this._tickingMode.SingletonIsActiveInThisMode)).ToImmutableArray<IParallelTickableSingleton>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000027FC File Offset: 0x000009FC
		public void TickAll()
		{
			this.FinishParallelTick();
			this.TickSingletons();
			this.StartParallelTick();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002810 File Offset: 0x00000A10
		public void ForceFinishParallelTick()
		{
			this.FinishParallelTick();
			EventHandler forcedParallelTickFinished = this.ForcedParallelTickFinished;
			if (forcedParallelTickFinished == null)
			{
				return;
			}
			forcedParallelTickFinished(this, EventArgs.Empty);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002830 File Offset: 0x00000A30
		public void StartParallelTick()
		{
			this.ParalleTicklIsFinished = false;
			this.IsStartingParallelTick = true;
			this._parallelizer.StartScheduling();
			this._parallelTickStartTimestamp = Stopwatch.GetTimestamp();
			this._snapshotCollector.AddMarker(TickableSingletonService.ScheduleStartMarkerId);
			foreach (IParallelTickableSingleton parallelTickableSingleton in this._parallelTickableSingletons)
			{
				parallelTickableSingleton.StartParallelTick();
			}
			this._snapshotCollector.AddMarker(TickableSingletonService.ScheduleEndMarkerId);
			this._parallelizer.StopScheduling();
			this.IsStartingParallelTick = false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void TickSingletons()
		{
			for (int i = 0; i < this._tickableSingletons.Length; i++)
			{
				this._tickableSingletons[i].Tick();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028F0 File Offset: 0x00000AF0
		public void FinishParallelTick()
		{
			this._snapshotCollector.AddMarker(TickableSingletonService.WaitStartMarkerId);
			this._parallelizer.Wait();
			this._snapshotCollector.AddMarker(TickableSingletonService.WaitEndMarkerId);
			this.LastParallelTickDuration = TimeSpan.FromTicks(this._parallelizer.LastTaskTimestamp - this._parallelTickStartTimestamp);
			this._parallelizer.ThrowIfAnyPendingTasks();
			this.ParalleTicklIsFinished = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002958 File Offset: 0x00000B58
		public TickableSingletonService.MeteredSingleton CreateMeteredSingleton(ITickableSingleton tickableSingleton)
		{
			string name = tickableSingleton.GetType().Name;
			ITimerMetric timerMetric = this._metricsService.GetTimerMetric("Tick", name);
			return new TickableSingletonService.MeteredSingleton(tickableSingleton, timerMetric, this._metricsService.MetricsEnabled);
		}

		// Token: 0x0400001E RID: 30
		public static readonly string WaitStartMarkerId = "Wait - Start";

		// Token: 0x0400001F RID: 31
		public static readonly string WaitEndMarkerId = "Wait - End";

		// Token: 0x04000020 RID: 32
		public static readonly string ScheduleStartMarkerId = "Schedule - Start";

		// Token: 0x04000021 RID: 33
		public static readonly string ScheduleEndMarkerId = "Schedule - End";

		// Token: 0x04000026 RID: 38
		public readonly ISingletonRepository _singletonRepository;

		// Token: 0x04000027 RID: 39
		public readonly ITickingMode _tickingMode;

		// Token: 0x04000028 RID: 40
		public readonly IMetricsService _metricsService;

		// Token: 0x04000029 RID: 41
		public readonly IParallelizer _parallelizer;

		// Token: 0x0400002A RID: 42
		public readonly ISnapshotCollector _snapshotCollector;

		// Token: 0x0400002B RID: 43
		public ImmutableArray<TickableSingletonService.MeteredSingleton> _tickableSingletons;

		// Token: 0x0400002C RID: 44
		public ImmutableArray<IParallelTickableSingleton> _parallelTickableSingletons;

		// Token: 0x0400002D RID: 45
		public long _parallelTickStartTimestamp;

		// Token: 0x02000016 RID: 22
		public readonly struct MeteredSingleton
		{
			// Token: 0x06000050 RID: 80 RVA: 0x000029BF File Offset: 0x00000BBF
			public MeteredSingleton(ITickableSingleton tickableSingleton, ITimerMetric metric, bool metricsEnabled)
			{
				this._tickableSingleton = tickableSingleton;
				this._metric = metric;
				this._metricsEnabled = metricsEnabled;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x000029D6 File Offset: 0x00000BD6
			public void Tick()
			{
				if (this._metricsEnabled)
				{
					this._metric.Resume();
				}
				this._tickableSingleton.Tick();
				if (this._metricsEnabled)
				{
					this._metric.Pause();
				}
			}

			// Token: 0x0400002E RID: 46
			public readonly ITickableSingleton _tickableSingleton;

			// Token: 0x0400002F RID: 47
			public readonly ITimerMetric _metric;

			// Token: 0x04000030 RID: 48
			public readonly bool _metricsEnabled;
		}
	}
}
