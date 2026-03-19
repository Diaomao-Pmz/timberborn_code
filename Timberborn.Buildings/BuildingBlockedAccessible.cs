using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Navigation;

namespace Timberborn.Buildings
{
	// Token: 0x0200000A RID: 10
	public class BuildingBlockedAccessible : BaseComponent, IAwakableComponent, IBlockedAccessible, IFinishedStateListener
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002475 File Offset: 0x00000675
		public BuildingBlockedAccessible(INavMeshService navMeshService)
		{
			this._navMeshService = navMeshService;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002484 File Offset: 0x00000684
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			base.DisableComponent();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002498 File Offset: 0x00000698
		public bool IsBlocked()
		{
			PositionedEntrance positionedEntrance = this._blockObject.PositionedEntrance;
			return base.Enabled && !this._navMeshService.AreConnected(positionedEntrance.Coordinates, positionedEntrance.DoorstepCoordinates);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024D8 File Offset: 0x000006D8
		public bool IsBlockedInstant()
		{
			PositionedEntrance positionedEntrance = this._blockObject.PositionedEntrance;
			return base.Enabled && !this._navMeshService.AreConnectedInstant(positionedEntrance.Coordinates, positionedEntrance.DoorstepCoordinates);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002515 File Offset: 0x00000715
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000251D File Offset: 0x0000071D
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x0400000F RID: 15
		public readonly INavMeshService _navMeshService;

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;
	}
}
