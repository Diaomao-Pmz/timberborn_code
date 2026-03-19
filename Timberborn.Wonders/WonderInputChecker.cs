using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Workshops;
using Timberborn.WorkSystem;

namespace Timberborn.Wonders
{
	// Token: 0x02000018 RID: 24
	public class WonderInputChecker : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002E5A File Offset: 0x0000105A
		public void Awake()
		{
			this._workplace = base.GetComponent<Workplace>();
			this._wonder = base.GetComponent<Wonder>();
			this._wonderInventory = base.GetComponent<WonderInventory>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._lackOfResourcesStatus = base.GetComponent<LackOfResourcesStatus>();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E98 File Offset: 0x00001098
		public void OnEnterFinishedState()
		{
			this._lackOfResourcesStatus.Initialize(() => this._inputUnavailable);
			this._wonderInventory.Inventory.InventoryChanged += this.OnInventoryChanged;
			this._workplace.WorkerAssigned += this.OnWorkerChanged;
			this._workplace.WorkerUnassigned += this.OnWorkerChanged;
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F0C File Offset: 0x0000110C
		public void OnExitFinishedState()
		{
			this._lackOfResourcesStatus.Disable();
			this._wonderInventory.Inventory.InventoryChanged -= this.OnInventoryChanged;
			this._workplace.WorkerAssigned -= this.OnWorkerChanged;
			this._workplace.WorkerUnassigned -= this.OnWorkerChanged;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F6E File Offset: 0x0000116E
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F6E File Offset: 0x0000116E
		public void OnWorkerChanged(object sender, WorkerChangedEventArgs e)
		{
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002F76 File Offset: 0x00001176
		public void CheckIfInputIsUnavailable()
		{
			if (this._workplace.NumberOfAssignedWorkers == 0 || !this._districtBuilding.District)
			{
				this._inputUnavailable = false;
				return;
			}
			this._inputUnavailable = this.AreGoodsUnavailable();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002FAC File Offset: 0x000011AC
		public bool AreGoodsUnavailable()
		{
			DistrictInventoryRegistry component = this._districtBuilding.District.GetComponent<DistrictInventoryRegistry>();
			if (!this._wonder.CanBeActivated())
			{
				foreach (GoodAmountSpec goodAmountSpec in this._wonderInventory.RequiredGoods)
				{
					if (component.ActiveInventoriesWithStock(goodAmountSpec.Id).Count == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000036 RID: 54
		public Workplace _workplace;

		// Token: 0x04000037 RID: 55
		public Wonder _wonder;

		// Token: 0x04000038 RID: 56
		public WonderInventory _wonderInventory;

		// Token: 0x04000039 RID: 57
		public DistrictBuilding _districtBuilding;

		// Token: 0x0400003A RID: 58
		public LackOfResourcesStatus _lackOfResourcesStatus;

		// Token: 0x0400003B RID: 59
		public bool _inputUnavailable;
	}
}
