using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.MechanicalSystem;
using Timberborn.TickSystem;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x02000012 RID: 18
	public class PoweredGoodConsumingBuilding : TickableComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002E70 File Offset: 0x00001070
		public void Awake()
		{
			this._goodConsumingBuilding = base.GetComponent<GoodConsumingBuilding>();
			this._mechanicalBuilding = base.GetComponent<MechanicalBuilding>();
			this._goodConsumingToggle = this._goodConsumingBuilding.GetGoodConsumingToggle();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E9B File Offset: 0x0000109B
		public void InitializeEntity()
		{
			this.UpdateState();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E9B File Offset: 0x0000109B
		public override void Tick()
		{
			this.UpdateState();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EA3 File Offset: 0x000010A3
		public void UpdateState()
		{
			this._mechanicalBuilding.SetConsumptionDisabled(!this._goodConsumingBuilding.CanUse);
			if (this._mechanicalBuilding.ActiveAndPowered)
			{
				this._goodConsumingToggle.ResumeConsumption();
				return;
			}
			this._goodConsumingToggle.PauseConsumption();
		}

		// Token: 0x04000029 RID: 41
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x0400002A RID: 42
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x0400002B RID: 43
		public GoodConsumingToggle _goodConsumingToggle;
	}
}
