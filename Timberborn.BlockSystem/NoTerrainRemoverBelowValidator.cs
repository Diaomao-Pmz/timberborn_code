using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000050 RID: 80
	public class NoTerrainRemoverBelowValidator : IBlockObjectValidator
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000065B4 File Offset: 0x000047B4
		public NoTerrainRemoverBelowValidator(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000065C4 File Offset: 0x000047C4
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			errorMessage = null;
			ImmutableArray<Block> allBlocks = blockObject.PositionedBlocks.GetAllBlocks();
			bool flag;
			if (blockObject.GetComponent<IGroundMatterBelowInvalidator>() == null)
			{
				flag = allBlocks.FastAll((Block block) => block.MatterBelow > MatterBelow.Ground);
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				foreach (Block block2 in allBlocks)
				{
					if (this.ConflictsWithTerrainRemover(block2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006640 File Offset: 0x00004840
		public bool ConflictsWithTerrainRemover(Block block)
		{
			if (block.MatterBelow == MatterBelow.Ground)
			{
				Vector3Int coordinates = block.Coordinates.Below();
				using (IEnumerator<ITerrainRemovingEntity> enumerator = this._blockService.GetObjectsWithComponentAt<ITerrainRemovingEntity>(coordinates).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.RemovesTerrainAt(coordinates))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x040000EC RID: 236
		public readonly IBlockService _blockService;
	}
}
