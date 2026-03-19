using System;
using Timberborn.EntitySystem;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200000F RID: 15
	public class InstantiatedSerializedEntity
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000024A4 File Offset: 0x000006A4
		public EntityComponent Entity { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024AC File Offset: 0x000006AC
		public SerializedEntity SerializedEntity { get; }

		// Token: 0x0600002A RID: 42 RVA: 0x000024B4 File Offset: 0x000006B4
		public InstantiatedSerializedEntity(EntityComponent entity, SerializedEntity serializedEntity)
		{
			this.Entity = entity;
			this.SerializedEntity = serializedEntity;
		}
	}
}
