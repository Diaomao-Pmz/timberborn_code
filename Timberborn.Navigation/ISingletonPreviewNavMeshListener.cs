using System;
using Timberborn.SingletonSystem;

namespace Timberborn.Navigation
{
	// Token: 0x02000049 RID: 73
	[Singleton]
	public interface ISingletonPreviewNavMeshListener
	{
		// Token: 0x06000163 RID: 355
		void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate);
	}
}
