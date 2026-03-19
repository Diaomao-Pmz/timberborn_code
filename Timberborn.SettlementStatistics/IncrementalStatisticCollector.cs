using System;
using System.Collections.Generic;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x0200000B RID: 11
	public class IncrementalStatisticCollector : ISaveableSingleton, ILoadableSingleton
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002251 File Offset: 0x00000451
		public IncrementalStatisticCollector(IncrementalStatisticSerializer incrementalStatisticSerializer, ISingletonLoader singletonLoader)
		{
			this._incrementalStatisticSerializer = incrementalStatisticSerializer;
			this._singletonLoader = singletonLoader;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002272 File Offset: 0x00000472
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(IncrementalStatisticCollector.IncrementalStatisticCollectorKey).Set<IncrementalStatistic>(IncrementalStatisticCollector.SettlementStatisticsKey, this._settlementStatistics.Values, this._incrementalStatisticSerializer);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000229C File Offset: 0x0000049C
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(IncrementalStatisticCollector.IncrementalStatisticCollectorKey, out objectLoader))
			{
				foreach (IncrementalStatistic incrementalStatistic in objectLoader.Get<IncrementalStatistic>(IncrementalStatisticCollector.SettlementStatisticsKey, this._incrementalStatisticSerializer))
				{
					this._settlementStatistics.Add(incrementalStatistic.Id, incrementalStatistic);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000231C File Offset: 0x0000051C
		public int GetOrDefault(string id)
		{
			IncrementalStatistic incrementalStatistic;
			if (!this._settlementStatistics.TryGetValue(id, out incrementalStatistic))
			{
				return 0;
			}
			return incrementalStatistic.Value;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002344 File Offset: 0x00000544
		public void Increment(string id)
		{
			IncrementalStatistic incrementalStatistic;
			if (!this._settlementStatistics.TryGetValue(id, out incrementalStatistic))
			{
				incrementalStatistic = new IncrementalStatistic(id, 0);
				this._settlementStatistics.Add(id, incrementalStatistic);
			}
			incrementalStatistic.Increment();
		}

		// Token: 0x04000014 RID: 20
		public static readonly SingletonKey IncrementalStatisticCollectorKey = new SingletonKey("IncrementalStatisticCollector");

		// Token: 0x04000015 RID: 21
		public static readonly ListKey<IncrementalStatistic> SettlementStatisticsKey = new ListKey<IncrementalStatistic>("SettlementStatistics");

		// Token: 0x04000016 RID: 22
		public readonly IncrementalStatisticSerializer _incrementalStatisticSerializer;

		// Token: 0x04000017 RID: 23
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000018 RID: 24
		public readonly Dictionary<string, IncrementalStatistic> _settlementStatistics = new Dictionary<string, IncrementalStatistic>();
	}
}
