using System;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000004 RID: 4
	public interface IToolbarButton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		bool IsVisible { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		bool IsActive { get; }

		// Token: 0x06000005 RID: 5
		void Select();
	}
}
