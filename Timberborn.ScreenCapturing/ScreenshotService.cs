using System;
using System.IO;
using Timberborn.FileSystem;
using Timberborn.InputSystem;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using Timberborn.UISound;
using UnityEngine;

namespace Timberborn.ScreenCapturing
{
	// Token: 0x02000005 RID: 5
	public class ScreenshotService : IPriorityInputProcessor, ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		public ScreenshotService(InputService inputService, IFileService fileService, UISoundController uiSoundController)
		{
			this._inputService = inputService;
			this._fileService = fileService;
			this._uiSoundController = uiSoundController;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EE File Offset: 0x000002EE
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public void ProcessInput()
		{
			this._shouldCaptureScreenshot = this._inputService.IsKeyDown(ScreenshotService.ScreenshotKey);
			this._shouldCaptureUpscaledScreenshot = this._inputService.IsKeyDown(ScreenshotService.ScreenshotUpscaledKey);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212A File Offset: 0x0000032A
		public void LateUpdateSingleton()
		{
			if (this._shouldCaptureScreenshot)
			{
				this.CaptureScreenshot(false);
				this._shouldCaptureScreenshot = false;
			}
			if (this._shouldCaptureUpscaledScreenshot)
			{
				this.CaptureScreenshot(true);
				this._shouldCaptureUpscaledScreenshot = false;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002158 File Offset: 0x00000358
		public static string ScreenshotsPath
		{
			get
			{
				return Path.Combine(UserDataFolder.Folder, "Screenshots");
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public void CaptureScreenshot(bool upscale)
		{
			this._fileService.CreateDirectory(ScreenshotService.ScreenshotsPath);
			int num = upscale ? ScreenshotService.UpscalingFactor : 1;
			ScreenCapture.CaptureScreenshot(this.GetScreenshotFilePath(num), num);
			this._uiSoundController.PlaySound(ScreenshotService.ScreenshotSoundName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		public string GetScreenshotFilePath(int upscalingFactor)
		{
			string str = string.Format("{0}x{1}", Screen.width * upscalingFactor, Screen.height * upscalingFactor);
			string str2 = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH\\hmm\\mss\\s");
			string path = str + " - " + str2 + ".png";
			return this._fileService.CombineIntoPath(ScreenshotService.ScreenshotsPath, path, "");
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ScreenshotKey = "Screenshot";

		// Token: 0x04000007 RID: 7
		public static readonly string ScreenshotUpscaledKey = "ScreenshotUpscaled";

		// Token: 0x04000008 RID: 8
		public static readonly string ScreenshotSoundName = "UI.Screenshot";

		// Token: 0x04000009 RID: 9
		public static readonly int UpscalingFactor = 2;

		// Token: 0x0400000A RID: 10
		public readonly InputService _inputService;

		// Token: 0x0400000B RID: 11
		public readonly IFileService _fileService;

		// Token: 0x0400000C RID: 12
		public readonly UISoundController _uiSoundController;

		// Token: 0x0400000D RID: 13
		public bool _shouldCaptureScreenshot;

		// Token: 0x0400000E RID: 14
		public bool _shouldCaptureUpscaledScreenshot;
	}
}
