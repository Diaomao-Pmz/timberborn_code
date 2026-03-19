using System;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.ScreenSystem
{
	// Token: 0x0200000B RID: 11
	public class ScreenSettingsLogger : IPostLoadableSingleton
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002D19 File Offset: 0x00000F19
		public ScreenSettingsLogger(ScreenSettings screenSettings)
		{
			this._screenSettings = screenSettings;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D28 File Offset: 0x00000F28
		public void PostLoad()
		{
			this._initialLog = true;
			this._loggingEnabled = true;
			ScreenResolution screenResolution = new ScreenResolution(Screen.width, Screen.height);
			this.LogResolutionChange(screenResolution, screenResolution);
			this.LogResolutionScaleChange();
			this.LogVSyncCountChange();
			this.LogBrightnessChange();
			this.LogFrameRateLimitChange();
			this._initialLog = false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D7C File Offset: 0x00000F7C
		public void LogResolutionChange(ScreenResolution currentResolution, ScreenResolution desiredResolution)
		{
			Display main = Display.main;
			this.Log(string.Format("Previous resolution: {0} x {1}", currentResolution.Width, currentResolution.Height) + string.Format("\nNew resolution {0} x {1}", desiredResolution.Width, desiredResolution.Height) + string.Format("\nDisplay resolution: {0} x {1}", main.systemWidth, main.systemHeight) + string.Format("\nFull screen: {0}", this._screenSettings.FullScreen));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E18 File Offset: 0x00001018
		public void LogResolutionScaleChange()
		{
			this.Log(string.Format("Resolution scale: {0}", this._screenSettings.ResolutionScale));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E3A File Offset: 0x0000103A
		public void LogVSyncCountChange()
		{
			this.Log(string.Format("VSync count: {0}", this._screenSettings.VSyncCount));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E5C File Offset: 0x0000105C
		public void LogBrightnessChange()
		{
			this.Log(string.Format("Brightness: {0}", this._screenSettings.Brightness));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E7E File Offset: 0x0000107E
		public void LogFrameRateLimitChange()
		{
			this.Log(string.Format("Frame rate limit: {0}", this._screenSettings.FrameRateLimit));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EA0 File Offset: 0x000010A0
		public void Log(string logText)
		{
			if (this._loggingEnabled && !Application.isEditor)
			{
				Debug.Log(this._initialLog ? (logText ?? "") : ("Screen settings changed:\n" + logText));
			}
		}

		// Token: 0x0400002A RID: 42
		public readonly ScreenSettings _screenSettings;

		// Token: 0x0400002B RID: 43
		public bool _initialLog;

		// Token: 0x0400002C RID: 44
		public bool _loggingEnabled;
	}
}
