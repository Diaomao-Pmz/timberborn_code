using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000032 RID: 50
	public static class BlockStackableExtensions
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005A97 File Offset: 0x00003C97
		public static bool IsStackable(this BlockStackable blockStackable)
		{
			return blockStackable == BlockStackable.BlockObject || blockStackable.IsUnfinishedGround();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public static bool IsUnfinishedGround(this BlockStackable blockStackable)
		{
			return blockStackable == BlockStackable.UnfinishedGround;
		}
	}
}
