using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.StatusSystemUI;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200001A RID: 26
	public class StockpileGoodSelectionBoxFactory
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000378E File Offset: 0x0000198E
		public StockpileGoodSelectionBoxFactory(InputService inputService, StatusListFragment statusListFragment, StockpileGoodSelectionBoxItemsFactory stockpileGoodSelectionBoxItemsFactory, VisualElementLoader visualElementLoader)
		{
			this._inputService = inputService;
			this._statusListFragment = statusListFragment;
			this._stockpileGoodSelectionBoxItemsFactory = stockpileGoodSelectionBoxItemsFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037B4 File Offset: 0x000019B4
		public StockpileGoodSelectionBox Create()
		{
			string elementName = "Game/StockpileGoodSelectionBox";
			VisualElement root = this._visualElementLoader.LoadVisualElement(elementName);
			StockpileGoodSelectionBox stockpileGoodSelectionBox = new StockpileGoodSelectionBox(this._inputService, this._statusListFragment, this._stockpileGoodSelectionBoxItemsFactory, root);
			stockpileGoodSelectionBox.Initialize();
			return stockpileGoodSelectionBox;
		}

		// Token: 0x04000063 RID: 99
		public readonly InputService _inputService;

		// Token: 0x04000064 RID: 100
		public readonly StatusListFragment _statusListFragment;

		// Token: 0x04000065 RID: 101
		public readonly StockpileGoodSelectionBoxItemsFactory _stockpileGoodSelectionBoxItemsFactory;

		// Token: 0x04000066 RID: 102
		public readonly VisualElementLoader _visualElementLoader;
	}
}
