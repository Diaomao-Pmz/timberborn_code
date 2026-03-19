using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000014 RID: 20
	public static class BlockObjectLayoutExtensions
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000358C File Offset: 0x0000178C
		public static int GetPreviewCount(this BlockObjectLayout blockObjectLayout)
		{
			int result;
			switch (blockObjectLayout)
			{
			case BlockObjectLayout.Single:
				result = 1;
				break;
			case BlockObjectLayout.Rectangle:
				result = 100;
				break;
			case BlockObjectLayout.Line:
				result = 25;
				break;
			case BlockObjectLayout.Half:
				result = 2;
				break;
			case BlockObjectLayout.SideLine:
				result = 25;
				break;
			case BlockObjectLayout.TwoSegmentLine:
				result = 40;
				break;
			default:
				throw new ArgumentOutOfRangeException("blockObjectLayout", blockObjectLayout, null);
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035E8 File Offset: 0x000017E8
		public static bool ShouldShowAllPreviews(this BlockObjectLayout blockObjectLayout)
		{
			return blockObjectLayout == BlockObjectLayout.Half;
		}
	}
}
