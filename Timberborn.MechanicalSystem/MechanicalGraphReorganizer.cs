using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000015 RID: 21
	public class MechanicalGraphReorganizer
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002C7F File Offset: 0x00000E7F
		public MechanicalGraphReorganizer(MechanicalGraphFactory mechanicalGraphFactory)
		{
			this._mechanicalGraphFactory = mechanicalGraphFactory;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void Reorganize(MechanicalGraph mechanicalGraph)
		{
			this._oldGraphNodes.AddRange(mechanicalGraph.Nodes);
			while (!this._oldGraphNodes.IsEmpty<MechanicalNode>())
			{
				MechanicalGraph graph = this._mechanicalGraphFactory.Create();
				this._currentGraphNodes.Add(this._oldGraphNodes.First<MechanicalNode>());
				while (!this._currentGraphNodes.IsEmpty<MechanicalNode>())
				{
					this.ProcessNodeAndVisitConnected(graph, this._currentGraphNodes.First<MechanicalNode>());
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D15 File Offset: 0x00000F15
		public void ProcessNodeAndVisitConnected(MechanicalGraph graph, MechanicalNode node)
		{
			this._oldGraphNodes.Remove(node);
			this._currentGraphNodes.Remove(node);
			graph.AddNode(node);
			this.VisitConnectedNodes(node);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D40 File Offset: 0x00000F40
		public void VisitConnectedNodes(MechanicalNode node)
		{
			foreach (MechanicalNode item in (from transput in node.Transputs
			select transput.ConnectedNode).Intersect(this._oldGraphNodes))
			{
				if (this._oldGraphNodes.Contains(item))
				{
					this._currentGraphNodes.Add(item);
				}
			}
		}

		// Token: 0x04000025 RID: 37
		public readonly MechanicalGraphFactory _mechanicalGraphFactory;

		// Token: 0x04000026 RID: 38
		public readonly HashSet<MechanicalNode> _oldGraphNodes = new HashSet<MechanicalNode>();

		// Token: 0x04000027 RID: 39
		public readonly HashSet<MechanicalNode> _currentGraphNodes = new HashSet<MechanicalNode>();
	}
}
