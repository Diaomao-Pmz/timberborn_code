using System;
using Timberborn.EntitySystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000024 RID: 36
	public class FinishedBuildingInstantRegisteredEventArgs
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000045B1 File Offset: 0x000027B1
		public EntityComponent Building { get; }

		// Token: 0x0600010E RID: 270 RVA: 0x000045B9 File Offset: 0x000027B9
		public FinishedBuildingInstantRegisteredEventArgs(EntityComponent building)
		{
			this.Building = building;
		}
	}
}
