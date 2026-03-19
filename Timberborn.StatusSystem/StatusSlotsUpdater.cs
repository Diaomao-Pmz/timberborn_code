using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001E RID: 30
	public class StatusSlotsUpdater : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public StatusSlotsUpdater(IStatusIconOffsetService statusIconOffsetService, EventBus eventBus)
		{
			this._statusIconOffsetService = statusIconOffsetService;
			this._eventBus = eventBus;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003C05 File Offset: 0x00001E05
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003C14 File Offset: 0x00001E14
		public void UpdateSingleton()
		{
			foreach (Vector2Int coordinates in this._dirtyCoordinates)
			{
				this._statusIconOffsetService.UpdateAffectedStatusSlot(coordinates);
			}
			this._dirtyCoordinates.Clear();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C78 File Offset: 0x00001E78
		[OnEvent]
		public void OnEntityDeletedEvent(EntityDeletedEvent entityDeletedEvent)
		{
			BlockObject component = entityDeletedEvent.Entity.GetComponent<BlockObject>();
			if (component)
			{
				this.MarkCoordinatesDirty(component);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003CA0 File Offset: 0x00001EA0
		[OnEvent]
		public void OnEnteredUnfinishedStateEvent(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			this.MarkCoordinatesDirty(enteredUnfinishedStateEvent.BlockObject);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003CAE File Offset: 0x00001EAE
		[OnEvent]
		public void OnEnteredFinishedStateEvent(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			this.MarkCoordinatesDirty(enteredFinishedStateEvent.BlockObject);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003CBC File Offset: 0x00001EBC
		public void MarkCoordinatesDirty(BlockObject blockObject)
		{
			foreach (Vector3Int value in blockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				this._dirtyCoordinates.Add(value.XY());
			}
		}

		// Token: 0x04000066 RID: 102
		public readonly IStatusIconOffsetService _statusIconOffsetService;

		// Token: 0x04000067 RID: 103
		public readonly EventBus _eventBus;

		// Token: 0x04000068 RID: 104
		public readonly HashSet<Vector2Int> _dirtyCoordinates = new HashSet<Vector2Int>();
	}
}
