using System;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace Timberborn.Language
{
	// Token: 0x02000005 RID: 5
	public class LanguageLoader : ILoadableSingleton
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020E1 File Offset: 0x000002E1
		public LanguageLoader(LanguageSettings languageSettings, ILocalizationService localizationService)
		{
			this._languageSettings = languageSettings;
			this._localizationService = localizationService;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F7 File Offset: 0x000002F7
		public void Load()
		{
			this._localizationService.Load(this._languageSettings.Language);
		}

		// Token: 0x04000006 RID: 6
		public readonly LanguageSettings _languageSettings;

		// Token: 0x04000007 RID: 7
		public readonly ILocalizationService _localizationService;
	}
}
