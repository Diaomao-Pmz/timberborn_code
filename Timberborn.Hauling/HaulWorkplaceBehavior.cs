using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Hauling
{
	// Token: 0x02000010 RID: 16
	public class HaulWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000029F3 File Offset: 0x00000BF3
		public void Awake()
		{
			this._haulingCenter = base.GetComponent<HaulingCenter>();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A04 File Offset: 0x00000C04
		public override Decision Decide(BehaviorAgent agent)
		{
			this._haulingCenter.GetWorkplaceBehaviorsOrdered(this._workplaceBehaviors);
			foreach (WorkplaceBehavior workplaceBehavior in this._workplaceBehaviors)
			{
				Decision decision = workplaceBehavior.Decide(agent);
				if (!decision.ShouldReleaseNow)
				{
					this._workplaceBehaviors.Clear();
					return Decision.TransferNow(workplaceBehavior, decision);
				}
			}
			this._workplaceBehaviors.Clear();
			return Decision.ReleaseNow();
		}

		// Token: 0x0400001B RID: 27
		public HaulingCenter _haulingCenter;

		// Token: 0x0400001C RID: 28
		public readonly List<WorkplaceBehavior> _workplaceBehaviors = new List<WorkplaceBehavior>();
	}
}
