using System;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Multithreading;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200001C RID: 28
	public class WaterRenderingTaskStarter : ILoadableSingleton
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000054D6 File Offset: 0x000036D6
		public WaterRenderingTaskStarter(IParallelizer parallelizer, MapIndexService mapIndexService)
		{
			this._parallelizer = parallelizer;
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000054EC File Offset: 0x000036EC
		public void Load()
		{
			this._stride = this._mapIndexService.Stride;
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._mapSize = this._mapIndexService.TerrainSize;
			this._tileCount = WorldTiling.TileCount2D(this._mapSize.x, this._mapSize.y);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005550 File Offset: 0x00003750
		public void StartTask(bool runSynchronously, int maxColumnCount, bool anyColumnChanged, ColumnChangeTracker columnChangeTracker, ColumnCountTracker columnCountTracker, DataTextureArray<float> waterDepths, DataTextureArray<Vector2> outflows, DataTextureArray<byte> contaminations, DataTextureArray<Vector2> columns, DataTextureArray<Vector2> linkBarriers, DataTextureArray<float> flowLimits, bool[] tilesWithWater, in ReadOnlyJaggedArray<float> flowLimitsBuffer, in ReadOnlyArray<byte> columnCounts, in ReadOnlyArray<ReadOnlyWaterColumn> waterColumns, in ReadOnlyArray<Vector2> flowDirections, in ReadOnlyArray<int> limitedDirections)
		{
			SwapWaterTexturesTask swapWaterTexturesTask = new SwapWaterTexturesTask(maxColumnCount, anyColumnChanged, columnChangeTracker, columnCountTracker, waterDepths, outflows, contaminations, columns, linkBarriers, flowLimits, tilesWithWater);
			UpdateWaterTexturesTask updateWaterTexturesTask = new UpdateWaterTexturesTask(columnChangeTracker, this._stride, this._verticalStride, this._mapSize, this._tileCount, waterDepths, outflows, contaminations, columns, linkBarriers, flowLimits, tilesWithWater, ref columnCounts, ref waterColumns, ref flowDirections, ref limitedDirections, ref flowLimitsBuffer);
			if (runSynchronously)
			{
				swapWaterTexturesTask.Run();
				for (int i = 0; i < this._mapSize.y; i++)
				{
					updateWaterTexturesTask.Run(i);
				}
				return;
			}
			this._parallelizer.Schedule<SwapWaterTexturesTask>(swapWaterTexturesTask).ContinueWith<UpdateWaterTexturesTask>(0, this._mapSize.y, 5, updateWaterTexturesTask);
		}

		// Token: 0x040000C0 RID: 192
		public readonly IParallelizer _parallelizer;

		// Token: 0x040000C1 RID: 193
		public readonly MapIndexService _mapIndexService;

		// Token: 0x040000C2 RID: 194
		public int _stride;

		// Token: 0x040000C3 RID: 195
		public int _verticalStride;

		// Token: 0x040000C4 RID: 196
		public Vector3Int _mapSize;

		// Token: 0x040000C5 RID: 197
		public Vector2Int _tileCount;
	}
}
