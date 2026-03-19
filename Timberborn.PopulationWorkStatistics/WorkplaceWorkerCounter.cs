using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.PopulationStatisticsSystem;
using Timberborn.WorkSystem;

namespace Timberborn.PopulationWorkStatistics
{
	// Token: 0x0200000A RID: 10
	public class WorkplaceWorkerCounter : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000021 RID: 33 RVA: 0x0000254C File Offset: 0x0000074C
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x00002584 File Offset: 0x00000784
		public event EventHandler<WorkerCountChangedEventArgs> WorkerCountChanged;

		// Token: 0x06000023 RID: 35 RVA: 0x000025BC File Offset: 0x000007BC
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._workplace = base.GetComponent<Workplace>();
			this._workplaceWorkerType = base.GetComponent<WorkplaceWorkerType>();
			this._blockableObject.ObjectBlocked += delegate(object _, EventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
			this._blockableObject.ObjectUnblocked += delegate(object _, EventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
			this._workplace.WorkerAssigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
			this._workplace.WorkerUnassigned += delegate(object _, WorkerChangedEventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
			this._workplace.DesiredWorkersChanged += delegate(object _, EventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
			this._workplaceWorkerType.WorkerTypeChanged += delegate(object _, WorkerTypeChangedEventArgs _)
			{
				this.InvokeWorkerCountChanged();
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002678 File Offset: 0x00000878
		public EmploymentStatistics GetCurrentEmploymentStatistics()
		{
			EmploymentStatistics value = this._currentEmploymentStatistics.GetValueOrDefault();
			if (this._currentEmploymentStatistics == null)
			{
				value = this.GetEmploymentStatistics();
				this._currentEmploymentStatistics = new EmploymentStatistics?(value);
			}
			return this._currentEmploymentStatistics.Value;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026BC File Offset: 0x000008BC
		public void InvokeWorkerCountChanged()
		{
			EmploymentStatistics employmentStatistics = this.GetEmploymentStatistics();
			EventHandler<WorkerCountChangedEventArgs> workerCountChanged = this.WorkerCountChanged;
			if (workerCountChanged != null)
			{
				workerCountChanged(this, new WorkerCountChangedEventArgs(this.GetCurrentEmploymentStatistics(), employmentStatistics));
			}
			this._currentEmploymentStatistics = new EmploymentStatistics?(employmentStatistics);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026FC File Offset: 0x000008FC
		public EmploymentStatistics GetEmploymentStatistics()
		{
			if (this._blockableObject.IsUnblocked)
			{
				int numberOfAssignedWorkers = this._workplace.NumberOfAssignedWorkers;
				int vacancies = Math.Max(this._workplace.DesiredWorkers - numberOfAssignedWorkers, 0);
				return new EmploymentStatistics(numberOfAssignedWorkers, vacancies, this._workplaceWorkerType.WorkerType);
			}
			return new EmploymentStatistics(0, 0, this._workplaceWorkerType.WorkerType);
		}

		// Token: 0x0400000F RID: 15
		public BlockableObject _blockableObject;

		// Token: 0x04000010 RID: 16
		public Workplace _workplace;

		// Token: 0x04000011 RID: 17
		public WorkplaceWorkerType _workplaceWorkerType;

		// Token: 0x04000012 RID: 18
		public EmploymentStatistics? _currentEmploymentStatistics;
	}
}
