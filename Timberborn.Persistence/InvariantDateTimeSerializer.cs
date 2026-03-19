using System;
using System.Globalization;

namespace Timberborn.Persistence
{
	// Token: 0x02000005 RID: 5
	public class InvariantDateTimeSerializer : IValueSerializer<DateTime>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000237F File Offset: 0x0000057F
		public void Serialize(DateTime value, IValueSaver valueSaver)
		{
			valueSaver.AsString(value.ToString(DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002393 File Offset: 0x00000593
		public Obsoletable<DateTime> Deserialize(IValueLoader valueLoader)
		{
			return DateTime.Parse(valueLoader.AsString(), DateTimeFormatInfo.InvariantInfo);
		}
	}
}
