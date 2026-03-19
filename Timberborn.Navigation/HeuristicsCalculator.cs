using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000027 RID: 39
	public class HeuristicsCalculator
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00004E57 File Offset: 0x00003057
		public HeuristicsCalculator(DistanceCalculator distanceCalculator, NodeIdService nodeIdService)
		{
			this._distanceCalculator = distanceCalculator;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004E6D File Offset: 0x0000306D
		public void SetDestinationNode(int nodeId)
		{
			this._destinationNodeCoords = this._nodeIdService.IdToGrid(nodeId);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004E81 File Offset: 0x00003081
		public void SetDestinationNodes(IReadOnlyList<int> nodeIds)
		{
			this._destinationNodeCoords = this.CalculateAverageCoordinates(nodeIds);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004E90 File Offset: 0x00003090
		public float H(int nodeId)
		{
			return this._distanceCalculator.Distance(nodeId, this._destinationNodeCoords);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004EA4 File Offset: 0x000030A4
		public Vector3Int CalculateAverageCoordinates(IReadOnlyList<int> nodeIds)
		{
			Vector3Int vector3Int = Vector3Int.zero;
			int count = nodeIds.Count;
			for (int i = 0; i < count; i++)
			{
				vector3Int += this._nodeIdService.IdToGrid(nodeIds[i]);
			}
			return new Vector3Int(HeuristicsCalculator.Average(vector3Int.x, count), HeuristicsCalculator.Average(vector3Int.y, count), HeuristicsCalculator.Average(vector3Int.z, count));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004F0F File Offset: 0x0000310F
		public static int Average(int vectorComponent, int numberOfNodes)
		{
			return Mathf.RoundToInt((float)vectorComponent / (float)numberOfNodes);
		}

		// Token: 0x04000081 RID: 129
		public readonly DistanceCalculator _distanceCalculator;

		// Token: 0x04000082 RID: 130
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000083 RID: 131
		public Vector3Int _destinationNodeCoords;
	}
}
