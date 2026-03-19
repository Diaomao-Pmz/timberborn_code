using System;

namespace Timberborn.WaterSystem
{
	// Token: 0x0200001F RID: 31
	public readonly struct TargetedDiffusion
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003835 File Offset: 0x00001A35
		public TargetedDiffusion(int targetIndex3D, int originIndex3D)
		{
			this.TargetIndex3D = targetIndex3D;
			this.OriginIndex3D = originIndex3D;
		}

		// Token: 0x04000066 RID: 102
		public readonly int TargetIndex3D;

		// Token: 0x04000067 RID: 103
		public readonly int OriginIndex3D;
	}
}
