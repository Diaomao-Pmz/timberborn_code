using System;
using Timberborn.Common;
using Timberborn.MapIndexSystem;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200000E RID: 14
	public class FlowLimitCalculator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000024F8 File Offset: 0x000006F8
		public FlowLimitCalculator(MapIndexService mapIndexService)
		{
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002508 File Offset: 0x00000708
		public unsafe bool CanInflowInDirection(ReadOnlyArray<int> limitedDirections, int index, byte waterBase, int direction)
		{
			int index2 = this._mapIndexService.Index2DTo3D(index, (int)waterBase);
			int num = *limitedDirections[index2];
			return num == 0 || num == direction;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002538 File Offset: 0x00000738
		public unsafe bool CanOutflowInDirection(ReadOnlyArray<int> limitedDirections, int index, byte waterBase, int direction)
		{
			int index2 = this._mapIndexService.Index2DTo3D(index, (int)waterBase);
			int num = *limitedDirections[index2];
			return num == 0 || num == direction || num == -direction;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002570 File Offset: 0x00000770
		public unsafe float GetInflowLimit(ReadOnlyArray<float> limitedValues, int index, int waterBase, int waterHeight)
		{
			for (int i = waterBase; i < waterHeight; i++)
			{
				int index2 = this._mapIndexService.Index2DTo3D(index, i);
				float num = *limitedValues[index2];
				if (num >= 0f)
				{
					return num;
				}
			}
			return float.MinValue;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025B4 File Offset: 0x000007B4
		public unsafe bool HasFlowController(ReadOnlyArray<sbyte> flowControllers, int index, int waterHeight, out bool flowAllowed)
		{
			int index2 = this._mapIndexService.Index2DTo3D(index, waterHeight);
			sbyte b = *flowControllers[index2];
			flowAllowed = (b == 1);
			return b != 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025E4 File Offset: 0x000007E4
		public unsafe bool HasInflowLimit(ReadOnlyArray<float> limitedValues, int index, int waterHeight)
		{
			int index2 = this._mapIndexService.Index2DTo3D(index, waterHeight);
			return *limitedValues[index2] >= 0f;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002614 File Offset: 0x00000814
		public unsafe float GetOutflowLimit(ReadOnlyArray<float> outflowLimits, int index, byte waterBase)
		{
			int index2 = this._mapIndexService.Index2DTo3D(index, (int)waterBase);
			return *outflowLimits[index2];
		}

		// Token: 0x04000021 RID: 33
		public readonly MapIndexService _mapIndexService;
	}
}
