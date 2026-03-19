using System;
using Timberborn.BlueprintSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200002B RID: 43
	public class WaterDepthSetter : ILoadableSingleton
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004850 File Offset: 0x00002A50
		public WaterDepthSetter(MapIndexService mapIndexService, ISpecService specService)
		{
			this._mapIndexService = mapIndexService;
			this._specService = specService;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004868 File Offset: 0x00002A68
		public void Load()
		{
			this._maxPressure = (float)(this._mapIndexService.TotalSize.z + 1);
			WaterSimulatorSpec singleSpec = this._specService.GetSingleSpec<WaterSimulatorSpec>();
			this._overflowPressureFactorInverted = 1f / singleSpec.OverflowPressureFactor;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000048B0 File Offset: 0x00002AB0
		public void SetWaterDepth(float waterDepthChange, ref WaterColumn waterColumn)
		{
			float num = waterColumn.WaterDepth + waterColumn.Overflow + waterDepthChange;
			int num2 = (int)(waterColumn.Ceiling - waterColumn.Floor);
			if (num < 0f)
			{
				waterColumn.WaterDepth = 0f;
				waterColumn.Overflow = 0f;
				return;
			}
			if (num > (float)num2)
			{
				waterColumn.WaterDepth = (float)num2;
				float num3 = num - (float)num2;
				float num4 = (this._maxPressure - (float)waterColumn.Ceiling) * this._overflowPressureFactorInverted;
				waterColumn.Overflow = ((num3 > num4) ? num4 : num3);
				return;
			}
			waterColumn.WaterDepth = num;
			waterColumn.Overflow = 0f;
		}

		// Token: 0x040000A4 RID: 164
		public readonly MapIndexService _mapIndexService;

		// Token: 0x040000A5 RID: 165
		public readonly ISpecService _specService;

		// Token: 0x040000A6 RID: 166
		public float _maxPressure;

		// Token: 0x040000A7 RID: 167
		public float _overflowPressureFactorInverted;
	}
}
