using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200000E RID: 14
	public class CriticalNeederRootBehavior : RootBehavior, IAwakableComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027E9 File Offset: 0x000009E9
		public bool NeedRunning
		{
			get
			{
				return this._behaviorManager.IsRunningBehavior<NeedBehavior>() || this._behaviorManager.IsRunningBehavior<EssentialNeedBehavior>();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002805 File Offset: 0x00000A05
		public void Awake()
		{
			this._needBehaviorPicker = base.GetComponent<INeedBehaviorPicker>();
			this._behaviorManager = base.GetComponent<BehaviorManager>();
			this._behaviorAgent = base.GetComponent<BehaviorAgent>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000282C File Offset: 0x00000A2C
		public override Decision Decide(BehaviorAgent agent)
		{
			Behavior bestNeedBehaviorAffectingNeedsInCriticalState = this._needBehaviorPicker.GetBestNeedBehaviorAffectingNeedsInCriticalState();
			if (bestNeedBehaviorAffectingNeedsInCriticalState != null)
			{
				Decision decision = bestNeedBehaviorAffectingNeedsInCriticalState.Decide(this._behaviorAgent);
				if (!decision.ShouldReleaseNow)
				{
					return Decision.TransferNow(bestNeedBehaviorAffectingNeedsInCriticalState, decision);
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x04000027 RID: 39
		public INeedBehaviorPicker _needBehaviorPicker;

		// Token: 0x04000028 RID: 40
		public BehaviorManager _behaviorManager;

		// Token: 0x04000029 RID: 41
		public BehaviorAgent _behaviorAgent;

		// Token: 0x0400002A RID: 42
		public bool _needsInCriticalStateOnly;
	}
}
