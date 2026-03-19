using System;

namespace Timberborn.Cutting
{
	// Token: 0x02000009 RID: 9
	public class CuttableCutEvent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023C4 File Offset: 0x000005C4
		public Cuttable Cuttable { get; }

		// Token: 0x0600001D RID: 29 RVA: 0x000023CC File Offset: 0x000005CC
		public CuttableCutEvent(Cuttable cuttable)
		{
			this.Cuttable = cuttable;
		}
	}
}
