using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.MapEditorTickSystem;
using Timberborn.MapIndexSystem;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000017 RID: 23
	[MapEditorTickable]
	public class ThreadSafeColumnTerrainMap : ILoadableSingleton, ITickableSingleton, IThreadSafeColumnTerrainMap
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000BE RID: 190 RVA: 0x00003FFC File Offset: 0x000021FC
		// (remove) Token: 0x060000BF RID: 191 RVA: 0x00004034 File Offset: 0x00002234
		public event EventHandler<int> ColumnMovedUp;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000C0 RID: 192 RVA: 0x0000406C File Offset: 0x0000226C
		// (remove) Token: 0x060000C1 RID: 193 RVA: 0x000040A4 File Offset: 0x000022A4
		public event EventHandler<int> ColumnMovedDown;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000C2 RID: 194 RVA: 0x000040DC File Offset: 0x000022DC
		// (remove) Token: 0x060000C3 RID: 195 RVA: 0x00004114 File Offset: 0x00002314
		public event EventHandler<int> ColumnReset;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000C4 RID: 196 RVA: 0x0000414C File Offset: 0x0000234C
		// (remove) Token: 0x060000C5 RID: 197 RVA: 0x00004184 File Offset: 0x00002384
		public event EventHandler<int> MaxTerrainColumnCountChanged;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000041B9 File Offset: 0x000023B9
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000041C1 File Offset: 0x000023C1
		public int MaxColumnCount { get; private set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x000041CA File Offset: 0x000023CA
		public ThreadSafeColumnTerrainMap(ColumnTerrainMap columnTerrainMap, MapIndexService mapIndexService, ITickableSingletonService tickableSingletonService)
		{
			this._columnTerrainMap = columnTerrainMap;
			this._mapIndexService = mapIndexService;
			this._tickableSingletonService = tickableSingletonService;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000041F2 File Offset: 0x000023F2
		public ReadOnlyArray<byte> ColumnCounts
		{
			get
			{
				return new ReadOnlyArray<byte>(this._columnCounts);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000041FF File Offset: 0x000023FF
		public ReadOnlyArray<ReadOnlyTerrainColumn> TerrainColumns
		{
			get
			{
				return new ReadOnlyArray<ReadOnlyTerrainColumn>(this._terrainColumns);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000420C File Offset: 0x0000240C
		public void Load()
		{
			this._verticalStride = this._mapIndexService.VerticalStride;
			this._columnCounts = new byte[this._mapIndexService.MaxIndex];
			this._terrainColumns = new ReadOnlyTerrainColumn[this._verticalStride * this._columnTerrainMap.MaxColumnCount];
			this.MaxColumnCount = this._columnTerrainMap.MaxColumnCount;
			this.UpdateData();
			this._columnTerrainMap.ColumnAdded += this.OnColumnAdded;
			this._columnTerrainMap.ColumnRemoved += this.OnColumnRemoved;
			this._tickableSingletonService.ForcedParallelTickFinished += delegate(object _, EventArgs _)
			{
				this.UpdateDataAndNotify();
			};
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000042B9 File Offset: 0x000024B9
		public void Tick()
		{
			this.UpdateDataAndNotify();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042C1 File Offset: 0x000024C1
		public int GetColumnCount(int index2D)
		{
			return (int)this._columnCounts[index2D];
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042CB File Offset: 0x000024CB
		public int GetColumnCeiling(int index3D)
		{
			return this._terrainColumns[index3D].Ceiling;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042DE File Offset: 0x000024DE
		public int GetColumnFloor(int index3D)
		{
			return this._terrainColumns[index3D].Floor;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042F4 File Offset: 0x000024F4
		public bool TryGetIndexAtCeiling(int index2D, int ceiling, out int index3D)
		{
			byte b = this._columnCounts[index2D];
			for (int i = 0; i < (int)b; i++)
			{
				index3D = i * this._verticalStride + index2D;
				if (this._terrainColumns[index3D].Ceiling == ceiling)
				{
					return true;
				}
			}
			index3D = 0;
			return false;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004340 File Offset: 0x00002540
		public bool TryGetIndexAtOrAboveCeiling(int index2D, int ceiling, out int index3D)
		{
			byte b = this._columnCounts[index2D];
			for (int i = 0; i < (int)b; i++)
			{
				index3D = i * this._verticalStride + index2D;
				if (this._terrainColumns[index3D].Ceiling >= ceiling)
				{
					return true;
				}
			}
			index3D = -1;
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004389 File Offset: 0x00002589
		public void OnColumnAdded(object sender, ColumnAddedEventArgs e)
		{
			this._columnChanges.Enqueue(new ThreadSafeColumnTerrainMap.ColumnChange(true, e.Index, e.ColumnCount));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000043AA File Offset: 0x000025AA
		public void OnColumnRemoved(object sender, ColumnRemovedEventArgs e)
		{
			this._columnChanges.Enqueue(new ThreadSafeColumnTerrainMap.ColumnChange(false, e.Index, e.ColumnCount));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000043CB File Offset: 0x000025CB
		public void UpdateDataAndNotify()
		{
			this.UpdateData();
			this.PostEvents();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000043DC File Offset: 0x000025DC
		public void UpdateData()
		{
			if (this._columnTerrainMap.AnyColumnChanged)
			{
				int maxColumnCount = this.MaxColumnCount;
				this.MaxColumnCount = this._columnTerrainMap.MaxColumnCount;
				if (maxColumnCount < this.MaxColumnCount)
				{
					Array.Resize<ReadOnlyTerrainColumn>(ref this._terrainColumns, this._verticalStride * this.MaxColumnCount);
				}
				this._columnTerrainMap.CopyTerrainColumnsData(this._terrainColumns, this._columnCounts, Math.Max(maxColumnCount, this.MaxColumnCount));
				if (maxColumnCount != this.MaxColumnCount)
				{
					EventHandler<int> maxTerrainColumnCountChanged = this.MaxTerrainColumnCountChanged;
					if (maxTerrainColumnCountChanged == null)
					{
						return;
					}
					maxTerrainColumnCountChanged(this, this.MaxColumnCount);
				}
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004474 File Offset: 0x00002674
		public void PostEvents()
		{
			ThreadSafeColumnTerrainMap.ColumnChange columnChange;
			while (this._columnChanges.TryDequeue(ref columnChange))
			{
				int num = columnChange.Index % this._verticalStride;
				int num2 = columnChange.Index / this._verticalStride;
				int columnCount = columnChange.ColumnCount;
				if (columnChange.Added)
				{
					for (int i = columnCount - 1; i > num2; i--)
					{
						EventHandler<int> columnMovedUp = this.ColumnMovedUp;
						if (columnMovedUp != null)
						{
							columnMovedUp(this, i * this._verticalStride + num);
						}
					}
					EventHandler<int> columnReset = this.ColumnReset;
					if (columnReset != null)
					{
						columnReset(this, num2 * this._verticalStride + num);
					}
				}
				else
				{
					for (int j = num2; j < columnCount; j++)
					{
						EventHandler<int> columnMovedDown = this.ColumnMovedDown;
						if (columnMovedDown != null)
						{
							columnMovedDown(this, j * this._verticalStride + num);
						}
					}
					EventHandler<int> columnReset2 = this.ColumnReset;
					if (columnReset2 != null)
					{
						columnReset2(this, columnCount * this._verticalStride + num);
					}
				}
			}
		}

		// Token: 0x04000047 RID: 71
		public readonly ColumnTerrainMap _columnTerrainMap;

		// Token: 0x04000048 RID: 72
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000049 RID: 73
		public readonly ITickableSingletonService _tickableSingletonService;

		// Token: 0x0400004A RID: 74
		public int _verticalStride;

		// Token: 0x0400004B RID: 75
		public byte[] _columnCounts;

		// Token: 0x0400004C RID: 76
		public ReadOnlyTerrainColumn[] _terrainColumns;

		// Token: 0x0400004D RID: 77
		public readonly Queue<ThreadSafeColumnTerrainMap.ColumnChange> _columnChanges = new Queue<ThreadSafeColumnTerrainMap.ColumnChange>();

		// Token: 0x02000018 RID: 24
		public readonly struct ColumnChange
		{
			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004563 File Offset: 0x00002763
			public bool Added { get; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000456B File Offset: 0x0000276B
			public int Index { get; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00004573 File Offset: 0x00002773
			public int ColumnCount { get; }

			// Token: 0x060000DB RID: 219 RVA: 0x0000457B File Offset: 0x0000277B
			public ColumnChange(bool added, int index, int columnCount)
			{
				this.Added = added;
				this.Index = index;
				this.ColumnCount = columnCount;
			}
		}
	}
}
