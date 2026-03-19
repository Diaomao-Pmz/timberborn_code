using System;
using Timberborn.InventoryNeedSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000011 RID: 17
	public class WaterConsumedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000258B File Offset: 0x0000078B
		public WaterConsumedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025A1 File Offset: 0x000007A1
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025AF File Offset: 0x000007AF
		[OnEvent]
		public void OnGoodConsumed(GoodConsumedEvent goodConsumedEvent)
		{
			if (goodConsumedEvent.GoodId == WaterConsumedStatisticCollector.WaterGoodId)
			{
				this._incrementalStatisticCollector.Increment(StatisticIds.WaterConsumed);
			}
		}

		// Token: 0x04000028 RID: 40
		public static readonly string WaterGoodId = "Water";

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
