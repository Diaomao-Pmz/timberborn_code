using System;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x0200001C RID: 28
	public class PlantingCoordinatesSetEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000351A File Offset: 0x0000171A
		public Vector3Int Coordinates { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003522 File Offset: 0x00001722
		public string Resource { get; }

		// Token: 0x06000097 RID: 151 RVA: 0x0000352A File Offset: 0x0000172A
		public PlantingCoordinatesSetEvent(Vector3Int coordinates, string resource)
		{
			this.Coordinates = coordinates;
			this.Resource = resource;
		}
	}
}
