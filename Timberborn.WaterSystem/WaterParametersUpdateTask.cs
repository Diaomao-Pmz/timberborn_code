using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.Multithreading;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000033 RID: 51
	public readonly struct WaterParametersUpdateTask : IParallelizerLoopTask
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004E68 File Offset: 0x00003068
		public WaterParametersUpdateTask(WaterDepthSetter waterDepthSetter, WaterColumn[] waterColumns, ColumnOutflows[] columnOutflows, ReadOnlyArray<byte> waterColumnCounts, ImmutableArray<int> neighborOffsets, ReadOnlyArray<WaterFlow> baseLevelFlows, ReadOnlyArray<float> evaporationModifiers, ReadOnlyArray<List<DirectedFlow>> directedFlows, int xMapSize, int stride, int verticalStride, float deltaTime, float outflowBalancingScaler, float fastEvaporationDepthThreshold, float fastEvaporationSpeed, float normalEvaporationSpeed)
		{
			this._waterDepthSetter = waterDepthSetter;
			this._waterColumns = waterColumns;
			this._columnOutflows = columnOutflows;
			this._waterColumnCounts = waterColumnCounts;
			this._neighborOffsets = neighborOffsets;
			this._baseLevelFlows = baseLevelFlows;
			this._evaporationModifiers = evaporationModifiers;
			this._directedFlows = directedFlows;
			this._xMapSize = xMapSize;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._deltaTime = deltaTime;
			this._outflowBalancingScaler = outflowBalancingScaler;
			this._fastEvaporationDepthThreshold = fastEvaporationDepthThreshold;
			this._fastEvaporationSpeed = fastEvaporationSpeed;
			this._normalEvaporationSpeed = normalEvaporationSpeed;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004EF4 File Offset: 0x000030F4
		public unsafe void Run(int y)
		{
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._xMapSize; i++)
			{
				int num2 = i + 1 + num;
				for (int j = 0; j < (int)(*this._waterColumnCounts[num2]); j++)
				{
					int num3 = num2 + j * this._verticalStride;
					ref WaterColumn waterColumn = ref this._waterColumns[num3];
					float waterDepthChange = this.ProcessWaterDepthChanges(waterColumn, num2, num3);
					this._waterDepthSetter.SetWaterDepth(waterDepthChange, ref waterColumn);
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004F74 File Offset: 0x00003174
		public unsafe float ProcessWaterDepthChanges(in WaterColumn waterColumn, int index, int index3D)
		{
			int num = index - this._stride;
			int num2 = index - 1;
			int num3 = index + this._stride;
			int num4 = index + 1;
			bool flag = index == index3D;
			float num5 = 0f;
			float outflowBalancingScaler = this._outflowBalancingScaler;
			ref ColumnOutflows ptr = ref this._columnOutflows[index3D];
			ptr.BottomFlow.Flow = 0f;
			ptr.BottomFlow.Index3D = -1;
			ptr.LeftFlow.Flow = 0f;
			ptr.LeftFlow.Index3D = -1;
			ptr.TopFlow.Flow = 0f;
			ptr.TopFlow.Index3D = -1;
			ptr.RightFlow.Flow = 0f;
			ptr.RightFlow.Index3D = -1;
			List<TargetedFlow> outflows = ptr.Outflows;
			if (outflows != null)
			{
				outflows.Clear();
			}
			if (flag)
			{
				ref readonly WaterFlow ptr2 = ref this._baseLevelFlows[index];
				float bottom = ptr2.Bottom;
				float tempBaseLevelFlow = this.GetTempBaseLevelFlow(num, index3D);
				num5 += tempBaseLevelFlow - bottom;
				if (bottom > 0f)
				{
					float num6 = bottom - tempBaseLevelFlow * outflowBalancingScaler;
					if (num6 > 0f)
					{
						ptr.BottomFlow.Flow = num6;
						ptr.BottomFlow.Index3D = num;
					}
				}
				float left = ptr2.Left;
				float tempBaseLevelFlow2 = this.GetTempBaseLevelFlow(num2, index3D);
				num5 += tempBaseLevelFlow2 - left;
				if (left > 0f)
				{
					float num7 = left - tempBaseLevelFlow2 * outflowBalancingScaler;
					if (num7 > 0f)
					{
						ptr.LeftFlow.Flow = num7;
						ptr.LeftFlow.Index3D = num2;
					}
				}
				float top = ptr2.Top;
				float tempBaseLevelFlow3 = this.GetTempBaseLevelFlow(num3, index3D);
				num5 += tempBaseLevelFlow3 - top;
				if (top > 0f)
				{
					float num8 = top - tempBaseLevelFlow3 * outflowBalancingScaler;
					if (num8 > 0f)
					{
						ptr.TopFlow.Flow = num8;
						ptr.TopFlow.Index3D = num3;
					}
				}
				float right = ptr2.Right;
				float tempBaseLevelFlow4 = this.GetTempBaseLevelFlow(num4, index3D);
				num5 += tempBaseLevelFlow4 - right;
				if (right > 0f)
				{
					float num9 = right - tempBaseLevelFlow4 * outflowBalancingScaler;
					if (num9 > 0f)
					{
						ptr.RightFlow.Flow = num9;
						ptr.RightFlow.Index3D = num4;
					}
				}
			}
			for (int i = 0; i < this._neighborOffsets.Length; i++)
			{
				int num10 = this._neighborOffsets[i];
				int num11 = index + num10;
				for (int j = flag ? 1 : 0; j < (int)(*this._waterColumnCounts[num11]); j++)
				{
					int num12 = num11 + j * this._verticalStride;
					float tempFlow = this.GetTempFlow(index, index3D, num12);
					float tempFlow2 = this.GetTempFlow(num11, num12, index3D);
					num5 += tempFlow2 - tempFlow;
					float num13 = tempFlow - tempFlow2 * outflowBalancingScaler;
					if (num13 > 0f)
					{
						if (num10 == -this._stride)
						{
							if (ptr.BottomFlow.Index3D == -1)
							{
								ptr.BottomFlow.Flow = num13;
								ptr.BottomFlow.Index3D = num12;
							}
							else if (ptr.BottomFlow.Index3D == num12)
							{
								ref ColumnOutflows ptr3 = ref ptr;
								ptr3.BottomFlow.Flow = ptr3.BottomFlow.Flow + num13;
							}
							else
							{
								ref List<TargetedFlow> ptr4 = ref ptr.Outflows;
								if (ptr4 == null)
								{
									ptr4 = new List<TargetedFlow>();
								}
								ptr.Outflows.Add(new TargetedFlow(num13, num12));
							}
						}
						else if (num10 == -1)
						{
							if (ptr.LeftFlow.Index3D == -1)
							{
								ptr.LeftFlow.Flow = num13;
								ptr.LeftFlow.Index3D = num12;
							}
							else if (ptr.LeftFlow.Index3D == num12)
							{
								ref ColumnOutflows ptr5 = ref ptr;
								ptr5.LeftFlow.Flow = ptr5.LeftFlow.Flow + num13;
							}
							else
							{
								ref List<TargetedFlow> ptr4 = ref ptr.Outflows;
								if (ptr4 == null)
								{
									ptr4 = new List<TargetedFlow>();
								}
								ptr.Outflows.Add(new TargetedFlow(num13, num12));
							}
						}
						else if (num10 == this._stride)
						{
							if (ptr.TopFlow.Index3D == -1)
							{
								ptr.TopFlow.Flow = num13;
								ptr.TopFlow.Index3D = num12;
							}
							else if (ptr.TopFlow.Index3D == num12)
							{
								ref ColumnOutflows ptr6 = ref ptr;
								ptr6.TopFlow.Flow = ptr6.TopFlow.Flow + num13;
							}
							else
							{
								ref List<TargetedFlow> ptr4 = ref ptr.Outflows;
								if (ptr4 == null)
								{
									ptr4 = new List<TargetedFlow>();
								}
								ptr.Outflows.Add(new TargetedFlow(num13, num12));
							}
						}
						else if (num10 == 1)
						{
							if (ptr.RightFlow.Index3D == -1)
							{
								ptr.RightFlow.Flow = num13;
								ptr.RightFlow.Index3D = num12;
							}
							else if (ptr.RightFlow.Index3D == num12)
							{
								ref ColumnOutflows ptr7 = ref ptr;
								ptr7.RightFlow.Flow = ptr7.RightFlow.Flow + num13;
							}
							else
							{
								ref List<TargetedFlow> ptr4 = ref ptr.Outflows;
								if (ptr4 == null)
								{
									ptr4 = new List<TargetedFlow>();
								}
								ptr.Outflows.Add(new TargetedFlow(num13, num12));
							}
						}
					}
				}
			}
			float num14 = (waterColumn.WaterDepth < this._fastEvaporationDepthThreshold) ? this._fastEvaporationSpeed : this._normalEvaporationSpeed;
			float num15 = *this._evaporationModifiers[index3D];
			float num16 = num14 * num15;
			return (num5 - num16) * this._deltaTime;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000054C8 File Offset: 0x000036C8
		public float GetTempBaseLevelFlow(int originIndex3D, int targetIndex3D)
		{
			ref readonly WaterFlow ptr = ref this._baseLevelFlows[originIndex3D];
			int num = originIndex3D - targetIndex3D;
			if (num == -this._stride)
			{
				return ptr.Top;
			}
			if (num == -1)
			{
				return ptr.Right;
			}
			if (num == this._stride)
			{
				return ptr.Bottom;
			}
			if (num == 1)
			{
				return ptr.Left;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005524 File Offset: 0x00003724
		public unsafe float GetTempFlow(int originIndex, int originIndex3D, int targetIndex3D)
		{
			float num = 0f;
			List<DirectedFlow> list = *this._directedFlows[originIndex];
			for (int i = 0; i < list.Count; i++)
			{
				DirectedFlow directedFlow = list[i];
				if (directedFlow.OriginIndex3D == originIndex3D && directedFlow.TargetIndex3D == targetIndex3D)
				{
					num += directedFlow.Flow;
				}
			}
			return num;
		}

		// Token: 0x040000C0 RID: 192
		public readonly WaterDepthSetter _waterDepthSetter;

		// Token: 0x040000C1 RID: 193
		public readonly WaterColumn[] _waterColumns;

		// Token: 0x040000C2 RID: 194
		public readonly ColumnOutflows[] _columnOutflows;

		// Token: 0x040000C3 RID: 195
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x040000C4 RID: 196
		public readonly ImmutableArray<int> _neighborOffsets;

		// Token: 0x040000C5 RID: 197
		public readonly ReadOnlyArray<WaterFlow> _baseLevelFlows;

		// Token: 0x040000C6 RID: 198
		public readonly ReadOnlyArray<float> _evaporationModifiers;

		// Token: 0x040000C7 RID: 199
		public readonly ReadOnlyArray<List<DirectedFlow>> _directedFlows;

		// Token: 0x040000C8 RID: 200
		public readonly int _xMapSize;

		// Token: 0x040000C9 RID: 201
		public readonly int _stride;

		// Token: 0x040000CA RID: 202
		public readonly int _verticalStride;

		// Token: 0x040000CB RID: 203
		public readonly float _deltaTime;

		// Token: 0x040000CC RID: 204
		public readonly float _outflowBalancingScaler;

		// Token: 0x040000CD RID: 205
		public readonly float _fastEvaporationDepthThreshold;

		// Token: 0x040000CE RID: 206
		public readonly float _fastEvaporationSpeed;

		// Token: 0x040000CF RID: 207
		public readonly float _normalEvaporationSpeed;
	}
}
