using System;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200000E RID: 14
	public class EntityInitializedEvent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002663 File Offset: 0x00000863
		public EntityComponent Entity { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x0000266B File Offset: 0x0000086B
		public EntityInitializedEvent(EntityComponent entity)
		{
			this.Entity = entity;
		}
	}
}
