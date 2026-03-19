using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x02000009 RID: 9
	public class DeconstructionNotifier : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		public DeconstructionNotifier(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002143 File Offset: 0x00000343
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002154 File Offset: 0x00000354
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			Deconstructible enabledComponent = entityDeletedEvent.Entity.GetEnabledComponent<Deconstructible>();
			if (enabledComponent != null)
			{
				this.NotifyOnBuildingDeconstructed(enabledComponent);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002177 File Offset: 0x00000377
		public void NotifyOnBuildingDeconstructed(Deconstructible deconstructible)
		{
			this._eventBus.Post(new BuildingDeconstructedEvent(deconstructible, DeconstructionNotifier.GetCoordinates(deconstructible)));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002190 File Offset: 0x00000390
		public static ReadOnlyList<Vector3Int> GetCoordinates(Deconstructible deconstructible)
		{
			PositionedBlocks positionedBlocks = deconstructible.GetComponent<BlockObject>().PositionedBlocks;
			List<Vector3Int> list = positionedBlocks.GetFoundationCoordinates().ToList<Vector3Int>();
			if (list.Count <= 0)
			{
				return positionedBlocks.GetAllCoordinates().ToList<Vector3Int>().AsReadOnlyList<Vector3Int>();
			}
			return list.AsReadOnlyList<Vector3Int>();
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;
	}
}
