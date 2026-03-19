using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.UndoSystem;

namespace Timberborn.StartingLocationSystem
{
	// Token: 0x02000009 RID: 9
	public class StartingLocationService : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021DB File Offset: 0x000003DB
		public StartingLocationService(EntityComponentRegistry entityComponentRegistry, EntityService entityService, EventBus eventBus, IUndoRegistry undoRegistry)
		{
			this._entityComponentRegistry = entityComponentRegistry;
			this._entityService = entityService;
			this._eventBus = eventBus;
			this._undoRegistry = undoRegistry;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002200 File Offset: 0x00000400
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002210 File Offset: 0x00000410
		[OnEvent]
		public void OnBlockObjectSet(BlockObjectSetEvent blockObjectSetEvent)
		{
			StartingLocation remainingStartingLocation;
			if (blockObjectSetEvent.BlockObject.TryGetComponent<StartingLocation>(out remainingStartingLocation))
			{
				this.DeleteOtherStartingLocations(remainingStartingLocation);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002233 File Offset: 0x00000433
		public bool HasStartingLocation()
		{
			return this._entityComponentRegistry.GetEnabled<StartingLocation>().Any<StartingLocation>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002245 File Offset: 0x00000445
		public Placement GetPlacement()
		{
			return this.GetStartingLocation().GetComponent<BlockObject>().Placement;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002258 File Offset: 0x00000458
		public void DeleteStartingLocations()
		{
			foreach (StartingLocation entity in this._entityComponentRegistry.GetEnabled<StartingLocation>().ToList<StartingLocation>())
			{
				this._entityService.Delete(entity);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		public StartingLocation GetStartingLocation()
		{
			List<StartingLocation> list = this._entityComponentRegistry.GetEnabled<StartingLocation>().ToList<StartingLocation>();
			if (list.IsEmpty<StartingLocation>())
			{
				throw new InvalidOperationException("No StartingLocationSpec exists.");
			}
			if (list.Count > 1)
			{
				throw new InvalidOperationException("There must be only one StartingLocationSpec.");
			}
			return list[0];
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022FC File Offset: 0x000004FC
		public void DeleteOtherStartingLocations(StartingLocation remainingStartingLocation)
		{
			if (!this._undoRegistry.IsProcessingStack)
			{
				foreach (StartingLocation entity in (from startingLocation in this._entityComponentRegistry.GetEnabled<StartingLocation>()
				where startingLocation != remainingStartingLocation
				select startingLocation).ToList<StartingLocation>())
				{
					this._entityService.Delete(entity);
				}
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400000A RID: 10
		public readonly EntityService _entityService;

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly IUndoRegistry _undoRegistry;
	}
}
