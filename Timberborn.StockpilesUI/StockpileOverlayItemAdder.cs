using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.SelectionSystem;
using Timberborn.Stockpiles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000026 RID: 38
	public class StockpileOverlayItemAdder : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x0000451E File Offset: 0x0000271E
		public StockpileOverlayItemAdder(StockpileOverlay stockpileOverlay, VisualElementLoader visualElementLoader, IGoodService goodService, EntitySelectionService entitySelectionService)
		{
			this._stockpileOverlay = stockpileOverlay;
			this._visualElementLoader = visualElementLoader;
			this._goodService = goodService;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004544 File Offset: 0x00002744
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._singleGoodAllower = base.GetComponent<SingleGoodAllower>();
			this._stockpile = base.GetComponent<Stockpile>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._blockObject = base.GetComponent<BlockObject>();
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/StockpileOverlayItem");
			this._item = UQueryExtensions.Q<VisualElement>(visualElement, "StockpileOverlayItem", null);
			UQueryExtensions.Q<Button>(this._item, "EntityButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._entitySelectionService.Select(this._stockpile);
			}, 0);
			this._selectionButton = UQueryExtensions.Q<Button>(this._item, "SelectionButton", null);
			this._selectionButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._stockpileOverlay.ToggleGoodSelection(this._stockpile, this._item);
			}, 0);
			this._itemIcon = UQueryExtensions.Q<Image>(this._item, "Icon", null);
			this._itemText = UQueryExtensions.Q<Label>(this._item, "Stock", null);
			this._fillLevel = UQueryExtensions.Q<VisualElement>(this._item, "Progress", null);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004648 File Offset: 0x00002848
		public void InitializeEntity()
		{
			this._singleGoodAllower.DisallowedGoodsChanged += this.OnDisallowedGoodsChanged;
			this.Inventory.InventoryChanged += this.OnInventoryChanged;
			this._blockObjectModelController.ModelsUpdated += this.OnModelsUpdated;
			this.Add();
			this.UpdateIcon();
			this.UpdateAmount();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000046AC File Offset: 0x000028AC
		public void DeleteEntity()
		{
			this._singleGoodAllower.DisallowedGoodsChanged -= this.OnDisallowedGoodsChanged;
			this.Inventory.InventoryChanged -= this.OnInventoryChanged;
			this._blockObjectModelController.ModelsUpdated -= this.OnModelsUpdated;
			this.Remove();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004704 File Offset: 0x00002904
		public Inventory Inventory
		{
			get
			{
				return this._stockpile.Inventory;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004711 File Offset: 0x00002911
		public void OnDisallowedGoodsChanged(object sender, DisallowedGoodsChangedEventArgs e)
		{
			this.UpdateIcon();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004719 File Offset: 0x00002919
		public void OnInventoryChanged(object sender, InventoryChangedEventArgs e)
		{
			this.UpdateAmount();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004724 File Offset: 0x00002924
		public void UpdateIcon()
		{
			if (this._singleGoodAllower.HasAllowedGood)
			{
				GoodSpec good = this._goodService.GetGood(this._singleGoodAllower.AllowedGood);
				this._itemIcon.sprite = good.IconSmall.Value;
				this._itemIcon.AddToClassList(StockpileOverlayItemAdder.IconHiddenClass);
				return;
			}
			this._itemIcon.RemoveFromClassList(StockpileOverlayItemAdder.IconHiddenClass);
			this._itemIcon.sprite = null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004798 File Offset: 0x00002998
		public void UpdateAmount()
		{
			if (this._singleGoodAllower.HasAllowedGood && this._blockObject.IsFinished)
			{
				int num = this.Inventory.AmountInStock(this._singleGoodAllower.AllowedGood);
				this._itemText.text = num.ToString();
				this._fillLevel.SetHeightAsPercent((float)num / (float)this.Inventory.Capacity);
				this._fillLevel.parent.ToggleDisplayStyle(true);
				return;
			}
			this._itemText.text = "0";
			this._fillLevel.parent.ToggleDisplayStyle(false);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004835 File Offset: 0x00002A35
		public void OnModelsUpdated(object sender, EventArgs e)
		{
			if (this._blockObjectModelController.IsAnyModelShown)
			{
				this.Add();
			}
			else
			{
				this.Remove();
			}
			this.UpdateIcon();
			this.UpdateAmount();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004860 File Offset: 0x00002A60
		public void Add()
		{
			Vector3 worldCenter = this._blockObjectCenter.WorldCenter;
			Vector3 worldCenterGrounded = this._blockObjectCenter.WorldCenterGrounded;
			float num = (worldCenter.y + worldCenterGrounded.y) * 0.5f;
			Vector3 anchor;
			anchor..ctor(worldCenter.x, num, worldCenter.z);
			this._stockpileOverlay.Add(this._item, anchor);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000048BF File Offset: 0x00002ABF
		public void Remove()
		{
			this._stockpileOverlay.Remove(this._item);
		}

		// Token: 0x0400009E RID: 158
		public static readonly string IconHiddenClass = "icon--hidden";

		// Token: 0x0400009F RID: 159
		public readonly StockpileOverlay _stockpileOverlay;

		// Token: 0x040000A0 RID: 160
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000A1 RID: 161
		public readonly IGoodService _goodService;

		// Token: 0x040000A2 RID: 162
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x040000A3 RID: 163
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x040000A4 RID: 164
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x040000A5 RID: 165
		public Stockpile _stockpile;

		// Token: 0x040000A6 RID: 166
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x040000A7 RID: 167
		public BlockObject _blockObject;

		// Token: 0x040000A8 RID: 168
		public VisualElement _item;

		// Token: 0x040000A9 RID: 169
		public Image _itemIcon;

		// Token: 0x040000AA RID: 170
		public Label _itemText;

		// Token: 0x040000AB RID: 171
		public VisualElement _fillLevel;

		// Token: 0x040000AC RID: 172
		public Button _selectionButton;
	}
}
