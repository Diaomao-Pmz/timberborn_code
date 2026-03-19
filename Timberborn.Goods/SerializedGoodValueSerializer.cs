using System;
using Timberborn.Common;
using Timberborn.Persistence;

namespace Timberborn.Goods
{
	// Token: 0x0200001D RID: 29
	public class SerializedGoodValueSerializer : IValueSerializer<SerializedGood>
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003CAC File Offset: 0x00001EAC
		public SerializedGoodValueSerializer(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CBB File Offset: 0x00001EBB
		public void Serialize(SerializedGood serializedGood, IValueSaver valueSaver)
		{
			valueSaver.AsString(serializedGood.Id);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003CCC File Offset: 0x00001ECC
		public Obsoletable<SerializedGood> Deserialize(IValueLoader valueLoader)
		{
			string goodId = SerializedGoodValueSerializer.GetGoodId(valueLoader);
			GoodSpec goodOrNull = this._goodService.GetGoodOrNull(goodId);
			if (!(goodOrNull != null))
			{
				return default(Obsoletable<SerializedGood>);
			}
			return new SerializedGood(goodOrNull.Id);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003D10 File Offset: 0x00001F10
		[BackwardCompatible(2025, 1, 31, Compatibility.Map)]
		public static string GetGoodId(IValueLoader valueLoader)
		{
			if (valueLoader.IsObject())
			{
				return valueLoader.AsObject().Get(new PropertyKey<string>("Id"));
			}
			return valueLoader.AsString();
		}

		// Token: 0x0400004D RID: 77
		public readonly IGoodService _goodService;
	}
}
