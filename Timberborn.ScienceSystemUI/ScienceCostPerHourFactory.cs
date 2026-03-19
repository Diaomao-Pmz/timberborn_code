using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.ScienceSystemUI
{
	// Token: 0x02000007 RID: 7
	public class ScienceCostPerHourFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021F9 File Offset: 0x000003F9
		public ScienceCostPerHourFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002208 File Offset: 0x00000408
		public ScienceCostPerHour Create()
		{
			string elementName = "Game/EntityPanel/ScienceCostPerHour";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Label scienceCostValue = UQueryExtensions.Q<Label>(visualElement, "ScienceCostValue", null);
			return new ScienceCostPerHour(visualElement, scienceCostValue);
		}

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;
	}
}
