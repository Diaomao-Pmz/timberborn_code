using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GoodConsumingBuildingSystem;
using Timberborn.InventorySystemUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.GoodConsumingBuildingSystemUI
{
	// Token: 0x02000007 RID: 7
	public class GoodConsumingBuildingFragment : IEntityPanelFragment
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000241B File Offset: 0x0000061B
		public GoodConsumingBuildingFragment(VisualElementLoader visualElementLoader, InventoryFragmentBuilderFactory inventoryFragmentBuilderFactory, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._inventoryFragmentBuilderFactory = inventoryFragmentBuilderFactory;
			this._loc = loc;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002438 File Offset: 0x00000638
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/GoodConsumingBuildingFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._inventoryFragment = this._inventoryFragmentBuilderFactory.CreateBuilder(this._root).ShowRowLimit().ShowEmptyRows().Build();
			this._hoursLeftBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._hoursLeft = UQueryExtensions.Q<Label>(this._root, "HoursLeft", null);
			return this._root;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024B7 File Offset: 0x000006B7
		public void ShowFragment(BaseComponent entity)
		{
			this._goodConsumingBuilding = entity.GetComponent<GoodConsumingBuilding>();
			if (this._goodConsumingBuilding)
			{
				this._inventoryFragment.ShowFragment(this._goodConsumingBuilding.Inventory);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E8 File Offset: 0x000006E8
		public void ClearFragment()
		{
			this._goodConsumingBuilding = null;
			this._inventoryFragment.ClearFragment();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateFragment()
		{
			if (this._goodConsumingBuilding && this._goodConsumingBuilding.Enabled)
			{
				this._inventoryFragment.UpdateFragment();
				this.UpdateProgressBar();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002550 File Offset: 0x00000750
		public void UpdateProgressBar()
		{
			float num = this._goodConsumingBuilding.HoursUntilNoSupply();
			this._hoursLeftBar.SetProgress(num / this._goodConsumingBuilding.MaximumWorkingTime);
			this._hoursLeft.text = this._loc.T<string>(GoodConsumingBuildingFragment.RemainingLocKey, string.Format("{0:F1}", num));
		}

		// Token: 0x0400001A RID: 26
		public static readonly string RemainingLocKey = "GoodConsuming.SupplyRemaining";

		// Token: 0x0400001B RID: 27
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001C RID: 28
		public readonly InventoryFragmentBuilderFactory _inventoryFragmentBuilderFactory;

		// Token: 0x0400001D RID: 29
		public readonly ILoc _loc;

		// Token: 0x0400001E RID: 30
		public InventoryFragment _inventoryFragment;

		// Token: 0x0400001F RID: 31
		public GoodConsumingBuilding _goodConsumingBuilding;

		// Token: 0x04000020 RID: 32
		public VisualElement _root;

		// Token: 0x04000021 RID: 33
		public ProgressBar _hoursLeftBar;

		// Token: 0x04000022 RID: 34
		public Label _hoursLeft;
	}
}
