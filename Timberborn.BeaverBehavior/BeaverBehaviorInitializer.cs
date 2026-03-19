using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.BehaviorSystem;
using Timberborn.Carrying;
using Timberborn.CharacterControlSystem;
using Timberborn.DeathSystem;
using Timberborn.MortalSystem;
using Timberborn.NeedBehaviorSystem;
using Timberborn.SleepSystem;
using Timberborn.Wandering;
using Timberborn.WorkSystem;

namespace Timberborn.BeaverBehavior
{
	// Token: 0x02000005 RID: 5
	public class BeaverBehaviorInitializer : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021DC File Offset: 0x000003DC
		public void Awake()
		{
			bool isAdult = !base.GetComponent<Child>();
			this.InitializeBehaviors(isAdult);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002200 File Offset: 0x00000400
		public void InitializeBehaviors(bool isAdult)
		{
			BehaviorManager component = base.GetComponent<BehaviorManager>();
			BeaverNeedBehaviorPicker component2 = base.GetComponent<BeaverNeedBehaviorPicker>();
			component.AddRootBehavior(base.GetComponent<CharacterControlRootBehavior>());
			component.AddRootBehavior(base.GetComponent<DeadRootBehavior>());
			if (isAdult)
			{
				component.AddRootBehavior(base.GetComponent<CarryRootBehavior>());
			}
			else
			{
				component.AddRootBehavior(base.GetComponent<ChildRootBehavior>());
			}
			component.AddRootBehavior(base.GetComponent<DieRootBehavior>());
			component.AddRootBehavior(base.GetComponent<ContaminateRootBehavior>());
			component.AddRootBehavior(base.GetComponent<CriticalNeederRootBehavior>());
			component.AddRootBehavior(base.GetComponent<StrandedRootBehavior>());
			if (isAdult)
			{
				component.AddRootBehavior(base.GetComponent<WorkerRootBehavior>());
			}
			component.AddRootBehavior(base.GetComponent<NeederRootBehavior>());
			component2.InitializeEssentialNeedBehavior(base.GetComponent<SleepNeedBehavior>());
			WanderRootBehavior component3 = base.GetComponent<WanderRootBehavior>();
			component.AddRootBehavior(component3);
			component3.AllowVisitingRestPlaces();
		}
	}
}
