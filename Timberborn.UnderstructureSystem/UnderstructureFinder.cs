using System;
using Timberborn.BlockSystem;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x0200000D RID: 13
	public class UnderstructureFinder
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002725 File Offset: 0x00000925
		public UnderstructureFinder(IBlockService blockService)
		{
			this._blockService = blockService;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002734 File Offset: 0x00000934
		public BlockObject FindNonStrict(BlockObject blockObject)
		{
			foreach (Block foundationBlock in blockObject.PositionedBlocks.GetFoundationBlocks())
			{
				if (UnderstructureFinder.BlockShouldBeOnUnderstructure(foundationBlock))
				{
					return this.GetUnderlyingObject(foundationBlock);
				}
			}
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002794 File Offset: 0x00000994
		public BlockObject FindStrict(BlockObject blockObject)
		{
			BlockObject blockObject2 = null;
			foreach (Block foundationBlock in blockObject.PositionedBlocks.GetFoundationBlocks())
			{
				if (UnderstructureFinder.BlockShouldBeOnUnderstructure(foundationBlock))
				{
					BlockObject underlyingObject = this.GetUnderlyingObject(foundationBlock);
					if (underlyingObject == null)
					{
						return null;
					}
					if (blockObject2 == null)
					{
						blockObject2 = underlyingObject;
					}
					else if (blockObject2 != underlyingObject)
					{
						return null;
					}
				}
			}
			return blockObject2;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002810 File Offset: 0x00000A10
		public BlockObject GetUnderlyingObject(Block foundationBlock)
		{
			return this._blockService.GetBottomObjectComponentAt<BlockObject>(foundationBlock.Coordinates);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002824 File Offset: 0x00000A24
		public static bool BlockShouldBeOnUnderstructure(Block foundationBlock)
		{
			return foundationBlock.Occupation.IsTopOrCornersOrBoth();
		}

		// Token: 0x0400001B RID: 27
		public readonly IBlockService _blockService;
	}
}
