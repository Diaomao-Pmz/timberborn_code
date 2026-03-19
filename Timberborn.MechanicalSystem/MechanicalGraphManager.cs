using System;
using System.Collections.Generic;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000012 RID: 18
	public class MechanicalGraphManager
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public MechanicalGraphManager(MechanicalGraphFactory mechanicalGraphFactory, MechanicalGraphReorganizer mechanicalGraphReorganizer, TransputMap transputMap)
		{
			this._mechanicalGraphFactory = mechanicalGraphFactory;
			this._mechanicalGraphReorganizer = mechanicalGraphReorganizer;
			this._transputMap = transputMap;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public void AddNode(MechanicalNode mechanicalNode)
		{
			this._mechanicalGraphFactory.Create().AddNode(mechanicalNode);
			HashSet<MechanicalGraph> hashSet = new HashSet<MechanicalGraph>
			{
				mechanicalNode.Graph
			};
			foreach (Transput transput in mechanicalNode.Transputs)
			{
				Transput facingTransput = this._transputMap.GetFacingTransput(transput);
				if (facingTransput != null && facingTransput.IsFinished)
				{
					MechanicalGraph graph = facingTransput.ParentNode.Graph;
					if (graph != null)
					{
						transput.Connect(facingTransput);
						facingTransput.Connect(transput);
						hashSet.Add(graph);
					}
				}
			}
			if (hashSet.Count > 1)
			{
				this._mechanicalGraphFactory.Join(hashSet);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B90 File Offset: 0x00000D90
		public void RemoveNode(MechanicalNode mechanicalNode)
		{
			foreach (Transput transput in mechanicalNode.Transputs)
			{
				if (transput.Connected)
				{
					transput.ConnectedTransput.Disconnect();
					transput.Disconnect();
				}
			}
			MechanicalGraph graph = mechanicalNode.Graph;
			graph.RemoveNode(mechanicalNode);
			this._mechanicalGraphReorganizer.Reorganize(graph);
		}

		// Token: 0x04000020 RID: 32
		public readonly MechanicalGraphFactory _mechanicalGraphFactory;

		// Token: 0x04000021 RID: 33
		public readonly MechanicalGraphReorganizer _mechanicalGraphReorganizer;

		// Token: 0x04000022 RID: 34
		public readonly TransputMap _transputMap;
	}
}
