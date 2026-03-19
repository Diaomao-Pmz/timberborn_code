using System;
using Timberborn.CoreUI;
using Timberborn.DistributionSystem;
using Timberborn.DistributionSystemUI;
using Timberborn.SliderToggleSystem;
using UnityEngine.UIElements;

namespace Timberborn.DistributionSystemBatchControl
{
	// Token: 0x02000010 RID: 16
	public class GoodDistributionSettingItem
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public VisualElement Root { get; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002BCE File Offset: 0x00000DCE
		public GoodDistributionSettingItem(VisualElement root, DistrictDistributableGoodProvider districtDistributableGoodProvider, GoodDistributionSetting setting, ImportGoodIcon importGoodIcon, ExportThresholdSlider exportThresholdSlider, SliderToggle importToggle, ProgressBar fillRateProgressBar)
		{
			this.Root = root;
			this._districtDistributableGoodProvider = districtDistributableGoodProvider;
			this._setting = setting;
			this._importGoodIcon = importGoodIcon;
			this._exportThresholdSlider = exportThresholdSlider;
			this._importToggle = importToggle;
			this._fillRateProgressBar = fillRateProgressBar;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void Update()
		{
			this._importGoodIcon.Update();
			this._exportThresholdSlider.Update();
			this._importToggle.Update();
			DistributableGood distributableGoodForExport = this._districtDistributableGoodProvider.GetDistributableGoodForExport(this._setting.GoodId);
			this._fillRateProgressBar.SetProgress(distributableGoodForExport.FillRate);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C63 File Offset: 0x00000E63
		public void Clear()
		{
			this._importGoodIcon.Clear();
			this._exportThresholdSlider.Clear();
		}

		// Token: 0x04000034 RID: 52
		public readonly DistrictDistributableGoodProvider _districtDistributableGoodProvider;

		// Token: 0x04000035 RID: 53
		public readonly GoodDistributionSetting _setting;

		// Token: 0x04000036 RID: 54
		public readonly ImportGoodIcon _importGoodIcon;

		// Token: 0x04000037 RID: 55
		public readonly ExportThresholdSlider _exportThresholdSlider;

		// Token: 0x04000038 RID: 56
		public readonly SliderToggle _importToggle;

		// Token: 0x04000039 RID: 57
		public readonly ProgressBar _fillRateProgressBar;
	}
}
