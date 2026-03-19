using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x0200000B RID: 11
	public class FinishableWaterObstacle : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002425 File Offset: 0x00000625
		public void Awake()
		{
			this._waterObstacle = base.GetComponent<WaterObstacle>();
			this._finishableWaterObstacleSpec = base.GetComponent<FinishableWaterObstacleSpec>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000243F File Offset: 0x0000063F
		public void OnEnterFinishedState()
		{
			this._waterObstacle.AddToWaterService(this._finishableWaterObstacleSpec.Height);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002457 File Offset: 0x00000657
		public void OnExitFinishedState()
		{
			this._waterObstacle.RemoveFromWaterService();
		}

		// Token: 0x0400000D RID: 13
		public WaterObstacle _waterObstacle;

		// Token: 0x0400000E RID: 14
		public FinishableWaterObstacleSpec _finishableWaterObstacleSpec;
	}
}
