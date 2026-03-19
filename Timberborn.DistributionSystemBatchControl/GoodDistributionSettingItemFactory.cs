using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.DistributionSystemUI;
using Timberborn.SliderToggleSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000011 RID: 17
	public class GoodDistributionSettingItemFactory
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002C7B File Offset: 0x00000E7B
		public GoodDistributionSettingItemFactory(ExportThresholdSliderFactory exportThresholdSliderFactory, ImportGoodIconFactory importGoodIconFactory, ImportToggleFactory importToggleFactory, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._exportThresholdSliderFactory = exportThresholdSliderFactory;
			this._importGoodIconFactory = importGoodIconFactory;
			this._importToggleFactory = importToggleFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public GoodDistributionSettingItem Create(DistrictDistributableGoodProvider districtDistributableGoodProvider, GoodDistributionSetting goodDistributionSetting)
		{
			string elementName = "Game/BatchControl/GoodDistributionSettingItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(visualElement, "ImportGoodIconWrapper", null);
			ImportGoodIcon importGoodIcon = this._importGoodIconFactory.CreateImportGoodIcon(parent, goodDistributionSetting.GoodId);
			importGoodIcon.SetDistrictDistributableGoodProvider(districtDistributableGoodProvider);
			Slider slider = UQueryExtensions.Q<Slider>(visualElement, "ExportThresholdSlider", null);
			ExportThresholdSlider exportThresholdSlider = this._exportThresholdSliderFactory.Create(slider, goodDistributionSetting);
			VisualElement parent2 = UQueryExtensions.Q<VisualElement>(visualElement, "ImportToggleWrapper", null);
			SliderToggle importToggle = this._importToggleFactory.Create(parent2, goodDistributionSetting);
			ProgressBar progressBar = UQueryExtensions.Q<ProgressBar>(visualElement, "FillRateProgressBar", null);
			this._tooltipRegistrar.RegisterUpdatable(progressBar, () => GoodDistributionSettingItemFactory.GetFillRateTooltip(districtDistributableGoodProvider, goodDistributionSetting));
			return new GoodDistributionSettingItem(visualElement, districtDistributableGoodProvider, goodDistributionSetting, importGoodIcon, exportThresholdSlider, importToggle, progressBar);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D94 File Offset: 0x00000F94
		public static string GetFillRateTooltip(DistrictDistributableGoodProvider districtDistributableGoodProvider, GoodDistributionSetting setting)
		{
			DistributableGood distributableGoodForExport = districtDistributableGoodProvider.GetDistributableGoodForExport(setting.GoodId);
			string arg = NumberFormatter.FormatAsPercentRounded((double)distributableGoodForExport.FillRate);
			return string.Format("{0}/{1} ({2})", distributableGoodForExport.Stock, distributableGoodForExport.Capacity, arg);
		}

		// Token: 0x0400003A RID: 58
		public readonly ExportThresholdSliderFactory _exportThresholdSliderFactory;

		// Token: 0x0400003B RID: 59
		public readonly ImportGoodIconFactory _importGoodIconFactory;

		// Token: 0x0400003C RID: 60
		public readonly ImportToggleFactory _importToggleFactory;

		// Token: 0x0400003D RID: 61
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400003E RID: 62
		public readonly VisualElementLoader _visualElementLoader;
	}
}
