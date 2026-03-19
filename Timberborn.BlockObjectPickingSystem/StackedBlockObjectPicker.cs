using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000017 RID: 23
	public class StackedBlockObjectPicker
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000033A1 File Offset: 0x000015A1
		public StackedBlockObjectPicker(AreaIterator areaIterator, IBlockService blockService)
		{
			this._areaIterator = areaIterator;
			this._blockService = blockService;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033C4 File Offset: 0x000015C4
		public IEnumerable<BlockObject> GetStackOfBlockObjectsInArea(Vector3Int start, Vector3Int end, BlockObjectPickingMode pickingMode, BlockObjectPickerFilter selectionFilter)
		{
			this._blockObjects.Clear();
			if (pickingMode != BlockObjectPickingMode.DownwardStack && pickingMode != BlockObjectPickingMode.UpwardStack)
			{
				throw new ArgumentException(string.Format("Invalid picking mode: {0}.", pickingMode));
			}
			foreach (BlockObject blockObject in this.GetBlockObjectsInCuboid(start, end, selectionFilter))
			{
				this.AddBlockObjectsRecursively(blockObject, pickingMode);
			}
			return this._blockObjects.AsReadOnlyEnumerable<BlockObject>();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000344C File Offset: 0x0000164C
		public IEnumerable<BlockObject> GetStackOfBlockObjectsFromBlockObject(BlockObject startBlockObject, BlockObjectPickingMode pickingMode, BlockObjectPickerFilter selectionFilter)
		{
			this._blockObjects.Clear();
			if (startBlockObject != null && selectionFilter.IsValid(startBlockObject))
			{
				this.AddBlockObjectsRecursively(startBlockObject, pickingMode);
			}
			return this._blockObjects.AsReadOnlyEnumerable<BlockObject>();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003479 File Offset: 0x00001679
		public void AddBlockObjectsRecursively(BlockObject blockObject, BlockObjectPickingMode pickingMode)
		{
			if (this._blockObjects.Add(blockObject))
			{
				this.AddConnectedBlockObjects(blockObject, pickingMode);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003494 File Offset: 0x00001694
		public IEnumerable<BlockObject> GetBlockObjectsInCuboid(Vector3Int start, Vector3Int end, BlockObjectPickerFilter selectionFilter)
		{
			return Enumerable.Distinct<BlockObject>(Enumerable.SelectMany<Vector3Int, BlockObject>(Enumerable.Where<Vector3Int>(this._areaIterator.GetCuboid(start, end, 0), (Vector3Int coords) => this._blockService.AnyObjectAt(coords)), (Vector3Int coords) => this.GetValidObjects(coords, selectionFilter)));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034EC File Offset: 0x000016EC
		public void AddConnectedBlockObjects(BlockObject blockObject, BlockObjectPickingMode pickingMode)
		{
			foreach (Block block2 in blockObject.PositionedBlocks.GetAllBlocks().Where(delegate(Block block)
			{
				if (pickingMode != BlockObjectPickingMode.DownwardStack)
				{
					return block.Stackable.IsStackable();
				}
				return block.IsFoundationBlock || block.Stackable.IsUnfinishedGround();
			}))
			{
				this.AddValidBlockObjectStackedWithBlock(block2, pickingMode);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003564 File Offset: 0x00001764
		public IEnumerable<BlockObject> GetValidObjects(Vector3Int coords, BlockObjectPickerFilter selectionFilter)
		{
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coords))
			{
				if (selectionFilter.IsValid(coords, blockObject))
				{
					yield return blockObject;
				}
			}
			List<BlockObject>.Enumerator enumerator = default(List<BlockObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003584 File Offset: 0x00001784
		public void AddValidBlockObjectStackedWithBlock(Block block, BlockObjectPickingMode pickingMode)
		{
			int num = (pickingMode == BlockObjectPickingMode.UpwardStack) ? 1 : -1;
			Vector3Int coordinates = block.Coordinates + new Vector3Int(0, 0, num);
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (StackedBlockObjectPicker.ShouldIncludeNearBlock(blockObject.PositionedBlocks.GetBlock(coordinates), pickingMode))
				{
					this.AddBlockObjectsRecursively(blockObject, pickingMode);
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003618 File Offset: 0x00001818
		public static bool ShouldIncludeNearBlock(Block block, BlockObjectPickingMode direction)
		{
			if (direction != BlockObjectPickingMode.UpwardStack)
			{
				return block.Stackable.IsStackable();
			}
			return block.IsFoundationBlock || block.Stackable.IsUnfinishedGround();
		}

		// Token: 0x04000046 RID: 70
		public readonly AreaIterator _areaIterator;

		// Token: 0x04000047 RID: 71
		public readonly IBlockService _blockService;

		// Token: 0x04000048 RID: 72
		public readonly HashSet<BlockObject> _blockObjects = new HashSet<BlockObject>();
	}
}
