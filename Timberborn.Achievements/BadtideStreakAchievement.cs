using System;
using Timberborn.AchievementSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x0200000A RID: 10
	public class BadtideStreakAchievement : Achievement, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000267B File Offset: 0x0000087B
		public BadtideStreakAchievement(ISingletonLoader singletonLoader, EventBus eventBus)
		{
			this._singletonLoader = singletonLoader;
			this._eventBus = eventBus;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002691 File Offset: 0x00000891
		public override string Id
		{
			get
			{
				return "BADTIDE_STREAK";
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002698 File Offset: 0x00000898
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._streakCount > 0 && this._streakCount < BadtideStreakAchievement.RequiredBadtidesInRow)
			{
				singletonSaver.GetSingleton(BadtideStreakAchievement.BadtideStreakAchievementKey).Set(BadtideStreakAchievement.StreakCountKey, this._streakCount);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026CC File Offset: 0x000008CC
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(BadtideStreakAchievement.BadtideStreakAchievementKey, out objectLoader))
			{
				this._streakCount = objectLoader.Get(BadtideStreakAchievement.StreakCountKey);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026FE File Offset: 0x000008FE
		[OnEvent]
		public void OnHazardousWeatherStarted(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
		{
			if (hazardousWeatherStartedEvent.HazardousWeather is BadtideWeather)
			{
				this._streakCount++;
				if (this._streakCount >= BadtideStreakAchievement.RequiredBadtidesInRow)
				{
					base.Unlock();
					return;
				}
			}
			else
			{
				this._streakCount = 0;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002736 File Offset: 0x00000936
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002744 File Offset: 0x00000944
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x0400000C RID: 12
		public static readonly SingletonKey BadtideStreakAchievementKey = new SingletonKey("BadtideStreakAchievement");

		// Token: 0x0400000D RID: 13
		public static readonly PropertyKey<int> StreakCountKey = new PropertyKey<int>("StreakCount");

		// Token: 0x0400000E RID: 14
		public static readonly int RequiredBadtidesInRow = 2;

		// Token: 0x0400000F RID: 15
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public int _streakCount;
	}
}
