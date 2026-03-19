using System;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000019 RID: 25
	public readonly struct SingletonKey
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000287C File Offset: 0x00000A7C
		public string Name { get; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002884 File Offset: 0x00000A84
		public SingletonKey(string name)
		{
			this.Name = name;
		}
	}
}
