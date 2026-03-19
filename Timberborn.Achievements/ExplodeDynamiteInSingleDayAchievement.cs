using System;
using Timberborn.AchievementSystem;
using Timberborn.Explosions;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x02000025 RID: 37
	public class ExplodeDynamiteInSingleDayAchievement : Achievement, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000036E2 File Offset: 0x000018E2
		public ExplodeDynamiteInSingleDayAchievement(EventBus eventBus, ISingletonLoader singletonLoader)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000036F8 File Offset: 0x000018F8
		public override string Id
		{
			get
			{
				return "EXPLODE_DYNAMITE_IN_SINGLE_DAY";
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000036FF File Offset: 0x000018FF
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._detonationCount > 0 && this._detonationCount < ExplodeDynamiteInSingleDayAchievement.DynamiteToDetonate)
			{
				singletonSaver.GetSingleton(ExplodeDynamiteInSingleDayAchievement.DynamiteExplodedInSingleDayAchievementKey).Set(ExplodeDynamiteInSingleDayAchievement.DetonationCountKey, this._detonationCount);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003734 File Offset: 0x00001934
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(ExplodeDynamiteInSingleDayAchievement.DynamiteExplodedInSingleDayAchievementKey, out objectLoader))
			{
				this._detonationCount = objectLoader.Get(ExplodeDynamiteInSingleDayAchievement.DetonationCountKey);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003766 File Offset: 0x00001966
		[OnEvent]
		public void OnDaytimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this._detonationCount = 0;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000376F File Offset: 0x0000196F
		[OnEvent]
		public void OnDynamiteDetonated(DynamiteDetonatedEvent dynamiteDetonatedEvent)
		{
			this._detonationCount++;
			if (this._detonationCount >= ExplodeDynamiteInSingleDayAchievement.DynamiteToDetonate)
			{
				base.Unlock();
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003792 File Offset: 0x00001992
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037A0 File Offset: 0x000019A0
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x04000052 RID: 82
		public static readonly SingletonKey DynamiteExplodedInSingleDayAchievementKey = new SingletonKey("DynamiteExplodedInSingleDayAchievement");

		// Token: 0x04000053 RID: 83
		public static readonly PropertyKey<int> DetonationCountKey = new PropertyKey<int>("DetonationCount");

		// Token: 0x04000054 RID: 84
		public static readonly int DynamiteToDetonate = 200;

		// Token: 0x04000055 RID: 85
		public readonly EventBus _eventBus;

		// Token: 0x04000056 RID: 86
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000057 RID: 87
		public int _detonationCount;
	}
}
