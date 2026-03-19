using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EnterableSystem;
using Timberborn.WalkingSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200001D RID: 29
	public class ProduceWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003E9A File Offset: 0x0000209A
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._workplace = base.GetComponent<Workplace>();
			this._enterable = base.GetComponent<Enterable>();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003EC0 File Offset: 0x000020C0
		public override Decision Decide(BehaviorAgent agent)
		{
			if (!this._manufactory.IsReadyToProduce)
			{
				return Decision.ReleaseNow();
			}
			WalkInsideExecutor component = agent.GetComponent<WalkInsideExecutor>();
			switch (component.Launch(this._enterable))
			{
			case ExecutorStatus.Success:
			{
				ProduceExecutor component2 = agent.GetComponent<ProduceExecutor>();
				if (!component2.Launch(0.25f))
				{
					return Decision.ReleaseNextTick();
				}
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

		// Token: 0x04000062 RID: 98
		public Manufactory _manufactory;

		// Token: 0x04000063 RID: 99
		public Workplace _workplace;

		// Token: 0x04000064 RID: 100
		public Enterable _enterable;
	}
}
