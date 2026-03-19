using System;
using Timberborn.Buildings;

namespace Timberborn.ScienceSystem
{
	// Token: 0x02000007 RID: 7
	public class BuildingUnlockedEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BuildingSpec BuildingSpec { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public BuildingUnlockedEvent(BuildingSpec buildingSpec)
		{
			this.BuildingSpec = buildingSpec;
		}
	}
}
