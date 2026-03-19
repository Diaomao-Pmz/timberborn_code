using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000039 RID: 57
	public class WaterMoverPowerConsumptionSwitch : TickableComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00007EE5 File Offset: 0x000060E5
		public void Awake()
		{
			this._mechanicalBuilding = base.GetComponent<MechanicalBuilding>();
			this._waterMover = base.GetComponent<WaterMover>();
			base.DisableComponent();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00005270 File Offset: 0x00003470
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00004BFE File Offset: 0x00002DFE
		public void OnExitFinishedState()
		{
			base.DisableComponent();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007F05 File Offset: 0x00006105
		public override void StartTickable()
		{
			this.UpdatePowerConsumption();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007F05 File Offset: 0x00006105
		public override void Tick()
		{
			this.UpdatePowerConsumption();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007F0D File Offset: 0x0000610D
		public void UpdatePowerConsumption()
		{
			this._mechanicalBuilding.SetConsumptionDisabled(!this._waterMover.IsWaterFlowPossible());
		}

		// Token: 0x0400010B RID: 267
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x0400010C RID: 268
		public WaterMover _waterMover;
	}
}
