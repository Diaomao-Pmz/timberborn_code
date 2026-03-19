using System;
using Timberborn.CameraSystem;
using Timberborn.ThumbnailCapturing;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000C RID: 12
	public class ShadowThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002500 File Offset: 0x00000700
		public ShadowThumbnailRenderingListener(MapThumbnailCameraMover mapThumbnailCameraMover, ShadowDistanceUpdater shadowDistanceUpdater)
		{
			this._mapThumbnailCameraMover = mapThumbnailCameraMover;
			this._shadowDistanceUpdater = shadowDistanceUpdater;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002518 File Offset: 0x00000718
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			this._preRenderingShadowDistance = this._shadowDistanceUpdater.GetShadowDistance();
			this._shadowDistanceUpdater.SetShadowDistance(this._mapThumbnailCameraMover.CurrentConfiguration.ShadowDistance);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002554 File Offset: 0x00000754
		public void PostThumbnailRendering()
		{
			this._shadowDistanceUpdater.SetShadowDistance(this._preRenderingShadowDistance);
		}

		// Token: 0x0400001F RID: 31
		public readonly MapThumbnailCameraMover _mapThumbnailCameraMover;

		// Token: 0x04000020 RID: 32
		public readonly ShadowDistanceUpdater _shadowDistanceUpdater;

		// Token: 0x04000021 RID: 33
		public float _preRenderingShadowDistance;
	}
}
