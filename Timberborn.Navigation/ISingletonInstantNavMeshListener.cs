using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000047 RID: 71
	[Singleton]
	public interface ISingletonInstantNavMeshListener
	{
		// Token: 0x06000161 RID: 353
		void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
