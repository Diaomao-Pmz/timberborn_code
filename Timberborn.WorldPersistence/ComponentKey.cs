using System;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000006 RID: 6
	public readonly struct ComponentKey
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002129 File Offset: 0x00000329
		public string Name { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002131 File Offset: 0x00000331
		public ComponentKey(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213A File Offset: 0x0000033A
		public ComponentKey AddSuffix(string suffix)
		{
			return new ComponentKey(this.Name + ":" + suffix);
		}
	}
}
