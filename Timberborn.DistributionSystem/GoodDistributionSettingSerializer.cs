using System;
using Timberborn.Goods;
using Timberborn.Persistence;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000016 RID: 22
	public class GoodDistributionSettingSerializer : IValueSerializer<GoodDistributionSetting>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004023 File Offset: 0x00002223
		public GoodDistributionSettingSerializer(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004034 File Offset: 0x00002234
		public void Serialize(GoodDistributionSetting value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(GoodDistributionSettingSerializer.GoodIdKey, value.GoodId);
			objectSaver.Set(GoodDistributionSettingSerializer.ExportThresholdKey, value.ExportThreshold);
			objectSaver.Set<ImportOption>(GoodDistributionSettingSerializer.ImportOptionKey, value.ImportOption);
			objectSaver.Set(GoodDistributionSettingSerializer.LastImportTimestampKey, value.LastImportTimestamp);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000408C File Offset: 0x0000228C
		public Obsoletable<GoodDistributionSetting> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			string id = objectLoader.Get(GoodDistributionSettingSerializer.GoodIdKey);
			GoodSpec goodOrNull = this._goodService.GetGoodOrNull(id);
			if (goodOrNull != null)
			{
				float exportThreshold = objectLoader.Get(GoodDistributionSettingSerializer.ExportThresholdKey);
				ImportOption importOption = objectLoader.Get<ImportOption>(GoodDistributionSettingSerializer.ImportOptionKey);
				float lastImportTimestamp = objectLoader.Get(GoodDistributionSettingSerializer.LastImportTimestampKey);
				return GoodDistributionSetting.Create(goodOrNull, exportThreshold, importOption, lastImportTimestamp);
			}
			return default(Obsoletable<GoodDistributionSetting>);
		}

		// Token: 0x04000043 RID: 67
		public static readonly PropertyKey<string> GoodIdKey = new PropertyKey<string>("GoodId");

		// Token: 0x04000044 RID: 68
		public static readonly PropertyKey<float> ExportThresholdKey = new PropertyKey<float>("ExportThreshold");

		// Token: 0x04000045 RID: 69
		public static readonly PropertyKey<ImportOption> ImportOptionKey = new PropertyKey<ImportOption>("ImportOption");

		// Token: 0x04000046 RID: 70
		public static readonly PropertyKey<float> LastImportTimestampKey = new PropertyKey<float>("LastImportTimestamp");

		// Token: 0x04000047 RID: 71
		public readonly IGoodService _goodService;
	}
}
