using System;
using System.Collections.Generic;

namespace Timberborn.Localization
{
	// Token: 0x0200000E RID: 14
	public class LocalizationDisplayNames
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000026E8 File Offset: 0x000008E8
		public LocalizationDisplayNames(LocalizationLoader localizationLoader, NewLocalizationService newLocalizationService)
		{
			this._localizationLoader = localizationLoader;
			this._newLocalizationService = newLocalizationService;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026FE File Offset: 0x000008FE
		public IEnumerable<LanguageInfo> GetLocalizationDisplayNames()
		{
			foreach (string localizationCode in this._localizationLoader.GetLocalizationNames())
			{
				yield return this.GetDisplayName(localizationCode);
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002710 File Offset: 0x00000910
		public LanguageInfo GetDisplayName(string localizationCode)
		{
			string valueOrDefault = CollectionExtensions.GetValueOrDefault<string, string>(this._localizationLoader.GetLocalizationRecords(localizationCode), LocalizationDisplayNames.LocalizationDisplayNameKey, localizationCode);
			return new LanguageInfo(localizationCode, valueOrDefault, this._newLocalizationService.LocalizationIsNew(localizationCode));
		}

		// Token: 0x04000020 RID: 32
		public static readonly string LocalizationDisplayNameKey = "Settings.Language.Name";

		// Token: 0x04000021 RID: 33
		public readonly LocalizationLoader _localizationLoader;

		// Token: 0x04000022 RID: 34
		public readonly NewLocalizationService _newLocalizationService;
	}
}
