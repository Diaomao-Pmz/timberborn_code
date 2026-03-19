using System;
using Timberborn.SkySystem;
using Timberborn.ThumbnailCapturing;
using UnityEngine;

namespace Timberborn.MapThumbnailCapturing
{
	// Token: 0x0200000E RID: 14
	public class SunThumbnailRenderingListener : IThumbnailRenderingListener
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000025D6 File Offset: 0x000007D6
		public SunThumbnailRenderingListener(Sun sun)
		{
			this._sun = sun;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E8 File Offset: 0x000007E8
		public void PreThumbnailRendering(ThumbnailCamera thumbnailCamera)
		{
			float cameraYAngle = this._sun.GetCameraYAngle(thumbnailCamera.Transform);
			Transform transform = this._sun.Transform;
			Vector3 eulerAngles = transform.eulerAngles;
			this._preRenderingSunYAngle = eulerAngles.y;
			transform.eulerAngles = new Vector3(eulerAngles.x, cameraYAngle, eulerAngles.z);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000263C File Offset: 0x0000083C
		public void PostThumbnailRendering()
		{
			Transform transform = this._sun.Transform;
			Vector3 eulerAngles = transform.eulerAngles;
			transform.eulerAngles = new Vector3(eulerAngles.x, this._preRenderingSunYAngle, eulerAngles.z);
		}

		// Token: 0x04000024 RID: 36
		public readonly Sun _sun;

		// Token: 0x04000025 RID: 37
		public float _preRenderingSunYAngle;
	}
}
