using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.GridTraversing;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SelectionSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000011 RID: 17
	public class BlockObjectPreviewPicker
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A92 File Offset: 0x00000C92
		public BlockObjectPreviewPicker(ITerrainService terrainService, GridTraversal gridTraversal, StackableBlockService stackableBlockService, ILevelVisibilityService levelVisibilityService, SelectableObjectRaycaster selectableObjectRaycaster)
		{
			this._terrainService = terrainService;
			this._gridTraversal = gridTraversal;
			this._stackableBlockService = stackableBlockService;
			this._levelVisibilityService = levelVisibilityService;
			this._selectableObjectRaycaster = selectableObjectRaycaster;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public PickedCoordinates? CenteredPreviewCoordinates(PlaceableBlockObjectSpec placeableBlockObjectSpec, Orientation orientation, Ray ray)
		{
			BlockObjectSpec spec = placeableBlockObjectSpec.GetSpec<BlockObjectSpec>();
			CustomPivotSpec customPivot = placeableBlockObjectSpec.CustomPivot;
			BlockObject objectHitByRaycast = this.GetObjectHitByRaycast();
			bool canBeAttachedToTerrainSide = placeableBlockObjectSpec.CanBeAttachedToTerrainSide;
			bool flag = spec.Blocks.FastAll((BlockSpec block) => block.Underground);
			foreach (TraversedCoordinates candidate in this._gridTraversal.TraverseRay(ray))
			{
				Vector3Int coordinates = candidate.Coordinates;
				bool flag2 = candidate.Face.z == 1;
				if (this._terrainService.Contains(coordinates.XY()))
				{
					if (flag2)
					{
						if ((flag && this.IsTerrainWithStump(coordinates)) || (!flag && this.IsTerrainOrStackable(coordinates)))
						{
							Vector3Int vector3Int = BlockObjectPreviewPicker.ComposeCoordinates(orientation, customPivot, spec, candidate);
							return flag ? new PickedCoordinates?(new PickedCoordinates(vector3Int.Below(), vector3Int.z, -1, canBeAttachedToTerrainSide)) : new PickedCoordinates?(new PickedCoordinates(vector3Int, vector3Int.z, 0, canBeAttachedToTerrainSide));
						}
					}
					else if ((flag && this.IsTerrainWithStump(coordinates)) || (canBeAttachedToTerrainSide && this.IsTerrainOrUnfinishedTerrain(coordinates)))
					{
						Vector3Int vector3Int2 = BlockObjectPreviewPicker.ComposeCoordinates(orientation, customPivot, spec, candidate);
						if (canBeAttachedToTerrainSide)
						{
							vector3Int2 += candidate.Face;
						}
						bool filterOverhangingCoordinates = this.HasStackableBelow(vector3Int2);
						return flag ? new PickedCoordinates?(new PickedCoordinates(vector3Int2, vector3Int2.z + 1, -1, filterOverhangingCoordinates)) : new PickedCoordinates?(new PickedCoordinates(vector3Int2, vector3Int2.z, 0, filterOverhangingCoordinates));
					}
				}
				if (BlockObjectPreviewPicker.ShouldObjectBlockCoordinates(objectHitByRaycast, coordinates))
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public BlockObject GetObjectHitByRaycast()
		{
			SelectableObject selectableObject;
			if (this._selectableObjectRaycaster.TryHitSelectableObject(out selectableObject))
			{
				Preview component = selectableObject.GetComponent<Preview>();
				if (component && !component.PreviewState.IsLast)
				{
					return selectableObject.GetComponent<BlockObject>();
				}
			}
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CFB File Offset: 0x00000EFB
		public bool IsTerrainOrUnfinishedTerrain(Vector3Int coords)
		{
			return (this._terrainService.Underground(coords) || this._stackableBlockService.IsUnfinishedGroundBlockAt(coords)) && coords.z <= this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D32 File Offset: 0x00000F32
		public bool IsTerrainOrStackable(Vector3Int coords)
		{
			return this.IsTerrain(coords) || this.IsStackable(coords);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D46 File Offset: 0x00000F46
		public bool IsTerrainWithStump(Vector3Int coords)
		{
			return this._terrainService.Underground(coords) && coords.z <= this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D70 File Offset: 0x00000F70
		public bool HasStackableBelow(Vector3Int coordinates)
		{
			Vector3Int coords = coordinates.Below();
			return this._terrainService.Underground(coords) || this._stackableBlockService.IsStackableBlockAt(coords, false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public bool IsTerrain(Vector3Int coords)
		{
			return this._terrainService.Underground(coords) && coords.z < this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public bool IsStackable(Vector3Int coords)
		{
			return this._levelVisibilityService.BlockIsVisible(coords + new Vector3Int(0, 0, 1)) && this._stackableBlockService.IsStackableBlockAt(coords, false);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static Vector3Int ComposeCoordinates(Orientation orientation, CustomPivotSpec customPivot, BlockObjectSpec blockObjectSpec, TraversedCoordinates candidate)
		{
			Vector3 centerOffset = customPivot.HasCustomPivot ? orientation.Transform(customPivot.Coordinates) : blockObjectSpec.CalculateCenterOffset(orientation);
			return BlockObjectPreviewPicker.CenterCoordinates(candidate, centerOffset) - new Vector3Int(0, 0, blockObjectSpec.BaseZ);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E38 File Offset: 0x00001038
		public static Vector3Int CenterCoordinates(TraversedCoordinates traversedCoordinates, Vector3 centerOffset)
		{
			Vector3 vector = BlockObjectPreviewPicker.FaceAdjustedIntersection(traversedCoordinates);
			Vector3 vector2;
			vector2..ctor(vector.x - centerOffset.x + 0.5f * Mathf.Sign(centerOffset.x), vector.y - centerOffset.y + 0.5f * Mathf.Sign(centerOffset.y), vector.z);
			return new Vector3Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y), Mathf.FloorToInt(vector2.z));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EC0 File Offset: 0x000010C0
		public static Vector3 FaceAdjustedIntersection(TraversedCoordinates traversedCoordinates)
		{
			Vector3Int face = traversedCoordinates.Face;
			Vector3 vector;
			vector..ctor((float)face.x * -0.001f, (float)face.y * -0.001f, 0.001f);
			return traversedCoordinates.Intersection + vector;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F0C File Offset: 0x0000110C
		public static bool ShouldObjectBlockCoordinates(BlockObject blockObject, Vector3Int coordinates)
		{
			Block block;
			return blockObject && !blockObject.IsPreview && !blockObject.Overridable && blockObject.PositionedBlocks.TryGetBlock(coordinates, out block) && block.Occupation.IsFull() && BlockObjectPreviewPicker.DoesBlockAboveHasFullOccupation(blockObject, block);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F58 File Offset: 0x00001158
		public static bool DoesBlockAboveHasFullOccupation(BlockObject blockObject, Block block)
		{
			Vector3Int coordinates;
			coordinates..ctor(block.Coordinates.x, block.Coordinates.y, block.Coordinates.z + 1);
			Block block2;
			return blockObject.PositionedBlocks.TryGetBlock(coordinates, out block2) && block2.Occupation.IsFull();
		}

		// Token: 0x04000034 RID: 52
		public readonly ITerrainService _terrainService;

		// Token: 0x04000035 RID: 53
		public readonly GridTraversal _gridTraversal;

		// Token: 0x04000036 RID: 54
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000037 RID: 55
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000038 RID: 56
		public readonly SelectableObjectRaycaster _selectableObjectRaycaster;
	}
}
