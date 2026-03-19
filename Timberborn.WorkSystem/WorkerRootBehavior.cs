using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Metrics;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000017 RID: 23
	public class WorkerRootBehavior : RootBehavior, IAwakableComponent
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000310B File Offset: 0x0000130B
		public WorkerRootBehavior(TimerMetricCache<WorkplaceBehavior> timerMetricCache, IMetricsService metricsService)
		{
			this._timerMetricCache = timerMetricCache;
			this._metricsService = metricsService;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003121 File Offset: 0x00001321
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
			this._workerWorkingHours = base.GetComponent<WorkerWorkingHours>();
			this._workRefuser = base.GetComponent<WorkRefuser>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
			this._communityServiceBehavior = base.GetComponent<CommunityServiceBehavior>();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003160 File Offset: 0x00001360
		public override Decision Decide(BehaviorAgent agent)
		{
			if (this._workRefuser.RefusesWork)
			{
				return Decision.ReleaseNow();
			}
			if (this._worker.Employed && this._workerWorkingHours.AreWorkingHours)
			{
				return this.DecideAsWorker();
			}
			return this._communityServiceBehavior.Decide(agent);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031AD File Offset: 0x000013AD
		public Decision DecideAsWorker()
		{
			if (this._worker.Workplace.Overstaffed)
			{
				this._worker.Unemploy();
				return Decision.ReleaseNextTick();
			}
			return this.WorkAtWorkplace();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031D8 File Offset: 0x000013D8
		public Decision WorkAtWorkplace()
		{
			foreach (WorkplaceBehavior workplaceBehavior in this._worker.Workplace.WorkplaceBehaviors)
			{
				if (this._metricsService.MetricsEnabled)
				{
					this._timerMetricCache.Get(workplaceBehavior).Resume();
				}
				Decision decision = workplaceBehavior.Decide(this._behaviorAgent);
				if (this._metricsService.MetricsEnabled)
				{
					this._timerMetricCache.Get(workplaceBehavior).Pause();
				}
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(workplaceBehavior, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x04000032 RID: 50
		public readonly TimerMetricCache<WorkplaceBehavior> _timerMetricCache;

		// Token: 0x04000033 RID: 51
		public readonly IMetricsService _metricsService;

		// Token: 0x04000034 RID: 52
		public Worker _worker;

		// Token: 0x04000035 RID: 53
		public WorkerWorkingHours _workerWorkingHours;

		// Token: 0x04000036 RID: 54
		public WorkRefuser _workRefuser;

		// Token: 0x04000037 RID: 55
		public BehaviorAgent _behaviorAgent;

		// Token: 0x04000038 RID: 56
		public CommunityServiceBehavior _communityServiceBehavior;
	}
}
