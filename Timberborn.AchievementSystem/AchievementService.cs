using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.SingletonSystem;

namespace Timberborn.AchievementSystem
{
	// Token: 0x02000005 RID: 5
	public class AchievementService : IPostLoadableSingleton
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000217F File Offset: 0x0000037F
		public AchievementService(IStoreAchievements storeAchievements, IEnumerable<Achievement> achievements)
		{
			this._storeAchievements = storeAchievements;
			this._achievements = achievements.ToImmutableArray<Achievement>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000219A File Offset: 0x0000039A
		public void PostLoad()
		{
			this._storeAchievements.Initialize(new Action(this.EnableLockedAchievements));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B4 File Offset: 0x000003B4
		public void EnableLockedAchievements()
		{
			using (IEnumerator<Achievement> enumerator = this.GetLockedAchievements().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Achievement achievement = enumerator.Current;
					achievement.Enable(delegate(object _, EventArgs _)
					{
						this.UnlockAchievement(achievement.Id);
					});
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002224 File Offset: 0x00000424
		public IEnumerable<Achievement> GetLockedAchievements()
		{
			foreach (Achievement achievement in this._achievements)
			{
				if (!this._storeAchievements.IsAchievementUnlocked(achievement.Id))
				{
					yield return achievement;
				}
			}
			ImmutableArray<Achievement>.Enumerator enumerator = default(ImmutableArray<Achievement>.Enumerator);
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		public void UnlockAchievement(string achievementId)
		{
			this._storeAchievements.UnlockAchievement(achievementId);
		}

		// Token: 0x04000007 RID: 7
		public readonly IStoreAchievements _storeAchievements;

		// Token: 0x04000008 RID: 8
		public readonly ImmutableArray<Achievement> _achievements;
	}
}
