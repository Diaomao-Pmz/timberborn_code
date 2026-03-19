using System;
using System.Collections.Generic;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000008 RID: 8
	public struct ColumnOutflows
	{
		// Token: 0x0400000C RID: 12
		public TargetedFlow BottomFlow;

		// Token: 0x0400000D RID: 13
		public TargetedFlow LeftFlow;

		// Token: 0x0400000E RID: 14
		public TargetedFlow TopFlow;

		// Token: 0x0400000F RID: 15
		public TargetedFlow RightFlow;

		// Token: 0x04000010 RID: 16
		public List<TargetedFlow> Outflows;
	}
}
