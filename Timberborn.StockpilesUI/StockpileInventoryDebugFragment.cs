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
	// Token: 0x02000020 RID: 32
	public class StockpileInventoryDebugFragment : IEntityPanelFragment
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public StockpileInventoryDebugFragment(StockpileInventoryFragment stockpileInventoryFragment, DebugFragmentFactory debugFragmentFactory)
		{
			this._stockpileInventoryFragment = stockpileInventoryFragment;
			this._debugFragmentFactory = debugFragmentFactory;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003BDC File Offset: 0x00001DDC
		public VisualElement InitializeFragment()
		{
			DebugFragmentButton debugFragmentButton = new DebugFragmentButton(new Action(this.OnGiveAllButtonClick), "Inventory: Give all");
			this._root = this._debugFragmentFactory.Create("StockpileInventoryFragment", debugFragmentButton);
			return this._root;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C1E File Offset: 0x00001E1E
		public void ShowFragment(BaseComponent entity)
		{
			this._singleGoodAllower = entity.GetComponent<SingleGoodAllower>();
			this._stockpile = entity.GetComponent<Stockpile>();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C38 File Offset: 0x00001E38
		public void ClearFragment()
		{
			this._singleGoodAllower = null;
			this._stockpile = null;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C48 File Offset: 0x00001E48
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._stockpile && this._singleGoodAllower);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003C70 File Offset: 0x00001E70
		public void OnGiveAllButtonClick()
		{
			if (this._singleGoodAllower.HasAllowedGood)
			{
				this.GiveAll();
				return;
			}
			this._stockpileInventoryFragment.ShowGoodSelectionBox();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C94 File Offset: 0x00001E94
		public void GiveAll()
		{
			Inventory inventory = this._stockpile.Inventory;
			string allowedGood = this._singleGoodAllower.AllowedGood;
			int amount = inventory.UnreservedCapacity(allowedGood);
			inventory.Give(new GoodAmount(allowedGood, amount));
		}

		// Token: 0x04000079 RID: 121
		public readonly StockpileInventoryFragment _stockpileInventoryFragment;

		// Token: 0x0400007A RID: 122
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x0400007B RID: 123
		public VisualElement _root;

		// Token: 0x0400007C RID: 124
		public SingleGoodAllower _singleGoodAllower;

		// Token: 0x0400007D RID: 125
		public Stockpile _stockpile;
	}
}
