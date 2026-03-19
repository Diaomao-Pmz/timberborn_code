using System;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Multithreading;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000025 RID: 37
	public readonly struct UpdateWaterSourcesTask : IParallelizerSingleTask
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000042A8 File Offset: 0x000024A8
		public UpdateWaterSourcesTask(MapIndexService mapIndexService, WaterDepthSetter waterDepthSetter, MutableWaterColumnRetriever mutableWaterColumnRetriever, WaterColumn[] waterColumns, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyList<ThreadSafeWaterSource> waterSources, int verticalStride, float deltaTime, float overflowPressureFactor, float maxWaterContamination)
		{
			this._mapIndexService = mapIndexService;
			this._waterDepthSetter = waterDepthSetter;
			this._mutableWaterColumnRetriever = mutableWaterColumnRetriever;
			this._waterColumns = waterColumns;
			this._waterColumnCounts = waterColumnCounts;
			this._waterSources = waterSources;
			this._verticalStride = verticalStride;
			this._deltaTime = deltaTime;
			this._overflowPressureFactor = overflowPressureFactor;
			this._maxWaterContamination = maxWaterContamination;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004304 File Offset: 0x00002504
		public void Run()
		{
			for (int i = 0; i < this._waterSources.Count; i++)
			{
				ThreadSafeWaterSource threadSafeWaterSource = this._waterSources[i];
				float waterDepthChange = this._deltaTime * threadSafeWaterSource.CurrentStrength / (float)threadSafeWaterSource.Coordinates.Length;
				for (int j = 0; j < threadSafeWaterSource.Coordinates.Length; j++)
				{
					Vector3Int value = threadSafeWaterSource.Coordinates[j];
					int index = this._mapIndexService.CellToIndex(value.XY());
					ref WaterColumn column = ref this._mutableWaterColumnRetriever.GetColumn(this._waterColumnCounts.AsSpan, this._waterColumns, this._verticalStride, index, value.z);
					float initialWaterDepth = column.WaterDepth + column.Overflow * this._overflowPressureFactor;
					this._waterDepthSetter.SetWaterDepth(waterDepthChange, ref column);
					this.UpdateContaminationFromWaterChange(ref column, initialWaterDepth, threadSafeWaterSource.Contamination);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004410 File Offset: 0x00002610
		public void UpdateContaminationFromWaterChange(ref WaterColumn waterColumn, float initialWaterDepth, float contaminationChange)
		{
			float num = waterColumn.WaterDepth + waterColumn.Overflow * this._overflowPressureFactor;
			if (num == 0f)
			{
				waterColumn.Contamination = 0f;
				return;
			}
			float num2 = contaminationChange * (num - initialWaterDepth);
			float num3 = (waterColumn.Contamination * initialWaterDepth + num2) / num;
			if (num3 < 0f)
			{
				waterColumn.Contamination = 0f;
				return;
			}
			waterColumn.Contamination = ((num3 > this._maxWaterContamination) ? this._maxWaterContamination : num3);
		}

		// Token: 0x0400008D RID: 141
		public readonly MapIndexService _mapIndexService;

		// Token: 0x0400008E RID: 142
		public readonly WaterDepthSetter _waterDepthSetter;

		// Token: 0x0400008F RID: 143
		public readonly MutableWaterColumnRetriever _mutableWaterColumnRetriever;

		// Token: 0x04000090 RID: 144
		public readonly WaterColumn[] _waterColumns;

		// Token: 0x04000091 RID: 145
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x04000092 RID: 146
		public readonly ReadOnlyList<ThreadSafeWaterSource> _waterSources;

		// Token: 0x04000093 RID: 147
		public readonly int _verticalStride;

		// Token: 0x04000094 RID: 148
		public readonly float _deltaTime;

		// Token: 0x04000095 RID: 149
		public readonly float _overflowPressureFactor;

		// Token: 0x04000096 RID: 150
		public readonly float _maxWaterContamination;
	}
}
