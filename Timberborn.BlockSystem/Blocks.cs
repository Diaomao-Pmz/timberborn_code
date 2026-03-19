using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Common;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000027 RID: 39
	public class Blocks
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000485A File Offset: 0x00002A5A
		public Vector3Int Size { get; }

		// Token: 0x06000107 RID: 263 RVA: 0x00004862 File Offset: 0x00002A62
		public Blocks(Vector3Int size, ImmutableArray<Block> all)
		{
			this.Size = size;
			this._all = all;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004878 File Offset: 0x00002A78
		public static Blocks From(BlockObjectSpec blockObjectSpec)
		{
			Vector3Int size = blockObjectSpec.Size;
			ImmutableArray<Block> all = (from coordinates in Blocks.GetAllCoordinates(size)
			select Block.From(coordinates, blockObjectSpec.BlockSpecFromCoordinates(coordinates))).ToImmutableArray<Block>();
			return new Blocks(size, all);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000048C0 File Offset: 0x00002AC0
		public IEnumerable<Vector3Int> GetAllCoordinates()
		{
			return Blocks.GetAllCoordinates(this.Size);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048CD File Offset: 0x00002ACD
		public IEnumerable<Vector3Int> GetOccupiedCoordinates()
		{
			return from block in this.GetOccupiedBlocks()
			select block.Coordinates;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000048F9 File Offset: 0x00002AF9
		public ImmutableArray<Block> GetAllBlocks()
		{
			return this._all;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004901 File Offset: 0x00002B01
		public IEnumerable<Block> GetOccupiedBlocks()
		{
			return from block in this._all
			where block.IsOccupied
			select block;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004930 File Offset: 0x00002B30
		public void PositionBlocks(IList<Block> positionedBlocks, Placement placement)
		{
			for (int i = 0; i < this._all.Length; i++)
			{
				Block block = this._all[i];
				Block block2 = this.PositionBlock(block, placement);
				positionedBlocks.Add(block2);
				Blocks.GetBottomBlocks(block2, positionedBlocks);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004978 File Offset: 0x00002B78
		public Vector2Int Transform(Vector2Int coordinates, Placement placement)
		{
			return placement.Orientation.Transform(placement.FlipMode.Transform(coordinates, this.Size.x)) + placement.Coordinates.XY();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000049C0 File Offset: 0x00002BC0
		public Vector3Int Transform(Vector3Int coordinates, Placement placement)
		{
			return placement.Orientation.Transform(placement.FlipMode.Transform(coordinates, this.Size.x)) + placement.Coordinates;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004A04 File Offset: 0x00002C04
		public Vector3 Transform(Vector3 coordinates, Placement placement)
		{
			return placement.Orientation.Transform(placement.FlipMode.Transform(coordinates, this.Size.x)) + this.Pivot(placement.Coordinates, placement.Orientation);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004A54 File Offset: 0x00002C54
		public Vector3 Pivot(Vector3Int coordinates, Orientation orientation)
		{
			return coordinates + orientation.ToPivotOffset();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004A67 File Offset: 0x00002C67
		public static IEnumerable<Vector3Int> GetAllCoordinates(Vector3Int size)
		{
			int num;
			for (int x = 0; x < size.x; x = num)
			{
				for (int y = 0; y < size.y; y = num)
				{
					for (int z = 0; z < size.z; z = num)
					{
						yield return new Vector3Int(x, y, z);
						num = z + 1;
					}
					num = y + 1;
				}
				num = x + 1;
			}
			yield break;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004A77 File Offset: 0x00002C77
		public Block PositionBlock(Block block, Placement placement)
		{
			return Block.From(this.Transform(block.Coordinates, placement), block);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004A90 File Offset: 0x00002C90
		public static void GetBottomBlocks(Block positionedBlock, ICollection<Block> bottomBlocks)
		{
			if (positionedBlock.OccupyAllBelow)
			{
				int num = -1;
				int i = positionedBlock.Coordinates.z - 1;
				while (i >= 0)
				{
					Vector3Int coordinates = positionedBlock.Coordinates + new Vector3Int(0, 0, num);
					bottomBlocks.Add(Block.FullFrom(coordinates));
					i--;
					num--;
				}
			}
		}

		// Token: 0x0400009C RID: 156
		public readonly ImmutableArray<Block> _all;
	}
}
