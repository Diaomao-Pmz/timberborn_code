using System;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000011 RID: 17
	public class EnteredEnterableEventArgs
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002FDD File Offset: 0x000011DD
		public Enterable Enterable { get; }

		// Token: 0x06000080 RID: 128 RVA: 0x00002FE5 File Offset: 0x000011E5
		public EnteredEnterableEventArgs(Enterable enterable)
		{
			this.Enterable = enterable;
		}
	}
}
