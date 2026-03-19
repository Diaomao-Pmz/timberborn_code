using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Multithreading;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001B RID: 27
	public readonly struct OutflowsUpdateTask : IParallelizerLoopTask
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002D70 File Offset: 0x00000F70
		public OutflowsUpdateTask(FlowLimitCalculator flowLimitCalculator, WaterFlowRetriever waterFlowRetriever, List<DirectedFlow>[] directedFlows, WaterFlow[] baseLevelFlows, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyArray<WaterColumn> waterColumns, ReadOnlyArray<int> limitedDirections, ReadOnlyArray<float> limitedValues, ReadOnlyArray<sbyte> flowControllers, ReadOnlyArray<float> outflowLimits, ReadOnlyArray<ColumnOutflows> outflows, int xMapSize, int stride, int verticalStride, float deltaTime, float overflowPressureFactor, float maxHardDamDecrease, float hardDamOffset, float softDamOffset, float waterSpillThreshold, float waterFlowFactor, float flowChangeLimit)
		{
			this._flowLimitCalculator = flowLimitCalculator;
			this._waterFlowRetriever = waterFlowRetriever;
			this._directedFlows = directedFlows;
			this._baseLevelFlows = baseLevelFlows;
			this._waterColumnCounts = waterColumnCounts;
			this._waterColumns = waterColumns;
			this._limitedDirections = limitedDirections;
			this._limitedValues = limitedValues;
			this._flowControllers = flowControllers;
			this._outflowLimits = outflowLimits;
			this._outflows = outflows;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._deltaTime = deltaTime;
			this._overflowPressureFactor = overflowPressureFactor;
			this._maxHardDamDecrease = maxHardDamDecrease;
			this._hardDamOffset = hardDamOffset;
			this._softDamOffset = softDamOffset;
			this._waterSpillThreshold = waterSpillThreshold;
			this._waterFlowFactor = waterFlowFactor;
			this._flowChangeLimit = flowChangeLimit;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E2C File Offset: 0x0000102C
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				this._directedFlows[num2].Clear();
				byte b = *this._waterColumnCounts[num2];
				for (int j = 0; j < (int)b; j++)
				{
					int index3D = num2 + j * this._verticalStride;
					this.UpdateOutflows(num2, index3D);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E9C File Offset: 0x0000109C
		public void UpdateOutflows(int index, int index3D)
		{
			ref readonly WaterColumn ptr = ref this._waterColumns[index3D];
			float num = ptr.WaterDepth + ptr.Overflow;
			bool flag = index == index3D;
			if (num == 0f)
			{
				return;
			}
			ref WaterFlow ptr2 = ref this._baseLevelFlows[index];
			List<DirectedFlow> list = this._directedFlows[index];
			int count = list.Count;
			float num2 = 0f;
			this.Outflow(ptr, index, index3D, -this._stride, ref ptr2, ref num2);
			this.Outflow(ptr, index, index3D, -1, ref ptr2, ref num2);
			this.Outflow(ptr, index, index3D, this._stride, ref ptr2, ref num2);
			this.Outflow(ptr, index, index3D, 1, ref ptr2, ref num2);
			if (num2 == 0f)
			{
				return;
			}
			float num3 = num / (num2 * this._deltaTime);
			if (num3 < 1f)
			{
				if (flag)
				{
					ptr2.Bottom *= num3;
					ptr2.Left *= num3;
					ptr2.Top *= num3;
					ptr2.Right *= num3;
				}
				for (int i = count; i < list.Count; i++)
				{
					list[i] = list[i].MultiplyFlow(num3);
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002FC0 File Offset: 0x000011C0
		public unsafe void Outflow(in WaterColumn waterColumn, int index, int index3D, int targetOffset, ref WaterFlow baseLevelFlow, ref float sumOfOutflows)
		{
			int num = index + targetOffset;
			for (int i = 0; i < (int)(*this._waterColumnCounts[num]); i++)
			{
				int num2 = num + i * this._verticalStride;
				ref readonly WaterColumn ptr = ref this._waterColumns[num2];
				if (ptr.Ceiling > waterColumn.Floor)
				{
					if ((float)ptr.Floor >= (float)waterColumn.Floor + waterColumn.WaterDepth)
					{
						break;
					}
					float num3 = this.GetOutflow(waterColumn, index, index3D, ptr, num, num2);
					if (num3 > 0f)
					{
						float outflowLimit = this._flowLimitCalculator.GetOutflowLimit(this._outflowLimits, index, waterColumn.Floor);
						if (num3 > outflowLimit)
						{
							num3 = outflowLimit;
						}
						if (index == index3D && num == num2)
						{
							if (targetOffset == -this._stride)
							{
								baseLevelFlow.Bottom = num3;
							}
							else if (targetOffset == -1)
							{
								baseLevelFlow.Left = num3;
							}
							else if (targetOffset == this._stride)
							{
								baseLevelFlow.Top = num3;
							}
							else if (targetOffset == 1)
							{
								baseLevelFlow.Right = num3;
							}
						}
						else
						{
							this._directedFlows[index].Add(new DirectedFlow(num3, num2, index3D));
						}
						sumOfOutflows += num3;
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000030E8 File Offset: 0x000012E8
		public float GetOutflow(in WaterColumn originColumn, int origin, int index3D, in WaterColumn targetColumn, int target, int targetIndex3D)
		{
			int direction = target - origin;
			byte floor = originColumn.Floor;
			byte floor2 = targetColumn.Floor;
			if (!this._flowLimitCalculator.CanInflowInDirection(this._limitedDirections, target, floor2, direction) || !this._flowLimitCalculator.CanOutflowInDirection(this._limitedDirections, origin, floor, direction))
			{
				return 0f;
			}
			float overflowPressureFactor = this._overflowPressureFactor;
			float num = (float)floor + originColumn.WaterDepth;
			int waterHeight = (int)Math.Ceiling((double)num);
			float num2 = originColumn.Overflow * overflowPressureFactor;
			float num3 = (float)floor2 + targetColumn.WaterDepth;
			float num4 = targetColumn.Overflow * overflowPressureFactor;
			byte waterBase = (floor > floor2) ? floor : floor2;
			float num5 = num + num2 - (num3 + num4);
			float num10;
			if (num5 > 0f)
			{
				float num6 = (float)targetColumn.Ceiling - num3;
				float num7 = num5 - num6;
				float num8 = (num5 > num2) ? num2 : num5;
				float num9 = (num8 > num7) ? num8 : num7;
				num10 = num5 - num9 + num9 / overflowPressureFactor;
			}
			else
			{
				float num11 = (float)originColumn.Ceiling - num;
				float num12 = -num5;
				float num13 = num12 - num11;
				float num14 = (num12 < num4) ? num12 : num4;
				float num15 = (num14 > num13) ? num14 : num13;
				num10 = num5 + num15 - num15 / overflowPressureFactor;
			}
			ref readonly ColumnOutflows outflows = ref this._outflows[index3D];
			float num16 = this._waterFlowRetriever.GetFlow(targetIndex3D, outflows) * 0.999f;
			float inflowLimit = this._flowLimitCalculator.GetInflowLimit(this._limitedValues, target, (int)waterBase, waterHeight);
			if (inflowLimit >= 0f)
			{
				float num17 = num + num2 - (float)floor2;
				if (num17 < inflowLimit)
				{
					float maxHardDamDecrease = this._maxHardDamDecrease;
					float num18 = inflowLimit - this._hardDamOffset;
					float num19 = 1f - Mathf.InverseLerp(num18, inflowLimit, num17);
					float num20 = (num17 < num18) ? maxHardDamDecrease : Mathf.Lerp(0f, maxHardDamDecrease, num19);
					return num16 - num20;
				}
				float num21 = inflowLimit + this._softDamOffset;
				if (num17 < num21 && num10 > 0f)
				{
					float num22 = num21 - inflowLimit;
					float num23 = (num17 - inflowLimit) / num22;
					num10 *= num23;
				}
			}
			else if (targetColumn.WaterDepth + targetColumn.Overflow == 0f && floor == floor2)
			{
				num10 -= this._waterSpillThreshold;
			}
			float num24 = this._waterFlowFactor * num10;
			bool flag;
			if (!this._flowLimitCalculator.HasFlowController(this._flowControllers, target, (int)floor2, out flag))
			{
				return num16 + num24;
			}
			float flowChangeLimit = this._flowChangeLimit;
			if (flag)
			{
				float num25 = (num24 > flowChangeLimit) ? flowChangeLimit : num24;
				return num16 + num25;
			}
			return num16 - flowChangeLimit;
		}

		// Token: 0x04000034 RID: 52
		public readonly FlowLimitCalculator _flowLimitCalculator;

		// Token: 0x04000035 RID: 53
		public readonly WaterFlowRetriever _waterFlowRetriever;

		// Token: 0x04000036 RID: 54
		public readonly List<DirectedFlow>[] _directedFlows;

		// Token: 0x04000037 RID: 55
		public readonly WaterFlow[] _baseLevelFlows;

		// Token: 0x04000038 RID: 56
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x04000039 RID: 57
		public readonly ReadOnlyArray<WaterColumn> _waterColumns;

		// Token: 0x0400003A RID: 58
		public readonly ReadOnlyArray<int> _limitedDirections;

		// Token: 0x0400003B RID: 59
		public readonly ReadOnlyArray<float> _limitedValues;

		// Token: 0x0400003C RID: 60
		public readonly ReadOnlyArray<sbyte> _flowControllers;

		// Token: 0x0400003D RID: 61
		public readonly ReadOnlyArray<float> _outflowLimits;

		// Token: 0x0400003E RID: 62
		public readonly ReadOnlyArray<ColumnOutflows> _outflows;

		// Token: 0x0400003F RID: 63
		public readonly int _xMapSize;

		// Token: 0x04000040 RID: 64
		public readonly int _stride;

		// Token: 0x04000041 RID: 65
		public readonly int _verticalStride;

		// Token: 0x04000042 RID: 66
		public readonly float _deltaTime;

		// Token: 0x04000043 RID: 67
		public readonly float _overflowPressureFactor;

		// Token: 0x04000044 RID: 68
		public readonly float _maxHardDamDecrease;

		// Token: 0x04000045 RID: 69
		public readonly float _hardDamOffset;

		// Token: 0x04000046 RID: 70
		public readonly float _softDamOffset;

		// Token: 0x04000047 RID: 71
		public readonly float _waterSpillThreshold;

		// Token: 0x04000048 RID: 72
		public readonly float _waterFlowFactor;

		// Token: 0x04000049 RID: 73
		public readonly float _flowChangeLimit;
	}
}
