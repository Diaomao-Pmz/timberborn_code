using System;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000016 RID: 22
	public class NoGroundOnlyBlockAboveValidator : IBlockObjectValidator
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003979 File Offset: 0x00001B79
		public NoGroundOnlyBlockAboveValidator(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003988 File Offset: 0x00001B88
		public bool IsValid(BlockObject blockObject, out string errorMessage)
		{
			errorMessage = null;
			if (blockObject.GetComponent<Tunnel>())
			{
				Vector3Int coordinates = blockObject.Coordinates.Above();
				foreach (BlockObject blockObject2 in this._blockService.GetObjectsAt(coordinates))
				{
					if (blockObject2.GetComponent<IGroundMatterBelowInvalidator>() == null && blockObject2.PositionedBlocks.GetBlock(coordinates).MatterBelow == MatterBelow.Ground)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0400005C RID: 92
		public readonly IBlockService _blockService;
	}
}
