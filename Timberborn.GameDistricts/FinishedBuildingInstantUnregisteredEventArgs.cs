using System;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000025 RID: 37
	public class FinishedBuildingInstantUnregisteredEventArgs
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000045C8 File Offset: 0x000027C8
		public EntityComponent Building { get; }

		// Token: 0x06000110 RID: 272 RVA: 0x000045D0 File Offset: 0x000027D0
		public FinishedBuildingInstantUnregisteredEventArgs(EntityComponent building)
		{
			this.Building = building;
		}
	}
}
