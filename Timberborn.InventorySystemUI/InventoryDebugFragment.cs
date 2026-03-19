using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.InventorySystem;
using UnityEngine.UIElements;

namespace Timberborn.InventorySystemUI
{
	// Token: 0x0200000C RID: 12
	public class InventoryDebugFragment : IEntityPanelFragment
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002753 File Offset: 0x00000953
		public InventoryDebugFragment(DebugFragmentFactory debugFragmentFactory, IGoodService goodService, GoodDescriber goodDescriber, ModifyInventoryBox modifyInventoryBox)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._goodService = goodService;
			this._goodDescriber = goodDescriber;
			this._modifyInventoryBox = modifyInventoryBox;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002790 File Offset: 0x00000990
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.OnModifyInventoryButtonClick), "Modify Inventory");
			this._root = this._debugFragmentFactory.Create("Inventory", debugFragmentButton);
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			return this._root;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027E9 File Offset: 0x000009E9
		public void ShowFragment(BaseComponent entity)
		{
			entity.GetComponents<Inventory>(this._inventories);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027F7 File Offset: 0x000009F7
		public void ClearFragment()
		{
			this._inventories.Clear();
			this.UpdateContent();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000280A File Offset: 0x00000A0A
		public void UpdateFragment()
		{
			this.UpdateContent();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002814 File Offset: 0x00000A14
		public void OnModifyInventoryButtonClick()
		{
			Inventory inventory2 = this._inventories.SingleOrDefault((Inventory inventory) => inventory && inventory.Enabled);
			if (inventory2)
			{
				this._modifyInventoryBox.Open(inventory2);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002860 File Offset: 0x00000A60
		public void UpdateContent()
		{
			this._description.Clear();
			if (this._inventories != null)
			{
				foreach (Inventory inventory in this._inventories)
				{
					if (inventory)
					{
						this.DescribeInventory(inventory, this._description);
					}
				}
			}
			if (this._description.Length > 0)
			{
				this._text.text = this._description.ToStringWithoutNewLineEnd();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002914 File Offset: 0x00000B14
		public void DescribeInventory(Inventory inventory, StringBuilder description)
		{
			description.Append(inventory.ComponentName);
			description.AppendLine(inventory.Enabled ? " (on)" : " (off)");
			foreach (string goodId in this._goodService.Goods)
			{
				this.DescribeGood(inventory, goodId, description);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000299C File Offset: 0x00000B9C
		public void DescribeGood(Inventory inventory, string goodId, StringBuilder description)
		{
			int num = inventory.AmountInStock(goodId);
			int num2 = inventory.ReservedCapacity(goodId);
			if (num > 0 || num2 > 0)
			{
				int num3 = num - inventory.UnreservedAmountInStock(goodId);
				string value = string.Format("{0}: {1}", this._goodDescriber.Describe(goodId), num) + string.Format(" ({0} reserved, {1} incoming)", num3, num2);
				description.AppendLine(value);
			}
		}

		// Token: 0x04000027 RID: 39
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000028 RID: 40
		public readonly IGoodService _goodService;

		// Token: 0x04000029 RID: 41
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400002A RID: 42
		public readonly ModifyInventoryBox _modifyInventoryBox;

		// Token: 0x0400002B RID: 43
		public Label _text;

		// Token: 0x0400002C RID: 44
		public readonly List<Inventory> _inventories = new List<Inventory>();

		// Token: 0x0400002D RID: 45
		public readonly StringBuilder _description = new StringBuilder();

		// Token: 0x0400002E RID: 46
		public VisualElement _root;
	}
}
