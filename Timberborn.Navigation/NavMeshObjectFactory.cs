using System;

namespace Timberborn.Navigation
{
	// Token: 0x0200005D RID: 93
	public class NavMeshObjectFactory : INavMeshObjectFactory
	{
		// Token: 0x060001DA RID: 474 RVA: 0x000065DF File Offset: 0x000047DF
		public NavMeshObjectFactory(NavMeshUpdater navMeshUpdater, RestrictedNodeUpdater restrictedNodeUpdater)
		{
			this._navMeshUpdater = navMeshUpdater;
			this._restrictedNodeUpdater = restrictedNodeUpdater;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000065F5 File Offset: 0x000047F5
		public NavMeshObject Create()
		{
			return new NavMeshObject(this._navMeshUpdater, this._restrictedNodeUpdater);
		}

		// Token: 0x040000CE RID: 206
		public readonly NavMeshUpdater _navMeshUpdater;

		// Token: 0x040000CF RID: 207
		public readonly RestrictedNodeUpdater _restrictedNodeUpdater;
	}
}
