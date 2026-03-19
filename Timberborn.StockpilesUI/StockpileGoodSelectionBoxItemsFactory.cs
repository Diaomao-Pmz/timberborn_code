using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.Goods;
using Timberborn.Stockpiles;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200001B RID: 27
	public class StockpileGoodSelectionBoxItemsFactory
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000037F2 File Offset: 0x000019F2
		public StockpileGoodSelectionBoxItemsFactory(GoodSelectionBoxItemFactory goodSelectionBoxItemFactory, GoodSelectionBoxRowFactory goodSelectionBoxRowFactory, IGoodService goodService)
		{
			this._goodSelectionBoxItemFactory = goodSelectionBoxItemFactory;
			this._goodSelectionBoxRowFactory = goodSelectionBoxRowFactory;
			this._goodService = goodService;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000380F File Offset: 0x00001A0F
		public IEnumerable<GoodSelectionBoxRow> CreateItems(Stockpile stockpile, Action<string> itemAction, VisualElement root)
		{
			Dictionary<string, GoodSelectionBoxRow> dictionary = new Dictionary<string, GoodSelectionBoxRow>();
			foreach (string text in stockpile.GetComponent<StockpileDropdownProvider>().Items)
			{
				if (text != StockpileOptionsService.NothingSelectedLocKey)
				{
					string goodGroupId = this._goodService.GetGood(text).GoodGroupId;
					GoodSelectionBoxRow orAdd = dictionary.GetOrAdd(goodGroupId, () => this._goodSelectionBoxRowFactory.Create(goodGroupId));
					GoodSelectionBoxItem item = this._goodSelectionBoxItemFactory.CreateForGood(text, itemAction);
					orAdd.AddItem(item);
				}
			}
			foreach (GoodSelectionBoxRow goodSelectionBoxRow in from row in dictionary.Values
			orderby row.Order
			select row)
			{
				root.Add(goodSelectionBoxRow.Root);
				yield return goodSelectionBoxRow;
			}
			IEnumerator<GoodSelectionBoxRow> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x04000067 RID: 103
		public readonly GoodSelectionBoxItemFactory _goodSelectionBoxItemFactory;

		// Token: 0x04000068 RID: 104
		public readonly GoodSelectionBoxRowFactory _goodSelectionBoxRowFactory;

		// Token: 0x04000069 RID: 105
		public readonly IGoodService _goodService;
	}
}
