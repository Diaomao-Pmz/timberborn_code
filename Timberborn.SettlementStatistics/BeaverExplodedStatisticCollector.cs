using System;
using Timberborn.Explosions;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000005 RID: 5
	public class BeaverExplodedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020F6 File Offset: 0x000002F6
		public BeaverExplodedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211A File Offset: 0x0000031A
		[OnEvent]
		public void OnMortalDiedFromExplosion(MortalDiedFromExplosionEvent mortalDiedFromExplosionEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.BeaversExploded);
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
