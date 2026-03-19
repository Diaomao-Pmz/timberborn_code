using System;

namespace Timberborn.ThumbnailCapturing
{
	// Token: 0x02000004 RID: 4
	public interface IThumbnailRenderingListener
	{
		// Token: 0x06000003 RID: 3
		void PreThumbnailRendering(ThumbnailCamera thumbnailCamera);

		// Token: 0x06000004 RID: 4
		void PostThumbnailRendering();
	}
}
