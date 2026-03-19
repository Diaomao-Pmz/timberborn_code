using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.TickSystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200000A RID: 10
	public class DistrictWorkplaceAssigner : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000021EC File Offset: 0x000003EC
		public void Awake()
		{
			DistrictBuildingRegistry component = base.GetComponent<DistrictBuildingRegistry>();
			component.FinishedBuildingRegistered += this.OnFinishedBuildingRegistered;
			component.FinishedBuildingUnregistered += this.OnFinishedBuildingUnregistered;
			DistrictPopulation component2 = base.GetComponent<DistrictPopulation>();
			component2.CitizenAssigned += this.OnCitizenAssigned;
			component2.CitizenUnassigned += this.OnCitizenUnassigned;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000224C File Offset: 0x0000044C
		public override void Tick()
		{
			foreach (WorkplaceAssigner workplaceAssigner in this._workplaceAssigners.Values)
			{
				workplaceAssigner.Assign();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A4 File Offset: 0x000004A4
		public void OnFinishedBuildingRegistered(object sender, FinishedBuildingRegisteredEventArgs finishedBuildingRegisteredEventArgs)
		{
			WorkplacePriority component = finishedBuildingRegisteredEventArgs.Building.GetComponent<WorkplacePriority>();
			if (component != null)
			{
				WorkplaceWorkerType component2 = component.GetComponent<WorkplaceWorkerType>();
				this._workplaceAssigners.GetOrAdd(component2.WorkerType).AddWorkplace(component);
				component2.WorkerTypeChanged += this.OnWorkerTypeChanged;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022F0 File Offset: 0x000004F0
		public void OnFinishedBuildingUnregistered(object sender, FinishedBuildingUnregisteredEventArgs finishedBuildingUnregisteredEventArgs)
		{
			WorkplacePriority component = finishedBuildingUnregisteredEventArgs.Building.GetComponent<WorkplacePriority>();
			if (component != null)
			{
				WorkplaceWorkerType component2 = component.GetComponent<WorkplaceWorkerType>();
				this._workplaceAssigners[component2.WorkerType].RemoveWorkplace(component);
				component2.WorkerTypeChanged -= this.OnWorkerTypeChanged;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000233C File Offset: 0x0000053C
		public void OnCitizenAssigned(object sender, CitizenAssignedEventArgs citizenAssignedEventArgs)
		{
			Worker component = citizenAssignedEventArgs.Citizen.GetComponent<Worker>();
			if (component != null)
			{
				this._workplaceAssigners.GetOrAdd(component.WorkerType).AddWorker(component);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002370 File Offset: 0x00000570
		public void OnCitizenUnassigned(object sender, CitizenUnassignedEventArgs citizenUnassignedEventArgs)
		{
			Worker component = citizenUnassignedEventArgs.Citizen.GetComponent<Worker>();
			if (component != null)
			{
				this._workplaceAssigners[component.WorkerType].RemoveWorker(component);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023A4 File Offset: 0x000005A4
		public void OnWorkerTypeChanged(object sender, WorkerTypeChangedEventArgs workerTypeChangedEventArgs)
		{
			WorkplacePriority component = ((WorkplaceWorkerType)sender).GetComponent<WorkplacePriority>();
			string previousWorkerType = workerTypeChangedEventArgs.PreviousWorkerType;
			string currentWorkerType = workerTypeChangedEventArgs.CurrentWorkerType;
			this._workplaceAssigners[previousWorkerType].RemoveWorkplace(component);
			this._workplaceAssigners.GetOrAdd(currentWorkerType).AddWorkplace(component);
		}

		// Token: 0x0400000E RID: 14
		public readonly Dictionary<string, WorkplaceAssigner> _workplaceAssigners = new Dictionary<string, WorkplaceAssigner>();
	}
}
