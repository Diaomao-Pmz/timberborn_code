using System;

namespace Timberborn.BehaviorSystem
{
	// Token: 0x02000006 RID: 6
	public readonly struct BehaviorInfo
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D5 File Offset: 0x000002D5
		public string Name { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020DD File Offset: 0x000002DD
		public BehaviorInfo(string name)
		{
			this.Name = name;
		}
	}
}
