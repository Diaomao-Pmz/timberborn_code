using System;

namespace Timberborn.Common
{
	// Token: 0x02000020 RID: 32
	public interface IFakeRandomNumberGenerator
	{
		// Token: 0x06000074 RID: 116
		float Range(float inclusiveMin, float inclusiveMax, int byteIndex);

		// Token: 0x06000075 RID: 117
		byte Byte(int byteIndex);
	}
}
