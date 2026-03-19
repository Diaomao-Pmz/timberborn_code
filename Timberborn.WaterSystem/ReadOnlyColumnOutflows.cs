using System;
using System.Collections.Generic;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001C RID: 28
	public readonly struct ReadOnlyColumnOutflows
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000335F File Offset: 0x0000155F
		public TargetedFlow BottomFlow { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003367 File Offset: 0x00001567
		public TargetedFlow LeftFlow { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000336F File Offset: 0x0000156F
		public TargetedFlow TopFlow { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003377 File Offset: 0x00001577
		public TargetedFlow RightFlow { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000337F File Offset: 0x0000157F
		public List<TargetedFlow> Outflows { get; }

		// Token: 0x06000084 RID: 132 RVA: 0x00003387 File Offset: 0x00001587
		public ReadOnlyColumnOutflows(TargetedFlow bottomFlow, TargetedFlow leftFlow, TargetedFlow topFlow, TargetedFlow rightFlow, List<TargetedFlow> outflows)
		{
			this.BottomFlow = bottomFlow;
			this.LeftFlow = leftFlow;
			this.TopFlow = topFlow;
			this.RightFlow = rightFlow;
			this.Outflows = outflows;
		}
	}
}
