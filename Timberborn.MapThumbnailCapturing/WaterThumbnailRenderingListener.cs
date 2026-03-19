using System;
using Timberborn.ThumbnailCapturing;
using Timberborn.WaterSystemRendering;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x02000010 RID: 16
	public class WaterThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000270F File Offset: 0x0000090F
		public WaterThumbnailRenderingListener(WaterOpacityService waterOpacityService)
		{
			this._waterOpacityService = waterOpacityService;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000271E File Offset: 0x0000091E
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			if (this._waterOpacityService.IsWaterTransparent)
			{
				this._waterOpacityService.ToggleOpacityOverride();
				this._preRenderingWaterHidden = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000273F File Offset: 0x0000093F
		public void PostThumbnailRendering()
		{
			if (this._preRenderingWaterHidden)
			{
				this._waterOpacityService.ToggleOpacityOverride();
				this._preRenderingWaterHidden = false;
			}
		}

		// Token: 0x04000028 RID: 40
		public readonly WaterOpacityService _waterOpacityService;

		// Token: 0x04000029 RID: 41
		public bool _preRenderingWaterHidden;
	}
}
