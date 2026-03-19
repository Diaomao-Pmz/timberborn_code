using System;
using Timberborn.BehaviorSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000032 RID: 50
	public class WorkWorkplaceBehavior : WorkplaceBehavior
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00005B63 File Offset: 0x00003D63
		public override Decision Decide(BehaviorAgent agent)
		{
			return agent.GetComponent<WorkplaceWorkStarter>().StartWorking();
		}
	}
}
