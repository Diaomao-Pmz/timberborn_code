using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000019 RID: 25
	public class FakeRandomNumberGenerator : IFakeRandomNumberGenerator
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public FakeRandomNumberGenerator(int hashCode)
		{
			this._hashCode = hashCode;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public float Range(float inclusiveMin, float inclusiveMax, int byteIndex)
		{
			return Mathf.Lerp(inclusiveMin, inclusiveMax, this.NormalizedFloat(byteIndex));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public byte Byte(int byteIndex)
		{
			if (byteIndex < 0 || byteIndex > 3)
			{
				throw new ArgumentException("byteIndex must be between 0 and 3.");
			}
			return (byte)(this._hashCode >> byteIndex * 8);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DEB File Offset: 0x00000FEB
		public float NormalizedFloat(int byteIndex)
		{
			return (float)this.Byte(byteIndex) / 255f;
		}

		// Token: 0x0400002F RID: 47
		public readonly int _hashCode;
	}
}
