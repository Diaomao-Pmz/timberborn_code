using System;
using System.Collections.Generic;

namespace Timberborn.Localization
{
	// Token: 0x02000006 RID: 6
	public interface ILocalizationService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14
		string CurrentLanguage { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15
		IEnumerable<LanguageInfo> AvailableLanguages { get; }

		// Token: 0x06000010 RID: 16
		void Load(string localizationCode);
	}
}
