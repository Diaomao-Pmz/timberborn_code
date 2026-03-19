using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.Localization;
using Timberborn.Planting;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000011 RID: 17
	public class PlantablePrioritizerBatchControlRowItemFactory
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002CDE File Offset: 0x00000EDE
		public PlantablePrioritizerBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D04 File Offset: 0x00000F04
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			PlantablePrioritizerDropdownProvider dropdownProvider = entity.GetComponent<PlantablePrioritizerDropdownProvider>();
			if (dropdownProvider != null && dropdownProvider.HasMultipleOptions)
			{
				string elementName = "Game/BatchControl/DropdownBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Dropdown dropdown = UQueryExtensions.Q<Dropdown>(visualElement, "Dropdown", null);
				this._dropdownItemsSetter.SetLocalizableItems(dropdown, dropdownProvider);
				this._tooltipRegistrar.Register(dropdown, () => this.GetTooltipText(dropdownProvider));
				PlantablePrioritizer component = entity.GetComponent<PlantablePrioritizer>();
				return new PlantablePrioritizerBatchControlRowItem(visualElement, dropdown, component);
			}
			return null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D98 File Offset: 0x00000F98
		public string GetTooltipText(PlantablePrioritizerDropdownProvider dropdownProvider)
		{
			string str = this._loc.T(PlantablePrioritizerBatchControlRowItemFactory.PlantingPrioritizeLocKey);
			string str2 = this._loc.T(dropdownProvider.GetValue());
			return str + " " + str2;
		}

		// Token: 0x04000033 RID: 51
		public static readonly string PlantingPrioritizeLocKey = "Planting.Prioritize";

		// Token: 0x04000034 RID: 52
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000035 RID: 53
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000036 RID: 54
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000037 RID: 55
		public readonly ILoc _loc;
	}
}
