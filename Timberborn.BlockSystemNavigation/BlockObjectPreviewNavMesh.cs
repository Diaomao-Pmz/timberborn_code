using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000011 RID: 17
	public class BlockObjectPreviewNavMesh : BaseComponent, IAwakableComponent, IPreviewServiceMember
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003191 File Offset: 0x00001391
		public void Awake()
		{
			this._blockObjectNavMesh = base.GetComponent<BlockObjectNavMesh>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000319F File Offset: 0x0000139F
		public void AddToPreviewService()
		{
			this.RemoveFromPreviewService();
			this._blockObjectNavMesh.RecalculateNavMeshObject();
			this._blockObjectNavMesh.NavMeshObject.AddToPreviewNavMesh();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031C2 File Offset: 0x000013C2
		public void RemoveFromPreviewService()
		{
			NavMeshObject navMeshObject = this._blockObjectNavMesh.NavMeshObject;
			if (navMeshObject == null)
			{
				return;
			}
			navMeshObject.RemoveFromPreviewNavMesh();
		}

		// Token: 0x04000024 RID: 36
		public BlockObjectNavMesh _blockObjectNavMesh;

		// Token: 0x04000025 RID: 37
		public bool _isActivePreview;
	}
}
