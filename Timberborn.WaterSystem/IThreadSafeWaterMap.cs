using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000015 RID: 21
	public interface IThreadSafeWaterMap
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000047 RID: 71
		// (remove) Token: 0x06000048 RID: 72
		event EventHandler<int> MaxWaterColumnCountChanged;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73
		int MaxColumnCount { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74
		bool AnyColumnChanged { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75
		ReadOnlyArray<byte> ColumnCounts { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76
		ReadOnlyArray<ReadOnlyWaterColumn> WaterColumns { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77
		ReadOnlyArray<Vector2> FlowDirections { get; }

		// Token: 0x0600004E RID: 78
		int ColumnCount(int index2D);

		// Token: 0x0600004F RID: 79
		byte ColumnFloor(int index3D);

		// Token: 0x06000050 RID: 80
		byte ColumnCeiling(int index3D);

		// Token: 0x06000051 RID: 81
		float WaterDepth(int index3D);

		// Token: 0x06000052 RID: 82
		float WaterDepth(Vector3Int coordinates);

		// Token: 0x06000053 RID: 83
		float ColumnContamination(Vector3Int coordinates);

		// Token: 0x06000054 RID: 84
		Vector2 WaterFlowDirection(Vector3Int coordinates);

		// Token: 0x06000055 RID: 85
		bool TryGetColumnFloor(Vector3Int coordinates, out int floor);

		// Token: 0x06000056 RID: 86
		int CeiledWaterHeight(Vector3Int coordinates);

		// Token: 0x06000057 RID: 87
		float WaterHeightOrFloor(Vector3Int coordinates);

		// Token: 0x06000058 RID: 88
		bool CellIsUnderwater(Vector3Int coordinates);

		// Token: 0x06000059 RID: 89
		bool IsWaterOnAnyHeight(Vector2Int coordinates);
	}
}
