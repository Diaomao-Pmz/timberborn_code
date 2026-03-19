using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.RecoveredGoodSystem
{
	// Token: 0x02000017 RID: 23
	public class RecoveredGoodStackMover : BaseComponent, IAwakableComponent, IInitializableEntity, IBlockObjectCustomOverriding, IDeletableEntity
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003508 File Offset: 0x00001708
		public RecoveredGoodStackMover(RecoveredGoodStackCoordinatesFinder recoveredGoodStackCoordinatesFinder, ITerrainService terrainService, IBlockService blockService, EventBus eventBus)
		{
			this._recoveredGoodStackCoordinatesFinder = recoveredGoodStackCoordinatesFinder;
			this._terrainService = terrainService;
			this._blockService = blockService;
			this._eventBus = eventBus;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000352D File Offset: 0x0000172D
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._recoveredGoodStack = base.GetComponent<RecoveredGoodStack>();
			this._recoveredGoodStackAccessible = base.GetComponent<RecoveredGoodStackAccessible>();
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000356A File Offset: 0x0000176A
		public void InitializeEntity()
		{
			this._blockObjectBelow = this.GetBlockObjectBelow(this._blockObject.CoordinatesAtBaseZ.Below());
			if (this._blockObjectBelow)
			{
				this._eventBus.Register(this);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000035A1 File Offset: 0x000017A1
		public void DeleteEntity()
		{
			this._eventBus.Unregister(this);
			this._terrainService.TerrainHeightChanged -= this.OnTerrainHeightChanged;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035C8 File Offset: 0x000017C8
		public void OverrideBy(BlockObject blockObject)
		{
			RecoveredGoodStack component = blockObject.GetComponent<RecoveredGoodStack>();
			if (component != null)
			{
				this._recoveredGoodStack.MergeInto(component);
				return;
			}
			this.TryToReposition(blockObject);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035F3 File Offset: 0x000017F3
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			if (this._blockObjectBelow && entityDeletedEvent.Entity.GetComponent<BlockObject>() == this._blockObjectBelow)
			{
				this.TryToReposition(null);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000361C File Offset: 0x0000181C
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			if (!this._blockObjectBelow)
			{
				TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
				if (!change.SetTerrain && this._blockObject.Coordinates == change.Coordinates.ToVector3Int(change.To + 1))
				{
					this.TryToReposition(null);
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003674 File Offset: 0x00001874
		public void TryToReposition(BlockObject overridingBlockObject = null)
		{
			Vector3Int vector3Int;
			if (this._recoveredGoodStackCoordinatesFinder.FindValidCoordinates(this._blockObject.Coordinates, overridingBlockObject, out vector3Int))
			{
				Placement placement = this._blockObject.Placement;
				this._blockObject.Reposition(new Placement(vector3Int, placement.Orientation, placement.FlipMode));
				this.UpdateBlockObjectBelow(vector3Int);
				this._recoveredGoodStackAccessible.UpdateAccesses();
				return;
			}
			this._recoveredGoodStack.Delete();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036E8 File Offset: 0x000018E8
		public void UpdateBlockObjectBelow(Vector3Int validCoordinates)
		{
			BlockObject blockObjectBelow = this.GetBlockObjectBelow(validCoordinates.Below());
			if (blockObjectBelow)
			{
				if (!this._blockObjectBelow)
				{
					this._eventBus.Register(this);
				}
				this._blockObjectBelow = blockObjectBelow;
				return;
			}
			if (this._blockObjectBelow)
			{
				this._eventBus.Unregister(this);
				this._blockObjectBelow = null;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000374C File Offset: 0x0000194C
		public BlockObject GetBlockObjectBelow(Vector3Int coordinates)
		{
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (blockObject.PositionedBlocks.HasStackableBlockAt(coordinates))
				{
					return blockObject;
				}
			}
			return null;
		}

		// Token: 0x0400004C RID: 76
		public readonly RecoveredGoodStackCoordinatesFinder _recoveredGoodStackCoordinatesFinder;

		// Token: 0x0400004D RID: 77
		public readonly ITerrainService _terrainService;

		// Token: 0x0400004E RID: 78
		public readonly IBlockService _blockService;

		// Token: 0x0400004F RID: 79
		public readonly EventBus _eventBus;

		// Token: 0x04000050 RID: 80
		public BlockObject _blockObject;

		// Token: 0x04000051 RID: 81
		public RecoveredGoodStack _recoveredGoodStack;

		// Token: 0x04000052 RID: 82
		public RecoveredGoodStackAccessible _recoveredGoodStackAccessible;

		// Token: 0x04000053 RID: 83
		public BlockObject _blockObjectBelow;
	}
}
