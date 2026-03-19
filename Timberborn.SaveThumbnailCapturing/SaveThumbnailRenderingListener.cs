using System;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.SaveThumbnailCapturing
{
	// Token: 0x02000005 RID: 5
	public class SaveThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020FB File Offset: 0x000002FB
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			thumbnailCamera.MoveToMainCameraPosition();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		public void PostThumbnailRendering()
		{
		}
	}
}
