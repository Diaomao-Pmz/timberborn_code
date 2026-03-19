using System;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000009 RID: 9
	public interface IEntityNamer
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12
		int EntityNamerPriority { get; }

		// Token: 0x0600000D RID: 13
		string GenerateEntityName();
	}
}
