using System;
using System.Globalization;
using System.Text;
using Timberborn.Common;
using Timberborn.MapIndexSystem;
using Timberborn.Persistence;

namespace Timberborn.WaterSystem
{
	// Token: 0x02000029 RID: 41
	public class WaterColumnPackedListSerializer : PackedListSerializer<WaterColumn>
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000457C File Offset: 0x0000277C
		public override void Serialize(WaterColumn value, StringBuilder stringBuilder)
		{
			byte floor = value.Floor;
			float waterDepth = value.WaterDepth;
			float contamination = value.Contamination;
			float overflow = value.Overflow;
			if (waterDepth == 0f && contamination == 0f && overflow == 0f)
			{
				stringBuilder.Append(WaterColumnPackedListSerializer.EmptyColumnValue);
				return;
			}
			stringBuilder.Append(waterDepth.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(WaterColumnPackedListSerializer.Separator);
			stringBuilder.Append(CommonNumberSerializer.SerializeFloat(contamination));
			stringBuilder.Append(WaterColumnPackedListSerializer.Separator);
			stringBuilder.Append(CommonNumberSerializer.SerializeFloat(overflow));
			stringBuilder.Append(WaterColumnPackedListSerializer.Separator);
			stringBuilder.Append(CommonNumberSerializer.SerializeInt((int)floor));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004628 File Offset: 0x00002828
		[BackwardCompatible(2024, 1, 3, Compatibility.Map)]
		public override WaterColumn Deserialize(string value)
		{
			WaterColumn result;
			if (value == WaterColumnPackedListSerializer.EmptyColumnValue)
			{
				result = default(WaterColumn);
				return result;
			}
			string[] array = value.Split(WaterColumnPackedListSerializer.Separator, StringSplitOptions.None);
			result = new WaterColumn
			{
				WaterDepth = float.Parse(array[0]),
				Contamination = float.Parse(array[1]),
				Overflow = float.Parse(array[2]),
				Floor = ((array.Length == 4) ? byte.Parse(array[3]) : 0)
			};
			return result;
		}

		// Token: 0x040000A1 RID: 161
		public new static readonly char Separator = ':';

		// Token: 0x040000A2 RID: 162
		public static readonly string EmptyColumnValue = "0";
	}
}
