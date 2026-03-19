using System;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Demolishing
{
	// Token: 0x02000020 RID: 32
	public class ReservedDemolishableSerializer : IValueSerializer<ReservedDemolishable>
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00003A81 File Offset: 0x00001C81
		public ReservedDemolishableSerializer(ReferenceSerializer referenceSerializer)
		{
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003A90 File Offset: 0x00001C90
		public void Serialize(ReservedDemolishable value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			objectSaver.Set<Demolishable>(ReservedDemolishableSerializer.DemolishableKey, value.Demolishable, this._referenceSerializer.Of<Demolishable>());
			objectSaver.Set(ReservedDemolishableSerializer.ForceDemolishKey, value.ForceDemolish);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public Obsoletable<ReservedDemolishable> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			Demolishable demolishable;
			if (objectLoader.GetObsoletable<Demolishable>(ReservedDemolishableSerializer.DemolishableKey, this._referenceSerializer.Of<Demolishable>(), out demolishable))
			{
				return new ReservedDemolishable(demolishable, objectLoader.Get(ReservedDemolishableSerializer.ForceDemolishKey));
			}
			return null;
		}

		// Token: 0x04000048 RID: 72
		public static readonly PropertyKey<Demolishable> DemolishableKey = new PropertyKey<Demolishable>("Demolishable");

		// Token: 0x04000049 RID: 73
		public static readonly PropertyKey<bool> ForceDemolishKey = new PropertyKey<bool>("ForceDemolish");

		// Token: 0x0400004A RID: 74
		public readonly ReferenceSerializer _referenceSerializer;
	}
}
