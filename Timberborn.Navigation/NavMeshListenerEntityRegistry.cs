using System;
using System.Collections.Generic;

namespace Timberborn.Navigation
{
	// Token: 0x02000059 RID: 89
	public class NavMeshListenerEntityRegistry : INavMeshListenerEntityRegistry
	{
		// Token: 0x060001BC RID: 444 RVA: 0x000060F0 File Offset: 0x000042F0
		public void NotifyAll(NavMeshUpdate navMeshUpdate)
		{
			for (int i = 0; i < this._navMeshListeners.Count; i++)
			{
				this._navMeshListeners[i].OnNavMeshUpdated(navMeshUpdate);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006128 File Offset: 0x00004328
		public void NotifyAllInstant(NavMeshUpdate navMeshUpdate)
		{
			for (int i = 0; i < this._instantNavMeshListeners.Count; i++)
			{
				this._instantNavMeshListeners[i].OnInstantNavMeshUpdated(navMeshUpdate);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000615D File Offset: 0x0000435D
		public void RegisterNavMeshListener(INavMeshListener navMeshListener)
		{
			this._navMeshListeners.Add(navMeshListener);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000616B File Offset: 0x0000436B
		public void UnregisterNavMeshListener(INavMeshListener navMeshListener)
		{
			this._navMeshListeners.Remove(navMeshListener);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000617A File Offset: 0x0000437A
		public void RegisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener)
		{
			this._instantNavMeshListeners.Add(instantNavMeshListener);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006188 File Offset: 0x00004388
		public void UnregisterInstantNavMeshListener(IInstantNavMeshListener instantNavMeshListener)
		{
			this._instantNavMeshListeners.Remove(instantNavMeshListener);
		}

		// Token: 0x040000BC RID: 188
		public readonly List<INavMeshListener> _navMeshListeners = new List<INavMeshListener>();

		// Token: 0x040000BD RID: 189
		public readonly List<IInstantNavMeshListener> _instantNavMeshListeners = new List<IInstantNavMeshListener>();
	}
}
