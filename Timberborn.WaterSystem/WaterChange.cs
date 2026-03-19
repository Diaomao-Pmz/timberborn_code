using System;
using UnityEngine;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000026 RID: 38
	public readonly struct WaterChange
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004485 File Offset: 0x00002685
		public Vector3Int Coordinates { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000448D File Offset: 0x0000268D
		public float DepthChange { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004495 File Offset: 0x00002695
		public float ContaminationChange { get; }

		// Token: 0x060000BE RID: 190 RVA: 0x0000449D File Offset: 0x0000269D
		public WaterChange(Vector3Int coordinates, float depthChange, float contaminationChange)
		{
			this.Coordinates = coordinates;
			this.DepthChange = depthChange;
			this.ContaminationChange = contaminationChange;
		}
	}
}
