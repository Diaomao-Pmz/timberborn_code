using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.Localization;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000013 RID: 19
	public class ImportToggleFactory
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public ImportToggleFactory(ILoc loc, SliderToggleFactory sliderToggleFactory, VisualElementLoader visualElementLoader)
		{
			this._loc = loc;
			this._sliderToggleFactory = sliderToggleFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E10 File Offset: 0x00001010
		public SliderToggle Create(VisualElement parent, GoodDistributionSetting setting)
		{
			SliderToggleItem sliderToggleItem = SliderToggleItem.Create(new Func<VisualElement>(this.GetImportDisabledTooltip), ImportToggleFactory.ImportDisabledIconClass, ImportToggleFactory.ImportDisabledBackgroundClass, delegate()
			{
				setting.SetImportOption(ImportOption.Disabled);
			}, () => setting.ImportOption == ImportOption.Disabled);
			SliderToggleItem sliderToggleItem2 = SliderToggleItem.Create(new Func<VisualElement>(this.GetImportAutoTooltip), ImportToggleFactory.ImportAutoIconClass, ImportToggleFactory.ImportAutoBackgroundClass, delegate()
			{
				setting.SetImportOption(ImportOption.Auto);
			}, () => setting.ImportOption == ImportOption.Auto);
			SliderToggleItem sliderToggleItem3 = SliderToggleItem.Create(new Func<VisualElement>(this.GetImportForcedTooltip), ImportToggleFactory.ImportForcedIconClass, ImportToggleFactory.ImportForcedBackgroundClass, delegate()
			{
				setting.SetImportOption(ImportOption.Forced);
			}, () => setting.ImportOption == ImportOption.Forced);
			return this._sliderToggleFactory.Create(parent, new SliderToggleItem[]
			{
				sliderToggleItem,
				sliderToggleItem2,
				sliderToggleItem3
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EF0 File Offset: 0x000010F0
		public VisualElement GetImportDisabledTooltip()
		{
			return this.GetTooltip(ImportToggleFactory.ImportDisabledLocKey, ImportToggleFactory.ImportDisabledDescriptionLocKey, false);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F03 File Offset: 0x00001103
		public VisualElement GetImportAutoTooltip()
		{
			return this.GetTooltip(ImportToggleFactory.ImportAutoLocKey, ImportToggleFactory.ImportAutoDescriptionLocKey, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F16 File Offset: 0x00001116
		public VisualElement GetImportForcedTooltip()
		{
			return this.GetTooltip(ImportToggleFactory.ImportForcedLocKey, ImportToggleFactory.ImportForcedDescriptionLocKey, true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F2C File Offset: 0x0000112C
		public VisualElement GetTooltip(string title, string description, bool withBalanceInfo)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/ImportToggleTooltip");
			UQueryExtensions.Q<Label>(visualElement, "Title", null).text = this._loc.T(title);
			UQueryExtensions.Q<Label>(visualElement, "Description", null).text = (withBalanceInfo ? (this._loc.T(description) + "\n" + this._loc.T(ImportToggleFactory.BalanceInfoLocKey)) : this._loc.T(description));
			return visualElement;
		}

		// Token: 0x04000041 RID: 65
		public static readonly string ImportDisabledIconClass = "import-icon--disabled";

		// Token: 0x04000042 RID: 66
		public static readonly string ImportAutoIconClass = "import-icon--auto";

		// Token: 0x04000043 RID: 67
		public static readonly string ImportForcedIconClass = "import-icon--forced";

		// Token: 0x04000044 RID: 68
		public static readonly string ImportDisabledBackgroundClass = "import-background--disabled";

		// Token: 0x04000045 RID: 69
		public static readonly string ImportAutoBackgroundClass = "import-background--auto";

		// Token: 0x04000046 RID: 70
		public static readonly string ImportForcedBackgroundClass = "import-background--forced";

		// Token: 0x04000047 RID: 71
		public static readonly string ImportDisabledLocKey = "Distribution.ImportDisabled";

		// Token: 0x04000048 RID: 72
		public static readonly string ImportDisabledDescriptionLocKey = "Distribution.ImportDisabled.Description";

		// Token: 0x04000049 RID: 73
		public static readonly string ImportAutoLocKey = "Distribution.ImportAuto";

		// Token: 0x0400004A RID: 74
		public static readonly string ImportAutoDescriptionLocKey = "Distribution.ImportAuto.Description";

		// Token: 0x0400004B RID: 75
		public static readonly string ImportForcedLocKey = "Distribution.ImportForced";

		// Token: 0x0400004C RID: 76
		public static readonly string ImportForcedDescriptionLocKey = "Distribution.ImportForced.Description";

		// Token: 0x0400004D RID: 77
		public static readonly string BalanceInfoLocKey = "Distribution.BalanceInfo";

		// Token: 0x0400004E RID: 78
		public readonly ILoc _loc;

		// Token: 0x0400004F RID: 79
		public readonly SliderToggleFactory _sliderToggleFactory;

		// Token: 0x04000050 RID: 80
		public readonly VisualElementLoader _visualElementLoader;
	}
}
