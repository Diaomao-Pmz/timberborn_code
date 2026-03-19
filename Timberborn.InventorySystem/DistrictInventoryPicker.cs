using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000006 RID: 6
	public class DistrictInventoryPicker : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002273 File Offset: 0x00000473
		public void Awake()
		{
			this._districtInventoryRegistry = base.GetComponent<DistrictInventoryRegistry>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002284 File Offset: 0x00000484
		public Inventory ClosestInventoryWithStock(Accessible start, string goodId, Predicate<Inventory> inventoryFilter)
		{
			ReadOnlyHashSet<Inventory> readOnlyHashSet = this._districtInventoryRegistry.ActiveInventoriesWithStock(goodId);
			Inventory result = null;
			float num = float.MaxValue;
			foreach (Inventory inventory in readOnlyHashSet)
			{
				Accessible enabledComponent = inventory.GetEnabledComponent<Accessible>();
				float num2;
				if (inventoryFilter(inventory) && start.FindRoadPath(enabledComponent, out num2) && num2 < num)
				{
					result = inventory;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002310 File Offset: 0x00000510
		public Inventory ClosestInventoryWithStock(Vector3 start, string goodId, Accessible accessibleReachableFromInventory)
		{
			ReadOnlyHashSet<Inventory> readOnlyHashSet = this._districtInventoryRegistry.ActiveInventoriesWithStock(goodId);
			Inventory result = null;
			float num = float.MaxValue;
			foreach (Inventory inventory in readOnlyHashSet)
			{
				Accessible enabledComponent = inventory.GetEnabledComponent<Accessible>();
				float num2;
				if (enabledComponent.IsReachableByRoad(accessibleReachableFromInventory) && enabledComponent.FindRoadPath(start, out num2) && num2 < num)
				{
					result = inventory;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000239C File Offset: 0x0000059C
		public Inventory ClosestInventoryWithCapacity(Vector3 start, GoodAmount goodAmount, out float closestDistance)
		{
			ReadOnlyHashSet<Inventory> inventories = this._districtInventoryRegistry.ActiveInventoriesWithCapacity(goodAmount.GoodId);
			Inventory inventory = null;
			closestDistance = float.MaxValue;
			foreach (Inventory inventory2 in inventories)
			{
				Accessible enabledComponent = inventory2.GetEnabledComponent<Accessible>();
				float num;
				if ((enabledComponent.FindRoadPath(start, out num) || enabledComponent.FindRoadToTerrainPath(start, out num)) && num < closestDistance && DistrictInventoryPicker.InventoryIsTaking(inventory2, goodAmount))
				{
					inventory = inventory2;
					closestDistance = num;
				}
			}
			if (!inventory)
			{
				return DistrictInventoryPicker.ClosestInventoryInStraightLine(start, goodAmount, inventories, out closestDistance);
			}
			return inventory;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002448 File Offset: 0x00000648
		public Inventory ClosestInventoryWithCapacity(Accessible start, GoodAmount goodAmount, Predicate<Inventory> inventoryFilter, out float closestDistance)
		{
			ReadOnlyHashSet<Inventory> readOnlyHashSet = this._districtInventoryRegistry.ActiveInventoriesWithCapacity(goodAmount.GoodId);
			Inventory result = null;
			closestDistance = float.MaxValue;
			foreach (Inventory inventory in readOnlyHashSet)
			{
				Accessible enabledComponent = inventory.GetEnabledComponent<Accessible>();
				float num;
				if (inventoryFilter(inventory) && start.FindRoadPath(enabledComponent, out num) && num < closestDistance && DistrictInventoryPicker.InventoryIsTaking(inventory, goodAmount))
				{
					result = inventory;
					closestDistance = num;
				}
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E4 File Offset: 0x000006E4
		public static bool InventoryIsTaking(Inventory inventory, GoodAmount goodAmount)
		{
			return inventory.HasUnreservedCapacity(goodAmount) && inventory.GetComponent<IInventoryValidator>().ValidInventory && inventory.GetComponent<BlockableObject>().IsUnblocked;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000250C File Offset: 0x0000070C
		public static Inventory ClosestInventoryInStraightLine(Vector3 start, GoodAmount goodAmount, ReadOnlyHashSet<Inventory> inventories, out float closestDistance)
		{
			Inventory inventory = null;
			closestDistance = float.MaxValue;
			foreach (Inventory inventory2 in inventories)
			{
				float num = Vector3.Distance(start, inventory2.Transform.position);
				if (num < closestDistance && DistrictInventoryPicker.InventoryIsTaking(inventory2, goodAmount))
				{
					inventory = inventory2;
					closestDistance = num;
				}
			}
			if (!inventory || !inventory.GetEnabledComponent<Accessible>().IsReachableUnlimitedRange(start))
			{
				return null;
			}
			return inventory;
		}

		// Token: 0x0400000A RID: 10
		public DistrictInventoryRegistry _districtInventoryRegistry;
	}
}
