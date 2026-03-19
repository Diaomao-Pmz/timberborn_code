using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000046 RID: 70
	[Singleton]
	public interface IPrioritizedSingletonPreviewNavMeshListener
	{
		// Token: 0x06000160 RID: 352
		void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
