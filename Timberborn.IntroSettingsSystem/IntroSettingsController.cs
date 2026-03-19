using System;
using UnityEngine.UIElements;

namespace Timberborn.IntroSettingsSystem
{
	// Token: 0x02000006 RID: 6
	public class IntroSettingsController
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002121 File Offset: 0x00000321
		public IntroSettingsController(IntroSettings introSettings)
		{
			this._introSettings = introSettings;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002130 File Offset: 0x00000330
		public void Initialize(VisualElement root)
		{
			this._disableIntroToggle = UQueryExtensions.Q<Toggle>(root, "DisableIntro", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._disableIntroToggle, delegate(ChangeEvent<bool> v)
			{
				this._introSettings.DisableIntro = v.newValue;
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215C File Offset: 0x0000035C
		public void Update()
		{
			this._disableIntroToggle.SetValueWithoutNotify(this._introSettings.DisableIntro);
		}

		// Token: 0x04000008 RID: 8
		public readonly IntroSettings _introSettings;

		// Token: 0x04000009 RID: 9
		public Toggle _disableIntroToggle;
	}
}
