using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.WaterSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200003D RID: 61
	public class WaterObstacleController : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x060002CA RID: 714 RVA: 0x000082AA File Offset: 0x000064AA
		public WaterObstacleController(IWaterService waterService)
		{
			this._waterService = waterService;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000082B9 File Offset: 0x000064B9
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000256E File Offset: 0x0000076E
		public void OnEnterFinishedState()
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000082C7 File Offset: 0x000064C7
		public void OnExitFinishedState()
		{
			if (this._wasAdded)
			{
				this._waterService.RemoveFullObstacle(this._blockObject.Coordinates);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000082E8 File Offset: 0x000064E8
		public void UpdateState(bool add)
		{
			if (add && !this._wasAdded)
			{
				this._waterService.AddFullObstacle(this._blockObject.Coordinates);
				this._wasAdded = true;
				return;
			}
			if (!add && this._wasAdded)
			{
				this._waterService.RemoveFullObstacle(this._blockObject.Coordinates);
				this._wasAdded = false;
			}
		}

		// Token: 0x04000114 RID: 276
		public readonly IWaterService _waterService;

		// Token: 0x04000115 RID: 277
		public BlockObject _blockObject;

		// Token: 0x04000116 RID: 278
		public bool _wasAdded;
	}
}
