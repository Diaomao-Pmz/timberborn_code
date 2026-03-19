using System;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Timberborn.ScreenSystem
{
	// Token: 0x0200000A RID: 10
	public class ScreenSettingsController : ILoadableSingleton
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002A70 File Offset: 0x00000C70
		public ScreenSettingsController(ScreenSettings screenSettings, CommandLineScreenSettings commandLineScreenSettings, ScreenSettingsLogger screenSettingsLogger)
		{
			this._screenSettings = screenSettings;
			this._commandLineScreenSettings = commandLineScreenSettings;
			this._screenSettingsLogger = screenSettingsLogger;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A8D File Offset: 0x00000C8D
		public void Load()
		{
			this.SubscribeToSettingsEvents();
			this.UpdateSettings();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A9B File Offset: 0x00000C9B
		public void UpdateSettings()
		{
			this.UpdateScreenResolution();
			this.UpdateResolutionScale();
			this.UpdateVSyncCount();
			this.UpdateFrameRateLimit();
			this._screenSettingsLogger.LogBrightnessChange();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public void SubscribeToSettingsEvents()
		{
			this._screenSettings.ScreenResolutionChanged += delegate(object _, SettingChangedEventArgs<ScreenResolution> _)
			{
				this.UpdateSettings();
			};
			this._screenSettings.FullScreenChanged += delegate(object _, SettingChangedEventArgs<bool> _)
			{
				this.UpdateSettings();
			};
			this._screenSettings.ResolutionScaleChanged += delegate(object _, SettingChangedEventArgs<float> _)
			{
				this.UpdateSettings();
			};
			this._screenSettings.VSyncCountChanged += delegate(object _, SettingChangedEventArgs<int> _)
			{
				this.UpdateSettings();
			};
			this._screenSettings.BrightnessChanged += delegate(object _, SettingChangedEventArgs<float> _)
			{
				this.UpdateSettings();
			};
			this._screenSettings.FrameRateLimitChanged += delegate(object _, SettingChangedEventArgs<int?> _)
			{
				this.UpdateSettings();
			};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B58 File Offset: 0x00000D58
		public void UpdateScreenResolution()
		{
			ScreenResolution screenResolution = this._screenSettings.ScreenResolution;
			FullScreenMode fullScreenMode = this._screenSettings.FullScreen ? 1 : 3;
			ScreenResolution currentResolution = new ScreenResolution(Screen.width, Screen.height);
			if (fullScreenMode != Screen.fullScreenMode || screenResolution.Width != currentResolution.Width || screenResolution.Height != currentResolution.Height)
			{
				Screen.SetResolution(screenResolution.Width, screenResolution.Height, fullScreenMode);
				this._screenSettingsLogger.LogResolutionChange(currentResolution, screenResolution);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void UpdateResolutionScale()
		{
			float resolutionScale = this._screenSettings.ResolutionScale;
			if (Math.Abs(ScreenSettingsController.GetCurrentRenderPipeline().renderScale - resolutionScale) > ScreenSettingsController.ResolutionScaleTolerance)
			{
				ScreenSettingsController.GetCurrentRenderPipeline().renderScale = resolutionScale;
				this._screenSettingsLogger.LogResolutionScaleChange();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C28 File Offset: 0x00000E28
		public void UpdateVSyncCount()
		{
			if (!Application.isEditor)
			{
				int num = this._commandLineScreenSettings.Uncapped ? 0 : this._screenSettings.VSyncCount;
				if (QualitySettings.vSyncCount != num)
				{
					QualitySettings.vSyncCount = num;
					this._screenSettingsLogger.LogVSyncCountChange();
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C74 File Offset: 0x00000E74
		public void UpdateFrameRateLimit()
		{
			if (!Application.isEditor)
			{
				int desiredTargetFrameRate = this.GetDesiredTargetFrameRate();
				if (Application.targetFrameRate != desiredTargetFrameRate)
				{
					Application.targetFrameRate = desiredTargetFrameRate;
					this._screenSettingsLogger.LogFrameRateLimitChange();
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public int GetDesiredTargetFrameRate()
		{
			if (this._screenSettings.VSyncCount != 0 || this._screenSettings.FrameRateLimit == null || this._commandLineScreenSettings.Uncapped)
			{
				return -1;
			}
			return this._screenSettings.FrameRateLimit.Value;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public static UniversalRenderPipelineAsset GetCurrentRenderPipeline()
		{
			return (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
		}

		// Token: 0x04000026 RID: 38
		public static readonly float ResolutionScaleTolerance = 0.01f;

		// Token: 0x04000027 RID: 39
		public readonly ScreenSettings _screenSettings;

		// Token: 0x04000028 RID: 40
		public readonly CommandLineScreenSettings _commandLineScreenSettings;

		// Token: 0x04000029 RID: 41
		public readonly ScreenSettingsLogger _screenSettingsLogger;
	}
}
