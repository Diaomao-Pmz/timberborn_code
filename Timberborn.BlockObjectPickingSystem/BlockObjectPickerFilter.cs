using System;
using System.Linq;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x0200000B RID: 11
	public readonly struct BlockObjectPickerFilter
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002747 File Offset: 0x00000947
		public BlockObjectPickerFilter(int referenceZ, bool ignoreTopBlockObjectsOnBaseZ, bool ignoreBottomBlockObjectsOnBaseZ, BlockOccupations blockOccupations, Func<BlockObject, bool> selectionPredicate)
		{
			this._referenceZ = referenceZ;
			this._ignoreTopBlockObjectsOnBaseZ = ignoreTopBlockObjectsOnBaseZ;
			this._ignoreBottomBlockObjectsOnBaseZ = ignoreBottomBlockObjectsOnBaseZ;
			this._blockOccupation = blockOccupations;
			this._selectionPredicate = selectionPredicate;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002770 File Offset: 0x00000970
		public static BlockObjectPickerFilter Create(int referenceZ, Func<BlockObject, bool> selectionPredicate)
		{
			BlockOccupations blockOccupations = (BlockOccupations)2147483647;
			return new BlockObjectPickerFilter(referenceZ, false, false, blockOccupations, selectionPredicate);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002790 File Offset: 0x00000990
		public static BlockObjectPickerFilter CreateWithConstraints(BlockObjectHit blockObjectHit, Vector3Int startCoords, int maxVisibleLevel, Func<BlockObject, bool> selectionPredicate)
		{
			BlockObject blockObject = blockObjectHit.BlockObject;
			bool ignoreTopBlockObjectsOnBaseZ = startCoords.z == maxVisibleLevel && BlockObjectPickerFilter.IsBlockWithBottomOccupation(blockObject, startCoords);
			bool ignoreBottomBlockObjectsOnBaseZ = BlockObjectPickerFilter.IsBlockWithTopOccupation(blockObject, startCoords);
			BlockOccupations occupation = blockObjectHit.HitBlock.Occupation;
			return new BlockObjectPickerFilter(startCoords.z, ignoreTopBlockObjectsOnBaseZ, ignoreBottomBlockObjectsOnBaseZ, occupation, selectionPredicate);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool IsValid(BlockObject blockObject)
		{
			if (this.IsOnValidLevel(blockObject) && blockObject.Blocks.GetAllBlocks().Any(new Func<Block, bool>(this.ValidateBlockOccupation)))
			{
				Func<BlockObject, bool> selectionPredicate = this._selectionPredicate;
				return selectionPredicate == null || selectionPredicate(blockObject);
			}
			return false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002836 File Offset: 0x00000A36
		public bool IsValid(Vector3Int coords, BlockObject blockObject)
		{
			if (this.IsOnValidLevel(blockObject) && this.ValidateBlockOccupation(blockObject.PositionedBlocks.GetBlock(coords)))
			{
				Func<BlockObject, bool> selectionPredicate = this._selectionPredicate;
				return selectionPredicate == null || selectionPredicate(blockObject);
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000286C File Offset: 0x00000A6C
		public static bool IsBlockWithBottomOccupation(BlockObject blockObject, Vector3Int coordinates)
		{
			return blockObject.PositionedBlocks.GetBlock(coordinates).Occupation.IsBottomOrFloorOrBoth();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002894 File Offset: 0x00000A94
		public static bool IsBlockWithTopOccupation(BlockObject blockObject, Vector3Int coordinates)
		{
			return blockObject.PositionedBlocks.GetBlock(coordinates).Occupation.IsTopOrCornersOrBoth();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028BC File Offset: 0x00000ABC
		public bool IsOnValidLevel(BlockObject blockObject)
		{
			return blockObject.CoordinatesAtBaseZ.z == this._referenceZ && (!this._ignoreTopBlockObjectsOnBaseZ || !BlockObjectPickerFilter.HasTopOccupationOnBaseZ(blockObject, this._referenceZ)) && (!this._ignoreBottomBlockObjectsOnBaseZ || !BlockObjectPickerFilter.HasBottomOccupationOnBaseZ(blockObject, this._referenceZ));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002910 File Offset: 0x00000B10
		public static bool HasBottomOccupationOnBaseZ(BlockObject blockObject, int baseZ)
		{
			return (from block in blockObject.PositionedBlocks.GetAllBlocks()
			where block.Coordinates.z == baseZ
			select block).Any((Block block) => block.Occupation.IsBottomOrFloorOrBoth());
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000296C File Offset: 0x00000B6C
		public static bool HasTopOccupationOnBaseZ(BlockObject blockObject, int baseZ)
		{
			return (from block in blockObject.PositionedBlocks.GetAllBlocks()
			where block.Coordinates.z == baseZ
			select block).Any((Block block) => block.Occupation.IsTopOrCornersOrBoth());
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029C6 File Offset: 0x00000BC6
		public bool ValidateBlockOccupation(Block block)
		{
			return (block.Occupation & this._blockOccupation) > BlockOccupations.None;
		}

		// Token: 0x04000026 RID: 38
		public readonly int _referenceZ;

		// Token: 0x04000027 RID: 39
		public readonly bool _ignoreTopBlockObjectsOnBaseZ;

		// Token: 0x04000028 RID: 40
		public readonly bool _ignoreBottomBlockObjectsOnBaseZ;

		// Token: 0x04000029 RID: 41
		public readonly BlockOccupations _blockOccupation;

		// Token: 0x0400002A RID: 42
		public readonly Func<BlockObject, bool> _selectionPredicate;
	}
}
