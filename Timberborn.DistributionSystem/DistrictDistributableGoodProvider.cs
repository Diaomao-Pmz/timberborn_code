using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Emptying;
using Timberborn.InventorySystem;
using Timberborn.StockpilePrioritySystem;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000012 RID: 18
	public class DistrictDistributableGoodProvider : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000033CC File Offset: 0x000015CC
		public void Awake()
		{
			this._districtDistributionSetting = base.GetComponent<DistrictDistributionSetting>();
			this._distributionInventoryRegistry = base.GetComponent<DistributionInventoryRegistry>();
			this._districtDistributionSetting.SettingChanged += delegate(object _, GoodDistributionSetting setting)
			{
				this.ClearCache(setting.GoodId);
			};
			this._distributionInventoryRegistry.GoodStorageChanged += delegate(object _, string goodId)
			{
				this.ClearCache(goodId);
			};
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003420 File Offset: 0x00001620
		public void GetDistributableGoodsForImport(List<DistributableGood> distributableGoods)
		{
			foreach (GoodDistributionSetting goodDistributionSetting in this._districtDistributionSetting.GoodDistributionSettings)
			{
				DistributableGood item;
				if (this.TryGetDistributableGoodForImport(goodDistributionSetting.GoodId, out item))
				{
					distributableGoods.Add(item);
				}
			}
			distributableGoods.Sort();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003494 File Offset: 0x00001694
		public bool TryGetDistributableGoodForImport(string goodId, out DistributableGood distributableGood)
		{
			ImportableGood importableGood = this.GetImportableGood(goodId);
			distributableGood = importableGood.DistributableGood;
			return importableGood.IsImportable;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000034C0 File Offset: 0x000016C0
		public DistributableGood GetDistributableGoodForExport(string goodId)
		{
			DistributableGood result;
			if (this._exportCache.TryGetValue(goodId, out result))
			{
				return result;
			}
			GoodDistributionSetting goodDistributionSetting = this._districtDistributionSetting.GetGoodDistributionSetting(goodId);
			return this.GetAndCacheExportDistributableGood(goodDistributionSetting);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034F4 File Offset: 0x000016F4
		public bool IsImportEnabled(string goodId)
		{
			ImportableGood importableGood = this.GetImportableGood(goodId);
			return importableGood.IsImportable || importableGood.HasCapacity;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000351B File Offset: 0x0000171B
		public ImportOption GetGoodImportOption(string goodId)
		{
			return this._districtDistributionSetting.GetGoodDistributionSetting(goodId).ImportOption;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000352E File Offset: 0x0000172E
		public void ClearCache(string goodId)
		{
			this._exportCache.Remove(goodId);
			this._importCache.Remove(goodId);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000354C File Offset: 0x0000174C
		public ImportableGood GetImportableGood(string goodId)
		{
			ImportableGood importableGood;
			if (this._importCache.TryGetValue(goodId, out importableGood))
			{
				return importableGood;
			}
			GoodDistributionSetting goodDistributionSetting = this._districtDistributionSetting.GetGoodDistributionSetting(goodId);
			bool flag;
			if (this.CanBeImported(goodDistributionSetting, out flag))
			{
				importableGood = ImportableGood.CreateImportableWithCapacity(this.GetDistributableGood(goodDistributionSetting, true));
			}
			else if (flag)
			{
				importableGood = ImportableGood.CreateNonImportableWithCapacity();
			}
			else
			{
				importableGood = ImportableGood.CreateNonImportable();
			}
			this._importCache.Add(goodId, importableGood);
			return importableGood;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000035B2 File Offset: 0x000017B2
		public bool CanBeImported(GoodDistributionSetting goodDistributionSetting, out bool hasCapacity)
		{
			hasCapacity = (goodDistributionSetting.ImportOption == ImportOption.Forced);
			return goodDistributionSetting.ImportOption == ImportOption.Forced || (goodDistributionSetting.ImportOption == ImportOption.Auto && this.HasUnreservedCapacity(goodDistributionSetting, out hasCapacity));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035E0 File Offset: 0x000017E0
		public bool HasUnreservedCapacity(GoodDistributionSetting goodDistributionSetting, out bool hasCapacity)
		{
			hasCapacity = false;
			foreach (Inventory inventory in this._distributionInventoryRegistry.CapacityInventories(goodDistributionSetting.GoodId))
			{
				if (inventory.IsUnblocked && DistrictDistributableGoodProvider.GetInventoryCapacity(inventory, goodDistributionSetting.GoodId) > 0)
				{
					hasCapacity = true;
					if (inventory.HasUnreservedCapacity(goodDistributionSetting.GoodId))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000366C File Offset: 0x0000186C
		public DistributableGood GetAndCacheExportDistributableGood(GoodDistributionSetting goodDistributionSetting)
		{
			DistributableGood distributableGood = this.GetDistributableGood(goodDistributionSetting, false);
			this._exportCache.Add(goodDistributionSetting.GoodId, distributableGood);
			return distributableGood;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003698 File Offset: 0x00001898
		public DistributableGood GetDistributableGood(GoodDistributionSetting goodDistributionSetting, bool withDistrictCrossingIncomingStock)
		{
			int capacity = this.GetCapacity(goodDistributionSetting);
			return new DistributableGood(this.GetStock(goodDistributionSetting.GoodId, withDistrictCrossingIncomingStock), capacity, goodDistributionSetting);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036C4 File Offset: 0x000018C4
		public int GetCapacity(GoodDistributionSetting goodDistributionSetting)
		{
			string goodId = goodDistributionSetting.GoodId;
			int num = 0;
			foreach (Inventory inventory in this._distributionInventoryRegistry.StoringInventories(goodId))
			{
				if (inventory.IsUnblocked)
				{
					num += DistrictDistributableGoodProvider.GetInventoryCapacity(inventory, goodId);
				}
			}
			if (goodDistributionSetting.ImportOption == ImportOption.Forced || (num == 0 && this.HasTakingInventory(goodId)))
			{
				num += this.GetDistrictCrossingsCapacity(goodId);
			}
			return num;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003758 File Offset: 0x00001958
		public static int GetInventoryCapacity(Inventory inventory, string goodId)
		{
			Emptiable component = inventory.GetComponent<Emptiable>();
			if (component == null || !component.IsMarkedForEmptying)
			{
				GoodSupplier component2 = inventory.GetComponent<GoodSupplier>();
				if (component2 == null || !component2.IsSupplying)
				{
					return inventory.LimitedAmount(goodId);
				}
			}
			return 0;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003794 File Offset: 0x00001994
		public bool HasTakingInventory(string goodId)
		{
			foreach (Inventory inventory in this._distributionInventoryRegistry.CapacityInventories(goodId))
			{
				if (inventory.IsUnblocked && DistrictDistributableGoodProvider.GetInventoryCapacity(inventory, goodId) > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003804 File Offset: 0x00001A04
		public int GetDistrictCrossingsCapacity(string goodId)
		{
			ReadOnlyHashSet<Inventory> districtCrossingInventories = this._distributionInventoryRegistry.DistrictCrossingInventories;
			int num = 0;
			foreach (Inventory inventory in districtCrossingInventories)
			{
				num += inventory.LimitedAmount(goodId);
			}
			return num;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003868 File Offset: 0x00001A68
		public int GetStock(string goodId, bool withDistrictCrossingIncomingStock)
		{
			int num = 0;
			foreach (Inventory inventory in this._distributionInventoryRegistry.StockInventories(goodId))
			{
				num += DistrictDistributableGoodProvider.GetInventoryStock(inventory, goodId, withDistrictCrossingIncomingStock);
			}
			return num;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000038CC File Offset: 0x00001ACC
		public static int GetInventoryStock(Inventory inventory, string goodId, bool withDistrictCrossingIncomingStock)
		{
			int num = inventory.UnreservedAmountInStock(goodId);
			if (DistrictDistributableGoodProvider.IsDistrictCrossingInventory(inventory))
			{
				if (withDistrictCrossingIncomingStock)
				{
					num += inventory.GetComponent<DistrictCrossingInventory>().IncomingStock(goodId);
				}
			}
			else
			{
				num += inventory.ReservedCapacity(goodId);
			}
			return num;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003908 File Offset: 0x00001B08
		public static bool IsDistrictCrossingInventory(Inventory inventory)
		{
			return inventory.ComponentName == DistrictCrossingInventoryInitializer.InventoryComponentName;
		}

		// Token: 0x0400002D RID: 45
		public DistrictDistributionSetting _districtDistributionSetting;

		// Token: 0x0400002E RID: 46
		public DistributionInventoryRegistry _distributionInventoryRegistry;

		// Token: 0x0400002F RID: 47
		public readonly Dictionary<string, DistributableGood> _exportCache = new Dictionary<string, DistributableGood>();

		// Token: 0x04000030 RID: 48
		public readonly Dictionary<string, ImportableGood> _importCache = new Dictionary<string, ImportableGood>();
	}
}
