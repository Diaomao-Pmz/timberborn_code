using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200000B RID: 11
	public class DistanceCalculator
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D13 File Offset: 0x00000F13
		public DistanceCalculator(NodeIdService nodeIdService)
		{
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D22 File Offset: 0x00000F22
		public float Distance(int aNodeId, Vector3Int bNavMeshCoords)
		{
			return DistanceCalculator.Distance(this._nodeIdService.IdToGrid(aNodeId), bNavMeshCoords);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D38 File Offset: 0x00000F38
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			int num = Math.Abs(a.x - b.x);
			int num2 = Math.Abs(a.y - b.y);
			return 0.9f * (float)(num + num2) + -0.52722f * (float)Math.Min(num, num2);
		}

		// Token: 0x0400001E RID: 30
		public readonly NodeIdService _nodeIdService;
	}
}
