using System;
using Timberborn.Beavers;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000004 RID: 4
	public class BeaverBornStatisticCollector : ILoadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public BeaverBornStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D6 File Offset: 0x000002D6
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		[OnEvent]
		public void OnBeaverBorn(BeaverBornEvent beaverBornEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.BeaversBorn);
		}

		// Token: 0x04000006 RID: 6
		public readonly EventBus _eventBus;

		// Token: 0x04000007 RID: 7
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
