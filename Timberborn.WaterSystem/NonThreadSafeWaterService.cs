using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001A RID: 26
	public class NonThreadSafeWaterService : INonThreadSafeWaterService
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00002C83 File Offset: 0x00000E83
		public NonThreadSafeWaterService(WaterSimulator waterSimulator, ThreadSafeWaterMap threadSafeWaterMap)
		{
			this._waterSimulator = waterSimulator;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void UpdateOutflowsData()
		{
			ReadOnlySpan<ColumnOutflows> outflows = this._waterSimulator.Outflows;
			Array.Resize<ColumnOutflows>(ref this._nonThreadSafeOutflows, outflows.Length);
			outflows.CopyTo(this._nonThreadSafeOutflows);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public unsafe ReadOnlyWaterColumn GetColumnByIndex(int index3D)
		{
			return *this._threadSafeWaterMap.WaterColumns[index3D];
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D0C File Offset: 0x00000F0C
		public ReadOnlyColumnOutflows ColumnOutflows(int index3D)
		{
			ref ColumnOutflows ptr = ref this._nonThreadSafeOutflows[index3D];
			return new ReadOnlyColumnOutflows(ptr.BottomFlow, ptr.LeftFlow, ptr.TopFlow, ptr.RightFlow, ptr.Outflows);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D4C File Offset: 0x00000F4C
		public unsafe int GetColumnCount(int index)
		{
			return (int)(*this._threadSafeWaterMap.ColumnCounts[index]);
		}

		// Token: 0x04000031 RID: 49
		public readonly WaterSimulator _waterSimulator;

		// Token: 0x04000032 RID: 50
		public readonly ThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000033 RID: 51
		public ColumnOutflows[] _nonThreadSafeOutflows = Array.Empty<ColumnOutflows>();
	}
}
