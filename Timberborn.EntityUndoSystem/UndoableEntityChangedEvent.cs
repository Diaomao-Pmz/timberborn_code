using System;
using Timberborn.EntitySystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x0200000D RID: 13
	public class UndoableEntityChangedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026A2 File Offset: 0x000008A2
		public EntityComponent Entity { get; }

		// Token: 0x06000028 RID: 40 RVA: 0x000026AA File Offset: 0x000008AA
		public UndoableEntityChangedEvent(EntityComponent entity)
		{
			this.Entity = entity;
		}
	}
}
