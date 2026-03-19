using System;

namespace Timberborn.Persistence
{
	// Token: 0x0200000B RID: 11
	public readonly struct ListKey<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000023AA File Offset: 0x000005AA
		public string Name { get; }

		// Token: 0x0600007F RID: 127 RVA: 0x000023B2 File Offset: 0x000005B2
		public ListKey(string name)
		{
			this.Name = name;
		}
	}
}
