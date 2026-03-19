using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Navigation
{
	// Token: 0x02000074 RID: 116
	public class RestrictedNodeUpdater
	{
		// Token: 0x0600026B RID: 619 RVA: 0x000082D5 File Offset: 0x000064D5
		public RestrictedNodeUpdater(RestrictedNodeMap restrictedNodeMap, NodeIdService nodeIdService)
		{
			this._restrictedNodeMap = restrictedNodeMap;
			this._nodeIdService = nodeIdService;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000082F8 File Offset: 0x000064F8
		public void EnqueueAddingChange(IReadOnlyCollection<Vector3Int> coordinates)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				int nodeId = this._nodeIdService.GridToId(coordinates2);
				this._enqueuedChanges.Enqueue(new RestrictedNodeUpdater.RestrictedNodeChange(nodeId, true));
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008358 File Offset: 0x00006558
		public void EnqueueRemovingChange(IReadOnlyCollection<Vector3Int> coordinates)
		{
			foreach (Vector3Int coordinates2 in coordinates)
			{
				int nodeId = this._nodeIdService.GridToId(coordinates2);
				this._enqueuedChanges.Enqueue(new RestrictedNodeUpdater.RestrictedNodeChange(nodeId, false));
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000083B8 File Offset: 0x000065B8
		public void ProcessRegularChanges()
		{
			while (!this._enqueuedChanges.IsEmpty<RestrictedNodeUpdater.RestrictedNodeChange>())
			{
				this.ProcessChange();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000083D0 File Offset: 0x000065D0
		public void ProcessChange()
		{
			RestrictedNodeUpdater.RestrictedNodeChange restrictedNodeChange = this._enqueuedChanges.Dequeue();
			if (restrictedNodeChange.AddingChange)
			{
				this._restrictedNodeMap.RestrictNode(restrictedNodeChange.NodeId);
				return;
			}
			this._restrictedNodeMap.UnrestrictNode(restrictedNodeChange.NodeId);
		}

		// Token: 0x04000125 RID: 293
		public readonly RestrictedNodeMap _restrictedNodeMap;

		// Token: 0x04000126 RID: 294
		public readonly NodeIdService _nodeIdService;

		// Token: 0x04000127 RID: 295
		public readonly Queue<RestrictedNodeUpdater.RestrictedNodeChange> _enqueuedChanges = new Queue<RestrictedNodeUpdater.RestrictedNodeChange>();

		// Token: 0x02000075 RID: 117
		public readonly struct RestrictedNodeChange
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000270 RID: 624 RVA: 0x00008417 File Offset: 0x00006617
			public int NodeId { get; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000271 RID: 625 RVA: 0x0000841F File Offset: 0x0000661F
			public bool AddingChange { get; }

			// Token: 0x06000272 RID: 626 RVA: 0x00008427 File Offset: 0x00006627
			public RestrictedNodeChange(int nodeId, bool addingChange)
			{
				this.NodeId = nodeId;
				this.AddingChange = addingChange;
			}
		}
	}
}
