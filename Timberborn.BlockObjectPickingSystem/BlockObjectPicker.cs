using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.LevelVisibilitySystem;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000006 RID: 6
	public class BlockObjectPicker
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021C8 File Offset: 0x000003C8
		public BlockObjectPicker(StackedBlockObjectPicker stackedBlockObjectPicker, ILevelVisibilityService levelVisibilityService, AreaIterator areaIterator, IBlockService blockService)
		{
			this._stackedBlockObjectPicker = stackedBlockObjectPicker;
			this._levelVisibilityService = levelVisibilityService;
			this._areaIterator = areaIterator;
			this._blockService = blockService;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F0 File Offset: 0x000003F0
		public IEnumerable<BlockObject> PickBlockObjects(SelectionStart selectionStart, Vector3Int endCoords, BlockObjectPickingMode pickingMode, Func<BlockObject, bool> blockObjectFilter, bool selectingArea)
		{
			Vector3Int coordinates = selectionStart.Coordinates;
			BlockObjectHit? blockObjectHit = selectionStart.GetBlockObjectHit();
			BlockObject blockObject = (blockObjectHit != null) ? blockObjectHit.GetValueOrDefault().BlockObject : null;
			if (blockObject != null)
			{
				if (!selectingArea && !BlockObjectPicker.IsStackable(blockObject) && (blockObjectFilter == null || blockObjectFilter.Invoke(blockObject)) && pickingMode != BlockObjectPickingMode.DownwardStack)
				{
					return Enumerables.One<BlockObject>(blockObject);
				}
				if (blockObject.BaseZ > 0)
				{
					coordinates.z = blockObject.CoordinatesAtBaseZ.z;
				}
			}
			BlockObjectPickerFilter filter = (blockObjectHit != null) ? BlockObjectPickerFilter.CreateWithConstraints(blockObjectHit.Value, coordinates, this._levelVisibilityService.MaxVisibleLevel, blockObjectFilter) : BlockObjectPickerFilter.Create(coordinates.z, blockObjectFilter);
			if (pickingMode == BlockObjectPickingMode.InsideArea)
			{
				return this.PickBlockObjectsInArea(coordinates, endCoords, filter);
			}
			return this.PickBlockObjectsInStack(blockObject, coordinates, endCoords, pickingMode, selectingArea, filter);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C3 File Offset: 0x000004C3
		public static bool IsStackable(BlockObject blockObject)
		{
			return blockObject.PositionedBlocks.GetAllBlocks().Any((Block block) => block.Stackable.IsStackable());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F4 File Offset: 0x000004F4
		public IEnumerable<BlockObject> PickBlockObjectsInArea(Vector3Int startCoords, Vector3Int endCoords, BlockObjectPickerFilter filter)
		{
			IEnumerable<BlockObject> enumerable = Enumerable.Distinct<BlockObject>(Enumerable.SelectMany<Vector3Int, BlockObject>(Enumerable.Where<Vector3Int>(this._areaIterator.GetCuboid(startCoords, endCoords, 0), (Vector3Int coords) => this._blockService.AnyObjectAt(coords)), (Vector3Int coords) => this.GetValidObjects(coords, filter)));
			foreach (BlockObject blockObject in enumerable)
			{
				yield return blockObject;
			}
			IEnumerator<BlockObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002319 File Offset: 0x00000519
		public IEnumerable<BlockObject> PickBlockObjectsInStack(BlockObject startBlockObject, Vector3Int startCoords, Vector3Int endCoords, BlockObjectPickingMode pickingMode, bool selectingArea, BlockObjectPickerFilter filter)
		{
			if (selectingArea)
			{
				return this._stackedBlockObjectPicker.GetStackOfBlockObjectsInArea(startCoords, endCoords, pickingMode, filter);
			}
			return this._stackedBlockObjectPicker.GetStackOfBlockObjectsFromBlockObject(startBlockObject, pickingMode, filter);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002341 File Offset: 0x00000541
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

		// Token: 0x0400000A RID: 10
		public readonly StackedBlockObjectPicker _stackedBlockObjectPicker;

		// Token: 0x0400000B RID: 11
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400000C RID: 12
		public readonly AreaIterator _areaIterator;

		// Token: 0x0400000D RID: 13
		public readonly IBlockService _blockService;
	}
}
