using System;
using Timberborn.Multithreading;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000D RID: 13
	public readonly struct SwapWaterTexturesTask : IParallelizerSingleTask
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000253C File Offset: 0x0000073C
		public SwapWaterTexturesTask(int maxColumnCount, bool anyColumnChanged, ColumnChangeTracker columnChangeTracker, ColumnCountTracker columnCountTracker, DataTextureArray<float> waterDepths, DataTextureArray<Vector2> outflows, DataTextureArray<byte> contaminations, DataTextureArray<Vector2> columns, DataTextureArray<Vector2> linkBarriers, DataTextureArray<float> flowLimits, bool[] tilesWithWater)
		{
			this._maxColumnCount = maxColumnCount;
			this._anyColumnChanged = anyColumnChanged;
			this._columnChangeTracker = columnChangeTracker;
			this._columnCountTracker = columnCountTracker;
			this._waterDepths = waterDepths;
			this._outflows = outflows;
			this._contaminations = contaminations;
			this._columns = columns;
			this._linkBarriers = linkBarriers;
			this._flowLimits = flowLimits;
			this._tilesWithWater = tilesWithWater;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000025A0 File Offset: 0x000007A0
		public void Run()
		{
			Array.Clear(this._tilesWithWater, 0, this._tilesWithWater.Length);
			this._columnCountTracker.Update(this._maxColumnCount);
			this._columnChangeTracker.Update(this._anyColumnChanged);
			this._outflows.SwapDataAndClear(this._columnCountTracker.MaxCount);
			this._contaminations.SwapDataAndClear(this._columnCountTracker.MaxCount);
			this._waterDepths.SwapDataAndClear(this._columnCountTracker.MaxCount);
			this._flowLimits.SwapDataAndClear(this._columnCountTracker.MaxCount);
			if (this._columnChangeTracker.AnyColumnChanged())
			{
				this._columns.SwapDataAndClear(this._columnCountTracker.MaxCount);
				this._linkBarriers.SwapDataAndClear(this._columnCountTracker.MaxCount);
			}
		}

		// Token: 0x04000014 RID: 20
		public readonly int _maxColumnCount;

		// Token: 0x04000015 RID: 21
		public readonly bool _anyColumnChanged;

		// Token: 0x04000016 RID: 22
		public readonly ColumnChangeTracker _columnChangeTracker;

		// Token: 0x04000017 RID: 23
		public readonly ColumnCountTracker _columnCountTracker;

		// Token: 0x04000018 RID: 24
		public readonly DataTextureArray<float> _waterDepths;

		// Token: 0x04000019 RID: 25
		public readonly DataTextureArray<Vector2> _outflows;

		// Token: 0x0400001A RID: 26
		public readonly DataTextureArray<byte> _contaminations;

		// Token: 0x0400001B RID: 27
		public readonly DataTextureArray<Vector2> _columns;

		// Token: 0x0400001C RID: 28
		public readonly DataTextureArray<Vector2> _linkBarriers;

		// Token: 0x0400001D RID: 29
		public readonly DataTextureArray<float> _flowLimits;

		// Token: 0x0400001E RID: 30
		public readonly bool[] _tilesWithWater;
	}
}
