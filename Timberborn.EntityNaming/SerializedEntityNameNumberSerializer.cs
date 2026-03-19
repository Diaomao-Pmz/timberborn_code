using System;
using Timberborn.Persistence;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000015 RID: 21
	public class SerializedEntityNameNumberSerializer : IValueSerializer<SerializedEntityNameNumber>
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002FF3 File Offset: 0x000011F3
		public void Serialize(SerializedEntityNameNumber value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(SerializedEntityNameNumberSerializer.GroupKey, value.Group);
			objectSaver.Set(SerializedEntityNameNumberSerializer.NextNumberKey, value.NextNumber);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000301C File Offset: 0x0000121C
		public Obsoletable<SerializedEntityNameNumber> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new SerializedEntityNameNumber(objectLoader.Get(SerializedEntityNameNumberSerializer.GroupKey), objectLoader.Get(SerializedEntityNameNumberSerializer.NextNumberKey));
		}

		// Token: 0x04000032 RID: 50
		public static readonly PropertyKey<string> GroupKey = new PropertyKey<string>("Group");

		// Token: 0x04000033 RID: 51
		public static readonly PropertyKey<int> NextNumberKey = new PropertyKey<int>("NextNumber");
	}
}
