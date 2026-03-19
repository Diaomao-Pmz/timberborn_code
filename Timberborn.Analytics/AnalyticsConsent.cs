using System;
using Timberborn.PlayerDataSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Analytics
{
	// Token: 0x02000005 RID: 5
	public class AnalyticsConsent : ILoadableSingleton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D1 File Offset: 0x000002D1
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D9 File Offset: 0x000002D9
		public bool IsConsentGiven { get; private set; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020E2 File Offset: 0x000002E2
		public AnalyticsConsent(IPlayerDataService playerDataService)
		{
			this._playerDataService = playerDataService;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020F1 File Offset: 0x000002F1
		public bool WasConsentAsked
		{
			get
			{
				return this._playerDataService.HasKey(AnalyticsConsent.ConsentKey);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002103 File Offset: 0x00000303
		public void Load()
		{
			this.IsConsentGiven = (this._playerDataService.HasKey(AnalyticsConsent.ConsentKey) && this._playerDataService.GetBool(AnalyticsConsent.ConsentKey, false));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002131 File Offset: 0x00000331
		public void GiveConsent()
		{
			this.IsConsentGiven = true;
			this._playerDataService.SetBool(AnalyticsConsent.ConsentKey, true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000214B File Offset: 0x0000034B
		public void RemoveConsent()
		{
			this.IsConsentGiven = false;
			this._playerDataService.SetBool(AnalyticsConsent.ConsentKey, false);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ConsentKey = "AnalyticsConsent_IsConsentGiven";

		// Token: 0x04000008 RID: 8
		public readonly IPlayerDataService _playerDataService;
	}
}
