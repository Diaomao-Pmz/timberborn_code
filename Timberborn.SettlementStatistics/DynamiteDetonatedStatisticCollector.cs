using System;
using Timberborn.Explosions;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000009 RID: 9
	public class DynamiteDetonatedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021CE File Offset: 0x000003CE
		public DynamiteDetonatedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E4 File Offset: 0x000003E4
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021F2 File Offset: 0x000003F2
		[OnEvent]
		public void OnDynamiteDetonated(DynamiteDetonatedEvent dynamiteDetonatedEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.DynamiteDetonated);
		}

		// Token: 0x04000010 RID: 16
		public readonly EventBus _eventBus;

		// Token: 0x04000011 RID: 17
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
