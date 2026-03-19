using System;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.AreaSelectionSystem
{
	// Token: 0x02000022 RID: 34
	public class RectangleBoundsDrawer
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000038D8 File Offset: 0x00001AD8
		public RectangleBoundsDrawer(MeshDrawer blockSideMeshDrawer0010, MeshDrawer blockSideMeshDrawer0011, MeshDrawer blockSideMeshDrawer0111, MeshDrawer blockSideMeshDrawer1010, MeshDrawer blockSideMeshDrawer1111, MeshDrawer blockBottomMeshDrawer)
		{
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0010, false, false, true, false);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0011, false, false, true, true);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0111, false, true, true, true);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer1010, true, false, true, false);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer1111, true, true, true, true);
			this._blockBottomMeshDrawer = blockBottomMeshDrawer;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003950 File Offset: 0x00001B50
		public void DrawOnLevel(Vector2Int start, Vector2Int end, int level)
		{
			ValueTuple<Vector2Int, Vector2Int> valueTuple = Vectors.MinMax(start, end);
			Vector2Int item = valueTuple.Item1;
			Vector2Int item2 = valueTuple.Item2;
			for (int i = item.x; i <= item2.x; i++)
			{
				for (int j = item.y; j <= item2.y; j++)
				{
					Vector3Int block = RectangleBoundsDrawer.ProjectOnLevel(new Vector2Int(i, j), level);
					this.DrawBottom(block);
					this.DrawSides(block, item, item2, level);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000039C3 File Offset: 0x00001BC3
		public void DrawBottom(Vector3Int block)
		{
			this._blockBottomMeshDrawer.DrawAtCoordinates(block, 0.02f);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039D8 File Offset: 0x00001BD8
		public void DrawSides(Vector3Int block, Vector2Int min, Vector2Int max, int minLevel)
		{
			bool flag = RectangleBoundsDrawer.VisibleSide(block, Vector2Int.down, min, max, minLevel);
			bool flag2 = RectangleBoundsDrawer.VisibleSide(block, Vector2Int.left, min, max, minLevel);
			bool flag3 = RectangleBoundsDrawer.VisibleSide(block, Vector2Int.up, min, max, minLevel);
			bool flag4 = RectangleBoundsDrawer.VisibleSide(block, Vector2Int.right, min, max, minLevel);
			if (flag || flag2 || flag3 || flag4)
			{
				MeshDrawer meshDrawer;
				Orientation orientation;
				this._blockSideMeshDrawers.GetMatch(flag, flag2, flag3, flag4).Deconstruct(out meshDrawer, out orientation);
				MeshDrawer meshDrawer2 = meshDrawer;
				Quaternion rotation = Quaternion.AngleAxis(orientation.ToAngle(), Vector3.up);
				meshDrawer2.DrawAtCoordinates(block, 0.02f, rotation);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A6C File Offset: 0x00001C6C
		public static bool VisibleSide(Vector3Int block, Vector2Int neighborOffset, Vector2Int min, Vector2Int max, int minLevel)
		{
			Vector2Int vector2Int = block.XY() + neighborOffset;
			Vector3Int vector3Int = RectangleBoundsDrawer.ProjectOnLevel(vector2Int, minLevel);
			return !RectangleBoundsDrawer.InBounds(vector2Int, min, max) || block.z != vector3Int.z;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static Vector3Int ProjectOnLevel(Vector2Int block, int level)
		{
			return new Vector3Int(block.x, block.y, level);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public static bool InBounds(Vector2Int coordinates, Vector2Int min, Vector2Int max)
		{
			return coordinates.x >= min.x && coordinates.x <= max.x && coordinates.y >= min.y && coordinates.y <= max.y;
		}

		// Token: 0x0400006C RID: 108
		public readonly MeshDrawer _blockBottomMeshDrawer;

		// Token: 0x0400006D RID: 109
		public readonly NeighboredValues4<MeshDrawer> _blockSideMeshDrawers = new NeighboredValues4<MeshDrawer>();
	}
}
