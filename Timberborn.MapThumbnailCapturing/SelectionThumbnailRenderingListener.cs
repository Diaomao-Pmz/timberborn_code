using System;
using Timberborn.SelectionSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000B RID: 11
	public class SelectionThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000024E2 File Offset: 0x000006E2
		public SelectionThumbnailRenderingListener(EntitySelectionService entitySelectionService)
		{
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024F1 File Offset: 0x000006F1
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			this._entitySelectionService.UnhighlightUntilNextUpdate();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024FE File Offset: 0x000006FE
		public void PostThumbnailRendering()
		{
		}

		// Token: 0x0400001E RID: 30
		public readonly EntitySelectionService _entitySelectionService;
	}
}
