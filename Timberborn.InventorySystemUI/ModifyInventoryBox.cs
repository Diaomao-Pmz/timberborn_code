using System;
using System.Linq;
using System.Text;
using Timberborn.CoreUI;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InputSystem;
using Timberborn.InventorySystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x0200001B RID: 27
	public class ModifyInventoryBox : IPanelController, ILoadableSingleton
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000033DE File Offset: 0x000015DE
		public ModifyInventoryBox(VisualElementLoader visualElementLoader, PanelStack panelStack, InputService inputService, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._inputService = inputService;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003404 File Offset: 0x00001604
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/ModifyInventory/ModifyInventoryBox");
			UQueryExtensions.Q<Button>(this._root, "CancelButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnUICancelled();
			}, 0);
			this._amount = UQueryExtensions.Q<TextField>(this._root, "Amount", null);
			this._amount.SetValueWithoutNotify(ModifyInventoryBox.DefaultAmount);
			this._buttons = UQueryExtensions.Q<VisualElement>(this._root, "Buttons", null);
			this._inventoryContents = UQueryExtensions.Q<Label>(this._root, "Inventory", null);
			this._warning = UQueryExtensions.Q<Label>(this._root, "Warning", null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034B6 File Offset: 0x000016B6
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034BE File Offset: 0x000016BE
		public void Open(Inventory inventory)
		{
			this._inventory = inventory;
			this._singleGoodAllower = this._inventory.GetComponent<SingleGoodAllower>();
			this.CreateButtons();
			this.UpdateContents();
			this._panelStack.PushOverlay(this);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000034F0 File Offset: 0x000016F0
		public bool OnUIConfirmed()
		{
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000034F3 File Offset: 0x000016F3
		public void OnUICancelled()
		{
			this._buttons.Clear();
			this._inventory = null;
			this._singleGoodAllower = null;
			this._panelStack.Pop(this);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000351A File Offset: 0x0000171A
		public int MaxAmountPerGood
		{
			get
			{
				return int.MaxValue / this._inventory.AllowedGoods.Count<StorableGoodAmount>();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003538 File Offset: 0x00001738
		public void CreateButtons()
		{
			this.CreateCustomButtons();
			foreach (StorableGoodAmount storableGoodAmount in this._inventory.AllowedGoods)
			{
				if (!this._singleGoodAllower || !this._singleGoodAllower.HasAllowedGood || this._singleGoodAllower.AllowedGood == storableGoodAmount.StorableGood.GoodId)
				{
					this.CreateGoodButton(storableGoodAmount.StorableGood.GoodId);
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035E4 File Offset: 0x000017E4
		public void CreateCustomButtons()
		{
			this.CreateCustomButton("<b>GIVE ALL</b>", new EventCallback<ClickEvent>(this.GiveAll));
			this.CreateCustomButton("<b>GIVE INPUT</b>", new EventCallback<ClickEvent>(this.GiveInput));
			this.CreateCustomButton("<b>CLEAR ALL</b>", new EventCallback<ClickEvent>(this.ClearAll));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003638 File Offset: 0x00001838
		public void CreateCustomButton(string text, EventCallback<ClickEvent> callback)
		{
			Button button = UQueryExtensions.Q<Button>(this._visualElementLoader.LoadVisualElement("Game/ModifyInventory/ModifyInventoryBoxButton"), "Button", null);
			button.text = text;
			button.RegisterCallback<ClickEvent>(callback, 0);
			this._buttons.Add(button);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000367C File Offset: 0x0000187C
		public void CreateGoodButton(string goodId)
		{
			Button button = UQueryExtensions.Q<Button>(this._visualElementLoader.LoadVisualElement("Game/ModifyInventory/ModifyInventoryBoxButton"), "Button", null);
			button.text = this._goodDescriber.Describe(goodId);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnGoodButtonClick(goodId);
			}, 0);
			this._buttons.Add(button);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000036F0 File Offset: 0x000018F0
		public void OnGoodButtonClick(string goodId)
		{
			this.UpdateAllowedGood(goodId);
			int num = this._inputService.IsKeyHeld(ModifyInventoryBox.InventoryGoodAmountMultiplierKey) ? 10 : 1;
			int amount = int.Parse(this._amount.text) * num;
			GoodAmount goodAmount = new GoodAmount(goodId, amount);
			if (goodAmount.Amount <= 0)
			{
				this._warning.text = "The amount must be positive.";
				return;
			}
			this._warning.text = "";
			if (this._inputService.IsKeyHeld(ModifyInventoryBox.InventorySubtractGoodKey))
			{
				this.TakeGood(goodAmount);
				return;
			}
			this.GiveGood(goodAmount);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003784 File Offset: 0x00001984
		public void GiveAll(ClickEvent evt)
		{
			foreach (StorableGoodAmount storableGoodAmount in this._inventory.AllowedGoods)
			{
				this.GiveGood(new GoodAmount(storableGoodAmount.StorableGood.GoodId, this.MaxAmountPerGood));
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000037F8 File Offset: 0x000019F8
		public void GiveInput(ClickEvent evt)
		{
			foreach (string goodId in this._inventory.InputGoods)
			{
				this.GiveGood(new GoodAmount(goodId, this.MaxAmountPerGood));
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003860 File Offset: 0x00001A60
		public void ClearAll(ClickEvent evt)
		{
			foreach (StorableGoodAmount storableGoodAmount in this._inventory.AllowedGoods)
			{
				this.TakeGood(new GoodAmount(storableGoodAmount.StorableGood.GoodId, this.MaxAmountPerGood));
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038D4 File Offset: 0x00001AD4
		public void UpdateAllowedGood(string goodId)
		{
			if (this._singleGoodAllower && !this._singleGoodAllower.HasAllowedGood)
			{
				this._singleGoodAllower.Allow(goodId);
				this._buttons.Clear();
				this.CreateButtons();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003910 File Offset: 0x00001B10
		public void GiveGood(GoodAmount goodAmount)
		{
			int num = this._inventory.UnreservedCapacity(goodAmount.GoodId);
			if (this._inventory.HasComponent<DistrictCenter>())
			{
				int num2 = this.MaxAmountPerGood - this._inventory.AmountInStock(goodAmount.GoodId);
				if (num2 > 0)
				{
					this._inventory.GiveIgnoringCapacity(new GoodAmount(goodAmount.GoodId, Math.Min(num2, goodAmount.Amount)));
					this.UpdateContents();
					return;
				}
			}
			else if (num > 0)
			{
				this._inventory.Give(new GoodAmount(goodAmount.GoodId, Math.Min(num, goodAmount.Amount)));
				this.UpdateContents();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000039B4 File Offset: 0x00001BB4
		public void TakeGood(GoodAmount goodAmount)
		{
			int num = this._inventory.UnreservedAmountInStock(goodAmount.GoodId);
			if (num > 0)
			{
				this._inventory.Take(new GoodAmount(goodAmount.GoodId, Math.Min(num, goodAmount.Amount)));
				this.UpdateContents();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A04 File Offset: 0x00001C04
		public void UpdateContents()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Stock ({0} / {1}):", this._inventory.TotalAmountInStock, this._inventory.Capacity));
			foreach (GoodAmount goodAmount in this._inventory.Stock)
			{
				stringBuilder.AppendLine("  " + this._goodDescriber.Describe(goodAmount));
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Reserved capacity:");
			foreach (GoodAmount goodAmount2 in this._inventory.ReservedCapacity())
			{
				stringBuilder.AppendLine("  " + this._goodDescriber.Describe(goodAmount2));
			}
			this._inventoryContents.text = stringBuilder.ToString();
		}

		// Token: 0x04000051 RID: 81
		public static readonly string InventoryGoodAmountMultiplierKey = "InventoryGoodAmountMultiplier";

		// Token: 0x04000052 RID: 82
		public static readonly string InventorySubtractGoodKey = "InventorySubtractGood";

		// Token: 0x04000053 RID: 83
		public static readonly string DefaultAmount = "10";

		// Token: 0x04000054 RID: 84
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000055 RID: 85
		public readonly PanelStack _panelStack;

		// Token: 0x04000056 RID: 86
		public readonly InputService _inputService;

		// Token: 0x04000057 RID: 87
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000058 RID: 88
		public Inventory _inventory;

		// Token: 0x04000059 RID: 89
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x0400005A RID: 90
		public VisualElement _root;

		// Token: 0x0400005B RID: 91
		public TextField _amount;

		// Token: 0x0400005C RID: 92
		public VisualElement _buttons;

		// Token: 0x0400005D RID: 93
		public Label _inventoryContents;

		// Token: 0x0400005E RID: 94
		public Label _warning;
	}
}
