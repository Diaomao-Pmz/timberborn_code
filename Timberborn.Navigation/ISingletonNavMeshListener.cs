using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000048 RID: 72
	[Singleton]
	public interface ISingletonNavMeshListener
	{
		// Token: 0x06000162 RID: 354
		void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
