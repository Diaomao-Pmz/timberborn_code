using System;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000015 RID: 21
	public readonly struct PickedCoordinates
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000323C File Offset: 0x0000143C
		public Vector3Int Coordinates { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003244 File Offset: 0x00001444
		public int ReferenceTerrainLevel { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000324C File Offset: 0x0000144C
		public int VerticalOffset { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003254 File Offset: 0x00001454
		public bool FilterOverhangingCoordinates { get; }

		// Token: 0x0600005C RID: 92 RVA: 0x0000325C File Offset: 0x0000145C
		public PickedCoordinates(Vector3Int coordinates, int referenceTerrainLevel, int verticalOffset, bool filterOverhangingCoordinates)
		{
			this.Coordinates = coordinates;
			this.ReferenceTerrainLevel = referenceTerrainLevel;
			this.VerticalOffset = verticalOffset;
			this.FilterOverhangingCoordinates = filterOverhangingCoordinates;
		}
	}
}
