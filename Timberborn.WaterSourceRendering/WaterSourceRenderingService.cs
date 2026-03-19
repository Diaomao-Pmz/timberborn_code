using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.WaterSourceRendering
{
	// Token: 0x0200000C RID: 12
	[MapEditorTickable]
	public class WaterSourceRenderingService : ILoadableSingleton, IPostLoadableSingleton, IUnloadableSingleton, ITickableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000023D5 File Offset: 0x000005D5
		public WaterSourceRenderingService(MapIndexService mapIndexService, IThreadSafeWaterMap threadSafeWaterMap)
		{
			this._mapIndexService = mapIndexService;
			this._threadSafeWaterMap = threadSafeWaterMap;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F6 File Offset: 0x000005F6
		public void Load()
		{
			this._mapSize = this._mapIndexService.TerrainSize;
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._threadSafeWaterMap.MaxWaterColumnCountChanged += this.OnMaxColumnCountChanged;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002431 File Offset: 0x00000631
		public void PostLoad()
		{
			this._isInitialized = true;
			this.UpdateAll();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002440 File Offset: 0x00000640
		public void Unload()
		{
			this.Cleanup();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002448 File Offset: 0x00000648
		public void Tick()
		{
			if (this._fullUpdateScheduled)
			{
				this.UpdateAll();
				this._fullUpdateScheduled = false;
				return;
			}
			if (this.ComputeMaskVisibilityChanges())
			{
				this.UpdateMask();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000246E File Offset: 0x0000066E
		public void LateUpdateSingleton()
		{
			if (this._maskIsDirty)
			{
				this.ApplyMaskToTexture();
				this._maskIsDirty = false;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002485 File Offset: 0x00000685
		public void AddRenderer(WaterSourceRenderer waterSourceRenderer)
		{
			this._maskItems[waterSourceRenderer] = new RendererMaskItem(waterSourceRenderer);
			this.ScheduleFullUpdate();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000249F File Offset: 0x0000069F
		public void RemoveRenderer(WaterSourceRenderer waterSourceRenderer)
		{
			this._maskItems.Remove(waterSourceRenderer);
			this.ScheduleFullUpdate();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B4 File Offset: 0x000006B4
		public void OnMaxColumnCountChanged(object sender, int columnCount)
		{
			this.SetupMaskResources(columnCount);
			this.ScheduleFullUpdate();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C4 File Offset: 0x000006C4
		public void SetupMaskResources(int columnCount)
		{
			this.Cleanup();
			Array.Resize<byte[]>(ref this._renderersMask, columnCount);
			int num = this._mapSize.x * this._mapSize.y;
			for (int i = 0; i < columnCount; i++)
			{
				this._renderersMask[i] = new byte[num];
			}
			this._renderersMaskTexture = new Texture2DArray(this._mapSize.x, this._mapSize.y, columnCount, 63, false)
			{
				filterMode = 0,
				wrapMode = 1
			};
			Shader.SetGlobalTexture(WaterSourceRenderingService.TextureId, this._renderersMaskTexture);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002558 File Offset: 0x00000758
		public void Cleanup()
		{
			if (this._renderersMaskTexture != null)
			{
				Object.Destroy(this._renderersMaskTexture);
				this._renderersMaskTexture = null;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000257A File Offset: 0x0000077A
		public void ScheduleFullUpdate()
		{
			this._fullUpdateScheduled = true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002583 File Offset: 0x00000783
		public void UpdateAll()
		{
			this.ComputeMaskVisibilityChanges();
			this.UpdateMask();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002594 File Offset: 0x00000794
		public bool ComputeMaskVisibilityChanges()
		{
			if (this._isInitialized)
			{
				bool result = false;
				foreach (RendererMaskItem rendererMaskItem in this._maskItems.Values)
				{
					bool hasFullyVisibleWaterSurfaceAbove = this.HasFullyVisibleWaterSurfaceAbove(rendererMaskItem);
					if (rendererMaskItem.UpdateVisibility(hasFullyVisibleWaterSurfaceAbove))
					{
						result = true;
					}
				}
				return result;
			}
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002608 File Offset: 0x00000808
		public bool HasFullyVisibleWaterSurfaceAbove(RendererMaskItem maskItem)
		{
			foreach (Vector3Int vector3Int in maskItem.Coordinates)
			{
				int columnIndex = this.GetColumnIndex(vector3Int);
				int index3D = this._mapIndexService.CellToIndex(vector3Int.XY()) + columnIndex * this._verticalStride;
				float num = this._threadSafeWaterMap.WaterDepth(index3D);
				float num2 = (float)this._threadSafeWaterMap.ColumnFloor(index3D);
				byte b = this._threadSafeWaterMap.ColumnCeiling(index3D);
				if (num2 + num >= (float)b)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002694 File Offset: 0x00000894
		public int GetColumnIndex(Vector3Int coordinates)
		{
			int num = this._mapIndexService.CellToIndex(coordinates.XY());
			int num2 = this._threadSafeWaterMap.ColumnCount(num);
			for (int i = 0; i < num2; i++)
			{
				int index3D = num + i * this._verticalStride;
				if ((int)this._threadSafeWaterMap.ColumnFloor(index3D) == coordinates.z)
				{
					return i;
				}
			}
			throw new InvalidOperationException(string.Format("Column at {0} not found.", coordinates));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002704 File Offset: 0x00000904
		public void UpdateMask()
		{
			if (this._isInitialized)
			{
				this.ClearMask();
				foreach (RendererMaskItem rendererMaskItem in this._maskItems.Values)
				{
					int num = WaterSourceRenderingService.GetMaskInitialOffset(rendererMaskItem);
					foreach (Vector3Int vector3Int in rendererMaskItem.Coordinates)
					{
						int columnIndex = this.GetColumnIndex(vector3Int);
						int num2 = this._mapIndexService.CoordinatesToActualMapIndex(vector3Int.XY());
						this._renderersMask[columnIndex][num2] = (byte)(rendererMaskItem.IsVisible ? (++num) : 0);
					}
				}
				this._maskIsDirty = true;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027D4 File Offset: 0x000009D4
		public void ClearMask()
		{
			int num = this._renderersMask.Length;
			for (int i = 0; i < num; i++)
			{
				byte[] array = this._renderersMask[i];
				Array.Clear(array, 0, array.Length);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000280C File Offset: 0x00000A0C
		public void ApplyMaskToTexture()
		{
			for (int i = 0; i < this._renderersMask.Length; i++)
			{
				this._renderersMaskTexture.SetPixelData<byte>(this._renderersMask[i], 0, i, 0);
			}
			this._renderersMaskTexture.Apply(false, false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002850 File Offset: 0x00000A50
		public static int GetMaskInitialOffset(RendererMaskItem maskItem)
		{
			int length = maskItem.Coordinates.Length;
			int result;
			if (length != 4)
			{
				if (length != 9)
				{
					throw new NotSupportedException(string.Format("Source with {0} tiles is not supported.", length));
				}
				result = WaterSourceRenderingService.ThreeByThreeUVOffset;
			}
			else
			{
				result = WaterSourceRenderingService.TwoByTwoUVOffset;
			}
			return result;
		}

		// Token: 0x04000014 RID: 20
		public static readonly int TextureId = Shader.PropertyToID("_WaterSourceMask");

		// Token: 0x04000015 RID: 21
		public static readonly int TwoByTwoUVOffset = 0;

		// Token: 0x04000016 RID: 22
		public static readonly int ThreeByThreeUVOffset = 4;

		// Token: 0x04000017 RID: 23
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000018 RID: 24
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000019 RID: 25
		public readonly Dictionary<WaterSourceRenderer, RendererMaskItem> _maskItems = new Dictionary<WaterSourceRenderer, RendererMaskItem>();

		// Token: 0x0400001A RID: 26
		public byte[][] _renderersMask;

		// Token: 0x0400001B RID: 27
		public Texture2DArray _renderersMaskTexture;

		// Token: 0x0400001C RID: 28
		public Vector3Int _mapSize;

		// Token: 0x0400001D RID: 29
		public int _verticalStride;

		// Token: 0x0400001E RID: 30
		public bool _isInitialized;

		// Token: 0x0400001F RID: 31
		public bool _maskIsDirty;

		// Token: 0x04000020 RID: 32
		public bool _fullUpdateScheduled;
	}
}
