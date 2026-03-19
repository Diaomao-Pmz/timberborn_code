using System;

namespace Timberborn.AchievementSystem
{
	// Token: 0x02000009 RID: 9
	public interface IStoreAchievements
	{
		// Token: 0x0600001E RID: 30
		void Initialize(Action successCallback);

		// Token: 0x0600001F RID: 31
		bool IsAchievementUnlocked(string achievementId);

		// Token: 0x06000020 RID: 32
		void UnlockAchievement(string achievementId);
	}
}
