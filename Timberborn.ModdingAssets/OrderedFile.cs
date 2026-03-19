using System;
using System.IO;

namespace Timberborn.ModdingAssets
{
	// Token: 0x02000015 RID: 21
	public readonly struct OrderedFile
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000038C3 File Offset: 0x00001AC3
		public int Order { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000038CB File Offset: 0x00001ACB
		public FileInfo File { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000038D3 File Offset: 0x00001AD3
		public string Source { get; }

		// Token: 0x0600006E RID: 110 RVA: 0x000038DB File Offset: 0x00001ADB
		public OrderedFile(int order, FileInfo file, string source)
		{
			this.Order = order;
			this.File = file;
			this.Source = source;
		}
	}
}
