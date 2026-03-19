using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200004E RID: 78
	public static class MatterBelowExtensions
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000063CB File Offset: 0x000045CB
		public static bool IsSolidMatter(this MatterBelow matterBelow)
		{
			return matterBelow == MatterBelow.Ground || matterBelow == MatterBelow.GroundOrStackable || matterBelow == MatterBelow.Stackable;
		}
	}
}
