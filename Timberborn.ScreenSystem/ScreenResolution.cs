using System;

namespace Timberborn.ScreenSystem
{
	// Token: 0x02000005 RID: 5
	public readonly struct ScreenResolution
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002102 File Offset: 0x00000302
		public int Width { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210A File Offset: 0x0000030A
		public int Height { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002112 File Offset: 0x00000312
		public ScreenResolution(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}
	}
}
