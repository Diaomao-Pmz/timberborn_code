using System;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200001D RID: 29
	public class DummyNavMeshService : INavMeshService
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003E78 File Offset: 0x00002078
		public void AddEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003E78 File Offset: 0x00002078
		public void RemoveEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003E78 File Offset: 0x00002078
		public void BlockEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E78 File Offset: 0x00002078
		public void UnblockEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E78 File Offset: 0x00002078
		public void AddPreviewEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003E78 File Offset: 0x00002078
		public void RemovePreviewEdge(NavMeshEdge navMeshEdge)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003E84 File Offset: 0x00002084
		public bool IsOnNavMesh(Vector3Int coordinates)
		{
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E84 File Offset: 0x00002084
		public bool AreConnected(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E84 File Offset: 0x00002084
		public bool AreConnectedInstant(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003E84 File Offset: 0x00002084
		public bool AreConnectedRoadInstant(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return false;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003E84 File Offset: 0x00002084
		public bool AreConnectedPreview(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003E84 File Offset: 0x00002084
		public bool AreConnectedRoadPreview(Vector3Int coordinatesA, Vector3Int coordinatesB)
		{
			return false;
		}
	}
}
