using System;
using System.Globalization;
using System.Text;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000004 RID: 4
	public class BoolPackedListSerializer : PackedListSerializer<bool>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public override void Serialize(bool value, StringBuilder stringBuilder)
		{
			stringBuilder.Append(value ? "1" : "0");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D6 File Offset: 0x000002D6
		public override bool Deserialize(string value)
		{
			return int.Parse(value, CultureInfo.InvariantCulture) > 0;
		}
	}
}
