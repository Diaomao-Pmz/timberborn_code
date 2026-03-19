using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.ReservableSystem;
using Timberborn.WorkSystem;

namespace Timberborn.Demolishing
{
	// Token: 0x02000017 RID: 23
	public class DemolishBehavior : Behavior, IJobBehavior
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003104 File Offset: 0x00001304
		public override Decision Decide(BehaviorAgent agent)
		{
			Demolisher component = agent.GetComponent<Demolisher>();
			if (!component.HasReservedDemolishable)
			{
				return Decision.ReleaseNow();
			}
			if (!component.ReservedDemolishable.CanBeDemolished)
			{
				return DemolishBehavior.UnreserveDemolishable(component);
			}
			BaseComponent demolishable = component.Demolishable;
			WalkToReservableExecutor component2 = agent.GetComponent<WalkToReservableExecutor>();
			DemolishableReacher component3 = demolishable.GetComponent<DemolishableReacher>();
			switch (component2.Launch(component3))
			{
			case ExecutorStatus.Success:
				return DemolishBehavior.Demolish(agent);
			case ExecutorStatus.Failure:
				return DemolishBehavior.UnreserveDemolishable(component);
			case ExecutorStatus.Running:
				return Decision.ReturnWhenFinished(component2);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003184 File Offset: 0x00001384
		public static Decision Demolish(BehaviorAgent agent)
		{
			DemolishExecutor component = agent.GetComponent<DemolishExecutor>();
			if (!component.Demolish())
			{
				return Decision.ReturnNextTick();
			}
			return Decision.ReleaseWhenFinished(component);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031AC File Offset: 0x000013AC
		public static Decision UnreserveDemolishable(Demolisher demolisher)
		{
			demolisher.Unreserve();
			return Decision.ReleaseNextTick();
		}
	}
}
