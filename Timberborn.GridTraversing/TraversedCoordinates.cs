using System;
using UnityEngine;

namespace Timberborn.GridTraversing
{
	// Token: 0x02000008 RID: 8
	public readonly struct TraversedCoordinates
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002689 File Offset: 0x00000889
		public Vector3Int Coordinates { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002691 File Offset: 0x00000891
		public Vector3Int Face { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002699 File Offset: 0x00000899
		public Vector3 Intersection { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000026A1 File Offset: 0x000008A1
		public Vector3Int CoordinatesWithFaceOffset { get; }

		// Token: 0x06000015 RID: 21 RVA: 0x000026A9 File Offset: 0x000008A9
		public TraversedCoordinates(Vector3Int coordinates, Vector3Int face, Vector3 intersection)
		{
			this.Coordinates = coordinates;
			this.Face = face;
			this.Intersection = intersection;
			this.CoordinatesWithFaceOffset = coordinates + face;
		}
	}
}
