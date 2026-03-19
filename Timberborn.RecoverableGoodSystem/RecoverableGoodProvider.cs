using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using UnityEngine;

namespace Timberborn.RecoverableGoodSystem
{
	// Token: 0x02000006 RID: 6
	public class RecoverableGoodProvider : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		public void Awake()
		{
			this._inventories = base.GetComponent<Inventories>();
			base.GetComponents<IRecoverableGoodMultiplier>(this._recoverableGoodMultipliers);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B2 File Offset: 0x000003B2
		public void DisableGoodRecovery()
		{
			this._recoveryDisabled = true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BB File Offset: 0x000003BB
		public void EnableGoodRecovery()
		{
			this._recoveryDisabled = false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021C4 File Offset: 0x000003C4
		public void GetRecoverableGoods(RecoverableGoodRegistry recoverableGoodRegistry)
		{
			if (!this._recoveryDisabled)
			{
				for (int i = 0; i < this._inventories.AllInventories.Count; i++)
				{
					Inventory inventory = this._inventories.AllInventories[i];
					this.AddGoodsFromInventory(recoverableGoodRegistry, inventory);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002214 File Offset: 0x00000414
		public void AddGoodsFromInventory(RecoverableGoodRegistry recoverableGoodRegistry, Inventory inventory)
		{
			float totalMultiplierForInventory = this.GetTotalMultiplierForInventory(inventory);
			foreach (GoodAmount goodAmount in inventory.Stock)
			{
				recoverableGoodRegistry.Add(RecoverableGoodProvider.GetMultipliedAmount(goodAmount, totalMultiplierForInventory));
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public float GetTotalMultiplierForInventory(Inventory inventory)
		{
			float num = 1f;
			for (int i = 0; i < this._recoverableGoodMultipliers.Count; i++)
			{
				num *= this._recoverableGoodMultipliers[i].GetMultiplierForInventory(inventory);
			}
			return num;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B8 File Offset: 0x000004B8
		public static GoodAmount GetMultipliedAmount(GoodAmount goodAmount, float multiplier)
		{
			float num = (float)goodAmount.Amount * multiplier;
			return new GoodAmount(goodAmount.GoodId, Mathf.CeilToInt(num));
		}

		// Token: 0x0400000C RID: 12
		public Inventories _inventories;

		// Token: 0x0400000D RID: 13
		public readonly List<IRecoverableGoodMultiplier> _recoverableGoodMultipliers = new List<IRecoverableGoodMultiplier>();

		// Token: 0x0400000E RID: 14
		public bool _recoveryDisabled;
	}
}
