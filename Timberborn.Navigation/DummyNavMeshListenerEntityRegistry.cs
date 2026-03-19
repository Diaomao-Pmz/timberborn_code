using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200001C RID: 28
	public class DummyNavMeshListenerEntityRegistry : INavMeshListenerEntityRegistry
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003E78 File Offset: 0x00002078
		public void NotifyAll(NavMeshUpdate navMeshUpdate)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003E78 File Offset: 0x00002078
		public void NotifyAllInstant(NavMeshUpdate navMeshUpdate)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003E78 File Offset: 0x00002078
		public void RegisterNavMeshListener(INavMeshListener navMeshListener)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E78 File Offset: 0x00002078
		public void UnregisterNavMeshListener(INavMeshListener navMeshListener)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E78 File Offset: 0x00002078
		public void RegisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003E78 File Offset: 0x00002078
		public void UnregisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener)
		{
		}
	}
}
