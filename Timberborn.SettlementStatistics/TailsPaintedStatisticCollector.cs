using System;
using Timberborn.SingletonSystem;
using Timberborn.TailDecalSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x0200000F RID: 15
	public class TailsPaintedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000251F File Offset: 0x0000071F
		public TailsPaintedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002535 File Offset: 0x00000735
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002543 File Offset: 0x00000743
		[OnEvent]
		public void OnTailDecalApplied(TailDecalAppliedEvent tailDecalAppliedEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.TailsPainted);
		}

		// Token: 0x04000024 RID: 36
		public readonly EventBus _eventBus;

		// Token: 0x04000025 RID: 37
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
