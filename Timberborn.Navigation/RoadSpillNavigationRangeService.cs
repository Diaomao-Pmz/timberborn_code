using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000083 RID: 131
	public class RoadSpillNavigationRangeService
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x000090DA File Offset: 0x000072DA
		public RoadSpillNavigationRangeService(NodeIdService nodeIdService, InstantDistrictMap instantDistrictMap, PreviewDistrictMap previewDistrictMap)
		{
			this._nodeIdService = nodeIdService;
			this._instantDistrictMap = instantDistrictMap;
			this._previewDistrictMap = previewDistrictMap;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000090F7 File Offset: 0x000072F7
		public IEnumerable<Vector3Int> GetNodesInRange(Vector3 position)
		{
			return this.GetNodesFromFlowFieldAt(this._instantDistrictMap, position);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009106 File Offset: 0x00007306
		public IEnumerable<Vector3Int> GetPreviewNodesInRange(Vector3 position)
		{
			return this.GetNodesFromFlowFieldAt(this._previewDistrictMap, position);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00009118 File Offset: 0x00007318
		public IEnumerable<Vector3Int> GetNodesFromFlowFieldAt(DistrictMap districtMap, Vector3 position)
		{
			int nodeId2 = this._nodeIdService.WorldToId(position);
			return from nodeId in districtMap.GetDistrictRoadSpillFlowFieldByRoadNodeId(nodeId2).GetAllNodes()
			select this._nodeIdService.IdToGrid(nodeId);
		}

		// Token: 0x04000159 RID: 345
		public readonly NodeIdService _nodeIdService;

		// Token: 0x0400015A RID: 346
		public readonly InstantDistrictMap _instantDistrictMap;

		// Token: 0x0400015B RID: 347
		public readonly PreviewDistrictMap _previewDistrictMap;
	}
}
