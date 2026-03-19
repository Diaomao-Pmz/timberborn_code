using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000088 RID: 136
	public class TerrainNavigationRangeService : ISingletonPreviewNavMeshListener, ISingletonInstantNavMeshListener
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x000096C1 File Offset: 0x000078C1
		public TerrainNavigationRangeService(NodeIdService nodeIdService, TerrainFlowFieldGenerator terrainFlowFieldGenerator, InstantTerrainNavMeshGraph instantTerrainNavMeshGraph, PreviewTerrainNavMeshGraph previewTerrainNavMeshGraph)
		{
			this._nodeIdService = nodeIdService;
			this._terrainFlowFieldGenerator = terrainFlowFieldGenerator;
			this._instantTerrainNavMeshGraph = instantTerrainNavMeshGraph;
			this._previewTerrainNavMeshGraph = previewTerrainNavMeshGraph;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000096F4 File Offset: 0x000078F4
		public IEnumerable<Vector3Int> GetNodesInRange(Vector3 position, float maxDistance)
		{
			AccessFlowField flowField = this.FilledReusableFlowField(this._instantTerrainNavMeshGraph, position, maxDistance);
			return this.GetNodes(flowField);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00009718 File Offset: 0x00007918
		public IEnumerable<Vector3Int> GetPreviewNodesInRange(Vector3 position, float maxDistance)
		{
			AccessFlowField flowField = this.FilledReusableFlowField(this._previewTerrainNavMeshGraph, position, maxDistance);
			return this.GetNodes(flowField);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000973B File Offset: 0x0000793B
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000973B File Offset: 0x0000793B
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00009744 File Offset: 0x00007944
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._flowField.OnNodesChanged(navMeshUpdate.TerrainNodeIds);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009758 File Offset: 0x00007958
		public AccessFlowField FilledReusableFlowField(TerrainNavMeshGraph terrainNavMeshGraph, Vector3 position, float maxDistance)
		{
			if (this._lastUsedFlowFieldPosition != position || this._lastUsedNavMeshGraph != terrainNavMeshGraph)
			{
				this._lastUsedFlowFieldPosition = position;
				this._lastUsedNavMeshGraph = terrainNavMeshGraph;
				this._flowField.Clear();
			}
			int startNodeId = this._nodeIdService.WorldToId(position);
			this._terrainFlowFieldGenerator.FillFlowFieldUpToDistance(terrainNavMeshGraph, this._flowField, maxDistance, startNodeId);
			return this._flowField;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000097BC File Offset: 0x000079BC
		public IEnumerable<Vector3Int> GetNodes(AccessFlowField flowField)
		{
			return from node in flowField.GetAllNodes()
			select this._nodeIdService.IdToGrid(node.Id);
		}

		// Token: 0x0400016D RID: 365
		public readonly NodeIdService _nodeIdService;

		// Token: 0x0400016E RID: 366
		public readonly TerrainFlowFieldGenerator _terrainFlowFieldGenerator;

		// Token: 0x0400016F RID: 367
		public readonly InstantTerrainNavMeshGraph _instantTerrainNavMeshGraph;

		// Token: 0x04000170 RID: 368
		public readonly PreviewTerrainNavMeshGraph _previewTerrainNavMeshGraph;

		// Token: 0x04000171 RID: 369
		public readonly AccessFlowField _flowField = new AccessFlowField();

		// Token: 0x04000172 RID: 370
		public Vector3 _lastUsedFlowFieldPosition;

		// Token: 0x04000173 RID: 371
		public TerrainNavMeshGraph _lastUsedNavMeshGraph;
	}
}
