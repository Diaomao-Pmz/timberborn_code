using System;
using UnityEngine;

namespace Timberborn.ThumbnailSystem
{
	// Token: 0x02000004 RID: 4
	public interface IThumbnailConfiguration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		int Width { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		int Height { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5
		int Quality { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6
		TextureFormat TextureFormat { get; }
	}
}
