using System;
using System.Collections.Generic;

namespace Timberborn.DwellingSystem
{
	// Token: 0x02000012 RID: 18
	public class StaleAssignableDwellingService
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003320 File Offset: 0x00001520
		public void SetAsStalest(AutoAssignableDwelling autoAssignableDwelling)
		{
			if (this._dwellings.Last.Value != autoAssignableDwelling)
			{
				throw new ArgumentException(string.Format("Provided {0} {1} is not last!", "AutoAssignableDwelling", autoAssignableDwelling));
			}
			this._dwellings.RemoveLast();
			this._dwellings.AddFirst(autoAssignableDwelling);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003370 File Offset: 0x00001570
		public AutoAssignableDwelling GetStalest()
		{
			int count = this._dwellings.Count;
			for (int i = 0; i < count; i++)
			{
				AutoAssignableDwelling value = this._dwellings.First.Value;
				this._dwellings.RemoveFirst();
				this.RegisterDwelling(value);
				if (value.HasAssignableSlot)
				{
					return value;
				}
			}
			return null;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033C3 File Offset: 0x000015C3
		public void RegisterDwelling(AutoAssignableDwelling autoAssignableDwelling)
		{
			this._dwellings.AddLast(autoAssignableDwelling);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033D2 File Offset: 0x000015D2
		public void UnregisterDwelling(AutoAssignableDwelling autoAssignableDwelling)
		{
			this._dwellings.Remove(autoAssignableDwelling);
		}

		// Token: 0x0400002A RID: 42
		public readonly LinkedList<AutoAssignableDwelling> _dwellings = new LinkedList<AutoAssignableDwelling>();
	}
}
