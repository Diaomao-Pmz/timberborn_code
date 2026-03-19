using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;

namespace Timberborn.RelationSystem
{
	// Token: 0x02000004 RID: 4
	public interface IRelationOwner
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3
		// (remove) Token: 0x06000004 RID: 4
		event EventHandler RelationsChanged;

		// Token: 0x06000005 RID: 5
		IEnumerable<BaseComponent> GetRelations();
	}
}
