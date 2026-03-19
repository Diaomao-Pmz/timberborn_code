using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.WaterObjects
{
	// Token: 0x02000009 RID: 9
	public class FinishableHorizontalWaterObstacle : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002298 File Offset: 0x00000498
		public void Awake()
		{
			this._horizontalWaterObstacle = base.GetComponent<HorizontalWaterObstacle>();
			this._finishableHorizontalWaterObstacleSpec = base.GetComponent<FinishableHorizontalWaterObstacleSpec>();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B2 File Offset: 0x000004B2
		public void OnEnterFinishedState()
		{
			this._horizontalWaterObstacle.AddToWaterService(this._finishableHorizontalWaterObstacleSpec.Obstacles);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022CF File Offset: 0x000004CF
		public void OnExitFinishedState()
		{
			this._horizontalWaterObstacle.RemoveFromWaterService();
		}

		// Token: 0x0400000A RID: 10
		public HorizontalWaterObstacle _horizontalWaterObstacle;

		// Token: 0x0400000B RID: 11
		public FinishableHorizontalWaterObstacleSpec _finishableHorizontalWaterObstacleSpec;
	}
}
