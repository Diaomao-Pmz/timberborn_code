using System;
using Timberborn.SaveThumbnail;
using Timberborn.SingletonSystem;
using Timberborn.ThumbnailCapturing;
using UnityEngine;

namespace Timberborn.SaveThumbnailCapturing
{
	// Token: 0x02000006 RID: 6
	public class SaveThumbnailRenderTextureProvider : IThumbnailRenderTextureProvider, ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000210D File Offset: 0x0000030D
		public RenderTexture RenderTexture { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002116 File Offset: 0x00000316
		public SaveThumbnailRenderTextureProvider(SaveThumbnailConfiguration saveThumbnailConfiguration)
		{
			this._saveThumbnailConfiguration = saveThumbnailConfiguration;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002125 File Offset: 0x00000325
		public void Load()
		{
			this.RenderTexture = new RenderTexture(this._saveThumbnailConfiguration.Width, this._saveThumbnailConfiguration.Height, 1, 0, 0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000214B File Offset: 0x0000034B
		public void Unload()
		{
			if (this.RenderTexture)
			{
				this.RenderTexture.Release();
				Object.Destroy(this.RenderTexture);
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly SaveThumbnailConfiguration _saveThumbnailConfiguration;
	}
}
