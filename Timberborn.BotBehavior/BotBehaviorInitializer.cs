using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.CharacterControlSystem;
using Timberborn.DeathSystem;
using Timberborn.MortalSystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.Wandering;
using Timberborn.WorkSystem;

namespace Timberborn.BotBehavior
{
	// Token: 0x02000005 RID: 5
	public class BotBehaviorInitializer : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021BB File Offset: 0x000003BB
		public void Awake()
		{
			this.InitializeBehaviors();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C4 File Offset: 0x000003C4
		public void InitializeBehaviors()
		{
			BehaviorManager component = base.GetComponent<BehaviorManager>();
			component.AddRootBehavior(base.GetComponent<CharacterControlRootBehavior>());
			component.AddRootBehavior(base.GetComponent<DeadRootBehavior>());
			component.AddRootBehavior(base.GetComponent<CarryRootBehavior>());
			component.AddRootBehavior(base.GetComponent<DieRootBehavior>());
			component.AddRootBehavior(base.GetComponent<CriticalNeederRootBehavior>());
			component.AddRootBehavior(base.GetComponent<StrandedRootBehavior>());
			component.AddRootBehavior(base.GetComponent<NeederRootBehavior>());
			component.AddRootBehavior(base.GetComponent<WorkerRootBehavior>());
			component.AddRootBehavior(base.GetComponent<WanderRootBehavior>());
		}
	}
}
