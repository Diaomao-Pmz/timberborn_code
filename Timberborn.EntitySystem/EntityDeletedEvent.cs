using System;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200000D RID: 13
	public class EntityDeletedEvent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000264C File Offset: 0x0000084C
		public EntityComponent Entity { get; }

		// Token: 0x06000028 RID: 40 RVA: 0x00002654 File Offset: 0x00000854
		public EntityDeletedEvent(EntityComponent entity)
		{
			this.Entity = entity;
		}
	}
}
