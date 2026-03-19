using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x0200007A RID: 122
	public class RoadNavigationRangeService : ISingletonPreviewNavMeshListener, ISingletonInstantNavMeshListener
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00008957 File Offset: 0x00006B57
		public RoadNavigationRangeService(NodeIdService nodeIdService, RoadFlowFieldGenerator roadFlowFieldGenerator, InstantRoadNavMeshGraph instantRoadNavMeshGraph, PreviewRoadNavMeshGraph previewRoadNavMeshGraph, InstantDistrictMap instantDistrictMap, PreviewDistrictMap previewDistrictMap)
		{
			this._nodeIdService = nodeIdService;
			this._roadFlowFieldGenerator = roadFlowFieldGenerator;
			this._instantRoadNavMeshGraph = instantRoadNavMeshGraph;
			this._previewRoadNavMeshGraph = previewRoadNavMeshGraph;
			this._instantDistrictMap = instantDistrictMap;
			this._previewDistrictMap = previewDistrictMap;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008998 File Offset: 0x00006B98
		public IEnumerable<WeightedCoordinates> GetNodesInRange(Vector3 position)
		{
			AccessFlowField flowField = this.FilledReusableFlowField(this._instantRoadNavMeshGraph, this._instantDistrictMap, position);
			return this.GetInRangeNodes(flowField);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000089C0 File Offset: 0x00006BC0
		public IEnumerable<WeightedCoordinates> GetPreviewNodesInRange(Vector3 position)
		{
			AccessFlowField flowField = this.FilledReusableFlowField(this._previewRoadNavMeshGraph, this._previewDistrictMap, position);
			return this.GetInRangeNodes(flowField);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000089E8 File Offset: 0x00006BE8
		public void OnInstantNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000089E8 File Offset: 0x00006BE8
		public void OnPreviewNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this.OnNavMeshUpdated(navMeshUpdate);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000089F1 File Offset: 0x00006BF1
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._flowField.OnNodesChanged(navMeshUpdate.RoadNodeIds);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008A08 File Offset: 0x00006C08
		public AccessFlowField FilledReusableFlowField(RoadNavMeshGraph roadNavMeshGraph, DistrictMap districtMap, Vector3 position)
		{
			if (this._lastUsedFlowFieldPosition != position || this._lastUsedDistrictMap != districtMap)
			{
				this._lastUsedFlowFieldPosition = position;
				this._lastUsedDistrictMap = districtMap;
				this._flowField.Clear();
			}
			int num = this._nodeIdService.WorldToId(position);
			AccessFlowField districtRoadFlowFieldByRoadNodeId = districtMap.GetDistrictRoadFlowFieldByRoadNodeId(num);
			this._roadFlowFieldGenerator.FillFlowField(roadNavMeshGraph, this._flowField, districtRoadFlowFieldByRoadNodeId, num);
			return this._flowField;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00008A74 File Offset: 0x00006C74
		public IEnumerable<WeightedCoordinates> GetInRangeNodes(AccessFlowField flowField)
		{
			return from node in flowField.GetAllNodes()
			select new WeightedCoordinates(this._nodeIdService.IdToGrid(node.Id), node.GScore);
		}

		// Token: 0x04000138 RID: 312
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000139 RID: 313
		public readonly RoadFlowFieldGenerator _roadFlowFieldGenerator;

		// Token: 0x0400013A RID: 314
		public readonly InstantRoadNavMeshGraph _instantRoadNavMeshGraph;

		// Token: 0x0400013B RID: 315
		public readonly PreviewRoadNavMeshGraph _previewRoadNavMeshGraph;

		// Token: 0x0400013C RID: 316
		public readonly InstantDistrictMap _instantDistrictMap;

		// Token: 0x0400013D RID: 317
		public readonly PreviewDistrictMap _previewDistrictMap;

		// Token: 0x0400013E RID: 318
		public readonly AccessFlowField _flowField = new AccessFlowField();

		// Token: 0x0400013F RID: 319
		public Vector3 _lastUsedFlowFieldPosition;

		// Token: 0x04000140 RID: 320
		public DistrictMap _lastUsedDistrictMap;
	}
}
