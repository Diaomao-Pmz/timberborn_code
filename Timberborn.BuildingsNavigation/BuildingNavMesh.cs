using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlockSystemNavigation;
using Timberborn.Navigation;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x0200000E RID: 14
	public class BuildingNavMesh : BaseComponent, IAwakableComponent, IFinishedStateListener, IUnfinishedStateListener
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public void Awake()
		{
			this._blockObjectNavMesh = base.GetComponent<IBlockObjectNavMesh>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public void OnEnterFinishedState()
		{
			if (!this._isBlocked)
			{
				this.AddToNavMesh();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void OnExitFinishedState()
		{
			if (!this._isBlocked)
			{
				this.RemoveFromNavMesh();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void OnEnterUnfinishedState()
		{
			if (!this._isAddedToPreviewNavMesh)
			{
				this.RecalculateNavMeshObject();
				this.NavMeshObject.EnqueueAddToPreviewNavMesh();
				this._isAddedToPreviewNavMesh = true;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C12 File Offset: 0x00000E12
		public void OnExitUnfinishedState()
		{
			if (this._isAddedToPreviewNavMesh)
			{
				this.NavMeshObject.EnqueueRemoveFromPreviewNavMesh();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C27 File Offset: 0x00000E27
		public void UnblockAndAddToNavMesh()
		{
			this._isBlocked = false;
			this.AddToNavMesh();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C36 File Offset: 0x00000E36
		public void BlockAndRemoveFromNavMesh()
		{
			this._isBlocked = true;
			this.RemoveFromNavMesh();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C45 File Offset: 0x00000E45
		public NavMeshObject NavMeshObject
		{
			get
			{
				return this._blockObjectNavMesh.NavMeshObject;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C52 File Offset: 0x00000E52
		public void AddToNavMesh()
		{
			if (!this._isAddedToRegularNavMesh)
			{
				this.RecalculateNavMeshObject();
				this.NavMeshObject.EnqueueAddToRegularNavMesh();
				this._isAddedToRegularNavMesh = true;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C74 File Offset: 0x00000E74
		public void RemoveFromNavMesh()
		{
			if (this._isAddedToRegularNavMesh)
			{
				this.NavMeshObject.EnqueueRemoveFromRegularNavMesh();
				this._isAddedToRegularNavMesh = false;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C90 File Offset: 0x00000E90
		public void RecalculateNavMeshObject()
		{
			this._blockObjectNavMesh.RecalculateNavMeshObject();
		}

		// Token: 0x04000026 RID: 38
		public IBlockObjectNavMesh _blockObjectNavMesh;

		// Token: 0x04000027 RID: 39
		public bool _isAddedToRegularNavMesh;

		// Token: 0x04000028 RID: 40
		public bool _isAddedToPreviewNavMesh;

		// Token: 0x04000029 RID: 41
		public bool _isBlocked;
	}
}
