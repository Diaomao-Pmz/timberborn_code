using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystem;
using Timberborn.InventorySystemUI;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000010 RID: 16
	public class ManufactoryInventoryFragment : IEntityPanelFragment
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public ManufactoryInventoryFragment(InventoryRowUpdater inventoryRowUpdater, VisualElementLoader visualElementLoader)
		{
			this._inventoryRowUpdater = inventoryRowUpdater;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/WorkplaceInventoryFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._inventoryContent = UQueryExtensions.Q<ScrollView>(this._root, "Content", null);
			this._isEmpty = UQueryExtensions.Q<VisualElement>(this._root, "IsEmpty", null);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E5C File Offset: 0x0000105C
		public void ShowFragment(BaseComponent entity)
		{
			this._manufactory = entity.GetComponent<Manufactory>();
			if (this._manufactory)
			{
				this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
				this._inventory = this._manufactory.Inventory;
				this._inventoryRowUpdater.AddRows(this._inventoryContent, this._inventory, this._rows, this._manufactory.CurrentRecipe);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002ED2 File Offset: 0x000010D2
		public void UpdateFragment()
		{
			this._inventoryRowUpdater.UpdateRowsVisibility(this._root, this._isEmpty, this._inventory, this._rows);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EF8 File Offset: 0x000010F8
		public void ClearFragment()
		{
			if (this._manufactory)
			{
				this._manufactory.RecipeChanged -= this.OnProductionRecipeChanged;
			}
			this._inventoryContent.Clear();
			this._rows.Clear();
			this._manufactory = null;
			this._inventory = null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F50 File Offset: 0x00001150
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this._inventoryContent.Clear();
			this._rows.Clear();
			this._inventoryRowUpdater.AddRows(this._inventoryContent, this._inventory, this._rows, this._manufactory.CurrentRecipe);
			this.UpdateFragment();
		}

		// Token: 0x04000043 RID: 67
		public readonly InventoryRowUpdater _inventoryRowUpdater;

		// Token: 0x04000044 RID: 68
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000045 RID: 69
		public Manufactory _manufactory;

		// Token: 0x04000046 RID: 70
		public Inventory _inventory;

		// Token: 0x04000047 RID: 71
		public VisualElement _root;

		// Token: 0x04000048 RID: 72
		public ScrollView _inventoryContent;

		// Token: 0x04000049 RID: 73
		public VisualElement _isEmpty;

		// Token: 0x0400004A RID: 74
		public readonly List<InformationalRow> _rows = new List<InformationalRow>();
	}
}
