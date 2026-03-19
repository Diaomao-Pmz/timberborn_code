using System;
using Timberborn.Persistence;

namespace Timberborn.HazardousWeatherSystem
{
	// Token: 0x0200000A RID: 10
	public class HazardousWeatherHistoryDataSerializer : IValueSerializer<HazardousWeatherHistoryData>
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002851 File Offset: 0x00000A51
		public void Serialize(HazardousWeatherHistoryData value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(HazardousWeatherHistoryDataSerializer.HazardousWeatherIdKey, value.HazardousWeatherId);
			objectSaver.Set(HazardousWeatherHistoryDataSerializer.DurationKey, value.Duration);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000287C File Offset: 0x00000A7C
		public Obsoletable<HazardousWeatherHistoryData> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new Obsoletable<HazardousWeatherHistoryData>(new HazardousWeatherHistoryData(objectLoader.Get(HazardousWeatherHistoryDataSerializer.HazardousWeatherIdKey), objectLoader.Get(HazardousWeatherHistoryDataSerializer.DurationKey)));
		}

		// Token: 0x04000030 RID: 48
		public static readonly PropertyKey<string> HazardousWeatherIdKey = new PropertyKey<string>("HazardousWeatherId");

		// Token: 0x04000031 RID: 49
		public static readonly PropertyKey<int> DurationKey = new PropertyKey<int>("Duration");
	}
}
