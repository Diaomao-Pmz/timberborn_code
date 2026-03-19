using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x0200000F RID: 15
	public class MechanicalGraphFactory
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000029E4 File Offset: 0x00000BE4
		public MechanicalGraphFactory(EventBus eventBus, MechanicalGraphRegistry mechanicalGraphRegistry, IDayNightCycle dayNightCycle)
		{
			this._eventBus = eventBus;
			this._mechanicalGraphRegistry = mechanicalGraphRegistry;
			this._dayNightCycle = dayNightCycle;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A01 File Offset: 0x00000C01
		public MechanicalGraph Create()
		{
			return new MechanicalGraph(this._eventBus, this._mechanicalGraphRegistry, this._dayNightCycle);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A1C File Offset: 0x00000C1C
		public void Join(IEnumerable<MechanicalGraph> graphs)
		{
			MechanicalGraph mechanicalGraph = this.Create();
			foreach (MechanicalGraph mechanicalGraph2 in graphs)
			{
				foreach (MechanicalNode mechanicalNode in mechanicalGraph2.Nodes.ToImmutableArray<MechanicalNode>())
				{
					mechanicalGraph.AddNode(mechanicalNode);
				}
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;

		// Token: 0x0400001C RID: 28
		public readonly MechanicalGraphRegistry _mechanicalGraphRegistry;

		// Token: 0x0400001D RID: 29
		public readonly IDayNightCycle _dayNightCycle;
	}
}
