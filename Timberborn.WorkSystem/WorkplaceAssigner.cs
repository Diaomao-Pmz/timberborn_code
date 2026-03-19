using System;
using Timberborn.Common;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000022 RID: 34
	public class WorkplaceAssigner
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00003FEC File Offset: 0x000021EC
		public void Assign()
		{
			WorkplacePriority highestPriorityUnderstaffedWorkplace = this._priorityOrderedWorkplaces.GetHighestPriorityUnderstaffedWorkplace();
			if (highestPriorityUnderstaffedWorkplace)
			{
				if (this._unemployedWorkers.AnyUnemployed)
				{
					this.AssignStalestUnemployed(highestPriorityUnderstaffedWorkplace.Workplace);
					return;
				}
				this.ReassignWorkersToHigherPriorityWorkplaces(highestPriorityUnderstaffedWorkplace);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000402E File Offset: 0x0000222E
		public void AddWorker(Worker worker)
		{
			this._unemployedWorkers.AddWorker(worker);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000403C File Offset: 0x0000223C
		public void RemoveWorker(Worker worker)
		{
			this._unemployedWorkers.RemoveWorker(worker);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000404A File Offset: 0x0000224A
		public void AddWorkplace(WorkplacePriority workplacePriority)
		{
			this._priorityOrderedWorkplaces.AddWorkplace(workplacePriority);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004058 File Offset: 0x00002258
		public void RemoveWorkplace(WorkplacePriority workplacePriority)
		{
			this._priorityOrderedWorkplaces.RemoveWorkplace(workplacePriority);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004068 File Offset: 0x00002268
		public void AssignStalestUnemployed(Workplace workplace)
		{
			int num = workplace.DesiredWorkers - workplace.NumberOfAssignedWorkers;
			while (num > 0 && this._unemployedWorkers.AnyUnemployed)
			{
				workplace.AssignWorker(this._unemployedWorkers.GetUnemployedWorker());
				num--;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000040AC File Offset: 0x000022AC
		public void ReassignWorkersToHigherPriorityWorkplaces(WorkplacePriority understaffedWorkplace)
		{
			WorkplacePriority lowestPriorityStaffedWorkplace = this._priorityOrderedWorkplaces.GetLowestPriorityStaffedWorkplace();
			if (lowestPriorityStaffedWorkplace && understaffedWorkplace.Priority > lowestPriorityStaffedWorkplace.Priority)
			{
				WorkplaceAssigner.ReassignWorkers(lowestPriorityStaffedWorkplace.Workplace, understaffedWorkplace.Workplace);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000040EC File Offset: 0x000022EC
		public static void ReassignWorkers(Workplace staffedWorkplace, Workplace understaffedWorkplace)
		{
			ReadOnlyList<Worker> assignedWorkers = staffedWorkplace.AssignedWorkers;
			for (int i = assignedWorkers.Count - 1; i >= 0; i--)
			{
				Worker worker = assignedWorkers[i];
				staffedWorkplace.UnassignWorker(worker);
				understaffedWorkplace.AssignWorker(worker);
				if (!understaffedWorkplace.Understaffed)
				{
					break;
				}
			}
		}

		// Token: 0x0400005B RID: 91
		public readonly PriorityOrderedWorkplaces _priorityOrderedWorkplaces = new PriorityOrderedWorkplaces();

		// Token: 0x0400005C RID: 92
		public readonly UnemployedWorkers _unemployedWorkers = new UnemployedWorkers();
	}
}
