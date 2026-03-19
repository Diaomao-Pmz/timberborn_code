using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000013 RID: 19
	public interface INonThreadSafeWaterService
	{
		// Token: 0x06000042 RID: 66
		void UpdateOutflowsData();

		// Token: 0x06000043 RID: 67
		ReadOnlyWaterColumn GetColumnByIndex(int index3D);

		// Token: 0x06000044 RID: 68
		ReadOnlyColumnOutflows ColumnOutflows(int index3D);

		// Token: 0x06000045 RID: 69
		int GetColumnCount(int index);
	}
}
