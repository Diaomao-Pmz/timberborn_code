using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EnterableSystem;
using Timberborn.WalkingSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000014 RID: 20
	public class WaitInsideIdlyWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._enterable = base.GetComponent<Enterable>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C00 File Offset: 0x00000E00
		public override Decision Decide(BehaviorAgent agent)
		{
			WalkInsideExecutor component = agent.GetComponent<WalkInsideExecutor>();
			switch (component.LaunchForLimitedTime(this._enterable))
			{
			case ExecutorStatus.Success:
			{
				WaitExecutor component2 = agent.GetComponent<WaitExecutor>();
				component2.LaunchForIdleTime();
				return Decision.ReleaseWhenFinished(component2);
			}
			case ExecutorStatus.Failure:
			{
				Worker component3 = agent.GetComponent<Worker>();
				this._workplace.UnassignWorker(component3);
				return Decision.ReleaseNextTick();
			}
			case ExecutorStatus.Running:
				return Decision.ReleaseWhenFinished(component);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000021 RID: 33
		public Workplace _workplace;

		// Token: 0x04000022 RID: 34
		public Enterable _enterable;
	}
}
