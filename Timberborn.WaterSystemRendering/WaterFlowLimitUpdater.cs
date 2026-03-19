using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.WaterObjects;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000013 RID: 19
	public class WaterFlowLimitUpdater : ILoadableSingleton
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000039E1 File Offset: 0x00001BE1
		public WaterFlowLimitUpdater(IFlowLimiterService flowLimiterService, IThreadSafeWaterMap threadSafeWaterMap, ITerrainService terrainService, MapIndexService mapIndexService, EventBus eventBus)
		{
			this._flowLimiterService = flowLimiterService;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._terrainService = terrainService;
			this._mapIndexService = mapIndexService;
			this._eventBus = eventBus;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003A19 File Offset: 0x00001C19
		public ReadOnlyJaggedArray<float> FlowLimits
		{
			get
			{
				return new ReadOnlyJaggedArray<float>(this._flowLimits);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A28 File Offset: 0x00001C28
		public void Load()
		{
			this._mapSize = this._mapIndexService.TerrainSize.XY();
			this._flowLimits = new float[][]
			{
				new float[this._mapSize.x * this._mapSize.y]
			};
			this._flowLimiterService.LimitedValueChanged += this.OnLimitedValueChanged;
			this._eventBus.Register(this);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A9C File Offset: 0x00001C9C
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			if (blockObjectSetEvent.BlockObject.HasComponent<WaterObstacle>())
			{
				Vector2Int item = blockObjectSetEvent.BlockObject.Coordinates.XY();
				this._coordinatesToUpdate.Add(item);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[OnEvent]
		public void OnBlockObjectUnset(BlockObjectUnsetEvent blockObjectUnsetEvent)
		{
			if (blockObjectUnsetEvent.BlockObject.HasComponent<WaterObstacle>())
			{
				Vector2Int item = blockObjectUnsetEvent.BlockObject.Coordinates.XY();
				this._coordinatesToUpdate.Add(item);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B0C File Offset: 0x00001D0C
		public void Resize(int maxColumnCount)
		{
			int num = this._flowLimits.Length;
			Array.Resize<float[]>(ref this._flowLimits, maxColumnCount);
			for (int i = num; i < maxColumnCount; i++)
			{
				this._flowLimits[i] = new float[this._mapSize.x * this._mapSize.y];
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003B5C File Offset: 0x00001D5C
		public void UpdateFlowLimits()
		{
			if (this._coordinatesToUpdate.Count > 0)
			{
				ReadOnlySpan<float> asSpan = this._flowLimiterService.LimitedValues.AsSpan;
				foreach (Vector2Int vector2Int in this._coordinatesToUpdate)
				{
					int index2D = this._mapIndexService.CellToIndex(vector2Int);
					int num = this._threadSafeWaterMap.ColumnCount(index2D);
					for (int i = 0; i < num; i++)
					{
						int num2 = this._mapIndexService.CoordinatesToActualMapIndex(vector2Int);
						this._flowLimits[i][num2] = this.GetFlowLimitInColumn(index2D, vector2Int, i, asSpan);
					}
				}
				this._coordinatesToUpdate.Clear();
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C2C File Offset: 0x00001E2C
		public unsafe float GetFlowLimitInColumn(int index2D, Vector2Int coordinates2D, int columnIndex, ReadOnlySpan<float> flowLimits)
		{
			int index3D = index2D + columnIndex * this._mapIndexService.VerticalStride;
			byte b = this._threadSafeWaterMap.ColumnFloor(index3D);
			byte b2 = this._threadSafeWaterMap.ColumnCeiling(index3D);
			for (byte b3 = b; b3 <= b2; b3 += 1)
			{
				Vector3Int coordinates;
				coordinates..ctor(coordinates2D.x, coordinates2D.y, (int)b3);
				int num = index2D + (int)b3 * this._mapIndexService.VerticalStride;
				float num2 = *flowLimits[num];
				if (this._terrainService.Contains(coordinates) && num2 > 0f)
				{
					return num2;
				}
			}
			return 0f;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public void OnLimitedValueChanged(object sender, int index3D)
		{
			this._coordinatesToUpdate.Add(this._mapIndexService.Index3DToCoordinates(index3D).XY());
		}

		// Token: 0x0400007B RID: 123
		public readonly IFlowLimiterService _flowLimiterService;

		// Token: 0x0400007C RID: 124
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400007D RID: 125
		public readonly ITerrainService _terrainService;

		// Token: 0x0400007E RID: 126
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400007F RID: 127
		public readonly EventBus _eventBus;

		// Token: 0x04000080 RID: 128
		public Vector2Int _mapSize;

		// Token: 0x04000081 RID: 129
		public float[][] _flowLimits;

		// Token: 0x04000082 RID: 130
		public readonly HashSet<Vector2Int> _coordinatesToUpdate = new HashSet<Vector2Int>();
	}
}
