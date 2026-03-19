using System;
using Timberborn.BlockSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x02000007 RID: 7
	public static class BlockObjectExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public static bool IsFloor(this BlockObject blockObject)
		{
			foreach (Block block in blockObject.Blocks.GetAllBlocks())
			{
				if (block.Occupation != BlockOccupations.Floor)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		public static int GetBaseLevel(this BlockObject blockObject)
		{
			return Math.Max(0, blockObject.CoordinatesAtBaseZ.z - (blockObject.IsFloor() ? 1 : 0));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002174 File Offset: 0x00000374
		public static int GetTopLevel(this BlockObject blockObject)
		{
			return blockObject.Coordinates.z + blockObject.Blocks.Size.z;
		}
	}
}
