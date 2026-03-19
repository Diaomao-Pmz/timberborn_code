using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200000B RID: 11
	public readonly struct DirectedFlow
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000247E File Offset: 0x0000067E
		public DirectedFlow(float flow, int targetIndex3D, int originIndex3D)
		{
			this.Flow = flow;
			this.TargetIndex3D = targetIndex3D;
			this.OriginIndex3D = originIndex3D;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002495 File Offset: 0x00000695
		public DirectedFlow MultiplyFlow(float modifer)
		{
			return new DirectedFlow(this.Flow * modifer, this.TargetIndex3D, this.OriginIndex3D);
		}

		// Token: 0x04000018 RID: 24
		public readonly float Flow;

		// Token: 0x04000019 RID: 25
		public readonly int TargetIndex3D;

		// Token: 0x0400001A RID: 26
		public readonly int OriginIndex3D;
	}
}
