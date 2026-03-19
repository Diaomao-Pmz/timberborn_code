using System;
using Timberborn.Common;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000C RID: 12
	public interface IThreadSafeColumnTerrainMap
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600004E RID: 78
		// (remove) Token: 0x0600004F RID: 79
		event EventHandler<int> ColumnMovedUp;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000050 RID: 80
		// (remove) Token: 0x06000051 RID: 81
		event EventHandler<int> ColumnMovedDown;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000052 RID: 82
		// (remove) Token: 0x06000053 RID: 83
		event EventHandler<int> ColumnReset;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000054 RID: 84
		// (remove) Token: 0x06000055 RID: 85
		event EventHandler<int> MaxTerrainColumnCountChanged;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86
		int MaxColumnCount { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000057 RID: 87
		ReadOnlyArray<byte> ColumnCounts { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88
		ReadOnlyArray<ReadOnlyTerrainColumn> TerrainColumns { get; }

		// Token: 0x06000059 RID: 89
		int GetColumnCount(int index);

		// Token: 0x0600005A RID: 90
		int GetColumnCeiling(int index3D);

		// Token: 0x0600005B RID: 91
		int GetColumnFloor(int index3D);

		// Token: 0x0600005C RID: 92
		bool TryGetIndexAtCeiling(int index2D, int ceiling, out int index3D);

		// Token: 0x0600005D RID: 93
		bool TryGetIndexAtOrAboveCeiling(int index2D, int ceiling, out int index3D);
	}
}
