using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000065 RID: 101
	public readonly struct PreviewState
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00007F5B File Offset: 0x0000615B
		public bool IsBuildable { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00007F63 File Offset: 0x00006163
		public bool IsSingle { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00007F6B File Offset: 0x0000616B
		public bool IsLast { get; }

		// Token: 0x060002A1 RID: 673 RVA: 0x00007F73 File Offset: 0x00006173
		public PreviewState(bool isBuildable, bool isSingle, bool isLast)
		{
			this.IsBuildable = isBuildable;
			this.IsSingle = isSingle;
			this.IsLast = isLast;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00007F8A File Offset: 0x0000618A
		public static PreviewState BuildableSingle
		{
			get
			{
				return new PreviewState(true, true, true);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00007F94 File Offset: 0x00006194
		public static PreviewState UnbuildableSingle
		{
			get
			{
				return new PreviewState(false, true, true);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007F9E File Offset: 0x0000619E
		public static PreviewState BuildableLast
		{
			get
			{
				return new PreviewState(true, false, true);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007FA8 File Offset: 0x000061A8
		public static PreviewState UnbuildableLast
		{
			get
			{
				return new PreviewState(false, false, true);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007FB2 File Offset: 0x000061B2
		public static PreviewState BuildableNotLast
		{
			get
			{
				return new PreviewState(true, false, false);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00007FBC File Offset: 0x000061BC
		public static PreviewState UnbuildableNotLast
		{
			get
			{
				return new PreviewState(false, false, false);
			}
		}
	}
}
