using System;
using Timberborn.SingletonSystem;
using Timberborn.WaterSystem;

namespace Timberborn.SoilMoistureSystem
{
	// Token: 0x0200001A RID: 26
	public class WaterEvaporationMapHelper : ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public WaterEvaporationMapHelper(IThreadSafeWaterMap threadSafeWaterMap, WaterEvaporationMap waterEvaporationMap)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._waterEvaporationMap = waterEvaporationMap;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004ADE File Offset: 0x00002CDE
		public void Load()
		{
			this._threadSafeWaterMap.MaxWaterColumnCountChanged += this.OnMaxWaterColumnCountChanged;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004AF7 File Offset: 0x00002CF7
		public void PostLoad()
		{
			this._waterEvaporationMap.LoadData(this._threadSafeWaterMap.ColumnCounts);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B0F File Offset: 0x00002D0F
		public void OnMaxWaterColumnCountChanged(object sender, int maxColumnCount)
		{
			this._waterEvaporationMap.Resize(maxColumnCount);
		}

		// Token: 0x0400008E RID: 142
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400008F RID: 143
		public readonly WaterEvaporationMap _waterEvaporationMap;
	}
}
