using System;
using Timberborn.Persistence;

namespace Timberborn.NotificationSystem
{
	// Token: 0x02000009 RID: 9
	public class NotificationValueSerializer : IValueSerializer<Notification>
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023C8 File Offset: 0x000005C8
		public void Serialize(Notification value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(NotificationValueSerializer.DescriptionKey, value.Description);
			objectSaver.Set(NotificationValueSerializer.SubjectKey, value.Subject);
			objectSaver.Set(NotificationValueSerializer.CycleKey, value.Cycle);
			objectSaver.Set(NotificationValueSerializer.CycleDayKey, value.CycleDay);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002420 File Offset: 0x00000620
		public Obsoletable<Notification> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			string description = objectLoader.Get(NotificationValueSerializer.DescriptionKey);
			Guid subject = objectLoader.Get(NotificationValueSerializer.SubjectKey);
			int cycle = objectLoader.Get(NotificationValueSerializer.CycleKey);
			int cycleDay = objectLoader.Get(NotificationValueSerializer.CycleDayKey);
			return new Notification(description, subject, cycle, cycleDay);
		}

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<string> DescriptionKey = new PropertyKey<string>("Description");

		// Token: 0x04000015 RID: 21
		public static readonly PropertyKey<Guid> SubjectKey = new PropertyKey<Guid>("Subject");

		// Token: 0x04000016 RID: 22
		public static readonly PropertyKey<int> CycleKey = new PropertyKey<int>("Cycle");

		// Token: 0x04000017 RID: 23
		public static readonly PropertyKey<int> CycleDayKey = new PropertyKey<int>("CycleDay");
	}
}
