using System;
using System.Collections.Generic;
using Timberborn.ExperimentalModeSystem;

namespace Timberborn.Localization
{
	// Token: 0x02000015 RID: 21
	public class LocalizationService : ILocalizationService
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000030D4 File Offset: 0x000012D4
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000030DC File Offset: 0x000012DC
		public string CurrentLanguage { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x000030E5 File Offset: 0x000012E5
		public LocalizationService(LocalizationLoader localizationLoader, LocalizationDisplayNames localizationDisplayNames, ILoc loc, ExperimentalMode experimentalMode, PanelTextSettingsUpdater panelTextSettingsUpdater)
		{
			this._localizationLoader = localizationLoader;
			this._localizationDisplayNames = localizationDisplayNames;
			this._loc = loc;
			this._experimentalMode = experimentalMode;
			this._panelTextSettingsUpdater = panelTextSettingsUpdater;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003112 File Offset: 0x00001312
		public IEnumerable<LanguageInfo> AvailableLanguages
		{
			get
			{
				return this._localizationDisplayNames.GetLocalizationDisplayNames();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003120 File Offset: 0x00001320
		public void Load(string localizationCode)
		{
			this.CurrentLanguage = localizationCode;
			bool isExperimental = this._experimentalMode.IsExperimental;
			Dictionary<string, string> localization = this._localizationLoader.GetLocalization(this.CurrentLanguage, isExperimental);
			this._loc.Initialize(localization);
			this._panelTextSettingsUpdater.Update(this.CurrentLanguage);
		}

		// Token: 0x04000046 RID: 70
		public readonly LocalizationLoader _localizationLoader;

		// Token: 0x04000047 RID: 71
		public readonly LocalizationDisplayNames _localizationDisplayNames;

		// Token: 0x04000048 RID: 72
		public readonly ILoc _loc;

		// Token: 0x04000049 RID: 73
		public readonly ExperimentalMode _experimentalMode;

		// Token: 0x0400004A RID: 74
		public readonly PanelTextSettingsUpdater _panelTextSettingsUpdater;
	}
}
