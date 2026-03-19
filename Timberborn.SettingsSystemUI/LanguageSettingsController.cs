using System;
using Timberborn.LanguageUI;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000014 RID: 20
	public class LanguageSettingsController
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000033FE File Offset: 0x000015FE
		public LanguageSettingsController(ChangeLanguageBox changeLanguageBox)
		{
			this._changeLanguageBox = changeLanguageBox;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000340D File Offset: 0x0000160D
		public void Initialize(VisualElement root)
		{
			UQueryExtensions.Q<Label>(root, "LanguageName", null).text = this._changeLanguageBox.LocalizedCurrentLanguageName;
			UQueryExtensions.Q<Button>(root, "LanguageChange", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._changeLanguageBox.ShowWithReloadConfirmation();
			}, 0);
		}

		// Token: 0x04000061 RID: 97
		public readonly ChangeLanguageBox _changeLanguageBox;
	}
}
