using System;
using System.Collections.Generic;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200000B RID: 11
	public interface ITerrainService
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002B RID: 43
		// (remove) Token: 0x0600002C RID: 44
		event EventHandler<TerrainHeightChangeEventArgs> PreTerrainHeightChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600002D RID: 45
		// (remove) Token: 0x0600002E RID: 46
		event EventHandler<TerrainHeightChangeEventArgs> TerrainHeightChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600002F RID: 47
		// (remove) Token: 0x06000030 RID: 48
		event EventHandler MinMaxTerrainHeightChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000031 RID: 49
		// (remove) Token: 0x06000032 RID: 50
		event EventHandler<Vector3Int> FieldOrCutoutChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51
		Vector3Int Size { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52
		int MaxTerrainHeight { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53
		int MinTerrainHeight { get; }

		// Token: 0x06000036 RID: 54
		int GetTerrainHeight(Vector3Int coordinates);

		// Token: 0x06000037 RID: 55
		bool TryGetRelativeHeight(Vector3Int coordinates, out int relativeHeight);

		// Token: 0x06000038 RID: 56
		int GetTerrainHeightBelow(Vector3Int coordinates);

		// Token: 0x06000039 RID: 57
		IEnumerable<Vector3Int> GetAllHeightsInCell(Vector2Int cellCoordinates);

		// Token: 0x0600003A RID: 58
		bool UnsafeCellIsTerrain(int index);

		// Token: 0x0600003B RID: 59
		bool CellIsField(Vector3Int cellCoordinates);

		// Token: 0x0600003C RID: 60
		bool CellIsCutout(Vector3Int cellCoordinates);

		// Token: 0x0600003D RID: 61
		bool Underground(Vector3Int coords);

		// Token: 0x0600003E RID: 62
		bool OnGround(Vector3Int coords);

		// Token: 0x0600003F RID: 63
		void SetTerrain(Vector3Int coordinates, int heightChange = 1);

		// Token: 0x06000040 RID: 64
		void UnsetTerrain(Vector3Int coordinates, int heightChange = 1);

		// Token: 0x06000041 RID: 65
		void UnsetTerrain(HashSet<Vector3Int> coordinates);

		// Token: 0x06000042 RID: 66
		void SetField(Vector3Int coordinates);

		// Token: 0x06000043 RID: 67
		void UnsetField(Vector3Int coordinates);

		// Token: 0x06000044 RID: 68
		void SetCutout(Vector3Int coordinates);

		// Token: 0x06000045 RID: 69
		void UnsetCutout(Vector3Int coordinates);

		// Token: 0x06000046 RID: 70
		bool Contains(Vector2Int coordinates);

		// Token: 0x06000047 RID: 71
		bool Contains(Vector3Int coordinates);

		// Token: 0x06000048 RID: 72
		Vector3Int Clamp(Vector3Int coordinates);

		// Token: 0x06000049 RID: 73
		int GetColumnCount(int index);

		// Token: 0x0600004A RID: 74
		int GetColumnFloor(int index3D);

		// Token: 0x0600004B RID: 75
		int GetColumnCeiling(int index3D);

		// Token: 0x0600004C RID: 76
		bool TryGetDistanceToTerrainAbove(Vector3Int coordinates, out int distance);

		// Token: 0x0600004D RID: 77
		bool IsVisible(Vector3Int coordinates);
	}
}
