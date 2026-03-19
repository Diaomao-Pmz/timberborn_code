using System;
using Timberborn.BeaverContaminationSystem;
using Timberborn.DwellingSystem;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.PopulationWorkStatistics;
using Timberborn.WorkerTypesUI;

namespace Timberborn.Population
{
	// Token: 0x02000009 RID: 9
	public class PopulationDataCollector
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002414 File Offset: 0x00000614
		public bool CollectData(int numberOfAdults, int numberOfChildren, int numberOfBots, IWorkRefusingStatisticsProvider workRefusingStatisticsProvider, IDwellingStatisticsProvider dwellingStatisticsProvider, IEmploymentStatisticsProvider employmentStatisticsProvider, IContaminationStatisticsProvider contaminationStatisticsProvider, PopulationData populationData)
		{
			WorkforceData workforceData = PopulationDataCollector.GetWorkforceData(workRefusingStatisticsProvider.GetWorkRefusingStatistics(WorkerTypeHelper.BeaverWorkerType));
			WorkforceData workforceData2 = PopulationDataCollector.GetWorkforceData(workRefusingStatisticsProvider.GetWorkRefusingStatistics(WorkerTypeHelper.BotWorkerType));
			BeaverContaminationStatistics contaminationStatistics = contaminationStatisticsProvider.GetContaminationStatistics();
			ContaminationData contaminationData = this.GetContaminationData(contaminationStatistics);
			BedData bedData = PopulationDataCollector.GetBedData(numberOfAdults + numberOfChildren, dwellingStatisticsProvider);
			EmploymentStatistics employmentStatistics = employmentStatisticsProvider.GetEmploymentStatistics(WorkerTypeHelper.BeaverWorkerType);
			WorkplaceData workplaceData = PopulationDataCollector.GetWorkplaceData(workforceData.Employable, employmentStatistics);
			EmploymentStatistics employmentStatistics2 = employmentStatisticsProvider.GetEmploymentStatistics(WorkerTypeHelper.BotWorkerType);
			WorkplaceData workplaceData2 = PopulationDataCollector.GetWorkplaceData(workforceData2.Employable, employmentStatistics2);
			if (populationData.NumberOfAdults != numberOfAdults || populationData.NumberOfChildren != numberOfChildren || populationData.NumberOfBots != numberOfBots || populationData.BeaverWorkforceData != workforceData || populationData.BotWorkforceData != workforceData2 || populationData.BedData != bedData || populationData.BeaverWorkplaceData != workplaceData || populationData.BotWorkplaceData != workplaceData2 || populationData.ContaminationData != contaminationData)
			{
				populationData.Update(numberOfAdults, numberOfChildren, numberOfBots, workforceData, workforceData2, bedData, workplaceData, workplaceData2, contaminationData);
				return true;
			}
			return false;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002530 File Offset: 0x00000730
		public bool CollectData(DistrictCenter districtCenter, PopulationData populationData)
		{
			return this.CollectData(districtCenter.DistrictPopulation.NumberOfAdults, districtCenter.DistrictPopulation.NumberOfChildren, districtCenter.DistrictPopulation.NumberOfBots, districtCenter.GetComponent<DistrictWorkRefusingStatisticsProvider>(), districtCenter.GetComponent<DistrictDwellingStatisticsProvider>(), districtCenter.GetComponent<DistrictEmploymentStatisticsProvider>(), districtCenter.GetComponent<DistrictBeaverContaminationStatisticsProvider>(), populationData);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000257D File Offset: 0x0000077D
		public static WorkforceData GetWorkforceData(WorkRefusingStatistics workRefusingStatistics)
		{
			return new WorkforceData(workRefusingStatistics.NotRefusingWorkers, workRefusingStatistics.RefusingWorkers);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002594 File Offset: 0x00000794
		public static BedData GetBedData(int numberOfDwellers, IDwellingStatisticsProvider dwellingStatisticsProvider)
		{
			DwellingStatistics dwellingStatistics = dwellingStatisticsProvider.GetDwellingStatistics();
			int homeless = numberOfDwellers - dwellingStatistics.OccupiedBeds;
			return new BedData(dwellingStatistics.OccupiedBeds, dwellingStatistics.FreeBeds, homeless);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000025C8 File Offset: 0x000007C8
		public static WorkplaceData GetWorkplaceData(int numberOfWorkers, EmploymentStatistics employmentStatistics)
		{
			int unemployed = numberOfWorkers - employmentStatistics.EmployedWorkers;
			return new WorkplaceData(employmentStatistics.EmployedWorkers, employmentStatistics.Vacancies, unemployed);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000025F3 File Offset: 0x000007F3
		public ContaminationData GetContaminationData(BeaverContaminationStatistics beaverContaminationStatistics)
		{
			return new ContaminationData(beaverContaminationStatistics.ContaminatedAdults, beaverContaminationStatistics.ContaminatedChildren);
		}
	}
}
