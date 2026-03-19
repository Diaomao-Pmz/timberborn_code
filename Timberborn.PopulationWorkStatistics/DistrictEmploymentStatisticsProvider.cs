using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000004 RID: 4
	public class DistrictEmploymentStatisticsProvider : BaseComponent, IAwakableComponent, IEmploymentStatisticsProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public void Awake()
		{
			DistrictBuildingRegistry component = base.GetComponent<DistrictBuildingRegistry>();
			component.FinishedBuildingRegistered += this.OnFinishedBuildingRegistered;
			component.FinishedBuildingUnregistered += this.OnFinishedBuildingUnregistered;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		public EmploymentStatistics GetEmploymentStatistics(string workerType)
		{
			EmploymentStatistics result;
			if (!this._workerTypesEmploymentStatistics.TryGetValue(workerType, out result))
			{
				return new EmploymentStatistics(0, 0, workerType);
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public void OnFinishedBuildingRegistered(object sender, FinishedBuildingRegisteredEventArgs finishedBuildingRegisteredEventArgs)
		{
			WorkplaceWorkerCounter component = finishedBuildingRegisteredEventArgs.Building.GetComponent<WorkplaceWorkerCounter>();
			if (component != null)
			{
				this.AddWorkplaceWorkerCounter(component);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		public void OnFinishedBuildingUnregistered(object sender, FinishedBuildingUnregisteredEventArgs finishedBuildingUnregisteredEventArgs)
		{
			WorkplaceWorkerCounter component = finishedBuildingUnregisteredEventArgs.Building.GetComponent<WorkplaceWorkerCounter>();
			if (component != null)
			{
				this.RemoveWorkplaceWorkerCounter(component);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000215C File Offset: 0x0000035C
		public void AddWorkplaceWorkerCounter(WorkplaceWorkerCounter workplaceWorkerCounter)
		{
			EmploymentStatistics currentEmploymentStatistics = workplaceWorkerCounter.GetCurrentEmploymentStatistics();
			this.AddWorkerTypeIfNeeded(currentEmploymentStatistics.WorkerType);
			Dictionary<string, EmploymentStatistics> workerTypesEmploymentStatistics = this._workerTypesEmploymentStatistics;
			string workerType = currentEmploymentStatistics.WorkerType;
			workerTypesEmploymentStatistics[workerType] += currentEmploymentStatistics;
			workplaceWorkerCounter.WorkerCountChanged += this.OnWorkerCountChanged;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		public void RemoveWorkplaceWorkerCounter(WorkplaceWorkerCounter workplaceWorkerCounter)
		{
			EmploymentStatistics currentEmploymentStatistics = workplaceWorkerCounter.GetCurrentEmploymentStatistics();
			Dictionary<string, EmploymentStatistics> workerTypesEmploymentStatistics = this._workerTypesEmploymentStatistics;
			string workerType = currentEmploymentStatistics.WorkerType;
			workerTypesEmploymentStatistics[workerType] -= currentEmploymentStatistics;
			workplaceWorkerCounter.WorkerCountChanged -= this.OnWorkerCountChanged;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002200 File Offset: 0x00000400
		public void OnWorkerCountChanged(object sender, WorkerCountChangedEventArgs workerCountChangedEventArgs)
		{
			EmploymentStatistics oldEmploymentStatistics = workerCountChangedEventArgs.OldEmploymentStatistics;
			Dictionary<string, EmploymentStatistics> workerTypesEmploymentStatistics = this._workerTypesEmploymentStatistics;
			string workerType = oldEmploymentStatistics.WorkerType;
			workerTypesEmploymentStatistics[workerType] -= oldEmploymentStatistics;
			EmploymentStatistics newEmploymentStatistics = workerCountChangedEventArgs.NewEmploymentStatistics;
			this.AddWorkerTypeIfNeeded(newEmploymentStatistics.WorkerType);
			workerTypesEmploymentStatistics = this._workerTypesEmploymentStatistics;
			workerType = newEmploymentStatistics.WorkerType;
			workerTypesEmploymentStatistics[workerType] += newEmploymentStatistics;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002270 File Offset: 0x00000470
		public void AddWorkerTypeIfNeeded(string workerType)
		{
			this._workerTypesEmploymentStatistics.TryAdd(workerType, new EmploymentStatistics(0, 0, workerType));
		}

		// Token: 0x04000006 RID: 6
		public readonly Dictionary<string, EmploymentStatistics> _workerTypesEmploymentStatistics = new Dictionary<string, EmploymentStatistics>();
	}
}
