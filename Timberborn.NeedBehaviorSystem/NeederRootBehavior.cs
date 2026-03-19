using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001D RID: 29
	public class NeederRootBehavior : RootBehavior, IAwakableComponent
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003284 File Offset: 0x00001484
		public void Awake()
		{
			this._needBehaviorPicker = base.GetComponent<INeedBehaviorPicker>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032A0 File Offset: 0x000014A0
		public override Decision Decide(BehaviorAgent agent)
		{
			Behavior bestNeedBehavior = this._needBehaviorPicker.GetBestNeedBehavior();
			if (bestNeedBehavior != null)
			{
				Decision decision = bestNeedBehavior.Decide(this._behaviorAgent);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(bestNeedBehavior, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x04000045 RID: 69
		public INeedBehaviorPicker _needBehaviorPicker;

		// Token: 0x04000046 RID: 70
		public BehaviorAgent _behaviorAgent;
	}
}
