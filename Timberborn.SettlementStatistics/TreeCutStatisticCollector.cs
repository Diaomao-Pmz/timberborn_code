using System;
using Timberborn.Forestry;
using Timberborn.SingletonSystem;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x02000010 RID: 16
	public class TreeCutStatisticCollector : ILoadableSingleton
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002555 File Offset: 0x00000755
		public TreeCutStatisticCollector(EventBus eventBus, IncrementalStatisticCollector incrementalStatisticCollector)
		{
			this._eventBus = eventBus;
			this._incrementalStatisticCollector = incrementalStatisticCollector;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000256B File Offset: 0x0000076B
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002579 File Offset: 0x00000779
		[OnEvent]
		public void OnTreeCut(TreeCutEvent treeCutEvent)
		{
			this._incrementalStatisticCollector.Increment(StatisticIds.TreesCut);
		}

		// Token: 0x04000026 RID: 38
		public readonly EventBus _eventBus;

		// Token: 0x04000027 RID: 39
		public readonly IncrementalStatisticCollector _incrementalStatisticCollector;
	}
}
