using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.EnterableSystem;
using Timberborn.WalkingSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x0200002B RID: 43
	public class WorkplaceWorkStarter : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000553B File Offset: 0x0000373B
		public void Awake()
		{
			this._worker = base.GetComponent<Worker>();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005549 File Offset: 0x00003749
		public void Start()
		{
			this._walkInsideExecutor = base.GetComponent<WalkInsideExecutor>();
			this._workExecutor = base.GetComponent<WorkExecutor>();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005564 File Offset: 0x00003764
		public Decision StartWorking()
		{
			Workplace workplace = this._worker.Workplace;
			Enterable component = workplace.GetComponent<Enterable>();
			switch (this._walkInsideExecutor.Launch(component))
			{
			case ExecutorStatus.Success:
				this._workExecutor.Launch(0.25f);
				return Decision.ReleaseWhenFinished(this._workExecutor);
			case ExecutorStatus.Failure:
				workplace.UnassignWorker(this._worker);
				return Decision.ReleaseNextTick();
			case ExecutorStatus.Running:
				return Decision.ReleaseWhenFinished(this._walkInsideExecutor);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000097 RID: 151
		public WalkInsideExecutor _walkInsideExecutor;

		// Token: 0x04000098 RID: 152
		public WorkExecutor _workExecutor;

		// Token: 0x04000099 RID: 153
		public Worker _worker;
	}
}
