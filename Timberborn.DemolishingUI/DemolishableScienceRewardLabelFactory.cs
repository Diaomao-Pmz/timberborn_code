using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.DemolishingUI
{
	// Token: 0x0200000B RID: 11
	public class DemolishableScienceRewardLabelFactory
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000028C1 File Offset: 0x00000AC1
		public DemolishableScienceRewardLabelFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028D0 File Offset: 0x00000AD0
		public DemolishableScienceRewardLabel Create()
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DemolishableScienceReward");
			return new DemolishableScienceRewardLabel(visualElement, UQueryExtensions.Q<Label>(visualElement, "SciencePoints", null));
		}

		// Token: 0x04000027 RID: 39
		public readonly VisualElementLoader _visualElementLoader;
	}
}
