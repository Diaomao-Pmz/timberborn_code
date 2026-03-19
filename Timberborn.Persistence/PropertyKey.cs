using System;

namespace Timberborn.Persistence
{
	// Token: 0x02000011 RID: 17
	public readonly struct PropertyKey<T>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000029F9 File Offset: 0x00000BF9
		public string Name { get; }

		// Token: 0x060000C9 RID: 201 RVA: 0x00002A01 File Offset: 0x00000C01
		public PropertyKey(string name)
		{
			this.Name = name;
		}
	}
}
