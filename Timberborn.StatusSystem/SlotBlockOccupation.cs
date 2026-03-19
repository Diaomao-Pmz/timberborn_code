using System;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200000C RID: 12
	public static class SlotBlockOccupation
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002475 File Offset: 0x00000675
		public static BlockOccupations GetOccupation(Vector2Int key, bool isMiddleSlot)
		{
			if (key.x % 2 == 0 && key.y % 2 == 0)
			{
				return SlotBlockOccupation.TileCorner;
			}
			if (!isMiddleSlot)
			{
				return SlotBlockOccupation.Default;
			}
			return SlotBlockOccupation.Middle;
		}

		// Token: 0x04000011 RID: 17
		public static readonly BlockOccupations Default = ~(BlockOccupations.Top | BlockOccupations.Corners);

		// Token: 0x04000012 RID: 18
		public static readonly BlockOccupations TileCorner = BlockOccupations.All;

		// Token: 0x04000013 RID: 19
		public static readonly BlockOccupations Middle = ~BlockOccupations.Corners;
	}
}
