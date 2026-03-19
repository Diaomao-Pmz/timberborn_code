using System;
using Timberborn.BaseComponentSystem;
using Timberborn.GameDistricts;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.WorkSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x02000005 RID: 5
	public class DistrictWorkRefusingStatisticsProvider : BaseComponent, IAwakableComponent, IWorkRefusingStatisticsProvider
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000229A File Offset: 0x0000049A
		public void Awake()
		{
			DistrictPopulation component = base.GetComponent<DistrictPopulation>();
			component.CitizenAssigned += this.OnCitizenAssigned;
			component.CitizenUnassigned += this.OnCitizenUnassigned;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C5 File Offset: 0x000004C5
		public WorkRefusingStatistics GetWorkRefusingStatistics(string workerType)
		{
			return this._workRefuserRegistry.GetWorkRefusingStatistics(workerType);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022D4 File Offset: 0x000004D4
		public void OnCitizenAssigned(object sender, CitizenAssignedEventArgs citizenAssignedEventArgs)
		{
			WorkRefuser component = citizenAssignedEventArgs.Citizen.GetComponent<WorkRefuser>();
			if (component != null)
			{
				this._workRefuserRegistry.AddWorkRefuser(component);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022FC File Offset: 0x000004FC
		public void OnCitizenUnassigned(object sender, CitizenUnassignedEventArgs citizenUnassignedEventArgs)
		{
			WorkRefuser component = citizenUnassignedEventArgs.Citizen.GetComponent<WorkRefuser>();
			if (component != null)
			{
				this._workRefuserRegistry.RemoveWorkRefuser(component);
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly WorkRefuserRegistry _workRefuserRegistry = new WorkRefuserRegistry();
	}
}
