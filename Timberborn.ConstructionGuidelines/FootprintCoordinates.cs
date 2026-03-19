using System;
using UnityEngine;

namespace Timberborn.ConstructionGuidelines
{
	// Token: 0x02000012 RID: 18
	public readonly struct FootprintCoordinates
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000034F9 File Offset: 0x000016F9
		public bool CanHaveFootprint { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003501 File Offset: 0x00001701
		public Vector3Int Coordinates { get; }

		// Token: 0x06000065 RID: 101 RVA: 0x00003509 File Offset: 0x00001709
		public FootprintCoordinates(Vector3Int coordinates, bool canHaveFootprint)
		{
			this.Coordinates = coordinates;
			this.CanHaveFootprint = canHaveFootprint;
		}
	}
}
