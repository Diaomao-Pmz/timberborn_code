using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.TickSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x0200000F RID: 15
	public class DistrictBreedingPodService : TickableComponent, IAwakableComponent
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002CC7 File Offset: 0x00000EC7
		public void Awake()
		{
			this._districtBuildingRegistry = base.GetComponent<DistrictBuildingRegistry>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public override void Tick()
		{
			this._breedingPodsNeedingNutrients.Clear();
			foreach (BreedingPod breedingPod in this._districtBuildingRegistry.GetEnabledBuildings<BreedingPod>())
			{
				if (breedingPod.NeedsNutrients)
				{
					this._breedingPodsNeedingNutrients.Enqueue(breedingPod);
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D44 File Offset: 0x00000F44
		public bool TryDequeueNeedingNutrients(out BreedingPod breedingPod)
		{
			return this._breedingPodsNeedingNutrients.TryDequeue(ref breedingPod);
		}

		// Token: 0x04000027 RID: 39
		public DistrictBuildingRegistry _districtBuildingRegistry;

		// Token: 0x04000028 RID: 40
		public readonly Queue<BreedingPod> _breedingPodsNeedingNutrients = new Queue<BreedingPod>();
	}
}
