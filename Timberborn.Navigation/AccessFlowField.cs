using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.Navigation
{
	// Token: 0x02000004 RID: 4
	public class AccessFlowField : IFlowField
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020CB File Offset: 0x000002CB
		public bool IsFilled { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
		public int NumberOfNodes
		{
			get
			{
				return this._nodes.Count;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E1 File Offset: 0x000002E1
		public bool FoundPath(int destinationNodeId)
		{
			return this.IsFilled && this.HasNode(destinationNodeId);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public void MarkAsFilled()
		{
			this.IsFilled = true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FD File Offset: 0x000002FD
		public void AddNode(int nodeId, int parentNodeId, float distance)
		{
			this._nodes[nodeId] = new AccessFlowField.VisitedNode(parentNodeId, distance);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		public bool HasNode(int nodeId)
		{
			return this._nodes.ContainsKey(nodeId);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002120 File Offset: 0x00000320
		public int GetParentId(int nodeId)
		{
			return this._nodes[nodeId].ParentNodeId;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002144 File Offset: 0x00000344
		public float GetDistance(int nodeId)
		{
			return this._nodes[nodeId].Distance;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002165 File Offset: 0x00000365
		public IEnumerable<FlowFieldNode> GetAllNodes()
		{
			foreach (KeyValuePair<int, AccessFlowField.VisitedNode> keyValuePair in this._nodes)
			{
				int num;
				AccessFlowField.VisitedNode visitedNode;
				keyValuePair.Deconstruct(ref num, ref visitedNode);
				int id = num;
				AccessFlowField.VisitedNode visitedNode2 = visitedNode;
				yield return new FlowFieldNode(id, visitedNode2.Distance);
			}
			Dictionary<int, AccessFlowField.VisitedNode>.Enumerator enumerator = default(Dictionary<int, AccessFlowField.VisitedNode>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002175 File Offset: 0x00000375
		public IReadOnlyCollection<int> GetAllNodeIds()
		{
			return this._nodes.Keys;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002184 File Offset: 0x00000384
		public void OnNodesChanged(ReadOnlyList<int> nodeIds)
		{
			for (int i = 0; i < nodeIds.Count; i++)
			{
				if (this.HasNode(nodeIds[i]))
				{
					this.Clear();
					return;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BA File Offset: 0x000003BA
		public void Clear()
		{
			this._nodes.Clear();
			this.IsFilled = false;
		}

		// Token: 0x04000007 RID: 7
		public readonly Dictionary<int, AccessFlowField.VisitedNode> _nodes = new Dictionary<int, AccessFlowField.VisitedNode>();

		// Token: 0x02000005 RID: 5
		public readonly struct VisitedNode
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000011 RID: 17 RVA: 0x000021E1 File Offset: 0x000003E1
			public int ParentNodeId { get; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000021E9 File Offset: 0x000003E9
			public float Distance { get; }

			// Token: 0x06000013 RID: 19 RVA: 0x000021F1 File Offset: 0x000003F1
			public VisitedNode(int parentNodeId, float distance)
			{
				this.ParentNodeId = parentNodeId;
				this.Distance = distance;
			}
		}
	}
}
