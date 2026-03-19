using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectNavMesh : BaseComponent, IAwakableComponent, IBlockObjectNavMesh
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public NavMeshObject NavMeshObject { get; private set; }

		// Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		public BlockObjectNavMesh(INavMeshObjectFactory navMeshObjectFactory, NavMeshObjectUpdater navMeshObjectUpdater)
		{
			this._navMeshObjectFactory = navMeshObjectFactory;
			this._navMeshObjectUpdater = navMeshObjectUpdater;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002125 File Offset: 0x00000325
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectNavMeshSettingsSpec = base.GetComponent<BlockObjectNavMeshSettingsSpec>();
			this.NavMeshObject = this._navMeshObjectFactory.Create();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		public void RecalculateNavMeshObject()
		{
			this._navMeshObjectUpdater.Update(this._blockObject, this.NavMeshObject, this._blockObjectNavMeshSettingsSpec);
		}

		// Token: 0x04000009 RID: 9
		public readonly INavMeshObjectFactory _navMeshObjectFactory;

		// Token: 0x0400000A RID: 10
		public readonly NavMeshObjectUpdater _navMeshObjectUpdater;

		// Token: 0x0400000B RID: 11
		public BlockObject _blockObject;

		// Token: 0x0400000C RID: 12
		public BlockObjectNavMeshSettingsSpec _blockObjectNavMeshSettingsSpec;
	}
}
