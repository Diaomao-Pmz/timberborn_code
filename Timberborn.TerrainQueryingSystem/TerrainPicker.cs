using System;
using Timberborn.GridTraversing;
using Timberborn.LevelVisibilitySystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainQueryingSystem
{
	// Token: 0x02000007 RID: 7
	public class TerrainPicker
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000251F File Offset: 0x0000071F
		public TerrainPicker(ITerrainService terrainService, GridTraversal gridTraversal, ILevelVisibilityService levelVisibilityService)
		{
			this._terrainService = terrainService;
			this._gridTraversal = gridTraversal;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000253C File Offset: 0x0000073C
		public TraversedCoordinates? PickTerrainCoordinates(Ray ray)
		{
			return this.PickCoordinates(ray, new Predicate<Vector3Int>(this.IsTerrainVoxel));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002551 File Offset: 0x00000751
		public TraversedCoordinates? PickTerrainCoordinatesWithStump(Ray ray)
		{
			return this.PickCoordinates(ray, new Predicate<Vector3Int>(this.IsTerrainVoxelIncludeTerrainStump));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002568 File Offset: 0x00000768
		public TraversedCoordinates? PickTerrainCoordinates(Ray ray, Predicate<Vector3Int> additionalStopCondition)
		{
			return this.PickCoordinates(ray, (Vector3Int coordinates) => this.IsTerrainVoxel(coordinates) || additionalStopCondition(coordinates));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000259C File Offset: 0x0000079C
		public TraversedCoordinates? FindCoordinatesOnLevelInMap(Ray ray, float level)
		{
			Vector3? vector = GridSpaceRaycasting.HitHorizontalPlane(ray, level);
			if (vector != null)
			{
				Vector3Int coordinates;
				coordinates..ctor(Mathf.FloorToInt(vector.Value.x), Mathf.FloorToInt(vector.Value.y), Mathf.RoundToInt(vector.Value.z));
				return new TraversedCoordinates?(new TraversedCoordinates(this._terrainService.Clamp(coordinates), new Vector3Int(0, 0, 1), vector.Value));
			}
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002624 File Offset: 0x00000824
		public TraversedCoordinates? PickCoordinates(Ray ray, Predicate<Vector3Int> predicate)
		{
			foreach (TraversedCoordinates value in this._gridTraversal.TraverseRay(ray))
			{
				Vector3Int coordinates = value.Coordinates;
				if (predicate(coordinates))
				{
					return new TraversedCoordinates?(value);
				}
			}
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002698 File Offset: 0x00000898
		public bool IsTerrainVoxel(Vector3Int coordinates)
		{
			return this._terrainService.Underground(coordinates) && coordinates.z < this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026BE File Offset: 0x000008BE
		public bool IsTerrainVoxelIncludeTerrainStump(Vector3Int coordinates)
		{
			return this._terrainService.Underground(coordinates) && coordinates.z <= this._levelVisibilityService.MaxVisibleLevel;
		}

		// Token: 0x0400001A RID: 26
		public readonly ITerrainService _terrainService;

		// Token: 0x0400001B RID: 27
		public readonly GridTraversal _gridTraversal;

		// Token: 0x0400001C RID: 28
		public readonly ILevelVisibilityService _levelVisibilityService;
	}
}
