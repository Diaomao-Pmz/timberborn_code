using System;
using Timberborn.BatchControl;
using Timberborn.DropdownSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000004 RID: 4
	public class ManufactoryBatchControlRowItem : IBatchControlRowItem, IClearableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public ManufactoryBatchControlRowItem(VisualElement root, Dropdown dropdown, Manufactory manufactory)
		{
			this.Root = root;
			this._dropdown = dropdown;
			this._manufactory = manufactory;
			this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public void ClearRowItem()
		{
			this._manufactory.RecipeChanged -= this.OnProductionRecipeChanged;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002115 File Offset: 0x00000315
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x04000007 RID: 7
		public readonly Dropdown _dropdown;

		// Token: 0x04000008 RID: 8
		public readonly Manufactory _manufactory;
	}
}
