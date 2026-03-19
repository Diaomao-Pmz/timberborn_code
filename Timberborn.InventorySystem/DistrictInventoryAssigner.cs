using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000005 RID: 5
	public class DistrictInventoryAssigner : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public void Awake()
		{
			this._districtBuilding = base.GetComponent<DistrictBuilding>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DF File Offset: 0x000002DF
		public void StartAssigningInventory(Inventory inventory)
		{
			if (this._districtInventoryRegistry)
			{
				this._districtInventoryRegistry.Add(inventory);
			}
			this._inventories.Add(inventory);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002106 File Offset: 0x00000306
		public void StopAssigningInventory(Inventory inventory)
		{
			if (this._districtInventoryRegistry)
			{
				this._districtInventoryRegistry.Remove(inventory);
			}
			this._inventories.Remove(inventory);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212E File Offset: 0x0000032E
		public void OnEnterFinishedState()
		{
			this.AddRegisteredInventories();
			this._districtBuilding.ReassignedDistrict += this.OnReassignedDistrict;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000214D File Offset: 0x0000034D
		public void OnExitFinishedState()
		{
			this.RemoveRegisteredInventories();
			this._districtBuilding.ReassignedDistrict -= this.OnReassignedDistrict;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public void OnReassignedDistrict(object sender, EventArgs e)
		{
			this.RemoveRegisteredInventories();
			this.AddRegisteredInventories();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
		public void RemoveRegisteredInventories()
		{
			if (this._districtInventoryRegistry)
			{
				foreach (Inventory inventory in this._inventories)
				{
					this._districtInventoryRegistry.Remove(inventory);
				}
				this._districtInventoryRegistry = null;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E8 File Offset: 0x000003E8
		public void AddRegisteredInventories()
		{
			DistrictCenter district = this._districtBuilding.District;
			if (district)
			{
				this._districtInventoryRegistry = district.GetComponent<DistrictInventoryRegistry>();
				foreach (Inventory inventory in this._inventories)
				{
					this._districtInventoryRegistry.Add(inventory);
				}
			}
		}

		// Token: 0x04000007 RID: 7
		public DistrictBuilding _districtBuilding;

		// Token: 0x04000008 RID: 8
		public DistrictInventoryRegistry _districtInventoryRegistry;

		// Token: 0x04000009 RID: 9
		public readonly List<Inventory> _inventories = new List<Inventory>();
	}
}
