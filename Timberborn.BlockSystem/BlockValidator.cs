using System;
using System.Linq;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000034 RID: 52
	public class BlockValidator
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00005C6D File Offset: 0x00003E6D
		public BlockValidator(IBlockService blockService, MatterBelowValidator matterBelowValidator, ITerrainService terrainService, StackableBlockService stackableBlockService)
		{
			this._blockService = blockService;
			this._matterBelowValidator = matterBelowValidator;
			this._terrainService = terrainService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005C92 File Offset: 0x00003E92
		public bool BlocksValid(PositionedBlocks positionedBlocks)
		{
			return positionedBlocks.GetAllBlocks().All((Block block) => this.BlockValid(block, false, false));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005CAB File Offset: 0x00003EAB
		public bool BlocksValid(BlockObjectSpec blockObjectSpec, Placement placement)
		{
			return blockObjectSpec.GetBlocks(placement).All((Block block) => this.BlockValid(block, false, false));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005CC5 File Offset: 0x00003EC5
		public bool BlocksAlmostValid(PositionedBlocks positionedBlocks)
		{
			return (from block in positionedBlocks.GetAllBlocks()
			where block.MatterBelow.IsSolidMatter()
			select block).Any((Block block) => this.BlockValid(block, true, false));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005D02 File Offset: 0x00003F02
		public bool BlockValidWithoutUnfinishedStackable(Block block)
		{
			return this.BlockValid(block, false, true);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005D10 File Offset: 0x00003F10
		public bool IsOccupiedByBlockAbove(Vector3Int coordinates)
		{
			Vector3Int coords = coordinates + new Vector3Int(0, 0, 1);
			return this.BlockRequiresAirBelow(coords);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005D34 File Offset: 0x00003F34
		public bool BlockValid(Block block, bool almost, bool ignoreUnfinishedStackable)
		{
			return this.FitsInMap(block, almost) && !this.BlockConflictsWithExistingObject(block) && !this.BlockConflictsWithBlockAbove(block) && !this.BlockConflictsWithBlocksBelow(block) && !this.BlockConflictsWithTerrain(block) && !this.ConflictsWithUndergroundBlockObject(block) && !this.UndergroundBlockIsNotUnderground(block) && (almost || !this.BlockConflictsWithMatterBelow(block, ignoreUnfinishedStackable));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public bool FitsInMap(Block block, bool almost)
		{
			if (!block.IsOccupied)
			{
				return true;
			}
			if (!block.OptionallyUnderground && !block.Underground && !almost)
			{
				return this._blockService.Contains(block.Coordinates);
			}
			return this._blockService.Contains(block.Coordinates.XY());
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005DF9 File Offset: 0x00003FF9
		public bool BlockConflictsWithExistingObject(Block block)
		{
			return this._blockService.AnyNonOverridableObjectsAt(block.Coordinates, block.Occupation);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005E14 File Offset: 0x00004014
		public bool BlockConflictsWithBlockAbove(Block block)
		{
			return block.Occupation.Intersects(BlockOccupations.Top) && this.IsOccupiedByBlockAbove(block.Coordinates);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005E34 File Offset: 0x00004034
		public bool BlockConflictsWithBlocksBelow(Block block)
		{
			return block.OccupyAllBelow && this._blockService.AnyNonOverridableObjectBelow(block.Coordinates);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005E54 File Offset: 0x00004054
		public bool BlockRequiresAirBelow(Vector3Int coords)
		{
			BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(coords);
			return bottomObjectAt && bottomObjectAt.PositionedBlocks.GetBlock(coords).MatterBelow == MatterBelow.Air;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005E90 File Offset: 0x00004090
		public bool BlockConflictsWithTerrain(Block block)
		{
			return block.IsOccupied && !block.OptionallyUnderground && !block.Underground && this._terrainService.Underground(block.Coordinates);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005EC4 File Offset: 0x000040C4
		public bool ConflictsWithUndergroundBlockObject(Block block)
		{
			return block.Underground && this._blockService.GetObjectsAt(block.Coordinates).Any((BlockObject blockObject) => blockObject.PositionedBlocks.GetBlock(block.Coordinates).Underground);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005F19 File Offset: 0x00004119
		public bool UndergroundBlockIsNotUnderground(Block block)
		{
			return block.Underground && !this._terrainService.Underground(block.Coordinates) && !this._stackableBlockService.IsUnfinishedGroundBlockAt(block.Coordinates);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005F4F File Offset: 0x0000414F
		public bool BlockConflictsWithMatterBelow(Block block, bool ignoreUnfinishedStackable)
		{
			if (!ignoreUnfinishedStackable)
			{
				return !this._matterBelowValidator.Validate(block);
			}
			return !this._matterBelowValidator.ValidateIgnoringUnfinishedStackable(block);
		}

		// Token: 0x040000CE RID: 206
		public readonly IBlockService _blockService;

		// Token: 0x040000CF RID: 207
		public readonly MatterBelowValidator _matterBelowValidator;

		// Token: 0x040000D0 RID: 208
		public readonly ITerrainService _terrainService;

		// Token: 0x040000D1 RID: 209
		public readonly StackableBlockService _stackableBlockService;
	}
}
