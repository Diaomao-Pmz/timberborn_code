using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.AchievementSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.Achievements
{
	// Token: 0x0200001B RID: 27
	public class BuildStackedHydroponicGardensAchievement : Achievement
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000030EE File Offset: 0x000012EE
		public BuildStackedHydroponicGardensAchievement(EventBus eventBus, IBlockService blockService, EntityComponentRegistry entityComponentRegistry)
		{
			this._eventBus = eventBus;
			this._blockService = blockService;
			this._entityComponentRegistry = entityComponentRegistry;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000312C File Offset: 0x0000132C
		public override string Id
		{
			get
			{
				return "BUILD_STACKED_HYDROPONIC_GARDENS";
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003133 File Offset: 0x00001333
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (this.IsValidStack(enteredFinishedStateEvent.BlockObject))
			{
				base.Unlock();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003149 File Offset: 0x00001349
		public override void EnableInternal()
		{
			if (this.HasValidStackOnStart())
			{
				base.Unlock();
				return;
			}
			this._eventBus.Register(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003166 File Offset: 0x00001366
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003174 File Offset: 0x00001374
		public bool HasValidStackOnStart()
		{
			foreach (Building building2 in from building in this._entityComponentRegistry.GetEnabled<Building>()
			where building.GetComponent<BlockObject>().IsFinished && building.GetComponent<TemplateSpec>().TemplateName == BuildStackedHydroponicGardensAchievement.TemplateName
			select building)
			{
				if (this.IsValidStack(building2.GetComponent<BlockObject>()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031F8 File Offset: 0x000013F8
		public static bool IsValidBuilding(BlockObject blockObject)
		{
			if (blockObject && blockObject.IsFinished)
			{
				TemplateSpec component = blockObject.GetComponent<TemplateSpec>();
				if (component != null)
				{
					return component.TemplateName == BuildStackedHydroponicGardensAchievement.TemplateName;
				}
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003234 File Offset: 0x00001434
		public bool IsValidStack(BlockObject blockObject)
		{
			if (BuildStackedHydroponicGardensAchievement.IsValidBuilding(blockObject) && blockObject.Coordinates.z >= BuildStackedHydroponicGardensAchievement.RequiredStackHeight - 1)
			{
				int num = 1;
				this.FillBlockCache(blockObject);
				do
				{
					BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(this._blockCache.Dequeue().Below());
					if (BuildStackedHydroponicGardensAchievement.IsValidBuilding(bottomObjectAt) && this.HasSameFootprint(blockObject, bottomObjectAt))
					{
						num++;
						blockObject = bottomObjectAt;
						this.FillBlockCache(bottomObjectAt);
					}
				}
				while (this._blockCache.Count > 0 && num < BuildStackedHydroponicGardensAchievement.RequiredStackHeight);
				if (num >= BuildStackedHydroponicGardensAchievement.RequiredStackHeight)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032C8 File Offset: 0x000014C8
		public bool HasSameFootprint(BlockObject first, BlockObject second)
		{
			ImmutableArray<Block> allBlocks = first.PositionedBlocks.GetAllBlocks();
			ImmutableArray<Block> allBlocks2 = second.PositionedBlocks.GetAllBlocks();
			if (allBlocks.Length != allBlocks2.Length)
			{
				return false;
			}
			for (int i = 0; i < allBlocks.Length; i++)
			{
				this._firstCoordinates.Add(allBlocks[i].Coordinates.XY());
				this._secondCoordinates.Add(allBlocks2[i].Coordinates.XY());
			}
			bool result = this._firstCoordinates.SetEquals(this._secondCoordinates);
			this._firstCoordinates.Clear();
			this._secondCoordinates.Clear();
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000337C File Offset: 0x0000157C
		public void FillBlockCache(BlockObject objectBelow)
		{
			this._blockCache.Clear();
			foreach (Vector3Int item in objectBelow.PositionedBlocks.GetFoundationCoordinates())
			{
				this._blockCache.Enqueue(item);
			}
		}

		// Token: 0x0400003C RID: 60
		public static readonly int RequiredStackHeight = 8;

		// Token: 0x0400003D RID: 61
		public static readonly string TemplateName = "HydroponicGarden.IronTeeth";

		// Token: 0x0400003E RID: 62
		public readonly EventBus _eventBus;

		// Token: 0x0400003F RID: 63
		public readonly IBlockService _blockService;

		// Token: 0x04000040 RID: 64
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000041 RID: 65
		public readonly Queue<Vector3Int> _blockCache = new Queue<Vector3Int>();

		// Token: 0x04000042 RID: 66
		public readonly HashSet<Vector2Int> _firstCoordinates = new HashSet<Vector2Int>();

		// Token: 0x04000043 RID: 67
		public readonly HashSet<Vector2Int> _secondCoordinates = new HashSet<Vector2Int>();
	}
}
