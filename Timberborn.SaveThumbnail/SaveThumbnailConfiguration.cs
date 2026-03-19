using System;
using Timberborn.ThumbnailSystem;
using UnityEngine;

namespace Timberborn.SaveThumbnail
{
	// Token: 0x02000004 RID: 4
	public class SaveThumbnailConfiguration : IThumbnailConfiguration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public int Width
		{
			get
			{
				return 650;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C5 File Offset: 0x000002C5
		public int Height
		{
			get
			{
				return 370;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		public int Quality
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		public TextureFormat TextureFormat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D3 File Offset: 0x000002D3
		public string Name
		{
			get
			{
				return "save_thumbnail.jpg";
			}
		}
	}
}
