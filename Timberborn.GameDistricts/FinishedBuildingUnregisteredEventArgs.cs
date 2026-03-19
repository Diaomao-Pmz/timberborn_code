using System;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000027 RID: 39
	public class FinishedBuildingUnregisteredEventArgs
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000045F6 File Offset: 0x000027F6
		public EntityComponent Building { get; }

		// Token: 0x06000114 RID: 276 RVA: 0x000045FE File Offset: 0x000027FE
		public FinishedBuildingUnregisteredEventArgs(EntityComponent building)
		{
			this.Building = building;
		}
	}
}
