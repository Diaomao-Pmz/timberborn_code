using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000036 RID: 54
	public interface INavMeshListenerEntityRegistry
	{
		// Token: 0x06000140 RID: 320
		void NotifyAll(NavMeshUpdate navMeshUpdate);

		// Token: 0x06000141 RID: 321
		void NotifyAllInstant(NavMeshUpdate navMeshUpdate);

		// Token: 0x06000142 RID: 322
		void RegisterNavMeshListener(INavMeshListener navMeshListener);

		// Token: 0x06000143 RID: 323
		void UnregisterNavMeshListener(INavMeshListener navMeshListener);

		// Token: 0x06000144 RID: 324
		void RegisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener);

		// Token: 0x06000145 RID: 325
		void UnregisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener);
	}
}
