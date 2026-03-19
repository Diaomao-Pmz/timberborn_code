using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.Localization
{
	// Token: 0x02000016 RID: 22
	public class NewLocalizationService : ILoadableSingleton
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003170 File Offset: 0x00001370
		public NewLocalizationService(LocalizationLoader localizationLoader)
		{
			this._localizationLoader = localizationLoader;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000318C File Offset: 0x0000138C
		public void Load()
		{
			string @string = PlayerPrefs.GetString(NewLocalizationService.LocalizationsOnLastLaunchKey, "");
			this._localizationsAvailableOnLastLaunch.AddRange(NewLocalizationService.DeserializeLocalizations(@string));
			string text = NewLocalizationService.SerializeLocalizations(this._localizationLoader.GetLocalizationNames());
			PlayerPrefs.SetString(NewLocalizationService.LocalizationsOnLastLaunchKey, text);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031D6 File Offset: 0x000013D6
		public bool LocalizationIsNew(string localizationCode)
		{
			return !this._localizationsAvailableOnLastLaunch.Contains(localizationCode);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000031E7 File Offset: 0x000013E7
		public static string SerializeLocalizations(IEnumerable<string> localizations)
		{
			return string.Join<string>(NewLocalizationService.Delimiter, from localization in localizations
			orderby localization
			select localization);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003218 File Offset: 0x00001418
		public static IEnumerable<string> DeserializeLocalizations(string localizations)
		{
			return localizations.Split(NewLocalizationService.Delimiter, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x0400004B RID: 75
		public static readonly string LocalizationsOnLastLaunchKey = "LocalizationsOnLastLaunch";

		// Token: 0x0400004C RID: 76
		public static readonly char Delimiter = ';';

		// Token: 0x0400004D RID: 77
		public readonly LocalizationLoader _localizationLoader;

		// Token: 0x0400004E RID: 78
		public readonly HashSet<string> _localizationsAvailableOnLastLaunch = new HashSet<string>();
	}
}
