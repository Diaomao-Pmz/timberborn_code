using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000009 RID: 9
	public class BatteryBatchControlRowItemFactory
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021F2 File Offset: 0x000003F2
		public BatteryBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002208 File Offset: 0x00000408
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			MechanicalNode component = entity.GetComponent<MechanicalNode>();
			if (component != null && component.IsBattery)
			{
				string elementName = "Game/BatchControl/BatteryBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				ProgressBar progressBar = UQueryExtensions.Q<ProgressBar>(visualElement, "ProgressBar", null);
				Label chargeLabel = UQueryExtensions.Q<Label>(visualElement, "Charge", null);
				return new BatteryBatchControlRowItem(this._loc, visualElement, progressBar, chargeLabel, component);
			}
			return null;
		}

		// Token: 0x04000010 RID: 16
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000011 RID: 17
		public readonly ILoc _loc;
	}
}
