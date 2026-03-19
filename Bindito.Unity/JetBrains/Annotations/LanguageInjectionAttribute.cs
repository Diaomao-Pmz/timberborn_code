using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200002C RID: 44
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
	internal sealed class LanguageInjectionAttribute : Attribute
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000023FE File Offset: 0x000005FE
		public InjectedLanguage InjectedLanguage { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002406 File Offset: 0x00000606
		[CanBeNull]
		public string InjectedLanguageName { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000240E File Offset: 0x0000060E
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002416 File Offset: 0x00000616
		[CanBeNull]
		public string Prefix { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000241F File Offset: 0x0000061F
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002427 File Offset: 0x00000627
		[CanBeNull]
		public string Suffix { get; set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00002430 File Offset: 0x00000630
		public LanguageInjectionAttribute(InjectedLanguage injectedLanguage)
		{
			this.InjectedLanguage = injectedLanguage;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000243F File Offset: 0x0000063F
		public LanguageInjectionAttribute([NotNull] string injectedLanguage)
		{
			this.InjectedLanguageName = injectedLanguage;
		}
	}
}
