using System;
using Timberborn.Healthcare;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000007 RID: 7
	public class ChippedTeethStatisticCollector : ILoadableSingleton
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002162 File Offset: 0x00000362
		public ChippedTeethStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002186 File Offset: 0x00000386
		[OnEvent]
		public void OnTeethChipped(TeethChippedEvent teethChippedEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.ChippedTeeth);
		}

		// Token: 0x0400000C RID: 12
		public readonly EventBus _eventBus;

		// Token: 0x0400000D RID: 13
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
