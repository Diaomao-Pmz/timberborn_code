using System;

namespace Timberborn.Localization
{
	// Token: 0x02000007 RID: 7
	public readonly struct LanguageInfo
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020C0 File Offset: 0x000002C0
		public string LocalizationCode { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020C8 File Offset: 0x000002C8
		public string DisplayName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool IsNew { get; }

		// Token: 0x06000014 RID: 20 RVA: 0x000020D8 File Offset: 0x000002D8
		public LanguageInfo(string localizationCode, string displayName, bool isNew)
		{
			this.LocalizationCode = localizationCode;
			this.DisplayName = displayName;
			this.IsNew = isNew;
		}
	}
}
