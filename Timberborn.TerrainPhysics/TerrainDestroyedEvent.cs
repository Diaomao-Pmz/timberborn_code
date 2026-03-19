using System;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000B RID: 11
	public class TerrainDestroyedEvent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000027F2 File Offset: 0x000009F2
		public Vector3Int Coordinates { get; }

		// Token: 0x06000023 RID: 35 RVA: 0x000027FA File Offset: 0x000009FA
		public TerrainDestroyedEvent(Vector3Int coordinates)
		{
			this.Coordinates = coordinates;
		}
	}
}
