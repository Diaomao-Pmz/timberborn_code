using System;

namespace Timberborn.MapMetadataSystem
{
	// Token: 0x02000004 RID: 4
	public class MapMetadata
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public int Width { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public int Height { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public string MapNameLocKey { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		public string MapDescriptionLocKey { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020DE File Offset: 0x000002DE
		public string MapDescription { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E6 File Offset: 0x000002E6
		public bool IsRecommended { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020EE File Offset: 0x000002EE
		public bool IsUnconventional { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F6 File Offset: 0x000002F6
		public bool IsDev { get; }

		// Token: 0x0600000B RID: 11 RVA: 0x00002100 File Offset: 0x00000300
		public MapMetadata(int width, int height, string mapNameLocKey, string mapDescriptionLocKey, string mapDescription, bool isRecommended, bool isUnconventional, bool isDev)
		{
			this.Width = width;
			this.Height = height;
			this.MapNameLocKey = mapNameLocKey;
			this.MapDescriptionLocKey = mapDescriptionLocKey;
			this.MapDescription = mapDescription;
			this.IsRecommended = isRecommended;
			this.IsUnconventional = isUnconventional;
			this.IsDev = isDev;
		}
	}
}
