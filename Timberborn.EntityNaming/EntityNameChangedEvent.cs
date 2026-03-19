using System;
using Timberborn.EntitySystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000007 RID: 7
	public class EntityNameChangedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public EntityComponent Entity { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public EntityNameChangedEvent(EntityComponent entity)
		{
			this.Entity = entity;
		}
	}
}
