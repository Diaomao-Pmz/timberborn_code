using System;

namespace Timberborn.Forestry
{
	// Token: 0x02000010 RID: 16
	public class TreeAddedToCuttingAreaEvent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002875 File Offset: 0x00000A75
		public TreeComponent TreeComponent { get; }

		// Token: 0x06000050 RID: 80 RVA: 0x0000287D File Offset: 0x00000A7D
		public TreeAddedToCuttingAreaEvent(TreeComponent treeComponent)
		{
			this.TreeComponent = treeComponent;
		}
	}
}
