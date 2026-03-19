using System;
using System.Collections.Generic;
using Timberborn.PrioritySystem;

namespace Timberborn.WorkSystem
{
	// Token: 0x0200000E RID: 14
	public class PriorityOrderedWorkplaces
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002694 File Offset: 0x00000894
		public WorkplacePriority GetHighestPriorityUnderstaffedWorkplace()
		{
			for (int i = 0; i < this._workplaces.Count; i++)
			{
				WorkplacePriority workplacePriority = this._workplaces.Values[i];
				if (workplacePriority.Workplace.Understaffed)
				{
					return workplacePriority;
				}
			}
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026DC File Offset: 0x000008DC
		public WorkplacePriority GetLowestPriorityStaffedWorkplace()
		{
			for (int i = this._workplaces.Count - 1; i >= 0; i--)
			{
				WorkplacePriority workplacePriority = this._workplaces.Values[i];
				if (workplacePriority.Workplace.NumberOfAssignedWorkers > 0)
				{
					return workplacePriority;
				}
			}
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002724 File Offset: 0x00000924
		public void AddWorkplace(WorkplacePriority workplacePriority)
		{
			PriorityOrderedWorkplaces.WorkplacePriorityKey key = new PriorityOrderedWorkplaces.WorkplacePriorityKey(workplacePriority.Priority, workplacePriority.InstantiationOrder);
			this._workplaces.Add(key, workplacePriority);
			workplacePriority.PriorityChanged += this.OnPriorityChanged;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002764 File Offset: 0x00000964
		public void RemoveWorkplace(WorkplacePriority workplacePriority)
		{
			PriorityOrderedWorkplaces.WorkplacePriorityKey key = new PriorityOrderedWorkplaces.WorkplacePriorityKey(workplacePriority.Priority, workplacePriority.InstantiationOrder);
			this._workplaces.Remove(key);
			workplacePriority.PriorityChanged -= this.OnPriorityChanged;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027A4 File Offset: 0x000009A4
		public void OnPriorityChanged(object sender, PriorityChangedEventArgs priorityChangedEventArgs)
		{
			WorkplacePriority workplacePriority = (WorkplacePriority)sender;
			Priority previousPriority = priorityChangedEventArgs.PreviousPriority;
			PriorityOrderedWorkplaces.WorkplacePriorityKey key = new PriorityOrderedWorkplaces.WorkplacePriorityKey(previousPriority, workplacePriority.InstantiationOrder);
			this._workplaces.Remove(key);
			PriorityOrderedWorkplaces.WorkplacePriorityKey key2 = new PriorityOrderedWorkplaces.WorkplacePriorityKey(workplacePriority.Priority, workplacePriority.InstantiationOrder);
			this._workplaces.Add(key2, workplacePriority);
		}

		// Token: 0x04000015 RID: 21
		public readonly SortedList<PriorityOrderedWorkplaces.WorkplacePriorityKey, WorkplacePriority> _workplaces = new SortedList<PriorityOrderedWorkplaces.WorkplacePriorityKey, WorkplacePriority>();

		// Token: 0x0200000F RID: 15
		public readonly struct WorkplacePriorityKey : IComparable<PriorityOrderedWorkplaces.WorkplacePriorityKey>
		{
			// Token: 0x0600003C RID: 60 RVA: 0x0000280D File Offset: 0x00000A0D
			public WorkplacePriorityKey(Priority priority, int order)
			{
				this._priority = priority;
				this._order = order;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00002820 File Offset: 0x00000A20
			public int CompareTo(PriorityOrderedWorkplaces.WorkplacePriorityKey other)
			{
				int num = other._priority.CompareTo(this._priority);
				if (num != 0)
				{
					return num;
				}
				return this._order.CompareTo(other._order);
			}

			// Token: 0x04000016 RID: 22
			public readonly Priority _priority;

			// Token: 0x04000017 RID: 23
			public readonly int _order;
		}
	}
}
