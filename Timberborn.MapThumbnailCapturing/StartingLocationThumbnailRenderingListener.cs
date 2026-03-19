using System;
using Timberborn.StartingLocationSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000D RID: 13
	public class StartingLocationThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002567 File Offset: 0x00000767
		public StartingLocationThumbnailRenderingListener(StartingLocationService startingLocationService)
		{
			this._startingLocationService = startingLocationService;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002578 File Offset: 0x00000778
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			if (this._startingLocationService.HasStartingLocation())
			{
				StartingLocation startingLocation = this._startingLocationService.GetStartingLocation();
				this._startingLocationRenderer = startingLocation.GetComponent<StartingLocationRenderer>();
				this._startingLocationRenderer.Hide();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025B5 File Offset: 0x000007B5
		public void PostThumbnailRendering()
		{
			if (this._startingLocationRenderer)
			{
				this._startingLocationRenderer.Show();
				this._startingLocationRenderer = null;
			}
		}

		// Token: 0x04000022 RID: 34
		public readonly StartingLocationService _startingLocationService;

		// Token: 0x04000023 RID: 35
		public StartingLocationRenderer _startingLocationRenderer;
	}
}
