using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000025 RID: 37
	[Flags]
	public enum BlockOccupations
	{
		// Token: 0x04000093 RID: 147
		None = 0,
		// Token: 0x04000094 RID: 148
		All = -1,
		// Token: 0x04000095 RID: 149
		Floor = 1,
		// Token: 0x04000096 RID: 150
		Bottom = 2,
		// Token: 0x04000097 RID: 151
		Top = 4,
		// Token: 0x04000098 RID: 152
		Corners = 8,
		// Token: 0x04000099 RID: 153
		Path = 16,
		// Token: 0x0400009A RID: 154
		Middle = 32
	}
}
