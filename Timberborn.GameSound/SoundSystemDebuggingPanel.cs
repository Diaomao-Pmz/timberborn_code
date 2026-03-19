using System;
using Timberborn.CoreSound;
using Timberborn.DebuggingUI;
using Timberborn.SingletonSystem;

namespace Timberborn.GameSound
{
	// Token: 0x02000010 RID: 16
	public class SoundSystemDebuggingPanel : ILoadableSingleton, IDebuggingPanel
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003054 File Offset: 0x00001254
		public SoundSystemDebuggingPanel(DebuggingPanel debuggingPanel, CameraHeightVolumeUpdater cameraHeightVolumeUpdater)
		{
			this._debuggingPanel = debuggingPanel;
			this._cameraHeightVolumeUpdater = cameraHeightVolumeUpdater;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000306A File Offset: 0x0000126A
		public void Load()
		{
			this._debuggingPanel.AddDebuggingPanel(this, "Sound system");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000307D File Offset: 0x0000127D
		public string GetText()
		{
			return string.Format("Camera height: {0}", this._cameraHeightVolumeUpdater.CameraHeight);
		}

		// Token: 0x04000030 RID: 48
		public readonly DebuggingPanel _debuggingPanel;

		// Token: 0x04000031 RID: 49
		public readonly CameraHeightVolumeUpdater _cameraHeightVolumeUpdater;
	}
}
