using System;
using Timberborn.Persistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200004E RID: 78
	public class TimerIntervalSerializer : IValueSerializer<TimerInterval>
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000093B0 File Offset: 0x000075B0
		public TimerIntervalSerializer(TimerIntervalFactory timerIntervalFactory)
		{
			this._timerIntervalFactory = timerIntervalFactory;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000093C0 File Offset: 0x000075C0
		public void Serialize(TimerInterval value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			float value2;
			if (value.TryGetHours(out value2))
			{
				objectSaver.Set<IntervalType>(TimerIntervalSerializer.TypeKey, value.Type);
				objectSaver.Set(TimerIntervalSerializer.HoursKey, value2);
				return;
			}
			objectSaver.Set(TimerIntervalSerializer.TicksKey, value.Ticks);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009410 File Offset: 0x00007610
		public Obsoletable<TimerInterval> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			if (objectLoader.Has<float>(TimerIntervalSerializer.HoursKey))
			{
				return this._timerIntervalFactory.CreateFromHours(objectLoader.Get(TimerIntervalSerializer.HoursKey), objectLoader.Get<IntervalType>(TimerIntervalSerializer.TypeKey));
			}
			return this._timerIntervalFactory.CreateFromTicks(objectLoader.Get(TimerIntervalSerializer.TicksKey));
		}

		// Token: 0x04000197 RID: 407
		public static readonly PropertyKey<IntervalType> TypeKey = new PropertyKey<IntervalType>("Type");

		// Token: 0x04000198 RID: 408
		public static readonly PropertyKey<int> TicksKey = new PropertyKey<int>("Ticks");

		// Token: 0x04000199 RID: 409
		public static readonly PropertyKey<float> HoursKey = new PropertyKey<float>("Hours");

		// Token: 0x0400019A RID: 410
		public readonly TimerIntervalFactory _timerIntervalFactory;
	}
}
