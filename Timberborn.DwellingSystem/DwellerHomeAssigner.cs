using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Beavers;
using Timberborn.GameDistricts;
using Timberborn.TickSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x0200000B RID: 11
	public class DwellerHomeAssigner : ITickableSingleton
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002855 File Offset: 0x00000A55
		public DwellerHomeAssigner(StaleAssignableDwellingService staleAssignableDwellingService)
		{
			this._staleAssignableDwellingService = staleAssignableDwellingService;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002864 File Offset: 0x00000A64
		public void Tick()
		{
			this.AssignToDwelling();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000286C File Offset: 0x00000A6C
		public void AssignToDwelling()
		{
			AutoAssignableDwelling stalest = this._staleAssignableDwellingService.GetStalest();
			if (stalest && DwellerHomeAssigner.AddDweller(stalest) && stalest.HasAssignableSlot)
			{
				this._staleAssignableDwellingService.SetAsStalest(stalest);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028AC File Offset: 0x00000AAC
		public static bool AddDweller(AutoAssignableDwelling dwelling)
		{
			DistrictCenter district = dwelling.GetComponent<DistrictBuilding>().District;
			if (!district)
			{
				return false;
			}
			DistrictPopulation districtPopulation = district.DistrictPopulation;
			if (!dwelling.ShouldAssignAdult)
			{
				return DwellerHomeAssigner.AssignDweller(dwelling, districtPopulation.Children, districtPopulation.Adults);
			}
			return DwellerHomeAssigner.AssignDweller(dwelling, districtPopulation.Adults, districtPopulation.Children);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002918 File Offset: 0x00000B18
		public static bool AssignDweller(AutoAssignableDwelling dwelling, IEnumerable<Beaver> primaryBeavers, IEnumerable<Beaver> secondaryBeavers)
		{
			foreach (Beaver beaver in primaryBeavers.Concat(secondaryBeavers))
			{
				Dweller component = beaver.GetComponent<Dweller>();
				if (component.IsLookingForBetterHome() && dwelling.CanAssignDweller(component))
				{
					dwelling.AssignDweller(component);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000017 RID: 23
		public readonly StaleAssignableDwellingService _staleAssignableDwellingService;
	}
}
