using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.TooltipSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200000F RID: 15
	public class ManufactoryFragment : IEntityPanelFragment
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002BE1 File Offset: 0x00000DE1
		public ManufactoryFragment(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, DropdownItemsSetter dropdownItemsSetter)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._dropdownItemsSetter = dropdownItemsSetter;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C00 File Offset: 0x00000E00
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/ManufactoryFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._dropdown = UQueryExtensions.Q<Dropdown>(this._root, "Recipes", null);
			this._tooltipRegistrar.RegisterLocalizable(this._dropdown, () => this._manufactoryDropdownProvider.GetValue());
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C6C File Offset: 0x00000E6C
		public void ShowFragment(BaseComponent entity)
		{
			this._manufactory = entity.GetComponent<Manufactory>();
			this._manufactoryTogglableRecipes = entity.GetComponent<ManufactoryTogglableRecipes>();
			this._isAutomaticRecipeManufactory = entity.GetComponent<AutomaticRecipeManufactory>();
			if (this.Visible && !this._isAutomaticRecipeManufactory)
			{
				this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
				this._manufactoryDropdownProvider = this._manufactory.GetComponent<ManufactoryDropdownProvider>();
				this._root.ToggleDisplayStyle(true);
				this._dropdownItemsSetter.SetLocalizableItems(this._dropdown, this._manufactoryDropdownProvider);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CFD File Offset: 0x00000EFD
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this.Visible && !this._isAutomaticRecipeManufactory);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D20 File Offset: 0x00000F20
		public void ClearFragment()
		{
			if (this.Visible)
			{
				this._manufactory.RecipeChanged -= this.OnProductionRecipeChanged;
			}
			this._dropdown.ClearItems();
			this._manufactory = null;
			this._manufactoryTogglableRecipes = null;
			this._manufactoryDropdownProvider = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D78 File Offset: 0x00000F78
		public bool Visible
		{
			get
			{
				return !this._manufactoryTogglableRecipes && this._manufactory && this._manufactory.ProductionRecipes.Length > 1;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x0400003A RID: 58
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003B RID: 59
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400003C RID: 60
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400003D RID: 61
		public Manufactory _manufactory;

		// Token: 0x0400003E RID: 62
		public ManufactoryTogglableRecipes _manufactoryTogglableRecipes;

		// Token: 0x0400003F RID: 63
		public ManufactoryDropdownProvider _manufactoryDropdownProvider;

		// Token: 0x04000040 RID: 64
		public Dropdown _dropdown;

		// Token: 0x04000041 RID: 65
		public VisualElement _root;

		// Token: 0x04000042 RID: 66
		public bool _isAutomaticRecipeManufactory;
	}
}
