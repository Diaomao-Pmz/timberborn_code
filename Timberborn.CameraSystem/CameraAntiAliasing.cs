using System;
using Timberborn.GraphicsQualitySystem;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine.Rendering.Universal;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000008 RID: 8
	public class CameraAntiAliasing : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021F2 File Offset: 0x000003F2
		public CameraAntiAliasing(GraphicsQualitySettings graphicsQualitySettings, CameraService cameraService)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._cameraService = cameraService;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		public void Load()
		{
			this.UpdateAntiAliasing(this._graphicsQualitySettings.AntiAliasingType);
			this._graphicsQualitySettings.AntiAliasingTypeChanged += delegate(object _, SettingChangedEventArgs<AntialiasingType> antiAliasingType)
			{
				this.UpdateAntiAliasing(antiAliasingType.Value);
			};
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002234 File Offset: 0x00000434
		public void UpdateAntiAliasing(AntialiasingType antiAliasingType)
		{
			UniversalAdditionalCameraData component = this._cameraService.Transform.GetComponent<UniversalAdditionalCameraData>();
			switch (antiAliasingType)
			{
			case AntialiasingType.Off:
			case AntialiasingType.MSAAx2:
			case AntialiasingType.MSAAx4:
			case AntialiasingType.MSAAx8:
				component.antialiasing = 0;
				return;
			case AntialiasingType.FXAA:
				component.antialiasing = 1;
				return;
			case AntialiasingType.SMAA:
				component.antialiasing = 2;
				return;
			default:
				throw new ArgumentOutOfRangeException("antiAliasingType", antiAliasingType, null);
			}
		}

		// Token: 0x0400000C RID: 12
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x0400000D RID: 13
		public readonly CameraService _cameraService;
	}
}
