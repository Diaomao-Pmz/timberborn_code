using System;

namespace Timberborn.Brushes
{
	// Token: 0x0200000C RID: 12
	public interface IBrushWithHeight
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		int BrushHeight { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36
		int MinimumBrushHeight { get; }
	}
}
