using System;
using Timberborn.Persistence;

namespace Timberborn.SettlementStatistics
{
	// Token: 0x0200000C RID: 12
	public class IncrementalStatisticSerializer : IValueSerializer<IncrementalStatistic>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000239C File Offset: 0x0000059C
		public void Serialize(IncrementalStatistic value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(IncrementalStatisticSerializer.IdKey, value.Id);
			objectSaver.Set(IncrementalStatisticSerializer.ValueKey, value.Value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023C8 File Offset: 0x000005C8
		public Obsoletable<IncrementalStatistic> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new IncrementalStatistic(objectLoader.Get(IncrementalStatisticSerializer.IdKey), objectLoader.Get(IncrementalStatisticSerializer.ValueKey));
		}

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<string> IdKey = new PropertyKey<string>("Id");

		// Token: 0x0400001A RID: 26
		public static readonly PropertyKey<int> ValueKey = new PropertyKey<int>("Value");
	}
}
