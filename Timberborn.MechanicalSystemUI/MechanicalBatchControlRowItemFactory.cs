using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000011 RID: 17
	public class MechanicalBatchControlRowItemFactory
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000026B8 File Offset: 0x000008B8
		public MechanicalBatchControlRowItemFactory(VisualElementLoader visualElementLoader, ILoc loc, MechanicalNodeTextFormatter mechanicalNodeTextFormatter)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._mechanicalNodeTextFormatter = mechanicalNodeTextFormatter;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026D8 File Offset: 0x000008D8
		public IBatchControlRowItem Create(BaseComponent entity)
		{
			MechanicalNode component = entity.GetComponent<MechanicalNode>();
			if (component != null && component.Enabled)
			{
				string elementName = "Game/BatchControl/MechanicalBatchControlRowItem";
				VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
				Label label = UQueryExtensions.Q<Label>(visualElement, "MechanicalBatchControlRowItem", null);
				return new MechanicalBatchControlRowItem(this._mechanicalNodeTextFormatter, visualElement, label, component);
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002728 File Offset: 0x00000928
		public IBatchControlRowItem Create(MechanicalGraph mechanicalGraph)
		{
			string elementName = "Game/BatchControl/MechanicalHeaderBatchControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Label label = UQueryExtensions.Q<Label>(visualElement, "MechanicalHeaderBatchControlRowItem", null);
			return new MechanicalHeaderBatchControlRowItem(this._loc, visualElement, label, mechanicalGraph);
		}

		// Token: 0x04000025 RID: 37
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000026 RID: 38
		public readonly ILoc _loc;

		// Token: 0x04000027 RID: 39
		public readonly MechanicalNodeTextFormatter _mechanicalNodeTextFormatter;
	}
}
