using System;

namespace Timberborn.Brushes
{
	// Token: 0x0200000B RID: 11
	public interface IBrushWithDirection
	{
		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600001F RID: 31
		bool Increase { set; }

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x06000020 RID: 32
		bool Inverse { set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33
		bool IsIncreasing { get; }
	}
}
