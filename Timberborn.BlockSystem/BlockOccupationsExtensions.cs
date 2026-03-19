using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000026 RID: 38
	public static class BlockOccupationsExtensions
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000047FB File Offset: 0x000029FB
		public static bool Intersects(this BlockOccupations a, BlockOccupations b)
		{
			return (a & b) > BlockOccupations.None;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004803 File Offset: 0x00002A03
		public static bool IsBottomOrFloorOrBoth(this BlockOccupations blockOccupations)
		{
			return blockOccupations == BlockOccupations.Floor || blockOccupations == BlockOccupations.Bottom || blockOccupations == (BlockOccupations.Floor | BlockOccupations.Bottom);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004813 File Offset: 0x00002A13
		public static bool IsTopOrCornersOrBoth(this BlockOccupations blockOccupations)
		{
			return blockOccupations == BlockOccupations.Top || blockOccupations == BlockOccupations.Corners || blockOccupations == (BlockOccupations.Top | BlockOccupations.Corners);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004824 File Offset: 0x00002A24
		public static bool HasBottomOrFloorOrFull(this BlockOccupations blockOccupations)
		{
			return blockOccupations.HasFlag(BlockOccupations.Floor) || blockOccupations.HasFlag(BlockOccupations.Bottom) || blockOccupations.IsFull();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004854 File Offset: 0x00002A54
		public static bool IsFull(this BlockOccupations blockOccupations)
		{
			return blockOccupations == BlockOccupations.All;
		}
	}
}
