using System;
using System.Collections.Generic;
using Timberborn.Characters;
using Timberborn.Navigation;
using Timberborn.TickSystem;

namespace Timberborn.GameDistricts
{
	// Token: 0x02000017 RID: 23
	public class DistrictCitizenAssigner : ITickableSingleton, ISingletonNavMeshListener
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000037D2 File Offset: 0x000019D2
		public DistrictCitizenAssigner(CharacterPopulation characterPopulation, DistrictCenterRegistry districtCenterRegistry, UnassignedCitizenRegistry unassignedCitizenRegistry)
		{
			this._characterPopulation = characterPopulation;
			this._districtCenterRegistry = districtCenterRegistry;
			this._unassignedCitizenRegistry = unassignedCitizenRegistry;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037FA File Offset: 0x000019FA
		public void Tick()
		{
			if (this._unassignCutOffCitizens)
			{
				this.UnassignCharactersCutOffFromTheirDistricts();
				this._unassignCutOffCitizens = false;
			}
			this.AssignCharactersWithoutDistricts();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003817 File Offset: 0x00001A17
		public void OnNavMeshUpdated(NavMeshUpdate navMeshUpdate)
		{
			this._unassignCutOffCitizens = true;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003820 File Offset: 0x00001A20
		public void AssignCharactersWithoutDistricts()
		{
			this._unassignedCitizenRegistry.GetUnassignedCitizens(this._unassignedCitizens);
			foreach (Citizen citizen in this._unassignedCitizens)
			{
				this.AssignToClosestDistrict(citizen);
			}
			this._unassignedCitizens.Clear();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003890 File Offset: 0x00001A90
		public void AssignToClosestDistrict(Citizen citizen)
		{
			DistrictCenter districtCenter = null;
			float num = float.PositiveInfinity;
			foreach (DistrictCenter districtCenter2 in this._districtCenterRegistry.FinishedDistrictCenters)
			{
				if (districtCenter2.IsGloballyReachableFromCitizen(citizen))
				{
					float num2 = districtCenter2.DistanceToCitizen(citizen);
					if (num2 < num)
					{
						districtCenter = districtCenter2;
						num = num2;
					}
				}
			}
			if (districtCenter)
			{
				citizen.AssignDistrict(districtCenter);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000391C File Offset: 0x00001B1C
		public void UnassignCharactersCutOffFromTheirDistricts()
		{
			foreach (Character character in this._characterPopulation.Characters)
			{
				character.GetComponent<Citizen>().UnassignDistrictIfCutOff();
			}
		}

		// Token: 0x04000040 RID: 64
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x04000041 RID: 65
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000042 RID: 66
		public readonly UnassignedCitizenRegistry _unassignedCitizenRegistry;

		// Token: 0x04000043 RID: 67
		public readonly List<Citizen> _unassignedCitizens = new List<Citizen>();

		// Token: 0x04000044 RID: 68
		public bool _unassignCutOffCitizens;
	}
}
