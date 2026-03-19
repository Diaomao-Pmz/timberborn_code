using System;
using Timberborn.Persistence;

namespace Timberborn.Goods
{
	// Token: 0x02000009 RID: 9
	public class GoodAmountSerializer : IValueSerializer<GoodAmount>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000217D File Offset: 0x0000037D
		public GoodAmountSerializer(SerializedGoodValueSerializer serializedGoodValueSerializer)
		{
			this._serializedGoodValueSerializer = serializedGoodValueSerializer;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		public void Serialize(GoodAmount goodAmount, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set<SerializedGood>(GoodAmountSerializer.GoodKey, new SerializedGood(goodAmount.GoodId), this._serializedGoodValueSerializer);
			objectSaver.Set(GoodAmountSerializer.AmountKey, goodAmount.Amount);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021C4 File Offset: 0x000003C4
		public Obsoletable<GoodAmount> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			int amount = objectLoader.Get(GoodAmountSerializer.AmountKey);
			SerializedGood serializedGood;
			if (!objectLoader.GetObsoletable<SerializedGood>(GoodAmountSerializer.GoodKey, this._serializedGoodValueSerializer, out serializedGood))
			{
				return default(Obsoletable<GoodAmount>);
			}
			return new GoodAmount(serializedGood.Id, amount);
		}

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<SerializedGood> GoodKey = new PropertyKey<SerializedGood>("Good");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<int> AmountKey = new PropertyKey<int>("Amount");

		// Token: 0x0400000C RID: 12
		public readonly SerializedGoodValueSerializer _serializedGoodValueSerializer;
	}
}
