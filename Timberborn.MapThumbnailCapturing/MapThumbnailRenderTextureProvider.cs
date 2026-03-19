using System;
using Timberborn.MapThumbnail;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000009 RID: 9
	public class MapThumbnailRenderTextureProvider : IThumbnailRenderTextureProvider, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002434 File Offset: 0x00000634
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000243C File Offset: 0x0000063C
		public RenderTexture RenderTexture { get; private set; }

		// Token: 0x0600001C RID: 28 RVA: 0x00002445 File Offset: 0x00000645
		public MapThumbnailRenderTextureProvider(MapThumbnailConfiguration mapThumbnailConfiguration)
		{
			this._mapThumbnailConfiguration = mapThumbnailConfiguration;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002454 File Offset: 0x00000654
		public void Load()
		{
			this.RenderTexture = new RenderTexture(this._mapThumbnailConfiguration.Width, this._mapThumbnailConfiguration.Height, 1, 0, 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000247A File Offset: 0x0000067A
		public void Unload()
		{
			this.RenderTexture.Release();
			Object.Destroy(this.RenderTexture);
		}

		// Token: 0x0400001A RID: 26
		public readonly MapThumbnailConfiguration _mapThumbnailConfiguration;
	}
}
