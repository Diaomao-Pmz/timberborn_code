using System;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.EntitySystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Achievements
{
	// Token: 0x02000049 RID: 73
	public class ReachBuildHeightLimitAchievement : Achievement
	{
		// Token: 0x06000118 RID: 280 RVA: 0x0000478B File Offset: 0x0000298B
		public ReachBuildHeightLimitAchievement(EventBus eventBus, MapSize mapSize, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._mapSize = mapSize;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000047A8 File Offset: 0x000029A8
		public override string Id
		{
			get
			{
				return "REACH_BUILD_HEIGHT_LIMIT";
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000047AF File Offset: 0x000029AF
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (this.IsReachingHeightLimit(enteredFinishedStateEvent.BlockObject))
			{
				base.Unlock();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000047C5 File Offset: 0x000029C5
		public override void EnableInternal()
		{
			if (this.AnyBuildingIsReachingHeightLimit())
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000047E2 File Offset: 0x000029E2
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000047F0 File Offset: 0x000029F0
		public bool AnyBuildingIsReachingHeightLimit()
		{
			foreach (BlockObject blockObject in from spec in this._entityComponentRegistry.GetEnabled<Building>()
			where spec.GetComponent<BlockObject>().IsFinished
			select spec.GetComponent<BlockObject>())
			{
				if (this.IsReachingHeightLimit(blockObject))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004894 File Offset: 0x00002A94
		public bool IsReachingHeightLimit(BlockObject blockObject)
		{
			foreach (Vector3Int vector3Int in blockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				if (vector3Int.z == this._mapSize.TotalSize.z - 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000096 RID: 150
		public readonly EventBus _eventBus;

		// Token: 0x04000097 RID: 151
		public readonly MapSize _mapSize;

		// Token: 0x04000098 RID: 152
		public readonly EntityComponentRegistry _entityComponentRegistry;
	}
}
