using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Goods;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000018 RID: 24
	public class InventoryInitializer
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004184 File Offset: 0x00002384
		public InventoryInitializer(IGoodService goodService, Inventory inventory, int capacity, string componentName)
		{
			this._goodService = goodService;
			this._inventory = inventory;
			this._capacity = capacity;
			this._componentName = componentName;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000041B4 File Offset: 0x000023B4
		public void AddAllowedGood(StorableGoodAmount good)
		{
			this._storableGoodAmounts.Add(good);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000041C2 File Offset: 0x000023C2
		public void AddAllowedGoods(IEnumerable<StorableGoodAmount> goods)
		{
			this._storableGoodAmounts.AddRange(goods);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000041D0 File Offset: 0x000023D0
		public void AddAllowedGoodType(string goodType)
		{
			IEnumerable<StorableGood> source = this.GetGoods(goodType).Select(new Func<string, StorableGood>(StorableGood.CreateGiveableAndTakeable));
			this._storableGoodAmounts.AddRange(from good in source
			select new StorableGoodAmount(good, this._capacity));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004213 File Offset: 0x00002413
		public void HasPublicInput()
		{
			this._publicInput = true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000421C File Offset: 0x0000241C
		public void HasPublicOutput()
		{
			this._publicOutput = true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004225 File Offset: 0x00002425
		public void SetIgnorableCapacity()
		{
			this._ignorableCapacity = true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000422E File Offset: 0x0000242E
		public void AddGoodDisallower(IGoodDisallower goodDisallower)
		{
			this._goodDisallower = goodDisallower;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004238 File Offset: 0x00002438
		public void Initialize()
		{
			IGoodDisallower goodDisallower = this._goodDisallower ?? new NullGoodDisallower();
			this._inventory.Initialize(this._componentName, this._capacity, this._storableGoodAmounts, this._publicInput, this._publicOutput, this._ignorableCapacity, goodDisallower);
			IInitializableGoodDisallower initializableGoodDisallower = goodDisallower as IInitializableGoodDisallower;
			if (initializableGoodDisallower != null)
			{
				initializableGoodDisallower.Initialize(this._inventory);
			}
			this.AddToInventories();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042A4 File Offset: 0x000024A4
		public IEnumerable<string> GetGoods(string goodType)
		{
			return from good in this._goodService.Goods
			where this._goodService.GetGood(good).GoodType == goodType
			select good;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042E6 File Offset: 0x000024E6
		public void AddToInventories()
		{
			this._inventory.GetComponent<Inventories>().AddInventory(this._inventory);
		}

		// Token: 0x04000048 RID: 72
		public readonly IGoodService _goodService;

		// Token: 0x04000049 RID: 73
		public readonly Inventory _inventory;

		// Token: 0x0400004A RID: 74
		public readonly int _capacity;

		// Token: 0x0400004B RID: 75
		public readonly string _componentName;

		// Token: 0x0400004C RID: 76
		public readonly List<StorableGoodAmount> _storableGoodAmounts = new List<StorableGoodAmount>();

		// Token: 0x0400004D RID: 77
		public bool _publicInput;

		// Token: 0x0400004E RID: 78
		public bool _publicOutput;

		// Token: 0x0400004F RID: 79
		public bool _ignorableCapacity;

		// Token: 0x04000050 RID: 80
		public IGoodDisallower _goodDisallower;
	}
}
