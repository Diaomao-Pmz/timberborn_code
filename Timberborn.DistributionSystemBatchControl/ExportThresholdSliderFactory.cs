using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x0200000F RID: 15
	public class ExportThresholdSliderFactory
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002B3F File Offset: 0x00000D3F
		public ExportThresholdSliderFactory(ILoc loc, TooltipBlocker tooltipBlocker, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._tooltipBlocker = tooltipBlocker;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B5C File Offset: 0x00000D5C
		public ExportThresholdSlider Create(Slider slider, GoodDistributionSetting goodDistributionSetting)
		{
			VisualElement tooltip = this.CreateExportThresholdSliderTooltip(slider);
			ExportThresholdSlider exportThresholdSlider = new ExportThresholdSlider(this._loc, this._tooltipBlocker, goodDistributionSetting, slider, tooltip);
			exportThresholdSlider.Initialize();
			return exportThresholdSlider;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B8C File Offset: 0x00000D8C
		public VisualElement CreateExportThresholdSliderTooltip(Slider slider)
		{
			string elementName = "Game/BatchControl/ExportThresholdSliderTooltip";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<VisualElement>(slider, "unity-dragger", null).Add(visualElement);
			visualElement.ToggleDisplayStyle(false);
			return visualElement;
		}

		// Token: 0x04000030 RID: 48
		public readonly ILoc _loc;

		// Token: 0x04000031 RID: 49
		public readonly TooltipBlocker _tooltipBlocker;

		// Token: 0x04000032 RID: 50
		public readonly VisualElementLoader _visualElementLoader;
	}
}
