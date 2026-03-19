using System;
using System.Collections.Generic;
using Timberborn.EntitySystem;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200000B RID: 11
	public interface IEntityBatchLoader
	{
		// Token: 0x06000020 RID: 32
		void BatchLoadEntities(IEnumerable<EntityComponent> entities);
	}
}
