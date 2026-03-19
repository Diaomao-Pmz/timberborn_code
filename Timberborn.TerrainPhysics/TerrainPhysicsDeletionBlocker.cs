using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000011 RID: 17
	public class TerrainPhysicsDeletionBlocker : BaseComponent, IAwakableComponent, IBlockObjectDeletionBlocker
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002B42 File Offset: 0x00000D42
		public TerrainPhysicsDeletionBlocker(ITerrainPhysicsService terrainPhysicsService)
		{
			this._terrainPhysicsService = terrainPhysicsService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B51 File Offset: 0x00000D51
		public bool NoForcedDelete
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool IsStackedDeletionBlocked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B57 File Offset: 0x00000D57
		public bool IsDeletionBlocked
		{
			get
			{
				return !this._terrainPhysicsService.CanBeDestroyed(this._blockObject);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B6D File Offset: 0x00000D6D
		public string ReasonLocKey
		{
			get
			{
				return "DeletionBlocker.TerrainPhysics";
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B74 File Offset: 0x00000D74
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0400001B RID: 27
		public readonly ITerrainPhysicsService _terrainPhysicsService;

		// Token: 0x0400001C RID: 28
		public BlockObject _blockObject;
	}
}
