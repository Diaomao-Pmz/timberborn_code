using System;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000B RID: 11
	[Flags]
	public enum Directions2D
	{
		// Token: 0x04000021 RID: 33
		None = 0,
		// Token: 0x04000022 RID: 34
		All = -1,
		// Token: 0x04000023 RID: 35
		Down = 1,
		// Token: 0x04000024 RID: 36
		Left = 2,
		// Token: 0x04000025 RID: 37
		Up = 4,
		// Token: 0x04000026 RID: 38
		Right = 8
	}
}
