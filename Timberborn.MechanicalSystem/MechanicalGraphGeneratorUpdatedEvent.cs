using System;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000011 RID: 17
	public class MechanicalGraphGeneratorUpdatedEvent
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002AAB File Offset: 0x00000CAB
		public MechanicalGraph MechanicalGraph { get; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002AB3 File Offset: 0x00000CB3
		public MechanicalGraphGeneratorUpdatedEvent(MechanicalGraph mechanicalGraph)
		{
			this.MechanicalGraph = mechanicalGraph;
		}
	}
}
