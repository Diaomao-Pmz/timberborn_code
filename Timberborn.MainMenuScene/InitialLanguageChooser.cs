using System;
using System.Linq;
using Timberborn.LanguageUI;
using Timberborn.Localization;

namespace Timberborn.MainMenuScene
{
	// Token: 0x02000007 RID: 7
	public class InitialLanguageChooser
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002294 File Offset: 0x00000494
		public InitialLanguageChooser(ChangeLanguageBox changeLanguageBox, ILocalizationService localizationService)
		{
			this._changeLanguageBox = changeLanguageBox;
			this._localizationService = localizationService;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022AA File Offset: 0x000004AA
		public void CheckInitialLanguage(Action onSuccessfulCheck)
		{
			if (this.NewLanguagesDetected || this.CurrentLanguageIsMissing)
			{
				this._changeLanguageBox.ShowWithoutReloadConfirmation(onSuccessfulCheck);
				return;
			}
			onSuccessfulCheck();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022CF File Offset: 0x000004CF
		public bool NewLanguagesDetected
		{
			get
			{
				return this._localizationService.AvailableLanguages.Any((LanguageInfo language) => language.IsNew);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002300 File Offset: 0x00000500
		public bool CurrentLanguageIsMissing
		{
			get
			{
				return this._localizationService.AvailableLanguages.All((LanguageInfo language) => language.LocalizationCode != this._localizationService.CurrentLanguage);
			}
		}

		// Token: 0x0400000D RID: 13
		public readonly ChangeLanguageBox _changeLanguageBox;

		// Token: 0x0400000E RID: 14
		public readonly ILocalizationService _localizationService;
	}
}
