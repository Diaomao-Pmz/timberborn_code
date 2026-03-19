using System;
using System.Diagnostics;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200001B RID: 27
	[MapEditorTickable]
	public class WaterRenderer : IWaterRenderer, ILoadableSingleton, IPostLoadableSingleton, IUnloadableSingleton, ITickableSingleton, IParallelTickableSingleton
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004F00 File Offset: 0x00003100
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004F08 File Offset: 0x00003108
		public long UpdateMeshTime { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004F11 File Offset: 0x00003111
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004F19 File Offset: 0x00003119
		public long UpdateTexturesTime { get; private set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00004F24 File Offset: 0x00003124
		public WaterRenderer(MapIndexService mapIndexService, IThreadSafeWaterMap threadSafeWaterMap, WaterColumnPostprocessor waterColumnPostprocessor, IWaterMesh waterMesh, IFlowLimiterService flowLimiterService, WaterRenderingTaskStarter waterRenderingTaskStarter, WaterFlowLimitUpdater waterFlowLimitUpdater)
		{
			this._mapIndexService = mapIndexService;
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._waterColumnPostprocessor = waterColumnPostprocessor;
			this._waterMesh = waterMesh;
			this._flowLimiterService = flowLimiterService;
			this._waterRenderingTaskStarter = waterRenderingTaskStarter;
			this._waterFlowLimitUpdater = waterFlowLimitUpdater;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004FA4 File Offset: 0x000031A4
		public void Load()
		{
			this._mapSize = this._mapIndexService.TerrainSize.XY();
			this._threadSafeWaterMap.MaxWaterColumnCountChanged += delegate(object _, int _)
			{
				this._shouldResize = true;
			};
			this._tileCount = WorldTiling.TileCount2D(this._mapSize.x, this._mapSize.y);
			this._tilesWithWater = new bool[this._tileCount.x * this._tileCount.y];
			this.CreateDataTextureArrays();
			Shader.SetGlobalVector(WaterRenderer.MapSizeProperty, new Vector2((float)this._mapSize.x, (float)this._mapSize.y));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005053 File Offset: 0x00003253
		public void PostLoad()
		{
			this.Resize();
			this._waterFlowLimitUpdater.UpdateFlowLimits();
			this.FullyUpdateWater();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000506C File Offset: 0x0000326C
		public void Unload()
		{
			this._waterDepths.Cleanup();
			this._outflows.Cleanup();
			this._contaminations.Cleanup();
			this._columns.Cleanup();
			this._linkBarriers.Cleanup();
			this._flowLimits.Cleanup();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000050BB File Offset: 0x000032BB
		public void Tick()
		{
			if (this._shouldResize)
			{
				this.Resize();
				this.FullyUpdateWater();
				this._shouldResize = false;
			}
			this._waterFlowLimitUpdater.UpdateFlowLimits();
			this.UpdateMeshAndTextures();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000050E9 File Offset: 0x000032E9
		public void StartParallelTick()
		{
			this.StartWaterRenderingTask(false);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000050F2 File Offset: 0x000032F2
		public void EnableMeshUpdate()
		{
			this._updateMeshes = true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000050FB File Offset: 0x000032FB
		public void DisableMeshUpdate()
		{
			this._updateMeshes = false;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005104 File Offset: 0x00003304
		public void DisableTextureUpdate()
		{
			this._updateTextures = false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000510D File Offset: 0x0000330D
		public void EnableTextureUpdate()
		{
			this._updateTextures = true;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005116 File Offset: 0x00003316
		public void DisablePostprocessing()
		{
			this._postprocessEnabled = false;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000511F File Offset: 0x0000331F
		public void EnablePostprocessing()
		{
			this._postprocessEnabled = true;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005128 File Offset: 0x00003328
		public void Resize()
		{
			int maxColumnCount = this._threadSafeWaterMap.MaxColumnCount;
			this._columnCountTracker.Update(maxColumnCount);
			this._waterDepths.Resize(maxColumnCount);
			this._outflows.Resize(maxColumnCount);
			this._contaminations.Resize(maxColumnCount);
			this._columns.Resize(maxColumnCount);
			this._linkBarriers.Resize(maxColumnCount);
			this._flowLimits.Resize(maxColumnCount);
			this._waterColumnPostprocessor.Resize(maxColumnCount);
			this._waterFlowLimitUpdater.Resize(maxColumnCount);
			Array.Resize<bool>(ref this._tilesWithWater, this._tileCount.x * this._tileCount.y * maxColumnCount);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000051D1 File Offset: 0x000033D1
		public void FullyUpdateWater()
		{
			this.StartWaterRenderingTask(true);
			this.StartWaterRenderingTask(true);
			this.UpdateMeshAndTextures();
			this.UpdateMeshAndTextures();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000051F0 File Offset: 0x000033F0
		public void CreateDataTextureArrays()
		{
			this._waterDepths = DataTextureArray<float>.Create(18, this._mapSize);
			this._outflows = DataTextureArray<Vector2>.Create(19, this._mapSize);
			this._contaminations = DataTextureArray<byte>.Create(63, this._mapSize);
			this._columns = DataTextureArray<Vector2>.Create(19, this._mapSize);
			this._linkBarriers = DataTextureArray<Vector2>.Create(19, this._mapSize);
			this._flowLimits = DataTextureArray<float>.Create(18, this._mapSize);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005270 File Offset: 0x00003470
		public void UpdateMeshAndTextures()
		{
			bool flag = this._columnChangeTracker.AnyColumnChanged();
			this._stopwatch.Restart();
			if (this._updateMeshes)
			{
				this._waterMesh.DisableAllTiles();
				for (int i = 0; i < this._tilesWithWater.Length; i++)
				{
					if (this._tilesWithWater[i])
					{
						Vector3Int tileIndex = WorldTiling.TileIndex3DToCoordinates(i, this._tileCount.x, this._tileCount.y);
						this._waterMesh.EnableTile(tileIndex);
					}
				}
				this.UpdateMeshTime = this._stopwatch.ElapsedMilliseconds;
			}
			this._stopwatch.Restart();
			if (this._updateTextures)
			{
				this._waterDepths.SwapTextureArrays();
				this._outflows.SwapTextureArrays();
				this._contaminations.SwapTextureArrays();
				this._flowLimits.SwapTextureArrays();
				if (flag)
				{
					this._columns.SwapTextureArrays();
					this._linkBarriers.SwapTextureArrays();
				}
				for (int j = 0; j < this._columnCountTracker.MaxCount; j++)
				{
					this._waterDepths.UpdateTextureArrays(j);
					this._outflows.UpdateTextureArrays(j);
					this._contaminations.UpdateTextureArrays(j);
					this._flowLimits.UpdateTextureArrays(j);
					if (flag)
					{
						this._columns.UpdateTextureArrays(j);
						this._linkBarriers.UpdateTextureArrays(j);
					}
				}
			}
			this.UpdateTexturesTime = this._stopwatch.ElapsedMilliseconds;
			if (this._postprocessEnabled)
			{
				this._waterColumnPostprocessor.Postprocess(this._columnCountTracker.MaxCount, this._waterDepths, this._columns, this._outflows, this._contaminations, this._linkBarriers, this._flowLimits);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005410 File Offset: 0x00003610
		public void StartWaterRenderingTask(bool waitForResult)
		{
			WaterRenderingTaskStarter waterRenderingTaskStarter = this._waterRenderingTaskStarter;
			int maxColumnCount = this._threadSafeWaterMap.MaxColumnCount;
			bool anyColumnChanged = this._threadSafeWaterMap.AnyColumnChanged;
			ColumnChangeTracker columnChangeTracker = this._columnChangeTracker;
			ColumnCountTracker columnCountTracker = this._columnCountTracker;
			DataTextureArray<float> waterDepths = this._waterDepths;
			DataTextureArray<Vector2> outflows = this._outflows;
			DataTextureArray<byte> contaminations = this._contaminations;
			DataTextureArray<Vector2> columns = this._columns;
			DataTextureArray<Vector2> linkBarriers = this._linkBarriers;
			DataTextureArray<float> flowLimits = this._flowLimits;
			bool[] tilesWithWater = this._tilesWithWater;
			ReadOnlyJaggedArray<float> flowLimits2 = this._waterFlowLimitUpdater.FlowLimits;
			ReadOnlyArray<byte> columnCounts = this._threadSafeWaterMap.ColumnCounts;
			ReadOnlyArray<ReadOnlyWaterColumn> waterColumns = this._threadSafeWaterMap.WaterColumns;
			ReadOnlyArray<Vector2> flowDirections = this._threadSafeWaterMap.FlowDirections;
			ReadOnlyArray<int> limitedDirections = this._flowLimiterService.LimitedDirections;
			waterRenderingTaskStarter.StartTask(waitForResult, maxColumnCount, anyColumnChanged, columnChangeTracker, columnCountTracker, waterDepths, outflows, contaminations, columns, linkBarriers, flowLimits, tilesWithWater, flowLimits2, columnCounts, waterColumns, flowDirections, limitedDirections);
		}

		// Token: 0x040000A6 RID: 166
		public static readonly int MapSizeProperty = Shader.PropertyToID("_MapSize");

		// Token: 0x040000A9 RID: 169
		public readonly MapIndexService _mapIndexService;

		// Token: 0x040000AA RID: 170
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x040000AB RID: 171
		public readonly WaterColumnPostprocessor _waterColumnPostprocessor;

		// Token: 0x040000AC RID: 172
		public readonly IWaterMesh _waterMesh;

		// Token: 0x040000AD RID: 173
		public readonly IFlowLimiterService _flowLimiterService;

		// Token: 0x040000AE RID: 174
		public readonly WaterRenderingTaskStarter _waterRenderingTaskStarter;

		// Token: 0x040000AF RID: 175
		public readonly WaterFlowLimitUpdater _waterFlowLimitUpdater;

		// Token: 0x040000B0 RID: 176
		public DataTextureArray<float> _waterDepths;

		// Token: 0x040000B1 RID: 177
		public DataTextureArray<Vector2> _outflows;

		// Token: 0x040000B2 RID: 178
		public DataTextureArray<byte> _contaminations;

		// Token: 0x040000B3 RID: 179
		public DataTextureArray<Vector2> _columns;

		// Token: 0x040000B4 RID: 180
		public DataTextureArray<Vector2> _linkBarriers;

		// Token: 0x040000B5 RID: 181
		public DataTextureArray<float> _flowLimits;

		// Token: 0x040000B6 RID: 182
		public bool[] _tilesWithWater;

		// Token: 0x040000B7 RID: 183
		public readonly ColumnCountTracker _columnCountTracker = new ColumnCountTracker();

		// Token: 0x040000B8 RID: 184
		public readonly ColumnChangeTracker _columnChangeTracker = new ColumnChangeTracker();

		// Token: 0x040000B9 RID: 185
		public Vector2Int _mapSize;

		// Token: 0x040000BA RID: 186
		public Vector2Int _tileCount;

		// Token: 0x040000BB RID: 187
		public bool _updateMeshes = true;

		// Token: 0x040000BC RID: 188
		public bool _updateTextures = true;

		// Token: 0x040000BD RID: 189
		public bool _postprocessEnabled = true;

		// Token: 0x040000BE RID: 190
		public readonly Stopwatch _stopwatch = new Stopwatch();

		// Token: 0x040000BF RID: 191
		public bool _shouldResize;
	}
}
