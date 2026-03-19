using System;
using Timberborn.AchievementSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Achievements
{
	// Token: 0x02000052 RID: 82
	public class WorkAllDayForWeekAchievement : Achievement, ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public WorkAllDayForWeekAchievement(EventBus eventBus, ISingletonLoader singletonLoader, WorkingHoursManager workingHoursManager, ITimeTriggerFactory timeTriggerFactory)
		{
			this._eventBus = eventBus;
			this._singletonLoader = singletonLoader;
			this._workingHoursManager = workingHoursManager;
			this._timeTrigger = timeTriggerFactory.Create(new Action(base.Unlock), (float)WorkAllDayForWeekAchievement.WorkingDaysRequired);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004DE1 File Offset: 0x00002FE1
		public override string Id
		{
			get
			{
				return "WORK_ALL_DAY_FOR_WEEK";
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004DE8 File Offset: 0x00002FE8
		[OnEvent]
		public void OnWorkingHoursChanged(WorkingHoursChangedEvent workingHoursChangedEvent)
		{
			this.CheckTimer();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public void Save(ISingletonSaver singletonSaver)
		{
			if (this._timeTrigger.Progress != 0f)
			{
				singletonSaver.GetSingleton(WorkAllDayForWeekAchievement.WorkAllDayForWeekAchievementKey).Set(WorkAllDayForWeekAchievement.ProgressKey, this._timeTrigger.Progress);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E24 File Offset: 0x00003024
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(WorkAllDayForWeekAchievement.WorkAllDayForWeekAchievementKey, out objectLoader))
			{
				this._timeTrigger.FastForwardProgress(objectLoader.Get(WorkAllDayForWeekAchievement.ProgressKey));
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004E5B File Offset: 0x0000305B
		public override void EnableInternal()
		{
			this._eventBus.Register(this);
			this.CheckTimer();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004E6F File Offset: 0x0000306F
		public override void DisableInternal()
		{
			this._eventBus.Unregister(this);
			this._timeTrigger.Reset();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004E88 File Offset: 0x00003088
		public void CheckTimer()
		{
			if (this._workingHoursManager.EndHours >= (float)WorkAllDayForWeekAchievement.WorkingHoursRequired)
			{
				this._timeTrigger.Resume();
				return;
			}
			this._timeTrigger.Reset();
		}

		// Token: 0x040000B8 RID: 184
		public static readonly SingletonKey WorkAllDayForWeekAchievementKey = new SingletonKey("WorkAllDayForWeekAchievement");

		// Token: 0x040000B9 RID: 185
		public static readonly PropertyKey<float> ProgressKey = new PropertyKey<float>("Progress");

		// Token: 0x040000BA RID: 186
		public static readonly int WorkingHoursRequired = 24;

		// Token: 0x040000BB RID: 187
		public static readonly int WorkingDaysRequired = 7;

		// Token: 0x040000BC RID: 188
		public readonly EventBus _eventBus;

		// Token: 0x040000BD RID: 189
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x040000BE RID: 190
		public readonly WorkingHoursManager _workingHoursManager;

		// Token: 0x040000BF RID: 191
		public readonly ITimeTrigger _timeTrigger;
	}
}
