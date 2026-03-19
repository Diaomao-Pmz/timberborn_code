using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.ThumbnailCapturing
{
	// Token: 0x02000008 RID: 8
	public class ThumbnailRenderer
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000219E File Offset: 0x0000039E
		public ThumbnailRenderer(ThumbnailCamera thumbnailCamera, IEnumerable<IThumbnailRenderingListener> thumbnailRenderingListeners)
		{
			this._thumbnailCamera = thumbnailCamera;
			this._thumbnailRenderingListeners = thumbnailRenderingListeners.ToImmutableArray<IThumbnailRenderingListener>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B9 File Offset: 0x000003B9
		public void Render()
		{
			this.PreCameraRendering();
			this._thumbnailCamera.Render();
			this.PostCameraRendering();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D4 File Offset: 0x000003D4
		public void PreCameraRendering()
		{
			foreach (IThumbnailRenderingListener thumbnailRenderingListener in this._thumbnailRenderingListeners)
			{
				thumbnailRenderingListener.PreThumbnailRendering(this._thumbnailCamera);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000220C File Offset: 0x0000040C
		public void PostCameraRendering()
		{
			foreach (IThumbnailRenderingListener thumbnailRenderingListener in this._thumbnailRenderingListeners)
			{
				thumbnailRenderingListener.PostThumbnailRendering();
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly ThumbnailCamera _thumbnailCamera;

		// Token: 0x0400000B RID: 11
		public readonly ImmutableArray<IThumbnailRenderingListener> _thumbnailRenderingListeners;
	}
}
