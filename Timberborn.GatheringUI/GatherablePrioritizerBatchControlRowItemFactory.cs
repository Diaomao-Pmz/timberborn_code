using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.Gathering;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.GatheringUI
{
	// Token: 0x02000006 RID: 6
	public class GatherablePrioritizerBatchControlRowItemFactory
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002459 File Offset: 0x00000659
		public GatherablePrioritizerBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, DropdownItemsSetter dropdownItemsSetter, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._loc = loc;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002480 File Offset: 0x00000680
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			GatherablePrioritizerDropdownProvider dropdownProvider = entity.GetComponent<GatherablePrioritizerDropdownProvider>();
			if (dropdownProvider != null && dropdownProvider.HasMultipleOptions)
			{
				string elementName = "Game/BatchControl/DropdownBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Dropdown dropdown = UQueryExtensions.Q<Dropdown>(visualElement, "Dropdown", null);
				this._dropdownItemsSetter.SetItems(dropdown, dropdownProvider);
				this._tooltipRegistrar.Register(dropdown, () => this.GetTooltipText(dropdownProvider));
				GatherablePrioritizer component = entity.GetComponent<GatherablePrioritizer>();
				return new GatherablePrioritizerBatchControlRowItem(visualElement, dropdown, component);
			}
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002514 File Offset: 0x00000714
		public string GetTooltipText(GatherablePrioritizerDropdownProvider dropdownProvider)
		{
			string str = this._loc.T(GatherablePrioritizerBatchControlRowItemFactory.GatheringPrioritizeLocKey);
			string value = dropdownProvider.GetValue();
			return str + " " + value;
		}

		// Token: 0x0400001B RID: 27
		public static readonly string GatheringPrioritizeLocKey = "Gathering.Prioritize";

		// Token: 0x0400001C RID: 28
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001D RID: 29
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400001E RID: 30
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;
	}
}
