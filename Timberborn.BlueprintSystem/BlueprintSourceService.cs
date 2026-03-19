using System;
using System.Collections.Generic;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000019 RID: 25
	public class BlueprintSourceService
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003959 File Offset: 0x00001B59
		public void Add(Blueprint blueprint, BlueprintFileBundle source)
		{
			this._sources[blueprint] = source;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003968 File Offset: 0x00001B68
		public BlueprintFileBundle Get(Blueprint blueprint)
		{
			return this._sources[blueprint];
		}

		// Token: 0x04000048 RID: 72
		public readonly Dictionary<Blueprint, BlueprintFileBundle> _sources = new Dictionary<Blueprint, BlueprintFileBundle>();
	}
}
