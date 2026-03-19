using System;

namespace Timberborn.NaturalResources
{
	// Token: 0x0200000E RID: 14
	public readonly struct SpawnableResource
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025F9 File Offset: 0x000007F9
		public string Id { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002601 File Offset: 0x00000801
		public bool IsSeedling { get; }

		// Token: 0x0600002E RID: 46 RVA: 0x00002609 File Offset: 0x00000809
		public SpawnableResource(string id, bool isSeedling)
		{
			this.Id = id;
			this.IsSeedling = isSeedling;
		}
	}
}
