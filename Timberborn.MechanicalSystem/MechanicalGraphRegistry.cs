using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000013 RID: 19
	public class MechanicalGraphRegistry : ILoadableSingleton
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002BF2 File Offset: 0x00000DF2
		public MechanicalGraphRegistry(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002C0C File Offset: 0x00000E0C
		public ReadOnlyList<MechanicalGraph> MechanicalGraphs
		{
			get
			{
				return this._mechanicalGraphs.AsReadOnlyList<MechanicalGraph>();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C19 File Offset: 0x00000E19
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C27 File Offset: 0x00000E27
		public void AddGraph(MechanicalGraph mechanicalGraph)
		{
			if (mechanicalGraph.Nodes.Count<MechanicalNode>() == 1)
			{
				this._mechanicalGraphs.Add(mechanicalGraph);
				this._eventBus.Post(new MechanicalGraphCreatedEvent());
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C53 File Offset: 0x00000E53
		public void RemoveGraph(MechanicalGraph mechanicalGraph)
		{
			if (!mechanicalGraph.Nodes.Any<MechanicalNode>())
			{
				this._mechanicalGraphs.Remove(mechanicalGraph);
				this._eventBus.Post(new MechanicalGraphRemovedEvent());
			}
		}

		// Token: 0x04000023 RID: 35
		public readonly EventBus _eventBus;

		// Token: 0x04000024 RID: 36
		public readonly List<MechanicalGraph> _mechanicalGraphs = new List<MechanicalGraph>();
	}
}
