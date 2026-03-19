using System;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.Planting
{
	// Token: 0x02000026 RID: 38
	public readonly struct PlantingSpot
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000427E File Offset: 0x0000247E
		public Vector3Int Coordinates { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004286 File Offset: 0x00002486
		public string ResourceToPlant { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000428E File Offset: 0x0000248E
		public BlockObject PlantingBlocker { get; }

		// Token: 0x060000DF RID: 223 RVA: 0x00004296 File Offset: 0x00002496
		public PlantingSpot(Vector3Int coordinates, string resourceToPlant, BlockObject plantingBlocker)
		{
			this.Coordinates = coordinates;
			this.ResourceToPlant = resourceToPlant;
			this.PlantingBlocker = plantingBlocker;
		}
	}
}
