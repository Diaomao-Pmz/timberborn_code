using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000056 RID: 86
	public class PositionedBlocks
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00006D2E File Offset: 0x00004F2E
		public PositionedBlocks(ImmutableArray<Block> all)
		{
			this._all = all;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006D40 File Offset: 0x00004F40
		public static PositionedBlocks From(Blocks blocks, Placement placement)
		{
			List<Block> list = new List<Block>();
			blocks.PositionBlocks(list, placement);
			return new PositionedBlocks(list.ToImmutableArray<Block>());
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006D66 File Offset: 0x00004F66
		public ImmutableArray<Block> GetAllBlocks()
		{
			return this._all;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006D6E File Offset: 0x00004F6E
		public IEnumerable<Block> GetOccupiedBlocks()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsOccupied)
				{
					yield return block;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006D7E File Offset: 0x00004F7E
		public IEnumerable<Block> GetOccupiedStackableBlocks()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsOccupied && block.Stackable.IsStackable())
				{
					yield return block;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006D8E File Offset: 0x00004F8E
		public IEnumerable<Block> GetOccupiedAndUndergroundBlocks()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsOccupied || block.Underground)
				{
					yield return block;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006D9E File Offset: 0x00004F9E
		public IEnumerable<Block> GetFoundationBlocks()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsFoundationBlock)
				{
					yield return block;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006DAE File Offset: 0x00004FAE
		public IEnumerable<Vector3Int> GetFoundationCoordinates()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsFoundationBlock)
				{
					yield return block.Coordinates;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006DBE File Offset: 0x00004FBE
		public IEnumerable<Vector3Int> GetAllCoordinates()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				yield return this._all[i].Coordinates;
				num = i;
			}
			yield break;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006DCE File Offset: 0x00004FCE
		public IEnumerable<Vector3Int> GetOccupiedCoordinates()
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.IsOccupied)
				{
					yield return block.Coordinates;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006DDE File Offset: 0x00004FDE
		public IEnumerable<Vector3Int> GetOccupiedCoordinatesIntersecting(BlockOccupations occupation)
		{
			int num;
			for (int i = 0; i < this._all.Length; i = num + 1)
			{
				Block block = this._all[i];
				if (block.Occupation.Intersects(occupation))
				{
					yield return block.Coordinates;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public bool TryGetBlock(Vector3Int coordinates, out Block result)
		{
			result = default(Block);
			for (int i = 0; i < this._all.Length; i++)
			{
				Block block = this._all[i];
				if (block.Coordinates == coordinates)
				{
					result = block;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006E48 File Offset: 0x00005048
		public Block GetBlock(Vector3Int coordinates)
		{
			Block result;
			if (this.TryGetBlock(coordinates, out result))
			{
				return result;
			}
			throw new NullReferenceException(string.Format("No {0} found at {1}", "Block", coordinates));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006E7C File Offset: 0x0000507C
		public bool HasIntersectingBlock(Block block)
		{
			for (int i = 0; i < this._all.Length; i++)
			{
				if (this._all[i].IsIntersecting(block))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006EBC File Offset: 0x000050BC
		public bool HasBlockAt(Vector3Int coordinates)
		{
			foreach (Block block in this._all)
			{
				if (block.Coordinates == coordinates)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006EFC File Offset: 0x000050FC
		public bool HasStackableBlockAt(Vector3Int coordinates)
		{
			foreach (Block block in this._all)
			{
				if (block.Coordinates == coordinates && block.Stackable.IsStackable())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000FC RID: 252
		public readonly ImmutableArray<Block> _all;
	}
}
