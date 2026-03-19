using System;

namespace Timberborn.ToolPanelSystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct OrderedToolFragment
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020BE File Offset: 0x000002BE
		public IToolFragment ToolFragment { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C6 File Offset: 0x000002C6
		public int Order { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020CE File Offset: 0x000002CE
		public OrderedToolFragment(IToolFragment toolFragment, int order)
		{
			this.ToolFragment = toolFragment;
			this.Order = order;
		}
	}
}
