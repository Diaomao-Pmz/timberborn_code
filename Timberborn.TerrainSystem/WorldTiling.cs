using System;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x0200001B RID: 27
	public static class WorldTiling
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004630 File Offset: 0x00002830
		public static Vector2Int TileCount2D(int xSize, int ySize)
		{
			return new Vector2Int(WorldTiling.HorizontalTileCount(xSize), WorldTiling.HorizontalTileCount(ySize));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004643 File Offset: 0x00002843
		public static Vector3Int TileCount3D(int xSize, int ySize, int zSize)
		{
			return new Vector3Int(WorldTiling.HorizontalTileCount(xSize), WorldTiling.HorizontalTileCount(ySize), WorldTiling.VerticalTileCount(zSize));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000465C File Offset: 0x0000285C
		public static Vector2Int CoordinatesToTileIndex2D(Vector2Int coordinates)
		{
			return WorldTiling.CoordinatesToTileIndex2D(coordinates.x, coordinates.y);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004671 File Offset: 0x00002871
		public static Vector2Int CoordinatesToTileIndex2D(int x, int y)
		{
			return new Vector2Int(x / WorldTiling.HorizontalTileSize, y / WorldTiling.HorizontalTileSize);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004686 File Offset: 0x00002886
		public static Vector3Int CoordinatesToTileIndex3D(Vector3Int coordinates)
		{
			return new Vector3Int(coordinates.x / WorldTiling.HorizontalTileSize, coordinates.y / WorldTiling.HorizontalTileSize, coordinates.z / WorldTiling.VerticalTileSize);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000046B4 File Offset: 0x000028B4
		public static Vector3Int TileIndex3DToCoordinates(int index, int tileCountX, int tileCountY)
		{
			int num = index % tileCountX;
			int num2 = index / tileCountX % tileCountY;
			int num3 = index / (tileCountX * tileCountY);
			return new Vector3Int(num, num2, num3);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000046D8 File Offset: 0x000028D8
		public static TileBounds2D TileBounds2D(Vector2Int tileIndex)
		{
			int x = tileIndex.x;
			int y = tileIndex.y;
			int num = x * WorldTiling.HorizontalTileSize;
			int maxX = num + WorldTiling.HorizontalTileSize;
			int num2 = y * WorldTiling.HorizontalTileSize;
			int maxY = num2 + WorldTiling.HorizontalTileSize;
			return new TileBounds2D(num, num2, maxX, maxY);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000471C File Offset: 0x0000291C
		public static TileBounds3D TileBounds3D(Vector3Int tileIndex)
		{
			int x = tileIndex.x;
			int y = tileIndex.y;
			int z = tileIndex.z;
			int num = x * WorldTiling.HorizontalTileSize;
			int maxX = num + WorldTiling.HorizontalTileSize;
			int num2 = y * WorldTiling.HorizontalTileSize;
			int maxY = num2 + WorldTiling.HorizontalTileSize;
			int num3 = z * WorldTiling.VerticalTileSize - 1;
			int maxZ = num3 + WorldTiling.VerticalTileSize;
			return new TileBounds3D(num, num2, num3, maxX, maxY, maxZ);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004781 File Offset: 0x00002981
		public static int HorizontalTileCount(int size)
		{
			return (size + WorldTiling.HorizontalTileSize - 1) / WorldTiling.HorizontalTileSize;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004792 File Offset: 0x00002992
		public static int VerticalTileCount(int size)
		{
			return (size + WorldTiling.VerticalTileSize - 1) / WorldTiling.VerticalTileSize;
		}

		// Token: 0x0400005B RID: 91
		public static readonly int HorizontalTileSize = 16;

		// Token: 0x0400005C RID: 92
		public static readonly int VerticalTileSize = 8;
	}
}
