using System;
using Timberborn.Persistence;

namespace Timberborn.Workshops
{
	// Token: 0x0200000D RID: 13
	public class HourlyProductivitySerializer : IValueSerializer<HourlyProductivity>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000025DB File Offset: 0x000007DB
		public void Serialize(HourlyProductivity value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(HourlyProductivitySerializer.MaxWorkPotentialKey, value.MaxWorkPotential);
			objectSaver.Set(HourlyProductivitySerializer.ActualWorkPerformedKey, value.ActualWorkPerformed);
			objectSaver.Set(HourlyProductivitySerializer.WasWorkingHourKey, value.WasWorkingHour);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002618 File Offset: 0x00000818
		public Obsoletable<HourlyProductivity> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new HourlyProductivity(objectLoader.Get(HourlyProductivitySerializer.MaxWorkPotentialKey), objectLoader.Get(HourlyProductivitySerializer.ActualWorkPerformedKey), objectLoader.Get(HourlyProductivitySerializer.WasWorkingHourKey));
		}

		// Token: 0x04000018 RID: 24
		public static readonly PropertyKey<int> MaxWorkPotentialKey = new PropertyKey<int>("MaxWorkPotential");

		// Token: 0x04000019 RID: 25
		public static readonly PropertyKey<int> ActualWorkPerformedKey = new PropertyKey<int>("ActualWorkPerformed");

		// Token: 0x0400001A RID: 26
		public static readonly PropertyKey<bool> WasWorkingHourKey = new PropertyKey<bool>("WasWorkingHour");
	}
}
