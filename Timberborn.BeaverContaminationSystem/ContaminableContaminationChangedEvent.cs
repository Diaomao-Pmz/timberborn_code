using System;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x02000008 RID: 8
	public class ContaminableContaminationChangedEvent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023EE File Offset: 0x000005EE
		public Contaminable Contaminable { get; }

		// Token: 0x0600001B RID: 27 RVA: 0x000023F6 File Offset: 0x000005F6
		public ContaminableContaminationChangedEvent(Contaminable contaminable)
		{
			this.Contaminable = contaminable;
		}
	}
}
