using System;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000008 RID: 8
	public class DaysPassedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002198 File Offset: 0x00000398
		public DaysPassedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021AE File Offset: 0x000003AE
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021BC File Offset: 0x000003BC
		[OnEvent]
		public void OnDaytimeStartEvent(DaytimeStartEvent daytimeStartEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.DaysPassed);
		}

		// Token: 0x0400000E RID: 14
		public readonly EventBus _eventBus;

		// Token: 0x0400000F RID: 15
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
