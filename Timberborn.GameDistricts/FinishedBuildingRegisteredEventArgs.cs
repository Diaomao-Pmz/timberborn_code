using System;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000026 RID: 38
	public class FinishedBuildingRegisteredEventArgs
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000045DF File Offset: 0x000027DF
		public EntityComponent Building { get; }

		// Token: 0x06000112 RID: 274 RVA: 0x000045E7 File Offset: 0x000027E7
		public FinishedBuildingRegisteredEventArgs(EntityComponent building)
		{
			this.Building = building;
		}
	}
}
