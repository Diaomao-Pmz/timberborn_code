using System;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200000C RID: 12
	public class EntityCreatedEvent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002635 File Offset: 0x00000835
		public EntityComponent Entity { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x0000263D File Offset: 0x0000083D
		public EntityCreatedEvent(EntityComponent entity)
		{
			this.Entity = entity;
		}
	}
}
