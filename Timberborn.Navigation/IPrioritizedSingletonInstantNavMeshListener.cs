using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000044 RID: 68
	[Singleton]
	public interface IPrioritizedSingletonInstantNavMeshListener
	{
		// Token: 0x0600015E RID: 350
		void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
