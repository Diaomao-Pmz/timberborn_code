using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DwellingSystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.TooltipSystem;
using Timberborn.WorkSystem;
using UnityEngine.UIElements;

namespace Timberborn.BeaversUI
{
	// Token: 0x02000009 RID: 9
	public class BeaverBuildingsBatchControlRowItemFactory
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002669 File Offset: 0x00000869
		public BeaverBuildingsBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ILoc loc, ITooltipRegistrar tooltipRegistrar, EntitySelectionService entitySelectionService)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._tooltipRegistrar = tooltipRegistrar;
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002690 File Offset: 0x00000890
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			Dweller component = entity.GetComponent<Dweller>();
			Worker component2 = entity.GetComponent<Worker>();
			if (component || component2)
			{
				string elementName = "Game/BatchControl/BeaverBuildingsBatchControlRowItem";
				VisualElement root = this._visualElementLoader.LoadVisualElement(elementName);
				return this.CreateRow(root, component, component2);
			}
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026DC File Offset: 0x000008DC
		public BeaverBuildingsBatchControlRowItem CreateRow(VisualElement root, Dweller dweller, Worker worker)
		{
			BeaverBuildingsBatchControlRowItem beaverBuildingsBatchControlRowItem = new BeaverBuildingsBatchControlRowItem(root, this._tooltipRegistrar, this._loc, this._entitySelectionService, dweller, UQueryExtensions.Q<Button>(root, "HomeButton", null), UQueryExtensions.Q<Image>(root, "HomeIcon", null), worker, UQueryExtensions.Q<Button>(root, "WorkplaceButton", null), UQueryExtensions.Q<Image>(root, "WorkplaceIcon", null));
			beaverBuildingsBatchControlRowItem.Initialize();
			return beaverBuildingsBatchControlRowItem;
		}

		// Token: 0x04000022 RID: 34
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000023 RID: 35
		public readonly ILoc _loc;

		// Token: 0x04000024 RID: 36
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000025 RID: 37
		public readonly EntitySelectionService _entitySelectionService;
	}
}
