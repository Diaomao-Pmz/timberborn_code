using System;
using Timberborn.Goods;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000015 RID: 21
	public class GoodDistributionSetting
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000AB RID: 171 RVA: 0x00003EC4 File Offset: 0x000020C4
		// (remove) Token: 0x060000AC RID: 172 RVA: 0x00003EFC File Offset: 0x000020FC
		public event EventHandler SettingChanged;

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003F31 File Offset: 0x00002131
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003F39 File Offset: 0x00002139
		public float ExportThreshold { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003F42 File Offset: 0x00002142
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003F4A File Offset: 0x0000214A
		public ImportOption ImportOption { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F53 File Offset: 0x00002153
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003F5B File Offset: 0x0000215B
		public float LastImportTimestamp { get; set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F64 File Offset: 0x00002164
		public GoodDistributionSetting(GoodSpec goodSpec)
		{
			this._goodSpec = goodSpec;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003F73 File Offset: 0x00002173
		public static GoodDistributionSetting CreateDefault(GoodSpec goodSpec)
		{
			GoodDistributionSetting goodDistributionSetting = new GoodDistributionSetting(goodSpec);
			goodDistributionSetting.SetDefault();
			return goodDistributionSetting;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F81 File Offset: 0x00002181
		public static GoodDistributionSetting Create(GoodSpec goodSpec, float exportThreshold, ImportOption importOption, float lastImportTimestamp)
		{
			return new GoodDistributionSetting(goodSpec)
			{
				ExportThreshold = exportThreshold,
				ImportOption = importOption,
				LastImportTimestamp = lastImportTimestamp
			};
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003F9E File Offset: 0x0000219E
		public string GoodId
		{
			get
			{
				return this._goodSpec.Id;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FAB File Offset: 0x000021AB
		public void SetDefault()
		{
			this.ExportThreshold = 0f;
			this.ImportOption = (this._goodSpec.ForceImport ? ImportOption.Forced : ImportOption.Auto);
			EventHandler settingChanged = this.SettingChanged;
			if (settingChanged == null)
			{
				return;
			}
			settingChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003FE5 File Offset: 0x000021E5
		public void SetImportOption(ImportOption importOption)
		{
			this.ImportOption = importOption;
			EventHandler settingChanged = this.SettingChanged;
			if (settingChanged == null)
			{
				return;
			}
			settingChanged(this, EventArgs.Empty);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004004 File Offset: 0x00002204
		public void SetExportThreshold(float exportThreshold)
		{
			this.ExportThreshold = exportThreshold;
			EventHandler settingChanged = this.SettingChanged;
			if (settingChanged == null)
			{
				return;
			}
			settingChanged(this, EventArgs.Empty);
		}

		// Token: 0x04000042 RID: 66
		public readonly GoodSpec _goodSpec;
	}
}
