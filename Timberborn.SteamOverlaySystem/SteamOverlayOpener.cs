using System;
using Steamworks;
using Timberborn.SteamStoreSystem;

namespace Timberborn.SteamOverlaySystem
{
	// Token: 0x02000005 RID: 5
	public class SteamOverlayOpener
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public void OpenLegalAgreement()
		{
			SteamOverlayOpener.OpenSteamPage(SteamOverlayOpener.LegalAgreementUrl);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		public void OpenWorkshopItem(ulong workshopItemId)
		{
			SteamOverlayOpener.OpenSteamPage(string.Format(SteamOverlayOpener.WorkshopItemUrl, workshopItemId));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002197 File Offset: 0x00000397
		public void OpenWorkshopSearch(string tag)
		{
			SteamOverlayOpener.OpenSteamPage(string.Format(SteamOverlayOpener.WorkshopSearchUrl, SteamAppId.AppId, tag));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B3 File Offset: 0x000003B3
		public static void OpenSteamPage(string page)
		{
			SteamFriends.ActivateGameOverlayToWebPage(page, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string WorkshopItemUrl = "https://steamcommunity.com/sharedfiles/filedetails/?id={0}";

		// Token: 0x0400000B RID: 11
		public static readonly string LegalAgreementUrl = "https://steamcommunity.com/sharedfiles/workshoplegalagreement";

		// Token: 0x0400000C RID: 12
		public static readonly string WorkshopSearchUrl = "https://steamcommunity.com/workshop/browse/?appid={0}&requiredtags[]={1}";
	}
}
