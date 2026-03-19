using System;
using Timberborn.Goods;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000022 RID: 34
	public class StockpileOptionsService
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004055 File Offset: 0x00002255
		public StockpileOptionsService(IGoodService goodService, ILoc loc)
		{
			this._goodService = goodService;
			this._loc = loc;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000406B File Offset: 0x0000226B
		public void UpdateItem(Label text, Image icon, string key)
		{
			text.text = this.GetItemDisplayText(key);
			icon.sprite = this.GetItemIcon(key);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004087 File Offset: 0x00002287
		public string GetItemDisplayText(string value)
		{
			if (!(value == StockpileOptionsService.NothingSelectedLocKey))
			{
				return this._goodService.GetGood(value).PluralDisplayName.Value;
			}
			return this._loc.T(StockpileOptionsService.NothingSelectedLocKey);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040BD File Offset: 0x000022BD
		public Sprite GetItemIcon(string key)
		{
			if (!(key == StockpileOptionsService.NothingSelectedLocKey))
			{
				return this._goodService.GetGood(key).IconSmall.Value;
			}
			return null;
		}

		// Token: 0x0400008D RID: 141
		public static readonly string NothingSelectedLocKey = "Inventory.NothingSelected";

		// Token: 0x0400008E RID: 142
		public readonly IGoodService _goodService;

		// Token: 0x0400008F RID: 143
		public readonly ILoc _loc;
	}
}
