using System;

namespace Timberborn.Coordinates
{
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum Directions3D
	{
		// Token: 0x04000028 RID: 40
		None = 0,
		// Token: 0x04000029 RID: 41
		All = -1,
		// Token: 0x0400002A RID: 42
		Down = 1,
		// Token: 0x0400002B RID: 43
		Left = 2,
		// Token: 0x0400002C RID: 44
		Up = 4,
		// Token: 0x0400002D RID: 45
		Right = 8,
		// Token: 0x0400002E RID: 46
		Bottom = 16,
		// Token: 0x0400002F RID: 47
		Top = 32
	}
}
