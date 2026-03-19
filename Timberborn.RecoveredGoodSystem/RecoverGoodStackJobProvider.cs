using System;
using Timberborn.BehaviorSystem;
using Timberborn.BuilderHubSystem;
using Timberborn.Navigation;
using Timberborn.PrioritySystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200001B RID: 27
	public class RecoverGoodStackJobProvider : IBuilderJobProvider
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003B59 File Offset: 0x00001D59
		public RecoverGoodStackJobProvider(PrioritizedRecoveredGoodStackRegistry prioritizedRecoveredGoodStackRegistry)
		{
			this._prioritizedRecoveredGoodStackRegistry = prioritizedRecoveredGoodStackRegistry;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003B68 File Offset: 0x00001D68
		public int ProviderPriority
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003B6C File Offset: 0x00001D6C
		public ValueTuple<Behavior, Decision> GetJob(Accessible start, BehaviorAgent agent, Priority priority)
		{
			RecoveredGoodStackCarryingBehavior component = agent.GetComponent<RecoveredGoodStackCarryingBehavior>();
			foreach (RecoveredGoodStack recoveredGoodStack in this._prioritizedRecoveredGoodStackRegistry.GetRecoveredGoodStacks(priority))
			{
				if (RecoverGoodStackJobProvider.IsStackRecoverable(recoveredGoodStack, start))
				{
					Decision item = component.FindInventoryAndStartCarrying(recoveredGoodStack);
					if (!item.ShouldReleaseNow)
					{
						return new ValueTuple<Behavior, Decision>(component, item);
					}
				}
			}
			return new ValueTuple<Behavior, Decision>(null, Decision.ReleaseNow());
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public static bool IsStackRecoverable(RecoveredGoodStack recoveredGoodStack, Accessible start)
		{
			if (recoveredGoodStack.Inventory.HasAnyUnreservedStock)
			{
				Accessible enabledComponent = recoveredGoodStack.GetEnabledComponent<Accessible>();
				if (enabledComponent != null)
				{
					return start.IsReachableByRoadToTerrain(enabledComponent);
				}
			}
			return false;
		}

		// Token: 0x04000059 RID: 89
		public readonly PrioritizedRecoveredGoodStackRegistry _prioritizedRecoveredGoodStackRegistry;
	}
}
