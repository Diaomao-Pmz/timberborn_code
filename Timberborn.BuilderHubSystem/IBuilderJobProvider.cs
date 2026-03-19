using System;
using Timberborn.BehaviorSystem;
using Timberborn.Navigation;
using Timberborn.PrioritySystem;

namespace Timberborn.BuilderHubSystem
{
	// Token: 0x0200000C RID: 12
	public interface IBuilderJobProvider
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32
		int ProviderPriority { get; }

		// Token: 0x06000021 RID: 33
		ValueTuple<Behavior, Decision> GetJob(Accessible start, BehaviorAgent agent, Priority priority);
	}
}
