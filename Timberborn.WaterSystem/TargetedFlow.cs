using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000020 RID: 32
	public struct TargetedFlow
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003845 File Offset: 0x00001A45
		public TargetedFlow(float flow, int index3D)
		{
			this.Flow = flow;
			this.Index3D = index3D;
		}

		// Token: 0x04000068 RID: 104
		public float Flow;

		// Token: 0x04000069 RID: 105
		public int Index3D;
	}
}
