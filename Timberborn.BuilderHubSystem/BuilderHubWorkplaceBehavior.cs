using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BehaviorSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.PrioritySystem;
using Timberborn.WorkSystem;

namespace Timberborn.BuilderHubSystem
{
	// Token: 0x02000009 RID: 9
	public class BuilderHubWorkplaceBehavior : WorkplaceBehavior, IInitializableEntity
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002262 File Offset: 0x00000462
		public BuilderHubWorkplaceBehavior(IEnumerable<IBuilderJobProvider> builderJobProviders)
		{
			this._providers = (from provider in builderJobProviders
			orderby provider.ProviderPriority
			select provider).ToImmutableArray<IBuilderJobProvider>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000229A File Offset: 0x0000049A
		public void InitializeEntity()
		{
			this._accessible = base.GetComponent<BuildingAccessible>().Accessible;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022B0 File Offset: 0x000004B0
		public override Decision Decide(BehaviorAgent agent)
		{
			foreach (Priority priority in Priorities.Descending)
			{
				foreach (IBuilderJobProvider builderJobProvider in this._providers)
				{
					ValueTuple<Behavior, Decision> job = builderJobProvider.GetJob(this._accessible, agent, priority);
					Behavior item = job.Item1;
					Decision item2 = job.Item2;
					if (!item2.ShouldReleaseNow)
					{
						return Decision.TransferNow(item, item2);
					}
				}
			}
			return Decision.ReleaseNow();
		}

		// Token: 0x04000008 RID: 8
		public readonly ImmutableArray<IBuilderJobProvider> _providers;

		// Token: 0x04000009 RID: 9
		public Accessible _accessible;
	}
}
