using System;
using Timberborn.TutorialSettingsSystem;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x0200001F RID: 31
	public class TutorialSettingsController
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000043C6 File Offset: 0x000025C6
		public TutorialSettingsController(TutorialSettings tutorialSettings)
		{
			this._tutorialSettings = tutorialSettings;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043D5 File Offset: 0x000025D5
		public void Initialize(VisualElement root)
		{
			this._disableTutorialToggle = UQueryExtensions.Q<Toggle>(root, "DisableTutorial", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._disableTutorialToggle, delegate(ChangeEvent<bool> v)
			{
				this._tutorialSettings.DisableTutorial = v.newValue;
			});
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004401 File Offset: 0x00002601
		public void Update()
		{
			this._disableTutorialToggle.SetValueWithoutNotify(this._tutorialSettings.DisableTutorial);
		}

		// Token: 0x040000A4 RID: 164
		public readonly TutorialSettings _tutorialSettings;

		// Token: 0x040000A5 RID: 165
		public Toggle _disableTutorialToggle;
	}
}
