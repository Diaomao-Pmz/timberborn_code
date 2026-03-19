using System;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.WorkSystem;

namespace Timberborn.Workshops
{
	// Token: 0x02000015 RID: 21
	public class ManufactoryInputChecker : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000033A8 File Offset: 0x000015A8
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._workplace = base.GetComponent<Workplace>();
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
			this._lackOfResourcesStatus = base.GetComponent<LackOfResourcesStatus>();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033DC File Offset: 0x000015DC
		public void OnEnterFinishedState()
		{
			this._lackOfResourcesStatus.Initialize(() => this._inputUnavailable);
			this.CheckIfInputIsUnavailable();
			if (this._manufactory.NeedsInventory)
			{
				this._manufactory.Inventory.InventoryChanged += this.OnInventoryChanged;
				if (this._workplace)
				{
					this._workplace.WorkerAssigned += this.OnWorkerChanged;
					this._workplace.WorkerUnassigned += this.OnWorkerChanged;
				}
				if (this._districtBuilding)
				{
					this._districtBuilding.ReassignedDistrict += this.OnDistrictReassigned;
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003490 File Offset: 0x00001690
		public void OnExitFinishedState()
		{
			this._lackOfResourcesStatus.Disable();
			if (this._manufactory.NeedsInventory)
			{
				this._manufactory.Inventory.InventoryChanged -= this.OnInventoryChanged;
				if (this._workplace)
				{
					this._workplace.WorkerAssigned -= this.OnWorkerChanged;
					this._workplace.WorkerUnassigned -= this.OnWorkerChanged;
				}
				if (this._districtBuilding)
				{
					this._districtBuilding.ReassignedDistrict -= this.OnDistrictReassigned;
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003530 File Offset: 0x00001730
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003530 File Offset: 0x00001730
		public void OnWorkerChanged(object sender, WorkerChangedEventArgs e)
		{
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003530 File Offset: 0x00001730
		public void OnDistrictReassigned(object sender, EventArgs e)
		{
			this.CheckIfInputIsUnavailable();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003538 File Offset: 0x00001738
		public void CheckIfInputIsUnavailable()
		{
			if (!this._workplace || this._workplace.NumberOfAssignedWorkers == 0 || !this._manufactory.HasCurrentRecipe || !this._districtBuilding.District)
			{
				this._inputUnavailable = false;
				return;
			}
			this._inputUnavailable = this.InputIsUnavailable();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003594 File Offset: 0x00001794
		public bool InputIsUnavailable()
		{
			DistrictInventoryRegistry component = this._districtBuilding.District.GetComponent<DistrictInventoryRegistry>();
			return this.FuelIsUnavailable(component) || this.IngredientsAreUnavailable(component);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035C4 File Offset: 0x000017C4
		public bool FuelIsUnavailable(DistrictInventoryRegistry inventoryRegistry)
		{
			string fuel = this._manufactory.CurrentRecipe.Fuel;
			return !this._manufactory.HasFuel && inventoryRegistry.ActiveInventoriesWithStock(fuel).Count == 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003604 File Offset: 0x00001804
		public bool IngredientsAreUnavailable(DistrictInventoryRegistry inventoryRegistry)
		{
			if (!this._manufactory.HasAllIngredients)
			{
				ImmutableArray<GoodAmountSpec> ingredients = this._manufactory.CurrentRecipe.Ingredients;
				for (int i = 0; i < ingredients.Length; i++)
				{
					if (inventoryRegistry.ActiveInventoriesWithStock(ingredients[i].Id).Count == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000041 RID: 65
		public Manufactory _manufactory;

		// Token: 0x04000042 RID: 66
		public Workplace _workplace;

		// Token: 0x04000043 RID: 67
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000044 RID: 68
		public LackOfResourcesStatus _lackOfResourcesStatus;

		// Token: 0x04000045 RID: 69
		public bool _inputUnavailable;
	}
}
