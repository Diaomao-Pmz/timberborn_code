using System;
using Timberborn.Persistence;

namespace Timberborn.NeedSystem
{
	// Token: 0x02000010 RID: 16
	public class SerializedNeedValueSerializer : IValueSerializer<SerializedNeed>
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003079 File Offset: 0x00001279
		public void Serialize(SerializedNeed value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set(SerializedNeedValueSerializer.NameKey, value.Id);
			objectSaver.Set(SerializedNeedValueSerializer.PointsKey, value.Points);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030A4 File Offset: 0x000012A4
		public Obsoletable<SerializedNeed> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			return new SerializedNeed(objectLoader.Get(SerializedNeedValueSerializer.NameKey), SerializedNeedValueSerializer.GetPoints(objectLoader));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030D3 File Offset: 0x000012D3
		public static float GetPoints(IObjectLoader objectLoader)
		{
			if (!objectLoader.Has<float>(SerializedNeedValueSerializer.PointsKey))
			{
				return 1f;
			}
			return objectLoader.Get(SerializedNeedValueSerializer.PointsKey);
		}

		// Token: 0x0400002E RID: 46
		public static readonly PropertyKey<string> NameKey = new PropertyKey<string>("Name");

		// Token: 0x0400002F RID: 47
		public static readonly PropertyKey<float> PointsKey = new PropertyKey<float>("Points");
	}
}
