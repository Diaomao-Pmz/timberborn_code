using System;
using UnityEngine;

namespace Timberborn.WebNavigation
{
	// Token: 0x02000004 RID: 4
	public class UrlOpener
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public void OpenDiscord()
		{
			this.OpenUrl(UrlOpener.DiscordUrl);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public void OpenMerchandise()
		{
			this.OpenUrl(UrlOpener.MerchandiseUrl);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D5 File Offset: 0x000002D5
		public void OpenBugInfo()
		{
			this.OpenUrl(UrlOpener.BugInfoUrl);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E2 File Offset: 0x000002E2
		public void OpenHowToRemoveMods()
		{
			this.OpenUrl(UrlOpener.HowToRemoveModsUrl);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EF File Offset: 0x000002EF
		public void OpenPrivacyPolicy()
		{
			this.OpenUrl(UrlOpener.PrivacyPolicyUrl);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		public void OpenAnalyticsPrivacyPolicy()
		{
			this.OpenUrl(UrlOpener.AnalyticsPrivacyPolicyUrl);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002109 File Offset: 0x00000309
		public void OpenFeatureUpvote()
		{
			this.OpenUrl(UrlOpener.FeatureUpvoteUrl);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002116 File Offset: 0x00000316
		public void OpenModdingDocumentation()
		{
			this.OpenUrl(UrlOpener.ModdingDocumentationUrl);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		public void OpenUrl(string url)
		{
			Application.OpenURL(url);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string BugInfoUrl = "https://mechanistry.com/bug";

		// Token: 0x04000007 RID: 7
		public static readonly string HowToRemoveModsUrl = "https://mechanistry.com/how-to-remove-mods";

		// Token: 0x04000008 RID: 8
		public static readonly string DiscordUrl = "https://discord.gg/timberborn";

		// Token: 0x04000009 RID: 9
		public static readonly string MerchandiseUrl = "https://merch.timberborn.com/";

		// Token: 0x0400000A RID: 10
		public static readonly string FeatureUpvoteUrl = "https://timberborn.featureupvote.com/";

		// Token: 0x0400000B RID: 11
		public static readonly string PrivacyPolicyUrl = "https://mechanistry.com/privacy";

		// Token: 0x0400000C RID: 12
		public static readonly string AnalyticsPrivacyPolicyUrl = "https://mechanistry.com/privacy";

		// Token: 0x0400000D RID: 13
		public static readonly string ModdingDocumentationUrl = "https://github.com/mechanistry/timberborn-modding/wiki";
	}
}
