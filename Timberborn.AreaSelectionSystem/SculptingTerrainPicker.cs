using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectPickingSystem;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GridTraversing;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SingletonSystem;
using Timberborn.TerrainQueryingSystem;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000025 RID: 37
	public class SculptingTerrainPicker : ILoadableSingleton
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004020 File Offset: 0x00002220
		public SculptingTerrainPicker(TerrainPicker terrainPicker, AreaSelectionController areaSelectionController, AreaIterator areaIterator, CameraService cameraService, ILevelVisibilityService levelVisibilityService, BlockObjectRaycaster blockObjectRaycaster, ISpecService specService)
		{
			this._terrainPicker = terrainPicker;
			this._areaSelectionController = areaSelectionController;
			this._areaIterator = areaIterator;
			this._cameraService = cameraService;
			this._levelVisibilityService = levelVisibilityService;
			this._blockObjectRaycaster = blockObjectRaycaster;
			this._specService = specService;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000405D File Offset: 0x0000225D
		public void Load()
		{
			this._maxBlocks = this._specService.GetSingleSpec<AreaPickersSpec>().SculptingMaxBlocks;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004078 File Offset: 0x00002278
		public bool PickTerrainAreaToAdd(AreaPicker.IntAreaCallback previewCallback, AreaPicker.IntAreaCallback actionCallback)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray start, Ray end, bool _)
			{
				previewCallback(this.GetBlocksToAdd(start, end), start);
			}, delegate(Ray start, Ray end, bool _)
			{
				actionCallback(this.GetBlocksToAdd(start, end), start);
			}, delegate
			{
			});
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000040E4 File Offset: 0x000022E4
		public bool PickTerrainAreaToRemove(AreaPicker.IntAreaCallback previewCallback, AreaPicker.IntAreaCallback actionCallback)
		{
			return this._areaSelectionController.ProcessInput(delegate(Ray start, Ray end, bool _)
			{
				previewCallback(this.GetBlocksToRemove(start, end), start);
			}, delegate(Ray start, Ray end, bool _)
			{
				actionCallback(this.GetBlocksToRemove(start, end), start);
			}, delegate
			{
			});
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000414E File Offset: 0x0000234E
		public void Reset()
		{
			this._areaSelectionController.Reset();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000415C File Offset: 0x0000235C
		public IEnumerable<Vector3Int> GetBlocksToAdd(Ray startRay, Ray endRay)
		{
			Vector3Int vector3Int;
			if (this.TryGetAddingStartBlock(startRay, out vector3Int))
			{
				Vector3Int addingRectangleEndBlock = this.GetAddingRectangleEndBlock(vector3Int, endRay);
				return this._areaIterator.GetRectangle(vector3Int, addingRectangleEndBlock, this._maxBlocks);
			}
			return Enumerable.Empty<Vector3Int>();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004198 File Offset: 0x00002398
		public bool TryGetAddingStartBlock(Ray startRay, out Vector3Int block)
		{
			if (this.TryGetStackableBlockObjectCoordinates(startRay, out block))
			{
				return true;
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinatesWithStump(startRay);
			if (traversedCoordinates != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates.GetValueOrDefault();
				if (valueOrDefault.CoordinatesWithFaceOffset.z <= this._levelVisibilityService.MaxVisibleLevel)
				{
					block = valueOrDefault.CoordinatesWithFaceOffset;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000041FC File Offset: 0x000023FC
		public Vector3Int GetAddingRectangleEndBlock(Vector3Int startBlock, Ray endRay)
		{
			Vector3Int result;
			if (this.TryGetStackableBlockObjectCoordinates(endRay, out result))
			{
				return result;
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.FindCoordinatesOnLevelInMap(endRay, (float)startBlock.z);
			Vector3 vector = CoordinateSystem.WorldToGrid(this._cameraService.Transform.position);
			TraversedCoordinates? traversedCoordinates2 = this._terrainPicker.PickTerrainCoordinatesWithStump(endRay);
			if (traversedCoordinates2 != null)
			{
				TraversedCoordinates valueOrDefault = traversedCoordinates2.GetValueOrDefault();
				if (valueOrDefault.CoordinatesWithFaceOffset.z <= this._levelVisibilityService.MaxVisibleLevel)
				{
					if (vector.z > (float)startBlock.z && traversedCoordinates != null && Vector3.Distance(vector, valueOrDefault.Intersection) > Vector3.Distance(vector, traversedCoordinates.Value.Intersection))
					{
						return traversedCoordinates.Value.Coordinates;
					}
					return valueOrDefault.Coordinates;
				}
			}
			if (traversedCoordinates != null && vector.z > (float)startBlock.z)
			{
				return traversedCoordinates.Value.Coordinates;
			}
			return startBlock;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004300 File Offset: 0x00002500
		public IEnumerable<Vector3Int> GetBlocksToRemove(Ray startRay, Ray endRay)
		{
			Vector3Int vector3Int;
			if (this.TryGetStackableBlockObjectCoordinates(startRay, out vector3Int))
			{
				return Enumerable.Empty<Vector3Int>();
			}
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinatesWithStump(startRay);
			if (traversedCoordinates != null)
			{
				Vector3Int coordinates = traversedCoordinates.GetValueOrDefault().Coordinates;
				Vector3Int removingRectangleEndBlock = this.GetRemovingRectangleEndBlock(coordinates, endRay);
				return this._areaIterator.GetRectangle(coordinates, removingRectangleEndBlock, this._maxBlocks);
			}
			return Enumerable.Empty<Vector3Int>();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004368 File Offset: 0x00002568
		public Vector3Int GetRemovingRectangleEndBlock(Vector3Int startBlock, Ray endRay)
		{
			TraversedCoordinates? traversedCoordinates = this._terrainPicker.PickTerrainCoordinatesWithStump(endRay);
			if (traversedCoordinates != null)
			{
				return traversedCoordinates.Value.Coordinates;
			}
			TraversedCoordinates? traversedCoordinates2 = this._terrainPicker.FindCoordinatesOnLevelInMap(endRay, (float)startBlock.z);
			if (traversedCoordinates2 != null)
			{
				return traversedCoordinates2.GetValueOrDefault().Coordinates;
			}
			return startBlock;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000043CC File Offset: 0x000025CC
		public bool TryGetStackableBlockObjectCoordinates(Ray ray, out Vector3Int coordinates)
		{
			BlockObjectHit blockObjectHit;
			if (this._blockObjectRaycaster.TryHitBlockObject<BlockObject>(ray, out blockObjectHit) && blockObjectHit.HitBlock.Stackable == BlockStackable.BlockObject)
			{
				coordinates = blockObjectHit.HitBlock.Coordinates.Above();
				return true;
			}
			coordinates = default(Vector3Int);
			return false;
		}

		// Token: 0x04000079 RID: 121
		public readonly TerrainPicker _terrainPicker;

		// Token: 0x0400007A RID: 122
		public readonly AreaSelectionController _areaSelectionController;

		// Token: 0x0400007B RID: 123
		public readonly AreaIterator _areaIterator;

		// Token: 0x0400007C RID: 124
		public readonly CameraService _cameraService;

		// Token: 0x0400007D RID: 125
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x0400007E RID: 126
		public readonly BlockObjectRaycaster _blockObjectRaycaster;

		// Token: 0x0400007F RID: 127
		public readonly ISpecService _specService;

		// Token: 0x04000080 RID: 128
		public int _maxBlocks;
	}
}
