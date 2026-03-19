using System;
using Timberborn.CameraSettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000009 RID: 9
	public class CameraSettingsController
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002418 File Offset: 0x00000618
		public CameraSettingsController(CameraSettings cameraSettings)
		{
			this._cameraSettings = cameraSettings;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002427 File Offset: 0x00000627
		public void Initialize(VisualElement root)
		{
			this._unlockZoomToggle = UQueryExtensions.Q<Toggle>(root, "UnlockZoom", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._unlockZoomToggle, delegate(ChangeEvent<bool> v)
			{
				this._cameraSettings.UnlockZoom = v.newValue;
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002453 File Offset: 0x00000653
		public void Update()
		{
			this._unlockZoomToggle.SetValueWithoutNotify(this._cameraSettings.UnlockZoom);
		}

		// Token: 0x04000018 RID: 24
		public readonly CameraSettings _cameraSettings;

		// Token: 0x04000019 RID: 25
		public Toggle _unlockZoomToggle;
	}
}
