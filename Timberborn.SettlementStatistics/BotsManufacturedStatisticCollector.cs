using System;
using Timberborn.BotUpkeep;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000006 RID: 6
	public class BotsManufacturedStatisticCollector : ILoadableSingleton
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public BotsManufacturedStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002142 File Offset: 0x00000342
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		[OnEvent]
		public void OnBotManufactured(BotManufacturedEvent botManufacturedEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.BotsManufactured);
		}

		// Token: 0x0400000A RID: 10
		public readonly EventBus _eventBus;

		// Token: 0x0400000B RID: 11
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
