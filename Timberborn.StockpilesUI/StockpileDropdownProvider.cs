using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using UnityEngine;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000017 RID: 23
	public class StockpileDropdownProvider : BaseComponent, IAwakableComponent, IInitializableEntity, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000328F File Offset: 0x0000148F
		public StockpileDropdownProvider(StockpileOptionsService stockpileOptionsService)
		{
			this._stockpileOptionsService = stockpileOptionsService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032A9 File Offset: 0x000014A9
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._items.AsReadOnlyList<string>();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032BB File Offset: 0x000014BB
		public void Awake()
		{
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._inventory = base.GetComponent<Stockpile>().Inventory;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032DC File Offset: 0x000014DC
		public void InitializeEntity()
		{
			IEnumerable<string> collection = from good in this._inventory.AllowedGoods
			select good.StorableGood.GoodId;
			this._items.AddRange(collection);
			this._items.Add(StockpileOptionsService.NothingSelectedLocKey);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000333A File Offset: 0x0000153A
		public string GetValue()
		{
			return this._singleGoodAllower.AllowedGood ?? StockpileOptionsService.NothingSelectedLocKey;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003350 File Offset: 0x00001550
		public void SetValue(string value)
		{
			if (value == StockpileOptionsService.NothingSelectedLocKey)
			{
				this._singleGoodAllower.Disallow();
				return;
			}
			this._singleGoodAllower.Allow(value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003377 File Offset: 0x00001577
		public string FormatDisplayText(string value, bool selected)
		{
			return this._stockpileOptionsService.GetItemDisplayText(value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003385 File Offset: 0x00001585
		public Sprite GetIcon(string value)
		{
			return this._stockpileOptionsService.GetItemIcon(value);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003393 File Offset: 0x00001593
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04000052 RID: 82
		public readonly StockpileOptionsService _stockpileOptionsService;

		// Token: 0x04000053 RID: 83
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000054 RID: 84
		public Inventory _inventory;

		// Token: 0x04000055 RID: 85
		public readonly List<string> _items = new List<string>();
	}
}
