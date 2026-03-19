using System;
using Timberborn.Persistence;

namespace Timberborn.Workshops
{
	// Token: 0x02000009 RID: 9
	public class DailyProductivitySerializer : IValueSerializer<DailyProductivity>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002200 File Offset: 0x00000400
		public DailyProductivitySerializer(HourlyProductivitySerializer hourlyProductivitySerializer)
		{
			this._hourlyProductivitySerializer = hourlyProductivitySerializer;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000220F File Offset: 0x0000040F
		public void Serialize(DailyProductivity value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set<HourlyProductivity>(DailyProductivitySerializer.HourlyProductivitiesKey, value.HourlyProductivities, this._hourlyProductivitySerializer);
			objectSaver.Set<HourlyProductivity>(DailyProductivitySerializer.CurrentProductivityKey, value.CurrentProductivity, this._hourlyProductivitySerializer);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002244 File Offset: 0x00000444
		public Obsoletable<DailyProductivity> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new DailyProductivity(objectLoader.Get<HourlyProductivity>(DailyProductivitySerializer.HourlyProductivitiesKey, this._hourlyProductivitySerializer).ToArray(), objectLoader.Get<HourlyProductivity>(DailyProductivitySerializer.CurrentProductivityKey, this._hourlyProductivitySerializer));
		}

		// Token: 0x0400000B RID: 11
		public static readonly ListKey<HourlyProductivity> HourlyProductivitiesKey = new ListKey<HourlyProductivity>("HourlyProductivities");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<HourlyProductivity> CurrentProductivityKey = new PropertyKey<HourlyProductivity>("CurrentProductivity");

		// Token: 0x0400000D RID: 13
		public readonly HourlyProductivitySerializer _hourlyProductivitySerializer;
	}
}
