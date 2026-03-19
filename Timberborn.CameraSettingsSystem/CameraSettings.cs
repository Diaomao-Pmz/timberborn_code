using System;
using Timberborn.SettingsSystem;

namespace Timberborn.CameraSettingsSystem
{
	// Token: 0x02000004 RID: 4
	public class CameraSettings
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public CameraSettings(ISettings settings)
		{
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CA File Offset: 0x000002CA
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020DD File Offset: 0x000002DD
		public bool UnlockZoom
		{
			get
			{
				return this._settings.GetBool(CameraSettings.UnlockZoomKey, false);
			}
			set
			{
				this._settings.SetBool(CameraSettings.UnlockZoomKey, value);
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly string UnlockZoomKey = "UnlockZoom";

		// Token: 0x04000007 RID: 7
		public readonly ISettings _settings;
	}
}
