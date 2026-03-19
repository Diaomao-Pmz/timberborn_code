using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Hauling;
using Timberborn.InventorySystem;
using Timberborn.Workshops;
using Timberborn.WorkSystem;

namespace Timberborn.GoodConsumingBuildingSystem
{
	// Token: 0x0200000D RID: 13
	public class GoodConsumingBuildingStatusInitializer : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002B49 File Offset: 0x00000D49
		public void Awake()
		{
			this._goodConsumingBuilding = base.GetComponent<GoodConsumingBuilding>();
			this._workplace = base.GetComponent<Workplace>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._lackOfResourcesStatus = base.GetComponent<LackOfResourcesStatus>();
			this._noHaulingPostStatus = base.GetComponent<NoHaulingPostStatus>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B88 File Offset: 0x00000D88
		public void OnEnterFinishedState()
		{
			this._lackOfResourcesStatus.Initialize(new Func<bool>(this.CheckIfSupplyIsUnavailable));
			if (!this._workplace)
			{
				this._noHaulingPostStatus.Initialize(() => true);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public void OnExitFinishedState()
		{
			this._lackOfResourcesStatus.Disable();
			if (!this._workplace)
			{
				this._noHaulingPostStatus.Disable();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C08 File Offset: 0x00000E08
		public bool CheckIfSupplyIsUnavailable()
		{
			if ((this._workplace && this._workplace.NumberOfAssignedWorkers == 0) || !this._districtBuilding.District || this._goodConsumingBuilding.CanUse)
			{
				return false;
			}
			DistrictInventoryRegistry component = this._districtBuilding.District.GetComponent<DistrictInventoryRegistry>();
			return this.CheckIfSupplyIsUnavailable(component);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C68 File Offset: 0x00000E68
		public bool CheckIfSupplyIsUnavailable(DistrictInventoryRegistry inventoryRegistry)
		{
			foreach (ConsumedGoodSpec consumedGoodSpec in this._goodConsumingBuilding.ConsumedGoods)
			{
				if (inventoryRegistry.ActiveInventoriesWithStock(consumedGoodSpec.GoodId).Count == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400001F RID: 31
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x04000020 RID: 32
		public Workplace _workplace;

		// Token: 0x04000021 RID: 33
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000022 RID: 34
		public LackOfResourcesStatus _lackOfResourcesStatus;

		// Token: 0x04000023 RID: 35
		public NoHaulingPostStatus _noHaulingPostStatus;
	}
}
