using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Timberborn.PrioritySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x0200000A RID: 10
	public class PrioritizedRecoveredGoodStackRegistry : ILoadableSingleton
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000023EF File Offset: 0x000005EF
		public ReadOnlyCollection<RecoveredGoodStack> GetRecoveredGoodStacks(Priority priority)
		{
			return this._recoveredGoodStacksAsReadOnly[priority];
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002400 File Offset: 0x00000600
		public void Load()
		{
			foreach (Priority key in Priorities.Ascending)
			{
				SortedList<int, RecoveredGoodStack> sortedList = new SortedList<int, RecoveredGoodStack>();
				this._recoveredGoodStacks[key] = sortedList;
				this._recoveredGoodStacksAsReadOnly[key] = new ReadOnlyCollection<RecoveredGoodStack>(sortedList.Values);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002455 File Offset: 0x00000655
		public void AddStack(RecoveredGoodStack recoveredGoodStack, Priority priority, int order)
		{
			this._recoveredGoodStacks[priority].Add(order, recoveredGoodStack);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000246A File Offset: 0x0000066A
		public void RemoveStack(Priority priority, int order)
		{
			this._recoveredGoodStacks[priority].Remove(order);
		}

		// Token: 0x04000015 RID: 21
		public readonly Dictionary<Priority, SortedList<int, RecoveredGoodStack>> _recoveredGoodStacks = new Dictionary<Priority, SortedList<int, RecoveredGoodStack>>();

		// Token: 0x04000016 RID: 22
		public readonly Dictionary<Priority, ReadOnlyCollection<RecoveredGoodStack>> _recoveredGoodStacksAsReadOnly = new Dictionary<Priority, ReadOnlyCollection<RecoveredGoodStack>>();
	}
}
