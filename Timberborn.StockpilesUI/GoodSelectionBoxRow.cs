using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000A RID: 10
	public class GoodSelectionBoxRow
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022B0 File Offset: 0x000004B0
		public VisualElement Root { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022B8 File Offset: 0x000004B8
		public int Order { get; }

		// Token: 0x06000013 RID: 19 RVA: 0x000022C0 File Offset: 0x000004C0
		public GoodSelectionBoxRow(VisualElement root, int order, VisualElement itemsRoot)
		{
			this.Root = root;
			this.Order = order;
			this._itemsRoot = itemsRoot;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		public void AddItem(GoodSelectionBoxItem item)
		{
			this._items.Add(item);
			this._itemsRoot.Add(item.Root);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002308 File Offset: 0x00000508
		public void Update()
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				this._items[i].Update();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000233C File Offset: 0x0000053C
		public void UpdateSelectedState(string selectedGoodId)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				this._items[i].UpdateSelectedState(selectedGoodId);
			}
		}

		// Token: 0x04000017 RID: 23
		public readonly VisualElement _itemsRoot;

		// Token: 0x04000018 RID: 24
		public readonly List<GoodSelectionBoxItem> _items = new List<GoodSelectionBoxItem>();
	}
}
