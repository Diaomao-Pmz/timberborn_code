using System;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x0200000C RID: 12
	public class GoodSelectionController : IGoodSelectionController
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000023E7 File Offset: 0x000005E7
		public GoodSelectionController(StockpileGoodSelectionBoxFactory stockpileGoodSelectionBoxFactory, StockpileOptionsService stockpileOptionsService)
		{
			this._stockpileGoodSelectionBoxFactory = stockpileGoodSelectionBoxFactory;
			this._stockpileOptionsService = stockpileOptionsService;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002400 File Offset: 0x00000600
		public void Initialize(VisualElement root)
		{
			this._stockpileGoodSelectionBox = this._stockpileGoodSelectionBoxFactory.Create();
			root.Add(this._stockpileGoodSelectionBox.Root);
			this._selectedGoodIcon = UQueryExtensions.Q<Image>(root, "GoodIcon", null);
			this._selectedGoodText = UQueryExtensions.Q<Label>(root, "SelectionItem", null);
			this._goodSelectionButton = UQueryExtensions.Q<Button>(root, "Selection", null);
			this._goodSelectionButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ShowGoodSelectionBox();
			}, 0);
			this._goodSelectionButton.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this._stockpileGoodSelectionBox.DisableInput();
			}, 0);
			this._goodSelectionButton.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this._stockpileGoodSelectionBox.EnableInput();
			}, 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024AD File Offset: 0x000006AD
		public void Update()
		{
			this._stockpileGoodSelectionBox.Update();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024BA File Offset: 0x000006BA
		public void SetStockpile(Stockpile stockpile)
		{
			this._stockpile = stockpile;
			this._singleGoodAllower = stockpile.GetComponent<SingleGoodAllower>();
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this.UpdateSelectedGood();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024EC File Offset: 0x000006EC
		public void ShowGoodSelectionBox()
		{
			this._stockpileGoodSelectionBox.ToggleGoodSelection(this._stockpile);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024FF File Offset: 0x000006FF
		public void Clear()
		{
			this._stockpile = null;
			if (this._singleGoodAllower)
			{
				this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
			}
			this._singleGoodAllower = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002533 File Offset: 0x00000733
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateSelectedGood();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		public void UpdateSelectedGood()
		{
			string key = this._singleGoodAllower.AllowedGood ?? StockpileOptionsService.NothingSelectedLocKey;
			this._stockpileOptionsService.UpdateItem(this._selectedGoodText, this._selectedGoodIcon, key);
			this._selectedGoodIcon.EnableInClassList(GoodSelectionController.NoIconClass, this._singleGoodAllower.HasAllowedGood);
		}

		// Token: 0x0400001B RID: 27
		public static readonly string NoIconClass = "icon--hidden";

		// Token: 0x0400001C RID: 28
		public readonly StockpileGoodSelectionBoxFactory _stockpileGoodSelectionBoxFactory;

		// Token: 0x0400001D RID: 29
		public readonly StockpileOptionsService _stockpileOptionsService;

		// Token: 0x0400001E RID: 30
		public StockpileGoodSelectionBox _stockpileGoodSelectionBox;

		// Token: 0x0400001F RID: 31
		public Image _selectedGoodIcon;

		// Token: 0x04000020 RID: 32
		public Label _selectedGoodText;

		// Token: 0x04000021 RID: 33
		public Button _goodSelectionButton;

		// Token: 0x04000022 RID: 34
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000023 RID: 35
		public Stockpile _stockpile;
	}
}
