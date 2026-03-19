using System;
using System.Collections.Generic;
using Timberborn.Buildings;
using Timberborn.Planting;
using Timberborn.ScienceSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.PlantingUI
{
	// Token: 0x02000027 RID: 39
	public class UnlockedPlantableGroupsRegistry : IPostLoadableSingleton
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000406B File Offset: 0x0000226B
		public UnlockedPlantableGroupsRegistry(BuildingService buildingService, BuildingUnlockingService buildingUnlockingService)
		{
			this._buildingService = buildingService;
			this._buildingUnlockingService = buildingUnlockingService;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000408C File Offset: 0x0000228C
		public void PostLoad()
		{
			foreach (BuildingSpec buildingSpec in this._buildingService.Buildings)
			{
				if (this._buildingUnlockingService.Unlocked(buildingSpec))
				{
					this.AddUnlockedPlantableGroups(buildingSpec);
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000040F8 File Offset: 0x000022F8
		public bool IsLocked(PlantableSpec plantableSpec)
		{
			return !this._unlockedResourceGroups.Contains(plantableSpec.ResourceGroup);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004110 File Offset: 0x00002310
		public void AddUnlockedPlantableGroups(BuildingSpec buildingSpec)
		{
			PlanterBuildingSpec spec = buildingSpec.GetSpec<PlanterBuildingSpec>();
			if (spec != null)
			{
				this._unlockedResourceGroups.Add(spec.PlantableResourceGroup);
			}
		}

		// Token: 0x04000081 RID: 129
		public readonly BuildingService _buildingService;

		// Token: 0x04000082 RID: 130
		public readonly BuildingUnlockingService _buildingUnlockingService;

		// Token: 0x04000083 RID: 131
		public readonly HashSet<string> _unlockedResourceGroups = new HashSet<string>();
	}
}
