using System;

namespace Timberborn.PrioritySystem
{
	// Token: 0x02000008 RID: 8
	public class PriorityChangedEventArgs
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002123 File Offset: 0x00000323
		public Priority PreviousPriority { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000212B File Offset: 0x0000032B
		public PriorityChangedEventArgs(Priority previousPriority)
		{
			this.PreviousPriority = previousPriority;
		}
	}
}
