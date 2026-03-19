using System;
using System.Globalization;
using System.Text;
using Timberborn.Persistence;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000007 RID: 7
	public class IntPackedListSerializer : PackedListSerializer<int>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021CB File Offset: 0x000003CB
		public override void Serialize(int value, StringBuilder stringBuilder)
		{
			stringBuilder.Append(CommonNumberSerializer.SerializeInt(value));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DA File Offset: 0x000003DA
		public override int Deserialize(string value)
		{
			return int.Parse(value, CultureInfo.InvariantCulture);
		}
	}
}
