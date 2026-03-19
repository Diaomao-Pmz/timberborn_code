using System;

namespace Timberborn.MechanicalSystem
{
	// Token: 0x02000010 RID: 16
	public class MechanicalGraphGeneratorAddedEvent
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A94 File Offset: 0x00000C94
		public MechanicalGraph MechanicalGraph { get; }

		// Token: 0x0600004B RID: 75 RVA: 0x00002A9C File Offset: 0x00000C9C
		public MechanicalGraphGeneratorAddedEvent(MechanicalGraph mechanicalGraph)
		{
			this.MechanicalGraph = mechanicalGraph;
		}
	}
}
