using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.Stockpiles;
using UnityEngine.UIElements;

namespace Timberborn.StockpilesUI
{
	// Token: 0x02000021 RID: 33
	public class StockpileInventoryFragment : IEntityPanelFragment
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00003CCC File Offset: 0x00001ECC
		public StockpileInventoryFragment(VisualElementLoader visualElementLoader, IGoodSelectionController goodSelectionController, IGoodService goodService)
		{
			this._visualElementLoader = visualElementLoader;
			this._goodSelectionController = goodSelectionController;
			this._goodService = goodService;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CEC File Offset: 0x00001EEC
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/StockpileInventoryFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._capacityAmount = UQueryExtensions.Q<Label>(this._root, "CapacityAmount", null);
			this._capacityLimit = UQueryExtensions.Q<Label>(this._root, "CapacityLimit", null);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._goodSelectionButton = UQueryExtensions.Q<Button>(this._root, "Selection", null);
			this._goodUnselectionButton = UQueryExtensions.Q<Button>(this._root, "Unselect", null);
			this._goodUnselectionButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._singleGoodAllower.Disallow();
			}, 0);
			this._goodSelectionController.Initialize(this._root);
			this._outputGood = UQueryExtensions.Q<VisualElement>(this._root, "OutputGood", null);
			this._outputGoodIcon = UQueryExtensions.Q<Image>(this._outputGood, "Image", null);
			this._outputGoodName = UQueryExtensions.Q<Label>(this._outputGood, "Name", null);
			return this._root;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public void ShowFragment(BaseComponent entity)
		{
			Stockpile component = entity.GetComponent<Stockpile>();
			if (component != null)
			{
				this._singleGoodAllower = component.GetComponent<SingleGoodAllower>();
				this._inventory = component.Inventory;
				this._capacityLimit.text = this._inventory.Capacity.ToString();
				this._goodSelectionController.SetStockpile(component);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E5D File Offset: 0x0000205D
		public void ClearFragment()
		{
			this.ToggleButtonHighlight(false);
			this._singleGoodAllower = null;
			this._inventory = null;
			this._goodSelectionController.Clear();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E8C File Offset: 0x0000208C
		public void UpdateFragment()
		{
			if (this._inventory)
			{
				int totalAmountInStock = this._inventory.TotalAmountInStock;
				this._progressBar.SetProgress((float)totalAmountInStock / (float)this._inventory.Capacity);
				this._capacityAmount.text = totalAmountInStock.ToString();
				this.UpdateUnallowedGoods();
				this._goodSelectionController.Update();
				this._goodUnselectionButton.ToggleDisplayStyle(this._singleGoodAllower.HasAllowedGood);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F12 File Offset: 0x00002112
		public void ShowGoodSelectionBox()
		{
			this._goodSelectionController.ShowGoodSelectionBox();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F1F File Offset: 0x0000211F
		public void ToggleButtonHighlight(bool highlight)
		{
			if (this._inventory)
			{
				this._goodSelectionButton.EnableInClassList(StockpileInventoryFragment.ButtonHighlightClass, highlight);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F40 File Offset: 0x00002140
		public void UpdateUnallowedGoods()
		{
			string allowedGood = this._singleGoodAllower.AllowedGood;
			if (((allowedGood != null) ? this._inventory.AmountInStock(allowedGood) : 0) == this._inventory.TotalAmountInStock)
			{
				this._outputGood.ToggleDisplayStyle(false);
				return;
			}
			this.ShowOutputGood(allowedGood);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003F8C File Offset: 0x0000218C
		public void ShowOutputGood(string allowedGood)
		{
			foreach (GoodAmount goodAmount in this._inventory.Stock)
			{
				string goodId = goodAmount.GoodId;
				if (goodId != allowedGood)
				{
					GoodSpec good = this._goodService.GetGood(goodId);
					this._outputGood.ToggleDisplayStyle(true);
					this._outputGoodIcon.sprite = good.IconSmall.Value;
					this._outputGoodName.text = good.PluralDisplayName.Value;
					break;
				}
			}
		}

		// Token: 0x0400007E RID: 126
		public static readonly string ButtonHighlightClass = "highlight";

		// Token: 0x0400007F RID: 127
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000080 RID: 128
		public readonly IGoodSelectionController _goodSelectionController;

		// Token: 0x04000081 RID: 129
		public readonly IGoodService _goodService;

		// Token: 0x04000082 RID: 130
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x04000083 RID: 131
		public Inventory _inventory;

		// Token: 0x04000084 RID: 132
		public VisualElement _root;

		// Token: 0x04000085 RID: 133
		public Label _capacityAmount;

		// Token: 0x04000086 RID: 134
		public Label _capacityLimit;

		// Token: 0x04000087 RID: 135
		public ProgressBar _progressBar;

		// Token: 0x04000088 RID: 136
		public Button _goodSelectionButton;

		// Token: 0x04000089 RID: 137
		public Button _goodUnselectionButton;

		// Token: 0x0400008A RID: 138
		public VisualElement _outputGood;

		// Token: 0x0400008B RID: 139
		public Image _outputGoodIcon;

		// Token: 0x0400008C RID: 140
		public Label _outputGoodName;
	}
}
