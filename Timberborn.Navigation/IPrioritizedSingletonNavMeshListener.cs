using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000045 RID: 69
	[Singleton]
	public interface IPrioritizedSingletonNavMeshListener
	{
		// Token: 0x0600015F RID: 351
		void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
