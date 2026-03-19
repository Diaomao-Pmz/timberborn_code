using System;
using Timberborn.Common;
using Timberborn.Multithreading;
using Timberborn.TerrainSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000E RID: 14
	public readonly struct UpdateWaterTexturesTask : IParallelizerLoopTask
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002674 File Offset: 0x00000874
		public UpdateWaterTexturesTask(ColumnChangeTracker columnChangeTracker, int stride, int verticalStride, Vector3Int mapSize, Vector2Int tileCount, DataTextureArray<float> waterDepths, DataTextureArray<Vector2> outflows, DataTextureArray<byte> contaminations, DataTextureArray<Vector2> columns, DataTextureArray<Vector2> linkBarriers, DataTextureArray<float> flowLimits, bool[] tilesWithWater, in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, in ReadOnlyArray<Vector2> flowDirections, in ReadOnlyArray<int> limitedDirections, in ReadOnlyJaggedArray<float> flowLimitsBuffer)
		{
			this._columnChangeTracker = columnChangeTracker;
			this._stride = stride;
			this._verticalStride = verticalStride;
			this._mapSize = mapSize;
			this._tileCount = tileCount;
			this._waterDepths = waterDepths;
			this._outflows = outflows;
			this._contaminations = contaminations;
			this._columns = columns;
			this._linkBarriers = linkBarriers;
			this._flowLimits = flowLimits;
			this._tilesWithWater = tilesWithWater;
			this._columnCounts = columnCounts;
			this._waterColumns = waterColumns;
			this._flowDirections = flowDirections;
			this._limitedDirections = limitedDirections;
			this._flowLimitsBuffer = flowLimitsBuffer;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002720 File Offset: 0x00000920
		public unsafe void Run(int y)
		{
			bool flag = this._columnChangeTracker.AnyColumnChanged();
			int num = (y + 1) * this._stride;
			for (int i = 0; i < this._mapSize.x; i++)
			{
				int num2 = num + i + 1;
				byte b = *this._columnCounts[num2];
				int num3 = i + y * this._mapSize.x;
				for (int j = 0; j < (int)b; j++)
				{
					int index = num2 + j * this._verticalStride;
					ref readonly ReadOnlyWaterColumn ptr = ref this._waterColumns[index];
					float waterDepth = ptr.WaterDepth;
					this._waterDepths.NewData[j][num3] = waterDepth;
					byte b2 = (byte)(255f * ptr.Contamination);
					this._contaminations.NewData[j][num3] = b2;
					float num4 = this._waterDepths.OldData[j][num3];
					if (waterDepth > 0f)
					{
						Vector2Int vector2Int = WorldTiling.CoordinatesToTileIndex2D(i, y);
						int num5 = vector2Int.x + vector2Int.y * this._tileCount.x + j * this._tileCount.x * this._tileCount.y;
						this._tilesWithWater[num5] = true;
						if (num4 <= 0f)
						{
							float num6 = waterDepth - 0.5f;
							if (num6 < 0f)
							{
								num6 = 0f;
							}
							this._waterDepths.OldData[j][num3] = num6;
							this._contaminations.OldData[j][num3] = b2;
						}
					}
					else if (num4 > 0f)
					{
						this._contaminations.NewData[j][num3] = this._contaminations.OldData[j][num3];
					}
					ref readonly Vector2 ptr2 = ref this._flowDirections[index];
					Vector2[] array = this._outflows.NewData[j];
					int num7 = num3;
					array[num7].x = ptr2.x / UpdateWaterTexturesTask.MaxOutflow;
					array[num7].y = ptr2.y / UpdateWaterTexturesTask.MaxOutflow;
					this._flowLimits.NewData[j][num3] = *this._flowLimitsBuffer.Get(j, num3);
					if (flag)
					{
						ref Vector2 ptr3 = ref this._columns.NewData[j][num3];
						byte floor = ptr.Floor;
						ptr3.x = (float)floor;
						ptr3.y = (float)ptr.Ceiling;
						int num8 = num2 % this._stride;
						int index2 = num2 / this._stride * this._stride + num8 + (int)floor * this._verticalStride;
						ref Vector2 ptr4 = ref this._linkBarriers.NewData[j][num3];
						ptr4 = new Vector2((float)(this.CanOutflowTopOrBottom(this._limitedDirections, index2) ? 0 : 1), (float)(UpdateWaterTexturesTask.CanOutflowLeftOrRight(this._limitedDirections, index2) ? 0 : 1));
						Vector2 vector = this._columns.OldData[j][num3];
						if (ptr3.x != vector.x || ptr3.y != vector.y)
						{
							this._columns.OldData[j][num3] = ptr3;
							this._waterDepths.OldData[j][num3] = waterDepth;
							this._linkBarriers.OldData[j][num3] = ptr4;
						}
					}
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A78 File Offset: 0x00000C78
		public unsafe static bool CanOutflowLeftOrRight(ReadOnlyArray<int> limitDirection, int index)
		{
			int num = *limitDirection[index];
			return num == 0 || num == 1 || num == -1;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A9C File Offset: 0x00000C9C
		public unsafe bool CanOutflowTopOrBottom(ReadOnlyArray<int> limitDirection, int index)
		{
			int num = *limitDirection[index];
			return num == 0 || num == this._stride || num == -this._stride;
		}

		// Token: 0x0400001F RID: 31
		public static readonly float MaxOutflow = 14f;

		// Token: 0x04000020 RID: 32
		public readonly ColumnChangeTracker _columnChangeTracker;

		// Token: 0x04000021 RID: 33
		public readonly int _stride;

		// Token: 0x04000022 RID: 34
		public readonly int _verticalStride;

		// Token: 0x04000023 RID: 35
		public readonly Vector3Int _mapSize;

		// Token: 0x04000024 RID: 36
		public readonly Vector2Int _tileCount;

		// Token: 0x04000025 RID: 37
		public readonly DataTextureArray<float> _waterDepths;

		// Token: 0x04000026 RID: 38
		public readonly DataTextureArray<Vector2> _outflows;

		// Token: 0x04000027 RID: 39
		public readonly DataTextureArray<byte> _contaminations;

		// Token: 0x04000028 RID: 40
		public readonly DataTextureArray<Vector2> _columns;

		// Token: 0x04000029 RID: 41
		public readonly DataTextureArray<Vector2> _linkBarriers;

		// Token: 0x0400002A RID: 42
		public readonly DataTextureArray<float> _flowLimits;

		// Token: 0x0400002B RID: 43
		public readonly bool[] _tilesWithWater;

		// Token: 0x0400002C RID: 44
		public readonly ReadOnlyArray<byte> _columnCounts;

		// Token: 0x0400002D RID: 45
		public readonly ReadOnlyArray<ReadOnlyWaterColumn> _waterColumns;

		// Token: 0x0400002E RID: 46
		public readonly ReadOnlyArray<Vector2> _flowDirections;

		// Token: 0x0400002F RID: 47
		public readonly ReadOnlyArray<int> _limitedDirections;

		// Token: 0x04000030 RID: 48
		public readonly ReadOnlyJaggedArray<float> _flowLimitsBuffer;
	}
}
