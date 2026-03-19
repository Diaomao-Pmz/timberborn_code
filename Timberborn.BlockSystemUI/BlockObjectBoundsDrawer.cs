using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectBoundsDrawer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public BlockObjectBoundsDrawer(MeshDrawer blockSideMeshDrawer0010, MeshDrawer blockSideMeshDrawer0011, MeshDrawer blockSideMeshDrawer0111, MeshDrawer blockSideMeshDrawer1010, MeshDrawer blockSideMeshDrawer1111)
		{
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0010, false, false, true, false);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0011, false, false, true, true);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer0111, false, true, true, true);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer1010, true, false, true, false);
			this._blockSideMeshDrawers.AddVariants(blockSideMeshDrawer1111, true, true, true, true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002170 File Offset: 0x00000370
		public void DrawBounds(BlockObject blockObject)
		{
			BlockObjectModelController component = blockObject.GetComponent<BlockObjectModelController>();
			if (component == null || component.IsAnyModelShown)
			{
				int bottomLevel = blockObject.CoordinatesAtBaseZ.z;
				IEnumerable<Vector3Int> source = from coordinates in blockObject.PositionedBlocks.GetOccupiedCoordinates()
				where coordinates.z == bottomLevel
				select coordinates;
				this.DrawBounds(source.ToList<Vector3Int>());
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
		public void DrawBounds(ICollection<Vector3Int> occupiedCoordinates)
		{
			foreach (Vector3Int vector3Int in occupiedCoordinates)
			{
				bool flag = BlockObjectBoundsDrawer.VisibleSide(vector3Int, Vector3Int.down, occupiedCoordinates);
				bool flag2 = BlockObjectBoundsDrawer.VisibleSide(vector3Int, Vector3Int.left, occupiedCoordinates);
				bool flag3 = BlockObjectBoundsDrawer.VisibleSide(vector3Int, Vector3Int.up, occupiedCoordinates);
				bool flag4 = BlockObjectBoundsDrawer.VisibleSide(vector3Int, Vector3Int.right, occupiedCoordinates);
				if (flag || flag2 || flag3 || flag4)
				{
					MeshDrawer meshDrawer;
					Orientation orientation;
					this._blockSideMeshDrawers.GetMatch(flag, flag2, flag3, flag4).Deconstruct(out meshDrawer, out orientation);
					MeshDrawer meshDrawer2 = meshDrawer;
					Quaternion rotation = Quaternion.AngleAxis(orientation.ToAngle(), Vector3.up);
					meshDrawer2.DrawAtCoordinates(vector3Int, 0.02f, rotation);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000229C File Offset: 0x0000049C
		public static bool VisibleSide(Vector3Int coordinate, Vector3Int delta, ICollection<Vector3Int> coordinates)
		{
			return !coordinates.Contains(coordinate + delta);
		}

		// Token: 0x04000008 RID: 8
		public readonly NeighboredValues4<MeshDrawer> _blockSideMeshDrawers = new NeighboredValues4<MeshDrawer>();
	}
}
