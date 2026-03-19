using System;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000017 RID: 23
	public class EntererRemovedEventArgs
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003605 File Offset: 0x00001805
		public Enterer Enterer { get; }

		// Token: 0x060000B0 RID: 176 RVA: 0x0000360D File Offset: 0x0000180D
		public EntererRemovedEventArgs(Enterer enterer)
		{
			this.Enterer = enterer;
		}
	}
}
