using System;
using System.Collections.Generic;

namespace Timberborn.Navigation
{
	// Token: 0x0200007F RID: 127
	public class RoadSpillFlowField : IFlowField
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00008DB0 File Offset: 0x00006FB0
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public bool IsFilled { get; private set; }

		// Token: 0x060002B1 RID: 689 RVA: 0x00008DC1 File Offset: 0x00006FC1
		public void FinishFilling()
		{
			this.IsFilled = true;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008DCA File Offset: 0x00006FCA
		public void AddNode(int nodeId, int parentNodeId, int roadParentNodeId, float distanceToRoad)
		{
			this._nodes[nodeId] = new RoadSpillFlowField.Node(parentNodeId, roadParentNodeId, distanceToRoad);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008DE4 File Offset: 0x00006FE4
		public float GetDistanceToRoad(int nodeId)
		{
			return this._nodes[nodeId].DistanceToRoad;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008E08 File Offset: 0x00007008
		public int GetParentId(int nodeId)
		{
			return this._nodes[nodeId].ParentNodeId;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008E2C File Offset: 0x0000702C
		public int GetRoadParentNodeId(int nodeId)
		{
			return this._nodes[nodeId].RoadParentNodeId;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008E4D File Offset: 0x0000704D
		public bool HasNode(int nodeId)
		{
			return this._nodes.ContainsKey(nodeId);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008E5B File Offset: 0x0000705B
		public IEnumerable<int> GetAllNodes()
		{
			return this._nodes.Keys;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008E68 File Offset: 0x00007068
		public void Clear()
		{
			this._nodes.Clear();
			this.IsFilled = false;
		}

		// Token: 0x0400014D RID: 333
		public readonly Dictionary<int, RoadSpillFlowField.Node> _nodes = new Dictionary<int, RoadSpillFlowField.Node>();

		// Token: 0x02000080 RID: 128
		public readonly struct Node
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060002BA RID: 698 RVA: 0x00008E8F File Offset: 0x0000708F
			public int ParentNodeId { get; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060002BB RID: 699 RVA: 0x00008E97 File Offset: 0x00007097
			public int RoadParentNodeId { get; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060002BC RID: 700 RVA: 0x00008E9F File Offset: 0x0000709F
			public float DistanceToRoad { get; }

			// Token: 0x060002BD RID: 701 RVA: 0x00008EA7 File Offset: 0x000070A7
			public Node(int parentNodeId, int roadParentNodeId, float distanceToRoad)
			{
				this.ParentNodeId = parentNodeId;
				this.RoadParentNodeId = roadParentNodeId;
				this.DistanceToRoad = distanceToRoad;
			}
		}
	}
}
