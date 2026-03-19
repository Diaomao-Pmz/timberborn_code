using System;
using Timberborn.Persistence;

namespace Timberborn.Goods
{
	// Token: 0x0200000F RID: 15
	public class GoodRegistryValueSerializer : IValueSerializer<GoodRegistry>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002A56 File Offset: 0x00000C56
		public GoodRegistryValueSerializer(GoodAmountSerializer goodAmountSerializer)
		{
			this._goodAmountSerializer = goodAmountSerializer;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A65 File Offset: 0x00000C65
		public void Serialize(GoodRegistry value, IValueSaver valueSaver)
		{
			valueSaver.AsObject().Set<GoodAmount>(GoodRegistryValueSerializer.GoodsKey, value.Goods, this._goodAmountSerializer);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A88 File Offset: 0x00000C88
		public Obsoletable<GoodRegistry> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			GoodRegistry goodRegistry = new GoodRegistry();
			foreach (GoodAmount good in objectLoader.Get<GoodAmount>(GoodRegistryValueSerializer.GoodsKey, this._goodAmountSerializer))
			{
				goodRegistry.Add(good);
			}
			return goodRegistry;
		}

		// Token: 0x0400001C RID: 28
		public static readonly ListKey<GoodAmount> GoodsKey = new ListKey<GoodAmount>("Goods");

		// Token: 0x0400001D RID: 29
		public readonly GoodAmountSerializer _goodAmountSerializer;
	}
}
