using System;
using Timberborn.AccessibilitySettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000004 RID: 4
	public class AccessibilitySettingsController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public AccessibilitySettingsController(AccessibilitySettings accessibilitySettings)
		{
			this._accessibilitySettings = accessibilitySettings;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public void Initialize(VisualElement root)
		{
			this._disableStarfieldRotation = UQueryExtensions.Q<Toggle>(root, "DisableStarfieldRotation", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._disableStarfieldRotation, delegate(ChangeEvent<bool> v)
			{
				this._accessibilitySettings.StarfieldRotationDisabled = v.newValue;
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FB File Offset: 0x000002FB
		public void Update()
		{
			this._disableStarfieldRotation.SetValueWithoutNotify(this._accessibilitySettings.StarfieldRotationDisabled);
		}

		// Token: 0x04000006 RID: 6
		public readonly AccessibilitySettings _accessibilitySettings;

		// Token: 0x04000007 RID: 7
		public Toggle _disableStarfieldRotation;
	}
}
