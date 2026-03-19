using System;
using System.Globalization;
using System.Text;
using Timberborn.Persistence;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x02000005 RID: 5
	public class FloatPackedListSerializer : PackedListSerializer<float>
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020EE File Offset: 0x000002EE
		public override void Serialize(float value, StringBuilder stringBuilder)
		{
			stringBuilder.Append(CommonNumberSerializer.SerializeFloat(value));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FD File Offset: 0x000002FD
		public override float Deserialize(string value)
		{
			return float.Parse(value, CultureInfo.InvariantCulture);
		}
	}
}
