using System;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000019 RID: 25
	public class SelectableObjectUnselectedEvent
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000372C File Offset: 0x0000192C
		public SelectableObject SelectableObject { get; }

		// Token: 0x06000094 RID: 148 RVA: 0x00003734 File Offset: 0x00001934
		public SelectableObjectUnselectedEvent(SelectableObject selectableObject)
		{
			this.SelectableObject = selectableObject;
		}
	}
}
