using System;
using Timberborn.GameDistricts;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000015 RID: 21
	public class MigrationNeighbours
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002DFB File Offset: 0x00000FFB
		public MigrationNeighbours(DistrictConnections districtConnections)
		{
			this._districtConnections = districtConnections;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E0C File Offset: 0x0000100C
		public PopulationDistributor GetHighestSpareNeighbour(PopulationDistributor populationDistributor)
		{
			PopulationDistributor populationDistributor2 = null;
			foreach (DistrictCenter districtCenter in this._districtConnections.GetDistrictsConnectedWith(populationDistributor.DistrictCenter))
			{
				PopulationDistributor otherDistrictPopulationDistributor = populationDistributor.GetOtherDistrictPopulationDistributor(districtCenter);
				if (otherDistrictPopulationDistributor.CanEmigrate && (populationDistributor2 == null || populationDistributor2.Spare < otherDistrictPopulationDistributor.Spare))
				{
					populationDistributor2 = otherDistrictPopulationDistributor;
				}
			}
			return populationDistributor2;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E84 File Offset: 0x00001084
		public PopulationDistributor GetLowestSpareNeighbour(PopulationDistributor populationDistributor)
		{
			PopulationDistributor populationDistributor2 = null;
			foreach (DistrictCenter districtCenter in this._districtConnections.GetDistrictsConnectedWith(populationDistributor.DistrictCenter))
			{
				PopulationDistributor otherDistrictPopulationDistributor = populationDistributor.GetOtherDistrictPopulationDistributor(districtCenter);
				if (otherDistrictPopulationDistributor.CanImmigrate && (populationDistributor2 == null || populationDistributor2.Spare > otherDistrictPopulationDistributor.Spare))
				{
					populationDistributor2 = otherDistrictPopulationDistributor;
				}
			}
			return populationDistributor2;
		}

		// Token: 0x04000032 RID: 50
		public readonly DistrictConnections _districtConnections;
	}
}
