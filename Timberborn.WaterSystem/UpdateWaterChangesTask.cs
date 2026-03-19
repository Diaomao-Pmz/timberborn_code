using System;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Multithreading;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000024 RID: 36
	public readonly struct UpdateWaterChangesTask : IParallelizerSingleTask
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000040F7 File Offset: 0x000022F7
		public UpdateWaterChangesTask(MapIndexService mapIndexService, MutableWaterColumnRetriever mutableWaterColumnRetriever, WaterColumn[] waterColumns, ReadOnlyArray<byte> waterColumnCounts, ReadOnlyList<WaterChange> waterChanges, int verticalStride, float overflowPressureFactor, float maxWaterContamination)
		{
			this._mapIndexService = mapIndexService;
			this._mutableWaterColumnRetriever = mutableWaterColumnRetriever;
			this._waterColumns = waterColumns;
			this._waterColumnCounts = waterColumnCounts;
			this._waterChanges = waterChanges;
			this._verticalStride = verticalStride;
			this._overflowPressureFactor = overflowPressureFactor;
			this._maxWaterContamination = maxWaterContamination;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004138 File Offset: 0x00002338
		public void Run()
		{
			for (int i = 0; i < this._waterChanges.Count; i++)
			{
				WaterChange waterChange = this._waterChanges[i];
				Vector3Int coordinates = waterChange.Coordinates;
				int index = this._mapIndexService.CellToIndex(coordinates.XY());
				ref WaterColumn column = ref this._mutableWaterColumnRetriever.GetColumn(this._waterColumnCounts.AsSpan, this._waterColumns, this._verticalStride, index, coordinates.z);
				float waterDepth = column.WaterDepth;
				float num = waterDepth + waterChange.DepthChange;
				if (waterChange.DepthChange > 0f)
				{
					int num2 = (int)(column.Ceiling - column.Floor);
					column.WaterDepth = ((num > (float)num2) ? ((float)num2) : num);
				}
				else
				{
					column.WaterDepth = ((num < 0f) ? 0f : num);
				}
				this.UpdateContaminationFromWaterChange(ref column, waterDepth, waterChange.ContaminationChange);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004230 File Offset: 0x00002430
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

		// Token: 0x04000085 RID: 133
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000086 RID: 134
		public readonly MutableWaterColumnRetriever _mutableWaterColumnRetriever;

		// Token: 0x04000087 RID: 135
		public readonly WaterColumn[] _waterColumns;

		// Token: 0x04000088 RID: 136
		public readonly ReadOnlyArray<byte> _waterColumnCounts;

		// Token: 0x04000089 RID: 137
		public readonly ReadOnlyList<WaterChange> _waterChanges;

		// Token: 0x0400008A RID: 138
		public readonly int _verticalStride;

		// Token: 0x0400008B RID: 139
		public readonly float _overflowPressureFactor;

		// Token: 0x0400008C RID: 140
		public readonly float _maxWaterContamination;
	}
}
