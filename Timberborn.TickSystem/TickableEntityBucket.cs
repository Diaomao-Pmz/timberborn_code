using System;
using System.Collections.Generic;

namespace Timberborn.TickSystem
{
	// Token: 0x02000012 RID: 18
	public class TickableEntityBucket
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000241E File Offset: 0x0000061E
		public void Add(TickableEntity tickableEntity)
		{
			this._tickableEntities.Add(tickableEntity.EntityId, tickableEntity);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002432 File Offset: 0x00000632
		public void Remove(TickableEntity tickableEntity)
		{
			if (this._isTicking)
			{
				this._entitiesToRemove.Add(tickableEntity);
				return;
			}
			this._tickableEntities.Remove(tickableEntity.EntityId);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000245C File Offset: 0x0000065C
		public void TickAll()
		{
			this._isTicking = true;
			for (int i = 0; i < this._tickableEntities.Count; i++)
			{
				this._tickableEntities.Values[i].Tick();
			}
			this._isTicking = false;
			for (int j = 0; j < this._entitiesToRemove.Count; j++)
			{
				this._tickableEntities.Remove(this._entitiesToRemove[j].EntityId);
			}
			this._entitiesToRemove.Clear();
		}

		// Token: 0x04000014 RID: 20
		public readonly SortedList<Guid, TickableEntity> _tickableEntities = new SortedList<Guid, TickableEntity>();

		// Token: 0x04000015 RID: 21
		public readonly List<TickableEntity> _entitiesToRemove = new List<TickableEntity>();

		// Token: 0x04000016 RID: 22
		public bool _isTicking;
	}
}
