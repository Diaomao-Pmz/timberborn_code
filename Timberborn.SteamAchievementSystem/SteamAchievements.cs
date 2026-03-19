using System;
using JetBrains.Annotations;
using Steamworks;
using Timberborn.AchievementSystem;
using Timberborn.SteamStoreSystem;
using UnityEngine;

namespace Timberborn.SteamAchievementSystem
{
	// Token: 0x02000004 RID: 4
	public class SteamAchievements : IStoreAchievements
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public SteamAchievements(SteamManager steamManager)
		{
			this._steamManager = steamManager;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public void Initialize(Action successCallback)
		{
			if (this._steamManager.Initialized)
			{
				this._initializationSuccessCallback = successCallback;
				this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.OnUserStatsReceived));
				SteamUserStats.RequestCurrentStats();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002100 File Offset: 0x00000300
		public bool IsAchievementUnlocked(string achievementId)
		{
			bool flag;
			return this._steamManager.Initialized && SteamUserStats.GetAchievement(achievementId, out flag) && flag;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002127 File Offset: 0x00000327
		public void UnlockAchievement(string achievementId)
		{
			if (this._steamManager.Initialized)
			{
				if (SteamUserStats.SetAchievement(achievementId))
				{
					SteamUserStats.StoreStats();
					return;
				}
				Debug.LogError("Failed to unlock achievement: " + achievementId + ".");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000215C File Offset: 0x0000035C
		public void OnUserStatsReceived(UserStatsReceived_t callback)
		{
			this._userStatsReceived.Dispose();
			this._userStatsReceived = null;
			if (callback.m_eResult == EResult.k_EResultOK)
			{
				this._initializationSuccessCallback();
				return;
			}
			Debug.LogError(string.Format("Failed to receive Steam user stats: {0}.", callback.m_eResult));
		}

		// Token: 0x04000006 RID: 6
		[UsedImplicitly]
		public Callback<UserStatsReceived_t> _userStatsReceived;

		// Token: 0x04000007 RID: 7
		public readonly SteamManager _steamManager;

		// Token: 0x04000008 RID: 8
		public Action _initializationSuccessCallback;
	}
}
