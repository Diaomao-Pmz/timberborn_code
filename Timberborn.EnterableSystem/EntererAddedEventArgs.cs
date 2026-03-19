using System;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000013 RID: 19
	public class EntererAddedEventArgs
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000339E File Offset: 0x0000159E
		public Enterer Enterer { get; }

		// Token: 0x06000098 RID: 152 RVA: 0x000033A6 File Offset: 0x000015A6
		public EntererAddedEventArgs(Enterer enterer)
		{
			this.Enterer = enterer;
		}
	}
}
