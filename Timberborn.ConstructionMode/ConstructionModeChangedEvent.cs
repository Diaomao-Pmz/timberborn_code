using System;

namespace Timberborn.ConstructionMode
{
	// Token: 0x02000007 RID: 7
	public class ConstructionModeChangedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public bool InConstructionMode { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public ConstructionModeChangedEvent(bool inConstructionMode)
		{
			this.InConstructionMode = inConstructionMode;
		}
	}
}
