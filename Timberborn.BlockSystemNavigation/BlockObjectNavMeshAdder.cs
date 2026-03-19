using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000008 RID: 8
	public class BlockObjectNavMeshAdder : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000216F File Offset: 0x0000036F
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectNavMesh = base.GetComponent<BlockObjectNavMesh>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002189 File Offset: 0x00000389
		public void InitializeEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				this._blockObjectNavMesh.RecalculateNavMeshObject();
				this._blockObjectNavMesh.NavMeshObject.EnqueueAddToRegularNavMesh();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B3 File Offset: 0x000003B3
		public void DeleteEntity()
		{
			if (!this._blockObject.IsPreview)
			{
				this._blockObjectNavMesh.NavMeshObject.EnqueueRemoveFromRegularNavMesh();
			}
		}

		// Token: 0x0400000D RID: 13
		public BlockObject _blockObject;

		// Token: 0x0400000E RID: 14
		public BlockObjectNavMesh _blockObjectNavMesh;
	}
}
