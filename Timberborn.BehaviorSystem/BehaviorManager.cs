using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Metrics;
using Timberborn.Persistence;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x02000007 RID: 7
	public class BehaviorManager : TickableComponent, IAwakableComponent, IPersistentEntity, ILateTickable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		public BehaviorManager(IDayNightCycle dayNightCycle, TimerMetricCache<RootBehavior> timerMetricCache, IMetricsService metricsService, ReferenceSerializer referenceSerializer, ITickService tickService)
		{
			this._dayNightCycle = dayNightCycle;
			this._timerMetricCache = timerMetricCache;
			this._metricsService = metricsService;
			this._referenceSerializer = referenceSerializer;
			this._tickService = tickService;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002138 File Offset: 0x00000338
		public BehaviorInfo RunningBehavior
		{
			get
			{
				Behavior runningBehavior = this._runningBehavior;
				return new BehaviorInfo((runningBehavior != null) ? runningBehavior.ComponentName : null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		public ExecutorInfo RunningExecutor
		{
			get
			{
				if (this._runningExecutor == null)
				{
					return default(ExecutorInfo);
				}
				return new ExecutorInfo(this._runningExecutor.GetName(), this._runningExecutorElapsedTime);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002189 File Offset: 0x00000389
		public IEnumerable<string> TimestampedBehaviorLog
		{
			get
			{
				return this._timestampedBehaviorLog.Values.AsReadOnlyEnumerable<string>();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000219B File Offset: 0x0000039B
		public void Awake()
		{
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A9 File Offset: 0x000003A9
		public override void Tick()
		{
			if (this._runningExecutor != null)
			{
				this.TickRunningExecutor();
			}
			if (this._runningExecutor == null)
			{
				this.ProcessBehaviors();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021C8 File Offset: 0x000003C8
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(BehaviorManager.BehaviorManagerKey);
			this.SaveRunningBehavior(component);
			this.SaveRunningExecutor(entitySaver, component);
			component.Set(BehaviorManager.TimestampedBehaviorLogKey, this._timestampedBehaviorLog.Values.ToList<string>());
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000220C File Offset: 0x0000040C
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(BehaviorManager.BehaviorManagerKey);
			this.LoadRunningBehavior(component);
			this.LoadRunningExecutor(entityLoader, component);
			this._timestampedBehaviorLog.AddRange(component.Get(BehaviorManager.TimestampedBehaviorLogKey));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000224A File Offset: 0x0000044A
		public void AddRootBehavior(RootBehavior rootBehavior)
		{
			this._rootBehaviors.Add(rootBehavior);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002258 File Offset: 0x00000458
		public bool IsRunningBehavior<TBehavior>()
		{
			return this._runningBehavior is TBehavior;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		public bool IsRunningExecutor<TExecutor>() where TExecutor : IExecutor
		{
			return this._runningExecutor is TExecutor;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002278 File Offset: 0x00000478
		public void SaveRunningBehavior(IObjectSaver behaviorManager)
		{
			if (this._runningBehavior)
			{
				behaviorManager.Set<Behavior>(BehaviorManager.RunningBehaviorKey, this._runningBehavior, this._referenceSerializer.Of<Behavior>());
				behaviorManager.Set(BehaviorManager.ReturnToBehaviorKey, this._returnToBehavior);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B4 File Offset: 0x000004B4
		public void LoadRunningBehavior(IObjectLoader behaviorManager)
		{
			if (behaviorManager.Has<Behavior>(BehaviorManager.RunningBehaviorKey) && behaviorManager.GetObsoletable<Behavior>(BehaviorManager.RunningBehaviorKey, this._referenceSerializer.Of<Behavior>(), out this._runningBehavior))
			{
				this._returnToBehavior = behaviorManager.Get(BehaviorManager.ReturnToBehaviorKey);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F2 File Offset: 0x000004F2
		public void SaveRunningExecutor(IEntitySaver entitySaver, IObjectSaver behaviorManager)
		{
			if (this._runningExecutor != null)
			{
				behaviorManager.Set(BehaviorManager.RunningExecutorIdKey, BehaviorManager.SerializeExecutor(this._runningExecutor));
				behaviorManager.Set(BehaviorManager.RunningExecutorElapsedTimeKey, this._runningExecutorElapsedTime);
				this._runningExecutor.Save(entitySaver);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002330 File Offset: 0x00000530
		public void LoadRunningExecutor(IEntityLoader entityLoader, IObjectLoader behaviorManager)
		{
			if (behaviorManager.Has<string>(BehaviorManager.RunningExecutorIdKey) && this._runningBehavior != null)
			{
				this._runningExecutor = this.DeserializeExecutor(behaviorManager.Get(BehaviorManager.RunningExecutorIdKey));
				if (this._runningExecutor != null)
				{
					this._runningExecutor.Load(entityLoader);
					this._runningExecutorElapsedTime = behaviorManager.Get(BehaviorManager.RunningExecutorElapsedTimeKey);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002390 File Offset: 0x00000590
		public void ProcessBehaviors()
		{
			if (this._returnToBehavior && this._runningBehavior && this.ProcessBehavior(this._runningBehavior))
			{
				return;
			}
			foreach (RootBehavior rootBehavior in this._rootBehaviors)
			{
				if (this._metricsService.MetricsEnabled)
				{
					this._timerMetricCache.Get(rootBehavior).Resume();
				}
				bool flag = this.ProcessBehavior(rootBehavior);
				if (this._metricsService.MetricsEnabled)
				{
					this._timerMetricCache.Get(rootBehavior).Pause();
				}
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002448 File Offset: 0x00000648
		public bool ProcessBehavior(Behavior behavior)
		{
			Decision decision = behavior.Decide(this._behaviorAgent);
			if (!decision.ShouldReleaseNow)
			{
				if (decision.Executor != null)
				{
					this._runningExecutor = decision.Executor;
					this._runningExecutorElapsedTime = 0f;
				}
				this.SetRunningBehavior(decision.Behavior ?? behavior);
				this._returnToBehavior = decision.ShouldReturnToBehavior;
				return true;
			}
			this._runningBehavior = null;
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024B8 File Offset: 0x000006B8
		public void SetRunningBehavior(Behavior behavior)
		{
			if (this._runningBehavior != behavior)
			{
				string value = string.Format("{0} {1:0.00}", behavior.ComponentName, this._dayNightCycle.PartialDayNumber);
				this._timestampedBehaviorLog.Add(value);
			}
			this._runningBehavior = behavior;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002504 File Offset: 0x00000704
		public void TickRunningExecutor()
		{
			this._runningExecutorElapsedTime += this._tickService.TickIntervalInSeconds;
			ExecutorStatus executorStatus = this._runningExecutor.Tick(this._dayNightCycle.FixedDeltaTimeInHours);
			if (executorStatus <= ExecutorStatus.Failure)
			{
				this._runningExecutor = null;
				return;
			}
			if (executorStatus != ExecutorStatus.Running)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002558 File Offset: 0x00000758
		public static string SerializeExecutor(IExecutor executor)
		{
			return executor.GetName();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002560 File Offset: 0x00000760
		public IExecutor DeserializeExecutor(string id)
		{
			return base.GetComponentsAllocating<IExecutor>().SingleOrDefault((IExecutor executor) => executor.GetName() == id);
		}

		// Token: 0x04000007 RID: 7
		public static readonly ComponentKey BehaviorManagerKey = new ComponentKey("BehaviorManager");

		// Token: 0x04000008 RID: 8
		public static readonly PropertyKey<Behavior> RunningBehaviorKey = new PropertyKey<Behavior>("RunningBehavior");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<bool> ReturnToBehaviorKey = new PropertyKey<bool>("ReturnToBehavior");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<string> RunningExecutorIdKey = new PropertyKey<string>("RunningExecutorId");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> RunningExecutorElapsedTimeKey = new PropertyKey<float>("RunningExecutorElapsedTime");

		// Token: 0x0400000C RID: 12
		public static readonly ListKey<string> TimestampedBehaviorLogKey = new ListKey<string>("TimestampedBehaviorLog");

		// Token: 0x0400000D RID: 13
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400000E RID: 14
		public readonly TimerMetricCache<RootBehavior> _timerMetricCache;

		// Token: 0x0400000F RID: 15
		public readonly IMetricsService _metricsService;

		// Token: 0x04000010 RID: 16
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000011 RID: 17
		public readonly ITickService _tickService;

		// Token: 0x04000012 RID: 18
		public BehaviorAgent _behaviorAgent;

		// Token: 0x04000013 RID: 19
		public readonly List<RootBehavior> _rootBehaviors = new List<RootBehavior>();

		// Token: 0x04000014 RID: 20
		public readonly CyclicBuffer<string> _timestampedBehaviorLog = new CyclicBuffer<string>(10);

		// Token: 0x04000015 RID: 21
		public Behavior _runningBehavior;

		// Token: 0x04000016 RID: 22
		public bool _returnToBehavior;

		// Token: 0x04000017 RID: 23
		public IExecutor _runningExecutor;

		// Token: 0x04000018 RID: 24
		public float _runningExecutorElapsedTime;
	}
}
