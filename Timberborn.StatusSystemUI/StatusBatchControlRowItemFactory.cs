using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.StatusSystem;
using Timberborn.TooltipSystem;

namespace Timberborn.StatusSystemUI
{
	// Token: 0x0200000C RID: 12
	public class StatusBatchControlRowItemFactory
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002928 File Offset: 0x00000B28
		public StatusBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002940 File Offset: 0x00000B40
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			StatusSubject component = entity.GetComponent<StatusSubject>();
			if (component != null)
			{
				string elementName = "Game/BatchControl/StatusBatchControlRowItem";
				StatusBatchControlRowItem statusBatchControlRowItem = new StatusBatchControlRowItem(this._visualElementLoader.LoadVisualElement(elementName), component, this._visualElementLoader, this._tooltipRegistrar);
				statusBatchControlRowItem.Initialize();
				return statusBatchControlRowItem;
			}
			return null;
		}

		// Token: 0x0400002E RID: 46
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002F RID: 47
		public readonly ITooltipRegistrar _tooltipRegistrar;
	}
}
