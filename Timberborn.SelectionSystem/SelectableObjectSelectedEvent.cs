using System;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000018 RID: 24
	public class SelectableObjectSelectedEvent
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003715 File Offset: 0x00001915
		public SelectableObject SelectableObject { get; }

		// Token: 0x06000092 RID: 146 RVA: 0x0000371D File Offset: 0x0000191D
		public SelectableObjectSelectedEvent(SelectableObject selectableObject)
		{
			this.SelectableObject = selectableObject;
		}
	}
}
