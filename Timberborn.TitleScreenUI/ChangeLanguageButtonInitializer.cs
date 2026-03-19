using System;
using Timberborn.LanguageUI;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.TitleScreenUI
{
	// Token: 0x02000004 RID: 4
	public class ChangeLanguageButtonInitializer : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public ChangeLanguageButtonInitializer(TitleScreenFooter titleScreenFooter, ChangeLanguageBox changeLanguageBox)
		{
			this._titleScreenFooter = titleScreenFooter;
			this._changeLanguageBox = changeLanguageBox;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			Button button = UQueryExtensions.Q<Button>(this._titleScreenFooter.Root, "ChangeLanguageButton", null);
			button.text = this._changeLanguageBox.LocalizedCurrentLanguageName;
			button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ChangeLanguageClicked), 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210F File Offset: 0x0000030F
		public void ChangeLanguageClicked(ClickEvent evt)
		{
			this._changeLanguageBox.ShowWithoutReloadConfirmation(null);
		}

		// Token: 0x04000006 RID: 6
		public readonly TitleScreenFooter _titleScreenFooter;

		// Token: 0x04000007 RID: 7
		public readonly ChangeLanguageBox _changeLanguageBox;
	}
}
