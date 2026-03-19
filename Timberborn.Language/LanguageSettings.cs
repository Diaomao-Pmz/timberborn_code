using System;
using Timberborn.SettingsSystem;
using Timberborn.StoreSystem;

namespace Timberborn.Language
{
	// Token: 0x02000006 RID: 6
	public class LanguageSettings
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000210F File Offset: 0x0000030F
		public LanguageSettings(IStore store, ISettings settings)
		{
			this._store = store;
			this._settings = settings;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002142 File Offset: 0x00000342
		public string Language
		{
			get
			{
				return this._settings.GetSafeString(LanguageSettings.LanguageKey, this._store.Language);
			}
			set
			{
				this._settings.SetString(LanguageSettings.LanguageKey, value);
			}
		}

		// Token: 0x04000008 RID: 8
		public static readonly string LanguageKey = "CurrentLanguage";

		// Token: 0x04000009 RID: 9
		public readonly IStore _store;

		// Token: 0x0400000A RID: 10
		public readonly ISettings _settings;
	}
}
