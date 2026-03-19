using System;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.InventorySystem
{
	// Token: 0x02000009 RID: 9
	public class GoodReservationValueSerializer : IValueSerializer<GoodReservation>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002760 File Offset: 0x00000960
		public GoodReservationValueSerializer(GoodAmountSerializer goodAmountSerializer, ReferenceSerializer referenceSerializer)
		{
			this._goodAmountSerializer = goodAmountSerializer;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002778 File Offset: 0x00000978
		public void Serialize(GoodReservation value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set<Inventory>(GoodReservationValueSerializer.InventoryKey, value.Inventory, this._referenceSerializer.Of<Inventory>());
			objectSaver.Set<GoodAmount>(GoodReservationValueSerializer.GoodAmountKey, value.GoodAmount, this._goodAmountSerializer);
			objectSaver.Set(GoodReservationValueSerializer.FixedAmountKey, value.FixedAmount);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D4 File Offset: 0x000009D4
		public Obsoletable<GoodReservation> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			GoodAmount goodAmount;
			Inventory inventory;
			if (objectLoader.GetObsoletable<GoodAmount>(GoodReservationValueSerializer.GoodAmountKey, this._goodAmountSerializer, out goodAmount) && objectLoader.GetObsoletable<Inventory>(GoodReservationValueSerializer.InventoryKey, this._referenceSerializer.Of<Inventory>(), out inventory))
			{
				return new GoodReservation(inventory, goodAmount, objectLoader.Get(GoodReservationValueSerializer.FixedAmountKey));
			}
			return default(Obsoletable<GoodReservation>);
		}

		// Token: 0x04000012 RID: 18
		public static readonly PropertyKey<Inventory> InventoryKey = new PropertyKey<Inventory>("Inventory");

		// Token: 0x04000013 RID: 19
		public static readonly PropertyKey<GoodAmount> GoodAmountKey = new PropertyKey<GoodAmount>("GoodAmount");

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<bool> FixedAmountKey = new PropertyKey<bool>("FixedAmount");

		// Token: 0x04000015 RID: 21
		public readonly GoodAmountSerializer _goodAmountSerializer;

		// Token: 0x04000016 RID: 22
		public readonly ReferenceSerializer _referenceSerializer;
	}
}
