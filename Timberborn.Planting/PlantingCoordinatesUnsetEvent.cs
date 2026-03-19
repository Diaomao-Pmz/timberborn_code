using System;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200001D RID: 29
	public class PlantingCoordinatesUnsetEvent
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003540 File Offset: 0x00001740
		public Vector3Int Coordinates { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003548 File Offset: 0x00001748
		public string Resource { get; }

		// Token: 0x0600009A RID: 154 RVA: 0x00003550 File Offset: 0x00001750
		public PlantingCoordinatesUnsetEvent(Vector3Int coordinates, string resource)
		{
			this.Coordinates = coordinates;
			this.Resource = resource;
		}
	}
}
