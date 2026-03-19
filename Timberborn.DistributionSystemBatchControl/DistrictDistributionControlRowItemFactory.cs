using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x0200000C RID: 12
	public class DistrictDistributionControlRowItemFactory
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002622 File Offset: 0x00000822
		public DistrictDistributionControlRowItemFactory(ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002638 File Offset: 0x00000838
		public IBatchControlRowItem Create(DistrictDistributionSetting districtDistributionSetting)
		{
			string elementName = "Game/BatchControl/DistrictDistributionControlRowItem";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Button button = UQueryExtensions.Q<Button>(visualElement, "Reset", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.ResetToDefault();
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button, DistrictDistributionControlRowItemFactory.ResetLocKey);
			Button button2 = UQueryExtensions.Q<Button>(visualElement, "ExportAll", null);
			button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.SetDistrictExportThreshold(0);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button2, DistrictDistributionControlRowItemFactory.ExportAllLocKey);
			Button button3 = UQueryExtensions.Q<Button>(visualElement, "ExportNone", null);
			button3.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.SetDistrictExportThreshold(1);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button3, DistrictDistributionControlRowItemFactory.ExportNoneLocKey);
			Button button4 = UQueryExtensions.Q<Button>(visualElement, "ImportDisabledAll", null);
			button4.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.SetDistrictImportOption(ImportOption.Disabled);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button4, DistrictDistributionControlRowItemFactory.ImportDisabledAllLocKey);
			Button button5 = UQueryExtensions.Q<Button>(visualElement, "ImportAutoAll", null);
			button5.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.SetDistrictImportOption(ImportOption.Auto);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button5, DistrictDistributionControlRowItemFactory.ImportAutoAllLocKey);
			Button button6 = UQueryExtensions.Q<Button>(visualElement, "ImportForcedAll", null);
			button6.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				districtDistributionSetting.SetDistrictImportOption(ImportOption.Forced);
			}, 0);
			this._tooltipRegistrar.RegisterLocalizable(button6, DistrictDistributionControlRowItemFactory.ImportForcedAllLocKey);
			return new EmptyBatchControlRowItem(visualElement);
		}

		// Token: 0x0400001A RID: 26
		public static readonly string ResetLocKey = "Distribution.Reset";

		// Token: 0x0400001B RID: 27
		public static readonly string ExportAllLocKey = "Distribution.ExportAll";

		// Token: 0x0400001C RID: 28
		public static readonly string ExportNoneLocKey = "Distribution.ExportNone";

		// Token: 0x0400001D RID: 29
		public static readonly string ImportAutoAllLocKey = "Distribution.ImportAutoAll";

		// Token: 0x0400001E RID: 30
		public static readonly string ImportDisabledAllLocKey = "Distribution.ImportDisabledAll";

		// Token: 0x0400001F RID: 31
		public static readonly string ImportForcedAllLocKey = "Distribution.ImportForcedAll";

		// Token: 0x04000020 RID: 32
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000021 RID: 33
		public readonly VisualElementLoader _visualElementLoader;
	}
}
