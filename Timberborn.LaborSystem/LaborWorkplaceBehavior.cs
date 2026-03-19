using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.BlockSystem;
using Timberborn.WorkSystem;

namespace Timberborn.LaborSystem
{
	// Token: 0x02000006 RID: 6
	public class LaborWorkplaceBehavior : WorkplaceBehavior, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public void Awake()
		{
			base.GetComponents<LaborBehavior>(this._laborBehaviors);
			base.DisableComponent();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F0 File Offset: 0x000002F0
		public override Decision Decide(BehaviorAgent agent)
		{
			foreach (LaborBehavior laborBehavior in this._laborBehaviors)
			{
				Decision decision = laborBehavior.Decide(agent);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(laborBehavior, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002168 File Offset: 0x00000368
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x04000006 RID: 6
		public readonly List<LaborBehavior> _laborBehaviors = new List<LaborBehavior>();
	}
}
