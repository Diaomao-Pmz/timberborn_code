using System;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.MapThumbnail
{
	// Token: 0x02000006 RID: 6
	public class MapThumbnailConfiguration : IThumbnailConfiguration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public int Width
		{
			get
			{
				return 960;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212B File Offset: 0x0000032B
		public int Height
		{
			get
			{
				return 540;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002132 File Offset: 0x00000332
		public int Quality
		{
			get
			{
				return 95;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		public TextureFormat TextureFormat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002139 File Offset: 0x00000339
		public string Name
		{
			get
			{
				return "map_thumbnail.jpg";
			}
		}
	}
}
