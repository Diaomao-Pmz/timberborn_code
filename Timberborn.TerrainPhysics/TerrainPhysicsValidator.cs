using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000018 RID: 24
	public class TerrainPhysicsValidator
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003574 File Offset: 0x00001774
		public TerrainPhysicsValidator(ITerrainService terrainService, StackableBlockService stackableBlockService, PreviewBlockService previewBlockService, SupportsToBeDeleted supportsToBeDeleted, bool validatePreviewBlocks)
		{
			this._terrainService = terrainService;
			this._stackableBlockService = stackableBlockService;
			this._previewBlockService = previewBlockService;
			this._supportsToBeDeleted = supportsToBeDeleted;
			this._validatePreviewBlocks = validatePreviewBlocks;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003604 File Offset: 0x00001804
		public void Initialize()
		{
			this.PrepareValidationOffsetData();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000360C File Offset: 0x0000180C
		public void CheckTerrainForDestruction(Queue<Vector3Int> terrainToCheck, HashSet<Vector3Int> terrainQueuedForDestruction)
		{
			while (terrainToCheck.Count > 0)
			{
				while (terrainToCheck.Count > 0)
				{
					this.AddDataToSets(terrainToCheck.Dequeue());
				}
				this.BuildDistanceField();
				foreach (Vector3Int vector3Int in this._checkedArea)
				{
					if (this.AreCoordinatesInvalid(vector3Int) && terrainQueuedForDestruction.Add(vector3Int))
					{
						this._supportsToBeDeleted.Mark(vector3Int);
						Vector3Int vector3Int2 = vector3Int.Above();
						if (!terrainQueuedForDestruction.Contains(vector3Int2) && this.IsUnderground(vector3Int2))
						{
							terrainToCheck.Enqueue(vector3Int2);
						}
						foreach (Vector3Int vector3Int3 in Deltas.Corners4Vector3Int)
						{
							Vector3Int vector3Int4 = vector3Int + vector3Int3;
							if (!terrainQueuedForDestruction.Contains(vector3Int4) && this.IsUnderground(vector3Int4))
							{
								terrainToCheck.Enqueue(vector3Int4);
							}
						}
					}
				}
				this.ClearData();
			}
			this.ClearAllData();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000371C File Offset: 0x0000191C
		public void GetValidTerrainToAdd(ICollection<Vector3Int> inputTerrain, HashSet<Vector3Int> terrainToAdd)
		{
			foreach (Vector3Int vector3Int in inputTerrain)
			{
				this._forcedTerrain.Add(vector3Int);
				this.AddDataToSets(vector3Int);
			}
			this.BuildDistanceField();
			foreach (Vector3Int vector3Int2 in inputTerrain)
			{
				if (this.IsTerrainValid(vector3Int2))
				{
					terrainToAdd.Add(vector3Int2);
				}
			}
			this.ClearAllData();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000037C0 File Offset: 0x000019C0
		public bool ValidateBlockObjectPreview(BlockObject blockObject)
		{
			foreach (Vector3Int vector3Int in blockObject.PositionedBlocks.GetAllCoordinates())
			{
				this._forcedTerrain.Add(vector3Int);
				this.AddDataToSets(vector3Int);
			}
			this.BuildDistanceField();
			bool result = this.ValidateCheckedArea();
			this.ClearAllData();
			return result;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003834 File Offset: 0x00001A34
		public bool CanTerrainBeAdded(Vector3Int coordinates)
		{
			this._forcedTerrain.Add(coordinates);
			this.AddDataToSets(coordinates);
			this.BuildDistanceField();
			bool result = this.IsTerrainValid(coordinates);
			this.ClearAllData();
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003860 File Offset: 0x00001A60
		public bool CanBeDestroyed(BlockObject blockObject)
		{
			ImmutableArray<Block> allBlocks = blockObject.PositionedBlocks.GetAllBlocks();
			this.MarkStackableBlocksForDeletion(allBlocks);
			this.AddDataToSetsFromStackableBlocks(allBlocks);
			this.BuildDistanceField();
			bool result = this.ValidateCheckedArea();
			this.ClearAllData();
			this._supportsToBeDeleted.Clear();
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000038A4 File Offset: 0x00001AA4
		public void PrepareValidationOffsetData()
		{
			for (int i = -TerrainPhysicsValidator.MaxSupportDistanceDoubled; i <= TerrainPhysicsValidator.MaxSupportDistanceDoubled; i++)
			{
				int num = Mathf.Abs(i);
				for (int j = -TerrainPhysicsValidator.MaxSupportDistanceDoubled; j <= TerrainPhysicsValidator.MaxSupportDistanceDoubled; j++)
				{
					int num2 = Mathf.Abs(j) + num;
					Vector3Int item;
					item..ctor(j, i, 0);
					if (num2 <= TerrainPhysicsValidator.MaxSupportDistanceDoubled)
					{
						this._extendedSearchAreaOffsets.Add(item);
						if (num2 <= TerrainPhysicsValidator.MaxSupportDistance)
						{
							this._checkedAreaOffsets.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003920 File Offset: 0x00001B20
		public void AddDataToSets(Vector3Int coordinates)
		{
			if (this._addedDataPoints.Add(coordinates))
			{
				foreach (Vector3Int vector3Int in this._extendedSearchAreaOffsets)
				{
					Vector3Int vector3Int2 = coordinates + vector3Int;
					if (this.IsUnderground(vector3Int2))
					{
						if (this._checkedAreaOffsets.Contains(vector3Int))
						{
							this._checkedArea.Add(vector3Int2);
						}
						if (this._extendedSearchArea.Add(vector3Int2) && this.HasSupport(vector3Int2))
						{
							this._coordinatesToVisit.Enqueue(vector3Int2);
							this._distanceField[vector3Int2] = 0;
						}
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000039DC File Offset: 0x00001BDC
		public void BuildDistanceField()
		{
			while (this._coordinatesToVisit.Count > 0)
			{
				Vector3Int vector3Int = this._coordinatesToVisit.Dequeue();
				int num = this._distanceField[vector3Int] + 1;
				bool flag = this._terrainService.Underground(vector3Int);
				foreach (Vector3Int vector3Int2 in Deltas.Neighbors4Vector3Int)
				{
					Vector3Int vector3Int3 = vector3Int + vector3Int2;
					bool flag2 = this._terrainService.Underground(vector3Int3);
					int num2;
					if ((flag || !flag2) && this._extendedSearchArea.Contains(vector3Int3) && this.IsUnderground(vector3Int3) && !this._supportsToBeDeleted.IsMarked(vector3Int3) && (!this._distanceField.TryGetValue(vector3Int3, out num2) || num2 > num))
					{
						this._distanceField[vector3Int3] = num;
						this._coordinatesToVisit.Enqueue(vector3Int3);
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public bool AreCoordinatesInvalid(Vector3Int coordinates)
		{
			int num;
			return this.IsUnderground(coordinates) && (!this._distanceField.TryGetValue(coordinates, out num) || num > TerrainPhysicsValidator.MaxSupportDistance);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003AFA File Offset: 0x00001CFA
		public void ClearAllData()
		{
			this._forcedTerrain.Clear();
			this._addedDataPoints.Clear();
			this.ClearData();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003B18 File Offset: 0x00001D18
		public void ClearData()
		{
			this._extendedSearchArea.Clear();
			this._checkedArea.Clear();
			this._distanceField.Clear();
			this._forcedTerrain.Clear();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B48 File Offset: 0x00001D48
		public bool IsTerrainValid(Vector3Int coordinates)
		{
			int num;
			return this._distanceField.TryGetValue(coordinates, out num) && num <= TerrainPhysicsValidator.MaxSupportDistance;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B74 File Offset: 0x00001D74
		public bool ValidateCheckedArea()
		{
			foreach (Vector3Int vector3Int in this._checkedArea)
			{
				int num;
				if (!this._supportsToBeDeleted.IsMarked(vector3Int) && (!this._distanceField.TryGetValue(vector3Int, out num) || num > TerrainPhysicsValidator.MaxSupportDistance))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public void MarkStackableBlocksForDeletion(ImmutableArray<Block> allBlocks)
		{
			foreach (Block block in allBlocks)
			{
				if (block.Stackable.IsStackable())
				{
					this._supportsToBeDeleted.Mark(block.Coordinates);
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003C38 File Offset: 0x00001E38
		public void AddDataToSetsFromStackableBlocks(ImmutableArray<Block> allBlocks)
		{
			foreach (Block block in allBlocks)
			{
				if (block.Stackable.IsStackable())
				{
					Vector3Int coordinates = block.Coordinates;
					this.AddDataToSets(coordinates.Above());
					this.AddDataToSets(coordinates);
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C8C File Offset: 0x00001E8C
		public bool IsUnderground(Vector3Int coordinates)
		{
			return !this._supportsToBeDeleted.IsMarked(coordinates) && (this._forcedTerrain.Contains(coordinates) || this._terrainService.Underground(coordinates) || (this._validatePreviewBlocks && (this._stackableBlockService.IsUnfinishedGroundBlockAt(coordinates) || this._previewBlockService.IsUnfinishedGroundBlockAt(coordinates))));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public bool HasSupport(Vector3Int coordinates)
		{
			Vector3Int vector3Int = coordinates.Below();
			return !this._supportsToBeDeleted.IsMarked(vector3Int) && (this.PreviewHasSupport(coordinates, vector3Int) || this.TerrainHasSupport(coordinates, vector3Int));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D28 File Offset: 0x00001F28
		public bool PreviewHasSupport(Vector3Int coordinates, Vector3Int coordinatesBelow)
		{
			return this._validatePreviewBlocks && (this._forcedTerrain.Contains(coordinates) || this._stackableBlockService.IsUnfinishedGroundBlockAt(coordinates) || this._previewBlockService.IsUnfinishedGroundBlockAt(coordinates)) && (this._forcedTerrain.Contains(coordinatesBelow) || this._terrainService.Underground(coordinatesBelow) || this._stackableBlockService.IsStackableBlockAt(coordinatesBelow, false) || this._previewBlockService.IsUnfinishedGroundBlockAt(coordinatesBelow));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public bool TerrainHasSupport(Vector3Int coordinates, Vector3Int coordinatesBelow)
		{
			return (this._forcedTerrain.Contains(coordinates) || this._terrainService.Underground(coordinates)) && (this._forcedTerrain.Contains(coordinatesBelow) || this._terrainService.Underground(coordinatesBelow) || this._stackableBlockService.IsFinishedStackableBlockAt(coordinatesBelow));
		}

		// Token: 0x0400003D RID: 61
		public static readonly int MaxSupportDistance = 3;

		// Token: 0x0400003E RID: 62
		public static readonly int MaxSupportDistanceDoubled = TerrainPhysicsValidator.MaxSupportDistance * 2;

		// Token: 0x0400003F RID: 63
		public readonly ITerrainService _terrainService;

		// Token: 0x04000040 RID: 64
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000041 RID: 65
		public readonly PreviewBlockService _previewBlockService;

		// Token: 0x04000042 RID: 66
		public readonly SupportsToBeDeleted _supportsToBeDeleted;

		// Token: 0x04000043 RID: 67
		public readonly bool _validatePreviewBlocks;

		// Token: 0x04000044 RID: 68
		public readonly HashSet<Vector3Int> _forcedTerrain = new HashSet<Vector3Int>();

		// Token: 0x04000045 RID: 69
		public readonly HashSet<Vector3Int> _extendedSearchArea = new HashSet<Vector3Int>();

		// Token: 0x04000046 RID: 70
		public readonly Queue<Vector3Int> _coordinatesToVisit = new Queue<Vector3Int>();

		// Token: 0x04000047 RID: 71
		public readonly HashSet<Vector3Int> _checkedArea = new HashSet<Vector3Int>();

		// Token: 0x04000048 RID: 72
		public readonly HashSet<Vector3Int> _addedDataPoints = new HashSet<Vector3Int>();

		// Token: 0x04000049 RID: 73
		public readonly Dictionary<Vector3Int, int> _distanceField = new Dictionary<Vector3Int, int>();

		// Token: 0x0400004A RID: 74
		public readonly List<Vector3Int> _extendedSearchAreaOffsets = new List<Vector3Int>();

		// Token: 0x0400004B RID: 75
		public readonly HashSet<Vector3Int> _checkedAreaOffsets = new HashSet<Vector3Int>();
	}
}
