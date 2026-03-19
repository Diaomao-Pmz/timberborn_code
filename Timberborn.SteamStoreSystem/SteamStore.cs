using System;
using Timberborn.Localization;
using Timberborn.StoreSystem;

namespace Timberborn.SteamStoreSystem
{
	// Token: 0x02000007 RID: 7
	public class SteamStore : IStore
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000253E File Offset: 0x0000073E
		public SteamStore(SteamManager steamManager, SteamLanguage steamLanguage, ILoc loc)
		{
			this._steamManager = steamManager;
			this._steamLanguage = steamLanguage;
			this._loc = loc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000255B File Offset: 0x0000075B
		public bool GameIsAllowedToStart
		{
			get
			{
				return this._steamManager.GameIsAllowedToRun;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002568 File Offset: 0x00000768
		public string Language
		{
			get
			{
				return this._steamLanguage.GetLanguageCode();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002575 File Offset: 0x00000775
		public string ShortUpdateUrl
		{
			get
			{
				return "https://store.steampowered.com/news/";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000257C File Offset: 0x0000077C
		public string FullUpdateUrl
		{
			get
			{
				return "https://store.steampowered.com/news/app/1062090/view/526491913334818795";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002583 File Offset: 0x00000783
		public string UpdateInfoTextLocKey
		{
			get
			{
				return "MainMenu.SteamUpdateInfoText";
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000258A File Offset: 0x0000078A
		public string GetCompatibilityMessage()
		{
			return Environment.NewLine + Environment.NewLine + this._loc.T(SteamStore.SteamCompatibilityMessageLocKey);
		}

		// Token: 0x0400000B RID: 11
		public static readonly string SteamCompatibilityMessageLocKey = "Saving.SteamCompatibilityMessage";

		// Token: 0x0400000C RID: 12
		public readonly SteamManager _steamManager;

		// Token: 0x0400000D RID: 13
		public readonly SteamLanguage _steamLanguage;

		// Token: 0x0400000E RID: 14
		public readonly ILoc _loc;
	}
}
