using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x02000005 RID: 5
	public class ManufactoryBatchControlRowItemFactory
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002122 File Offset: 0x00000322
		public ManufactoryBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Manufactory component = entity.GetComponent<Manufactory>();
			if (component != null && component.ProductionRecipes.Length > 1 && !entity.GetComponent<ManufactoryTogglableRecipes>())
			{
				string elementName = "Game/BatchControl/DropdownBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Dropdown dropdown = UQueryExtensions.Q<Dropdown>(visualElement, "Dropdown", null);
				ManufactoryDropdownProvider manufactoryDropdownProvider = component.GetComponent<ManufactoryDropdownProvider>();
				this._dropdownItemsSetter.SetLocalizableItems(dropdown, manufactoryDropdownProvider);
				this._tooltipRegistrar.Register(dropdown, () => this.GetTooltipText(manufactoryDropdownProvider));
				return new ManufactoryBatchControlRowItem(visualElement, dropdown, component);
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021EC File Offset: 0x000003EC
		public string GetTooltipText(ManufactoryDropdownProvider manufactoryDropdownProvider)
		{
			string str = this._loc.T(ManufactoryBatchControlRowItemFactory.CurrentRecipeLocKey);
			string str2 = this._loc.T(manufactoryDropdownProvider.GetValue());
			return str + " " + str2;
		}

		// Token: 0x04000009 RID: 9
		public static readonly string CurrentRecipeLocKey = "Manufactory.CurrentRecipe";

		// Token: 0x0400000A RID: 10
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000B RID: 11
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000C RID: 12
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;
	}
}
