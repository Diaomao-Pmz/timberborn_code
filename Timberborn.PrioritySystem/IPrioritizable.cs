using System;

namespace Timberborn.PrioritySystem
{
	// Token: 0x02000004 RID: 4
	public interface IPrioritizable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		Priority Priority { get; }

		// Token: 0x06000004 RID: 4
		void SetPriority(Priority priority);
	}
}
