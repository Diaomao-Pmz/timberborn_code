using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000038 RID: 56
	public interface INavMeshService
	{
		// Token: 0x06000147 RID: 327
		void AddEdge(NavMeshEdge navMeshEdge);

		// Token: 0x06000148 RID: 328
		void RemoveEdge(NavMeshEdge navMeshEdge);

		// Token: 0x06000149 RID: 329
		void BlockEdge(NavMeshEdge navMeshEdge);

		// Token: 0x0600014A RID: 330
		void UnblockEdge(NavMeshEdge navMeshEdge);

		// Token: 0x0600014B RID: 331
		void AddPreviewEdge(NavMeshEdge navMeshEdge);

		// Token: 0x0600014C RID: 332
		void RemovePreviewEdge(NavMeshEdge navMeshEdge);

		// Token: 0x0600014D RID: 333
		bool IsOnNavMesh(Vector3Int coordinates);

		// Token: 0x0600014E RID: 334
		bool AreConnected(Vector3Int coordinatesA, Vector3Int coordinatesB);

		// Token: 0x0600014F RID: 335
		bool AreConnectedInstant(Vector3Int coordinatesA, Vector3Int coordinatesB);

		// Token: 0x06000150 RID: 336
		bool AreConnectedRoadInstant(Vector3Int coordinatesA, Vector3Int coordinatesB);

		// Token: 0x06000151 RID: 337
		bool AreConnectedPreview(Vector3Int coordinatesA, Vector3Int coordinatesB);

		// Token: 0x06000152 RID: 338
		bool AreConnectedRoadPreview(Vector3Int coordinatesA, Vector3Int coordinatesB);
	}
}
