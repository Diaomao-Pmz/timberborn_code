using System;

namespace Timberborn.Navigation
{
	// Token: 0x02000065 RID: 101
	public class NavMeshUpdateNotifier
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00006EA7 File Offset: 0x000050A7
		public NavMeshUpdateNotifier(NavMeshListenerSingletonRegistry navMeshListenerSingletonRegistry, INavMeshListenerEntityRegistry navMeshListenerEntityRegistry)
		{
			this._navMeshListenerSingletonRegistry = navMeshListenerSingletonRegistry;
			this._navMeshListenerEntityRegistry = navMeshListenerEntityRegistry;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006EBD File Offset: 0x000050BD
		public void NotifyOfNavMeshUpdates(NavMeshUpdate navMeshUpdate)
		{
			this._navMeshListenerSingletonRegistry.NotifyAll(navMeshUpdate);
			this._navMeshListenerEntityRegistry.NotifyAll(navMeshUpdate);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006ED7 File Offset: 0x000050D7
		public void NotifyOfPreviewNavMeshUpdates(NavMeshUpdate navMeshUpdate)
		{
			this._navMeshListenerSingletonRegistry.NotifyAllPreview(navMeshUpdate);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006EE5 File Offset: 0x000050E5
		public void NotifyOfInstantNavMeshUpdates(NavMeshUpdate navMeshUpdate)
		{
			this._navMeshListenerSingletonRegistry.NotifyAllInstant(navMeshUpdate);
			this._navMeshListenerEntityRegistry.NotifyAllInstant(navMeshUpdate);
		}

		// Token: 0x040000F0 RID: 240
		public readonly NavMeshListenerSingletonRegistry _navMeshListenerSingletonRegistry;

		// Token: 0x040000F1 RID: 241
		public readonly INavMeshListenerEntityRegistry _navMeshListenerEntityRegistry;
	}
}
