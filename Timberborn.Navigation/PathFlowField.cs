using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x0200006A RID: 106
	public class PathFlowField : IFlowField
	{
		// Token: 0x0600024B RID: 587 RVA: 0x000080EE File Offset: 0x000062EE
		public bool CheckedPath(int startNodeId, int destinationNodeId)
		{
			return this.FoundPath(startNodeId, destinationNodeId) || (startNodeId == this._startNodeId && this._fullyFilled && this._refreshed);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008115 File Offset: 0x00006315
		public bool FoundPath(int startNodeId, int destinationNodeId)
		{
			return startNodeId == this._startNodeId && this.HasNode(destinationNodeId);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008129 File Offset: 0x00006329
		public void MarkAsFullyFilled()
		{
			this._fullyFilled = true;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008132 File Offset: 0x00006332
		public void MarkAsPartiallyFilled()
		{
			this._fullyFilled = false;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000813B File Offset: 0x0000633B
		public void AddNode(int nodeId, int parentNodeId, float distance)
		{
			this._nodes[nodeId] = new PathFlowField.VisitedNode(parentNodeId, distance);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008150 File Offset: 0x00006350
		public bool HasNode(int nodeId)
		{
			return this._nodes.ContainsKey(nodeId);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008160 File Offset: 0x00006360
		public int GetParentId(int nodeId)
		{
			return this._nodes[nodeId].ParentNodeId;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008184 File Offset: 0x00006384
		public float GetDistance(int nodeId)
		{
			return this._nodes[nodeId].Distance;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000081A8 File Offset: 0x000063A8
		public void OnNodesChanged(ReadOnlyList<int> nodeIds)
		{
			for (int i = 0; i < nodeIds.Count; i++)
			{
				if (this.HasNode(nodeIds[i]))
				{
					this.Clear(this._startNodeId, false);
					return;
				}
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000081E5 File Offset: 0x000063E5
		public void Clear(int startNodeId)
		{
			this.Clear(startNodeId, true);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000081EF File Offset: 0x000063EF
		public void Clear(int startNodeId, bool refreshed)
		{
			this._startNodeId = startNodeId;
			this._refreshed = refreshed;
			this._nodes.Clear();
			this._fullyFilled = false;
		}

		// Token: 0x0400011A RID: 282
		public readonly Dictionary<int, PathFlowField.VisitedNode> _nodes = new Dictionary<int, PathFlowField.VisitedNode>();

		// Token: 0x0400011B RID: 283
		public int _startNodeId = -1;

		// Token: 0x0400011C RID: 284
		public bool _refreshed;

		// Token: 0x0400011D RID: 285
		public bool _fullyFilled;

		// Token: 0x0200006B RID: 107
		public readonly struct VisitedNode
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000257 RID: 599 RVA: 0x0000822B File Offset: 0x0000642B
			public int ParentNodeId { get; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000258 RID: 600 RVA: 0x00008233 File Offset: 0x00006433
			public float Distance { get; }

			// Token: 0x06000259 RID: 601 RVA: 0x0000823B File Offset: 0x0000643B
			public VisitedNode(int parentNodeId, float distance)
			{
				this.ParentNodeId = parentNodeId;
				this.Distance = distance;
			}
		}
	}
}
